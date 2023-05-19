namespace EventAPI.DomainModel
{
    public class Invitation
    {
        
            public int EventId { get; set; }
            public Event Event { get; set; }
            public int UserId { get; set; }
            public bool Accepted { get; set; }

       
    }

}
}
