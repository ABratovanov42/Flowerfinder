using System.Diagnostics;
using Flowerfinder.Data;
using Flowerfinder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flowerfinder.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _db;

        public HomeController(ILogger<HomeController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            // "In bloom right now" — flowers at their best this time of year
            var month = DateTime.Now.Month;
            var season = month switch
            {
                12 or 1 or 2 => BloomSeason.Winter,
                >= 3 and <= 5 => BloomSeason.Spring,
                >= 6 and <= 8 => BloomSeason.Summer,
                _ => BloomSeason.Autumn
            };

            var blooming = await _db.Flowers
                .Where(f => f.BloomSeason == season || f.BloomSeason == BloomSeason.YearRound)
                .OrderBy(f => EF.Functions.Random())
                .Take(4)
                .ToListAsync();

            ViewData["Season"] = season;
            ViewData["Blooming"] = blooming;

            // "This month in the garden" — pull the current month's task out
            // of the care calendars (format: "Jul|task"). Once the user has
            // marked flowers as theirs, only their garden speaks here.
            var monthKey = DateTime.Now.ToString("MMM");
            var hasGarden = await _db.Flowers.AnyAsync(f => f.IsInGarden);
            var withCalendars = await _db.Flowers
                .Where(f => f.CareCalendar != null && (!hasGarden || f.IsInGarden))
                .ToListAsync();
            ViewData["HasGarden"] = hasGarden;
            var monthTasks = withCalendars
                .Select(f => new
                {
                    Flower = f,
                    Task = f.CareCalendar!
                        .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                        .Select(l => l.Split('|', 2))
                        .Where(p => p.Length == 2 && p[0].Equals(monthKey, StringComparison.OrdinalIgnoreCase))
                        .Select(p => p[1])
                        .FirstOrDefault()
                })
                .Where(x => x.Task != null)
                .OrderBy(x => x.Flower.CommonName)
                .Select(x => (x.Flower, x.Task!))
                .ToList();

            ViewData["MonthTasks"] = monthTasks;
            ViewData["MonthName"] = DateTime.Now.ToString("MMMM");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // Friendly 404 — wired up via UseStatusCodePagesWithReExecute in Program.cs
        public IActionResult Missing()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
