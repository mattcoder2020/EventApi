namespace EventAPI.DomainModel
{
    public class Event
    {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime DateTime { get; set; }
            public TimeZone TimeZone { get; set; }
            public string Location { get; set; }
            

            public List<string> RegisteredUsers { get; set; } = new List<string>();
       
    }
}
