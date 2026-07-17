using Flowerfinder.Data;
using Flowerfinder.Models;
using Flowerfinder.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flowerfinder.Controllers
{
    public class FlowersController : Controller
    {
        private readonly AppDbContext _db;
        private readonly PhotoStorage _photos;

        public FlowersController(AppDbContext db, PhotoStorage photos)
        {
            _db = db;
            _photos = photos;
        }

        private const int PageSize = 12;

        // GET /Flowers — the catalog, with optional search, filters and paging
        public async Task<IActionResult> Index(string? q, string? color, Sunlight? sun, BloomSeason? season,
            Watering? water, bool? perennial, int page = 1)
        {
            var flowers = _db.Flowers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(q))
                flowers = flowers.Where(f =>
                    f.CommonName.ToLower().Contains(q.ToLower()) ||
                    (f.ScientificName != null && f.ScientificName.ToLower().Contains(q.ToLower())));

            if (!string.IsNullOrWhiteSpace(color))
                flowers = flowers.Where(f => f.Colors != null && f.Colors.ToLower().Contains(color.ToLower()));

            if (sun.HasValue)
                flowers = flowers.Where(f => f.Sunlight == sun.Value);

            if (season.HasValue)
                flowers = flowers.Where(f => f.BloomSeason == season.Value);

            if (water.HasValue)
                flowers = flowers.Where(f => f.Watering == water.Value);

            if (perennial.HasValue)
                flowers = flowers.Where(f => f.IsPerennial == perennial.Value);

            var total = await flowers.CountAsync();
            var totalPages = Math.Max(1, (int)Math.Ceiling(total / (double)PageSize));
            page = Math.Clamp(page, 1, totalPages);

            // Flower of the day — the catalog's front door, page 1 unfiltered.
            // Picked by the date, so everyone sees the same flower all day.
            var unfiltered = string.IsNullOrWhiteSpace(q) && string.IsNullOrWhiteSpace(color)
                && !sun.HasValue && !season.HasValue && !water.HasValue && !perennial.HasValue;
            if (unfiltered && page == 1 && total > 0)
            {
                var ids = await _db.Flowers.OrderBy(f => f.Id).Select(f => f.Id).ToListAsync();
                var today = DateTime.Now;
                ViewData["Fotd"] = await _db.Flowers.FindAsync(ids[(today.Year * 366 + today.DayOfYear) % ids.Count]);
            }

            ViewData["q"] = q;
            ViewData["color"] = color;
            ViewData["sun"] = sun;
            ViewData["season"] = season;
            ViewData["water"] = water;
            ViewData["perennial"] = perennial;
            ViewData["page"] = page;
            ViewData["totalPages"] = totalPages;
            ViewData["total"] = total;

            return View(await flowers.OrderBy(f => f.CommonName)
                .Skip((page - 1) * PageSize).Take(PageSize).ToListAsync());
        }

        // GET /Flowers/Compare?a=1&b=2&c=3 — up to three flowers side by side
        public async Task<IActionResult> Compare(int? a, int? b, int? c)
        {
            ViewData["Options"] = await _db.Flowers
                .OrderBy(f => f.CommonName)
                .Select(f => new ValueTuple<int, string>(f.Id, f.CommonName))
                .ToListAsync();

            var slots = new[] { a, b, c };
            var wanted = slots.Where(i => i.HasValue).Select(i => i!.Value).ToList();
            var found = await _db.Flowers.Where(f => wanted.Contains(f.Id)).ToListAsync();

            ViewData["a"] = a;
            ViewData["b"] = b;
            ViewData["c"] = c;

            // keep the slot order so each flower stays under its own dropdown
            return View(slots.Select(i => i.HasValue ? found.FirstOrDefault(f => f.Id == i.Value) : null).ToList());
        }

        // GET /Flowers/SearchData — lightweight list for the command palette
        [ResponseCache(Duration = 300)]
        public async Task<IActionResult> SearchData()
        {
            var flowers = await _db.Flowers
                .OrderBy(f => f.CommonName)
                .Select(f => new { id = f.Id, name = f.CommonName, sci = f.ScientificName, image = f.ImagePath })
                .ToListAsync();
            return Json(flowers);
        }

        // GET /Flowers/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var flower = await _db.Flowers.FindAsync(id);
            if (flower == null) return NotFound();

            // "More like this": same season first, then top up with anything else
            var related = await _db.Flowers
                .Where(f => f.Id != id && f.BloomSeason == flower.BloomSeason)
                .OrderBy(f => f.CommonName)
                .Take(3)
                .ToListAsync();
            if (related.Count < 3)
            {
                var pickedIds = related.Select(r => r.Id).ToList();
                related.AddRange(await _db.Flowers
                    .Where(f => f.Id != id && !pickedIds.Contains(f.Id))
                    .OrderBy(f => f.CommonName)
                    .Take(3 - related.Count)
                    .ToListAsync());
            }
            ViewData["Related"] = related;

            return View(flower);
        }

        // GET /Flowers/Garden — the flowers you grow, laid out like a bed,
        // plus today's care check-in list
        public async Task<IActionResult> Garden()
        {
            var flowers = await _db.Flowers
                .Where(f => f.IsInGarden)
                .OrderBy(f => f.CommonName)
                .ToListAsync();

            // each flower's job for the current month, from its care calendar
            var monthKey = DateTime.Now.ToString("MMM");
            ViewData["Tasks"] = flowers.ToDictionary(f => f.Id, f =>
                (f.CareCalendar ?? "")
                    .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(l => l.Split('|', 2))
                    .Where(p => p.Length == 2 && p[0].Equals(monthKey, StringComparison.OrdinalIgnoreCase))
                    .Select(p => p[1])
                    .FirstOrDefault());
            ViewData["MonthName"] = DateTime.Now.ToString("MMMM");

            // today's check-in list (recent checks cover the longest cadence + this month)
            var today = DateTime.Today;
            var lookback = new DateTime(today.Year, today.Month, 1) < today.AddDays(-7)
                ? new DateTime(today.Year, today.Month, 1) : today.AddDays(-7);
            var recent = await _db.CareChecks.Where(c => c.Date >= lookback).ToListAsync();
            ViewData["CareItems"] = CareSchedule.ItemsFor(flowers, recent, today);

            return View(flowers);
        }

        // POST /Flowers/CheckCare — tick or untick one care task (AJAX)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckCare(int flowerId, string task)
        {
            if (task != "water" && task != "month") return BadRequest();
            var flower = await _db.Flowers.FindAsync(flowerId);
            if (flower == null || !flower.IsInGarden) return NotFound();

            var today = DateTime.Today;
            // "month" toggles the whole month's row; "water" just today's
            var windowStart = task == "month" ? new DateTime(today.Year, today.Month, 1) : today;
            var existing = await _db.CareChecks
                .Where(c => c.FlowerId == flowerId && c.TaskKey == task && c.Date >= windowStart)
                .ToListAsync();

            bool done;
            if (existing.Count > 0)
            {
                _db.CareChecks.RemoveRange(existing);
                done = false;
            }
            else
            {
                _db.CareChecks.Add(new CareCheck { FlowerId = flowerId, TaskKey = task, Date = today });
                done = true;
            }
            await _db.SaveChangesAsync();
            return Json(new { done });
        }

        // POST /Flowers/ToggleGarden/5 — add or remove a flower from "my garden"
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleGarden(int id)
        {
            var flower = await _db.Flowers.FindAsync(id);
            if (flower == null) return NotFound();
            flower.IsInGarden = !flower.IsInGarden;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id });
        }

        // ----- Admin actions (no login yet — protect these when accounts arrive) -----

        // GET /Flowers/Create — optionally pre-filled from a saved identification
        public async Task<IActionResult> Create(int? fromRecord, string? sci, string? common)
        {
            var flower = new Flower();
            if (fromRecord.HasValue)
            {
                var rec = await _db.IdentifyRecords.FindAsync(fromRecord.Value);
                if (rec != null)
                {
                    flower.ImagePath = rec.ImagePath;
                    flower.ScientificName = sci ?? rec.TopScientificName;
                    flower.CommonName = common ?? rec.TopCommonName ?? "";
                }
            }
            return View(flower);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(PhotoStorage.MaxBytes + 64 * 1024)]
        public async Task<IActionResult> Create(Flower flower, IFormFile? photo)
        {
            await ApplyUploadedPhotoAsync(flower, photo);
            if (!ModelState.IsValid) return View(flower);
            flower.DateAdded = DateTime.UtcNow;
            _db.Flowers.Add(flower);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = flower.Id });
        }

        // GET /Flowers/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var flower = await _db.Flowers.FindAsync(id);
            if (flower == null) return NotFound();
            return View(flower);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(PhotoStorage.MaxBytes + 64 * 1024)]
        public async Task<IActionResult> Edit(int id, Flower flower, IFormFile? photo)
        {
            if (id != flower.Id) return NotFound();
            await ApplyUploadedPhotoAsync(flower, photo);
            if (!ModelState.IsValid) return View(flower);
            _db.Update(flower);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = flower.Id });
        }

        // Saves an uploaded photo onto the flower (replacing any previous
        // upload); a bad file becomes a validation message on the form.
        private async Task ApplyUploadedPhotoAsync(Flower flower, IFormFile? photo)
        {
            if (photo == null) return; // keeping the current picture

            var problem = PhotoStorage.Problem(photo);
            if (problem == null)
            {
                await using var stream = photo.OpenReadStream();
                var saved = await _photos.SaveAsync(stream);
                if (saved == null)
                {
                    problem = "That image couldn't be read — try exporting it again as JPEG or PNG.";
                }
                else
                {
                    await DeleteUnlessSharedAsync(flower.ImagePath);
                    flower.ImagePath = saved;
                }
            }
            if (problem != null) ModelState.AddModelError("photo", problem);
        }

        // A flower added from an identification shares that photo file with
        // the history record — leave shared files alone.
        private async Task DeleteUnlessSharedAsync(string? webPath)
        {
            if (string.IsNullOrWhiteSpace(webPath)) return;
            if (await _db.IdentifyRecords.AnyAsync(r => r.ImagePath == webPath)) return;
            _photos.Delete(webPath);
        }

        // GET /Flowers/Delete/5 — confirmation page
        public async Task<IActionResult> Delete(int id)
        {
            var flower = await _db.Flowers.FindAsync(id);
            if (flower == null) return NotFound();
            return View(flower);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flower = await _db.Flowers.FindAsync(id);
            if (flower != null)
            {
                await DeleteUnlessSharedAsync(flower.ImagePath); // uploads only; seed images stay
                _db.Flowers.Remove(flower);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
