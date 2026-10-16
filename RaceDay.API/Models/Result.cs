namespace RaceDay.API.Models
{
    public class Result
    {
        public int ResultId { get; set; }
        public int EnrolmentId { get; set; }
        public Enrolment? Enrolment { get; set; }
        public TimeSpan? FinishTime { get; set; }
        public int? Position { get; set; }
        public string Status { get; set; } = "DNS"; // DNS, DNF, Finished
        public string? Notes { get; set; }
    }
}