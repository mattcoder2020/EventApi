namespace EventAPI.DomainModel
{
    //Event is aggregate root entity, participants should be managed and navigated from Event entity
    public class Event : ModelBase
    {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime StartDateTime { get; set; }
            public DateTime EndDateTime { get; set; }
            public TimeZone TimeZone { get; set; }
            public string Location { get; set; }
            public User ContactPerson { get; set; }
            public List<User> Participants { get; set; } = new List<User>();

            //method to add a participant to the event
            public void AddParticipant(User user)
            {
                Participants.Add(user);
            }
     }
}
