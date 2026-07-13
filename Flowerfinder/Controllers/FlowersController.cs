using Flowerfinder.Data;
using Flowerfinder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flowerfinder.Controllers
{
    public class FlowersController : Controller
    {
        private readonly AppDbContext _db;

        public FlowersController(AppDbContext db)
        {
            _db = db;
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

        // ----- Admin actions (no login yet — protect these when accounts arrive) -----

        // GET /Flowers/Create
        public IActionResult Create() => View(new Flower());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Flower flower)
        {
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
        public async Task<IActionResult> Edit(int id, Flower flower)
        {
            if (id != flower.Id) return NotFound();
            if (!ModelState.IsValid) return View(flower);
            _db.Update(flower);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { id = flower.Id });
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
                _db.Flowers.Remove(flower);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
