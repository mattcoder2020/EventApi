using EventAPI.DomainModel;
using EventAPI.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EventAPI.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventController : ControllerBase
    {
        private readonly IGenericDbRepository<Event> _eventRepository;
        private readonly IMemoryCache _cache;

        public EventController(IGenericDbRepository<Event> eventRepository, IMemoryCache cache)
        {
            _eventRepository = eventRepository;
            _cache = cache;
        }

        [HttpGet]
        public async Task<IActionResult> GetEventsWithPaginationAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var events = _cache.Get<IEnumerable<Event>>("events");
            if (events == null)
            {
                events = await _eventRepository.GetAll();
                var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                _cache.Set("events", events, cacheOptions);
            }

            var totalCount = events.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var results = events.Skip((page - 1) * pageSize).Take(pageSize);

            var response = new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                Page = page,
                PageSize = pageSize,
                Results = results
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var result = _eventRepository.GetByPrimaryKey(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(Event newevent)
        {
            await _eventRepository.AddModel(newevent);
            _cache.Remove("events");
            return CreatedAtAction(nameof(GetEventById), new { id = newevent.Id }, newevent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, Event updateevent)
        {
            var existingEvent = await _eventRepository.GetByPrimaryKey(id);
            if (existingEvent == null)
                return NotFound();
            existingEvent.Title = updateevent.Title;
            existingEvent.Description = updateevent.Description;
            // Update other properties as needed
            existingEvent.EndDateTime = updateevent.EndDateTime;
            existingEvent.StartDateTime = updateevent.StartDateTime;
            existingEvent.TimeZone = updateevent.TimeZone;
            existingEvent.Location = updateevent.Location;
            await _eventRepository.UpdateModel(existingEvent);

            _cache.Remove("events");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var existingEvent = await _eventRepository.GetByPrimaryKey(id);
            if (existingEvent == null)
                return NotFound();
            await _eventRepository.DeleteModel(existingEvent);
            _cache.Remove("events");
            return NoContent();
        }

        //add a method to add a participants to an event
        [HttpPost("{id}/participants")]
        public async Task<IActionResult> AddParticipantToEvent(int id, Participant participant)
        {
            var existingEvent = await _eventRepository.GetByPrimaryKey(id);
            if (existingEvent == null)
                return NotFound();
            existingEvent.Participants.Add(participant);
            await _eventRepository.UpdateModel(existingEvent);
            _cache.Remove("events");
            return NoContent();
        }
       

    }
}


