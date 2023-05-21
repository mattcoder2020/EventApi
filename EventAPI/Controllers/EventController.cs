using EventAPI.DomainModel;
using EventAPI.Exceptions;
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
            catch (NotFoundException<Event> ex)
            {
                ModelState.AddModelError("Client Error", ex.Message);
                return NotFound(ModelState);
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
            catch (NotFoundException<Event> ex) 
            {
                ModelState.AddModelError("Client Error", ex.Message); 
                return NotFound(ModelState); 
            }
            return Ok();
        }

       //what is correct url for this?

        [HttpPost("addparticipant")]
        public async Task<IActionResult> AddParticipantToEvent([FromBody] AddParticipantParams @params)
        {
            try
            {
                await _eventService.AddParticipantToEventAsync(@params);
            }
            catch (NotFoundException<Event> ex) 
            {   ModelState.AddModelError("Client Error", ex.Message); 
                return NotFound(ModelState); 
            }
            catch (NotFoundException<User> ex)
            {
                ModelState.AddModelError("Client Error", ex.Message);
                return BadRequest(ModelState);
            }
            catch (InvalidAddOperationException<Participant> ex)
            {
                ModelState.AddModelError("Client Error", ex.Message);
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [HttpPost("addinvitation")]
        public async Task<IActionResult> AddInvitationToEvent([FromBody] AddInvitationParams @params)
        {
            try
            {
                await _eventService.AddInvitationToEventAsync(@params);
            }
            catch (NotFoundException<Event> ex)
            {
                ModelState.AddModelError("Client Error", ex.Message);
                return NotFound(ModelState);
            }
            catch (NotFoundException<User> ex)
            {
                ModelState.AddModelError("Client Error", ex.Message);
                return BadRequest(ModelState);
            }
            return Ok();
        }

        [HttpPost("approveinvitation")]
        public async Task<IActionResult> ApproveInvitation([FromBody] ApproveInvitationParams @params)
        {
            try
            {
                await _eventService.ApproveInvitationAsync(@params);
            }
            catch (NotFoundException<Event> ex )
            {
                ModelState.AddModelError("Client Error", ex.Message);
                return NotFound(ModelState);
            }
          
            return Ok();
        }

    }

    public class AddParticipantParams
    {
        public int eventid { get; set; }
        public int userid { get; set;}
    }

    public class AddInvitationParams
    {
        public int eventid { get; set; }
        public int userid { get; set; }
    }

    public class ApproveInvitationParams
    {
        public int eventid { get; set; }
        public int userid { get; set; }
    }
}


