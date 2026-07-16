namespace Flowerfinder.Models
{
    /// <summary>One saved photo identification — powers the "past finds" page.</summary>
    public class IdentifyRecord
    {
        public int Id { get; set; }
        public DateTime DateTaken { get; set; }        // UTC
        public string ImagePath { get; set; } = "";    // the uploaded photo, saved locally

        // the best guess at the time
        public string? TopScientificName { get; set; }
        public string? TopCommonName { get; set; }
        public int TopScorePercent { get; set; }

        public int? MatchedFlowerId { get; set; }      // catalog flower it matched, if any
        public bool Demo { get; set; }                 // staged demo answer, not a real one
    }
}
