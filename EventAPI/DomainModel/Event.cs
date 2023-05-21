namespace EventAPI.DomainModel
{
    //Event is aggregate root entity, participants should be managed and navigated from Event entity
    public class Event : ModelBase
    {
            public string Title { get; set; }
            public string Description { get; set; }
            public DateTime StartDateTime { get; set; }
            public DateTime EndDateTime { get; set; }
            public string TimeZone { get; set; }
            public string Location { get; set; }
            public string ContactPerson { get; set; }
        // Aggregates that are read only and managed by Aggregate Root i.e. Event entity
            public List<Participant> Participants { get; private set; } = new List<Participant>();
            public List<Invitation> Invitations { get; private set; } = new List<Invitation>();

        //method to add a participant to the event
        public bool AddParticipant(Participant user)
        {
                if (Invitations.Any(p => p.UserId == user.UserId && p.EventId==this.Id && p.Accepted == true))
                {
                  Participants.Add(user);
                  return true;
                }
            return false;

        }

            public bool AddInvitation(Invitation user)
            {
               if (Invitations.Any(p => p.UserId == user.UserId))
                   return false;
               Invitations.Add(user);
                   return true;
            }
    }
}
