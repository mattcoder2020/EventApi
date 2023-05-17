namespace EventAPI.DomainModel
{
    public class Participant : ModelBase
    {
        public int EventId { get; set; }
        public Event Event { get; set; }

        public int UserId { get; set; }
        
    }
}
