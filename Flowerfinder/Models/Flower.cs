using System.ComponentModel.DataAnnotations;

namespace Flowerfinder.Models
{
    public class Flower
    {
        public int Id { get; set; }              // unique number for each flower

        [Required]
        public string CommonName { get; set; } = "";   // e.g. "Sunflower"
        public string? ScientificName { get; set; }    // e.g. "Helianthus annuus"
        public string? Family { get; set; }            // botanical family, e.g. "Asteraceae"

        public string? Description { get; set; }  // the story: folklore, meaning, history
        public string? CareGuide { get; set; }    // free-text growing tips

        // Step-by-step growing timeline. One step per line, each line:
        //   timeframe|title|instructions
        // e.g.  Day 1|Sow the seeds|Push each seed 2 cm deep...
        public string? GrowingPlan { get; set; }

        // ----- The in-depth guide (all optional; the detail page shows
        //       whatever exists). Simple line formats, like GrowingPlan. -----

        // Long-form sections. Blocks start with "## Title"; body lines are
        // paragraphs, "- " bullets, or "TIP:" / "WARN:" callouts.
        public string? GuideSections { get; set; }

        // Year of care, one line per month: "Jan|What to do..."  (12 lines)
        public string? CareCalendar { get; set; }

        // Troubleshooting, one per line: "Problem|What you'll see|The fix"
        public string? Problems { get; set; }

        // One per line: "Question?|Answer"
        public string? Faqs { get; set; }

        // What you'll need, one per line: "Item|Why it matters"
        public string? GearList { get; set; }
        public string? ImagePath { get; set; }    // where the photo is saved
        public DateTime DateAdded { get; set; }

        // Structured care facts — these power the catalog's search and filters
        public Sunlight Sunlight { get; set; }
        public Watering Watering { get; set; }
        public BloomSeason BloomSeason { get; set; }
        public string? SoilType { get; set; }     // e.g. "Well-drained, slightly acidic"
        public string? Colors { get; set; }       // comma-separated, e.g. "Pink, White"
        public string? NativeRegion { get; set; } // e.g. "Central Asia"
        public bool IsPerennial { get; set; }     // comes back every year vs replanted

        public bool IsInGarden { get; set; }      // "My garden": flowers the user actually grows
    }

    public enum Sunlight { FullSun, PartialShade, Shade }
    public enum Watering { Low, Moderate, High }
    public enum BloomSeason { Spring, Summer, Autumn, Winter, YearRound }

    // Friendly labels for enums shown on the site
    public static class CareLabels
    {
        public static string Label(this Sunlight s) => s switch
        {
            Sunlight.FullSun => "Full sun",
            Sunlight.PartialShade => "Partial shade",
            _ => "Shade"
        };

        public static string Label(this Watering w) => w switch
        {
            Watering.Low => "Low — drought tolerant",
            Watering.Moderate => "Moderate",
            _ => "High — keep moist"
        };

        public static string Label(this BloomSeason b) => b switch
        {
            BloomSeason.Spring => "Spring",
            BloomSeason.Summer => "Summer",
            BloomSeason.Autumn => "Autumn",
            BloomSeason.Winter => "Winter",
            _ => "Year-round"
        };

        // Soft swatch colours for the little dots shown on catalog cards
        public static string Hex(string colorName) => colorName.Trim().ToLowerInvariant() switch
        {
            "red" => "#c94f4f",
            "pink" => "#e88ab0",
            "white" => "#f5f0e8",
            "yellow" => "#e8c95a",
            "orange" => "#e39554",
            "purple" => "#9b6bb3",
            "blue" => "#6b8fc9",
            "magenta" => "#c94f8e",
            _ => "#b8b0a4"
        };
    }
}
