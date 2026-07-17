namespace Flowerfinder.Models
{
    /// <summary>
    /// One ticked-off care task for a garden flower — a row means "done".
    /// TaskKey is "water" (rhythm set by the flower's watering need) or
    /// "month" (this month's care-calendar job, once per month).
    /// </summary>
    public class CareCheck
    {
        public int Id { get; set; }
        public int FlowerId { get; set; }
        public DateTime Date { get; set; }     // local date, midnight
        public string TaskKey { get; set; } = "";
    }
}
