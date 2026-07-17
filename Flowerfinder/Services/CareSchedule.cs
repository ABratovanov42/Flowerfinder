using Flowerfinder.Models;

namespace Flowerfinder.Services
{
    /// <summary>One item on the garden's daily to-do list.</summary>
    public record CareItem(Flower Flower, string TaskKey, string Label, string? Detail, bool Done);

    /// <summary>
    /// Turns garden flowers + past check-offs into "what needs you today".
    /// Watering rhythm follows the flower's thirst; the care-calendar job
    /// asks once a month.
    /// </summary>
    public static class CareSchedule
    {
        /// <summary>Days between watering check-ins.</summary>
        public static int WaterCadence(Watering w) => w switch
        {
            Watering.High => 1,
            Watering.Moderate => 2,
            _ => 4
        };

        public static List<CareItem> ItemsFor(IEnumerable<Flower> gardenFlowers, IReadOnlyList<CareCheck> recentChecks, DateTime today)
        {
            var items = new List<CareItem>();
            var monthKey = today.ToString("MMM");

            foreach (var f in gardenFlowers)
            {
                // watering: due when the last check-in is older than the rhythm
                var lastWater = recentChecks
                    .Where(c => c.FlowerId == f.Id && c.TaskKey == "water")
                    .OrderByDescending(c => c.Date)
                    .Select(c => (DateTime?)c.Date)
                    .FirstOrDefault();
                var cadence = WaterCadence(f.Watering);
                var doneToday = lastWater == today;
                var due = lastWater == null || (today - lastWater.Value).TotalDays >= cadence;
                if (due || doneToday)
                {
                    items.Add(new CareItem(f, "water",
                        $"Water the {f.CommonName.ToLower()}",
                        f.Watering switch
                        {
                            Watering.High => "it likes to stay moist",
                            Watering.Moderate => "a steady drink every couple of days",
                            _ => "drought-tough — an occasional soak is plenty"
                        },
                        doneToday));
                }

                // the month's calendar job, once per month
                var monthTask = (f.CareCalendar ?? "")
                    .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(l => l.Split('|', 2))
                    .Where(p => p.Length == 2 && p[0].Equals(monthKey, StringComparison.OrdinalIgnoreCase))
                    .Select(p => p[1])
                    .FirstOrDefault();
                if (monthTask != null)
                {
                    var monthDone = recentChecks.Any(c => c.FlowerId == f.Id && c.TaskKey == "month"
                        && c.Date.Year == today.Year && c.Date.Month == today.Month);
                    items.Add(new CareItem(f, "month", monthTask, $"once this {today:MMMM}", monthDone));
                }
            }

            return items
                .OrderBy(i => i.Done)
                .ThenBy(i => i.Flower.CommonName)
                .ThenBy(i => i.TaskKey)
                .ToList();
        }
    }
}
