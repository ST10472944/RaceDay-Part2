namespace RaceDay.API.Models
{
    public class EventCategory
    {
        public int EventCategoryId { get; set; }
        public int EventId { get; set; }
        public Event? Event { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public decimal? Price { get; set; }
        public int? MaxParticipants { get; set; }
    }
}