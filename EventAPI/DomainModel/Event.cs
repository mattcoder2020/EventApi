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
            public List<Participant> Participants { get; set; } = new List<Participant>();
            public List<Invitation> Invitations { get; set; } = new List<Invitation>();

            //method to add a participant to the event
            public bool AddParticipant(Participant user)
            {
                if (Invitations.Any(p => p.UserId == user.UserId && p.Accepted == true))
                {
                  return false;
                }
                Participants.Add(user);
                return true;
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
