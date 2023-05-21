using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EventAPI.DomainModel
{
    public class Participant : ModelBase
    {
        public int EventId { get; set; }
        [JsonIgnore]
        public Event Event { get; set; }
        public int UserId { get; set; }
        
    }
}
