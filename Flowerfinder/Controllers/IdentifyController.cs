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

        public IdentifyController(AppDbContext db, PlantNetService plantNet)
        {
            _db = db;
            _plantNet = plantNet;
        }

        // GET /Identify — the drop-a-photo page
        public IActionResult Index()
        {
            ViewData["Configured"] = _plantNet.IsConfigured;
            ViewData["Demo"] = _plantNet.IsDemo;
            return View();
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

            List<PlantGuess>? guesses;
            await using (var stream = photo.OpenReadStream())
            {
                guesses = await _plantNet.IdentifyAsync(stream, photo.FileName, photo.ContentType, ct);
            }

            if (guesses == null)
                return StatusCode(502, new { error = "The identification service didn't answer — try again in a moment." });
            if (guesses.Count == 0)
                return Ok(new { demo = _plantNet.IsDemo, guesses = Array.Empty<object>() });

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
            });

            return Ok(new { demo = _plantNet.IsDemo, guesses = result });
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
