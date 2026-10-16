namespace RaceDay.API.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan EventTime { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Status { get; set; } = "Open"; // Open or Closed
        public int OrganiserId { get; set; }
        public User? Organiser { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<EventCategory> EventCategories { get; set; } = new List<EventCategory>();
    }
}