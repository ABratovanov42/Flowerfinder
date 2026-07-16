using Flowerfinder.Data;
using Flowerfinder.Models;
using Flowerfinder.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flowerfinder.Controllers
{
    public class IdentifyController : Controller
    {
        private static readonly string[] AllowedTypes = { "image/jpeg", "image/png", "image/webp" };
        private const long MaxBytes = 10 * 1024 * 1024;

        private readonly AppDbContext _db;
        private readonly PlantNetService _plantNet;
        private readonly PhotoStorage _photos;

        public IdentifyController(AppDbContext db, PlantNetService plantNet, PhotoStorage photos)
        {
            _db = db;
            _plantNet = plantNet;
            _photos = photos;
        }

        // GET /Identify — the drop-a-photo page
        public IActionResult Index()
        {
            ViewData["Configured"] = _plantNet.IsConfigured;
            ViewData["Demo"] = _plantNet.IsDemo;
            return View();
        }

        // GET /Identify/History — every photo identified so far, newest first
        public async Task<IActionResult> History()
        {
            var records = await _db.IdentifyRecords
                .OrderByDescending(r => r.DateTaken)
                .Take(100)
                .ToListAsync();

            var matchedIds = records.Where(r => r.MatchedFlowerId != null)
                .Select(r => r.MatchedFlowerId!.Value).Distinct().ToList();
            ViewData["Matches"] = await _db.Flowers
                .Where(f => matchedIds.Contains(f.Id))
                .ToDictionaryAsync(f => f.Id);

            return View(records);
        }

        // POST /Identify/Analyze — photo in, best guesses + catalog matches out
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(MaxBytes + 1024)]
        public async Task<IActionResult> Analyze(IFormFile? photo, CancellationToken ct)
        {
            if (!_plantNet.IsConfigured)
                return StatusCode(503, new { error = "Identification isn't set up yet." });

            if (photo == null || photo.Length == 0)
                return BadRequest(new { error = "Choose a photo first." });
            if (photo.Length > MaxBytes)
                return BadRequest(new { error = "That photo is over 10 MB — try a smaller one." });
            if (!AllowedTypes.Contains(photo.ContentType, StringComparer.OrdinalIgnoreCase))
                return BadRequest(new { error = "That doesn't look like a photo — JPEG, PNG or WebP, please." });

            // buffer the photo so it can go to PlantNet AND be saved for
            // history (each gets its own stream — PlantNet closes the one
            // it is handed when the request is disposed)
            byte[] bytes;
            using (var buffer = new MemoryStream())
            {
                await photo.CopyToAsync(buffer, ct);
                bytes = buffer.ToArray();
            }

            var guesses = await _plantNet.IdentifyAsync(new MemoryStream(bytes), photo.FileName, photo.ContentType, ct);

            if (guesses == null)
                return StatusCode(502, new { error = "The identification service didn't answer — try again in a moment." });
            if (guesses.Count == 0)
                return Ok(new { demo = _plantNet.IsDemo, guesses = Array.Empty<object>(), record = (int?)null });

            // pair each guess with our own catalog where we can
            var flowers = await _db.Flowers.ToListAsync(ct);
            var result = guesses.Select(g =>
            {
                var match = FindMatch(flowers, g);
                return new
                {
                    sci = g.ScientificName,
                    common = g.CommonName,
                    score = Math.Round(g.Score * 100),
                    match = match == null ? null : new
                    {
                        id = match.Id,
                        name = match.CommonName,
                        sci = match.ScientificName,
                        image = match.ImagePath,
                        season = match.BloomSeason.Label()
                    }
                };
            }).ToList();

            // remember this find (photo + best guess) for the history page
            int? recordId = null;
            var savedPath = await _photos.SaveAsync(new MemoryStream(bytes), ct);
            if (savedPath != null)
            {
                var best = guesses[0];
                var record = new IdentifyRecord
                {
                    DateTaken = DateTime.UtcNow,
                    ImagePath = savedPath,
                    TopScientificName = best.ScientificName,
                    TopCommonName = best.CommonName,
                    TopScorePercent = (int)Math.Round(best.Score * 100),
                    MatchedFlowerId = FindMatch(flowers, best)?.Id,
                    Demo = _plantNet.IsDemo
                };
                _db.IdentifyRecords.Add(record);
                await _db.SaveChangesAsync(ct);
                recordId = record.Id;
            }

            return Ok(new { demo = _plantNet.IsDemo, guesses = result, record = recordId });
        }

        // exact scientific name first, then same genus, then common name
        private static Flower? FindMatch(List<Flower> flowers, PlantGuess guess)
        {
            var exact = flowers.FirstOrDefault(f =>
                string.Equals(f.ScientificName, guess.ScientificName, StringComparison.OrdinalIgnoreCase));
            if (exact != null) return exact;

            var genus = guess.ScientificName.Split(' ')[0];
            var sameGenus = flowers.FirstOrDefault(f =>
                f.ScientificName != null &&
                f.ScientificName.Split(' ')[0].Equals(genus, StringComparison.OrdinalIgnoreCase));
            if (sameGenus != null) return sameGenus;

            if (!string.IsNullOrWhiteSpace(guess.CommonName))
                return flowers.FirstOrDefault(f =>
                    f.CommonName.Contains(guess.CommonName, StringComparison.OrdinalIgnoreCase) ||
                    guess.CommonName.Contains(f.CommonName, StringComparison.OrdinalIgnoreCase));

            return null;
        }
    }
}
