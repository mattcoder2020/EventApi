using EventAPI.DomainModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EventAPI.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventController : ControllerBase
    {
        private readonly EventRepository _eventRepository;
        private readonly IMemoryCache _cache;

        public EventController(EventRepository eventRepository, IMemoryCache cache)
        {
            _eventRepository = eventRepository;
            _cache = cache;
        }

        [HttpGet]
        public IActionResult GetAllEvents()
        {
            var events = _cache.Get<IEnumerable<Event>>("events");
            if (events == null)
            {
                events = _eventRepository.GetAllEvents();
                var cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
                _cache.Set("events", events, cacheOptions);
            }
            return Ok(events);
        }

        [HttpGet("{id}")]
        public IActionResult GetEventById(int id)
        {
            var event = _eventRepository.GetEventById(id);
        if (event == null)
            return NotFound();
        return Ok(event);
        }

        [HttpPost]
        public IActionResult CreateEvent(Event event)
    {
            _eventRepository.AddEvent(event);
        _cache.Remove("events");
        return CreatedAtAction(nameof(GetEventById), new { id = event.Id }, event);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEvent(int id, Event event)
    {
            var existingEvent = _eventRepository.GetEventById(id);
            if (existingEvent == null)
                return NotFound();
            existingEvent.Name = event.Name;
        existingEvent.Date = event.Date;
        // Update other properties as needed
        _eventRepository.UpdateEvent(existingEvent);
        _cache.Remove("events");
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteEvent(int id)
    {
        var existingEvent = _eventRepository.GetEventById(id);
        if (existingEvent == null)
            return NotFound();
        _eventRepository.DeleteEvent(id);
        _cache.Remove("events");
        return NoContent();
    }

}
