using EventAPI.DomainModel;
using EventAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace EventAPI.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEventsWithPaginationAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var resultset = await _eventService.GetEventsWithPaginationAsync(page, pageSize);
            return Ok(resultset);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var result = await _eventService.GetEventByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(Event newevent)
        {
            await _eventService.CreateEventAsync(newevent);
            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateEvent(Event updateevent)
        {
            try
            {
                await _eventService.UpdateEventAsync(updateevent.Id, updateevent);
            }
            catch (NotFoundException<Event>)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            try
            {
                await _eventService.DeleteEventAsync(id);
            }
            catch (NotFoundException<Event>)
            {
                return NotFound();
            }
            return Ok();
        }

       //what is correct url for this?

        [HttpPost()]
        public async Task<IActionResult> AddParticipantToEvent([FromBody] AddParticipantParams @params)
        {
            try
            {
                await _eventService.AddParticipantToEventAsync(@params);
            }
            catch (NotFoundException<Event>)
            {
                return NotFound();
            }
            return Ok();
        }
       

    }

    public class AddParticipantParams
    {
        public int eventid { get; set; }
        public int userid { get; set;}
    }
}


