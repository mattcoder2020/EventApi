using EventAPI.Controllers;
using EventAPI.DomainModel;

namespace EventAPI.Service
{
    public interface IEventService
    {
        Task AddParticipantToEventAsync(AddParticipantParams @params);
        Task CreateEventAsync(Event newevent);
        Task DeleteEventAsync(int id);
        Task<Event> GetEventByIdAsync(int id);
        Task<ResultSet> GetEventsWithPaginationAsync(int page, int pageSize);
        Task UpdateEventAsync(int id, Event updateevent);
    }
}