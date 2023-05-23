using EventAPI.Controllers;
using EventAPI.DomainModel;
using EventAPI.Exceptions;
using EventAPI.Infrastructure.Repository;
using EventAPI.Service;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System.Linq.Expressions;

namespace EventAPI.Tests
{
    public class EventServiceTests
    {
        private readonly Mock<IGenericDbRepository<Event>> _eventRepositoryMock;
        private readonly Mock<IMemoryCache> _memoryCacheMock;
        private readonly EventService _eventService;

        public EventServiceTests()
        {
            _eventRepositoryMock = new Mock<IGenericDbRepository<Event>>();
            _memoryCacheMock = new Mock<IMemoryCache>();
            _eventService = new EventService(_eventRepositoryMock.Object, _memoryCacheMock.Object);
        }

        [Fact]
        public async Task GetEventByIdAsync_Should_Return_Event()
        {
            // Arrange
            var eventId = 1;
            var existingEvent = new Event { Id = eventId, Title = "Event 1" };

            _eventRepositoryMock.Setup(r => r.GetByPrimaryKeyAsync(eventId, It.IsAny<Expression<Func<Event, object>>[]>()))
   .ReturnsAsync(existingEvent);
            // Act
            var result = await _eventService.GetEventByIdAsync(eventId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(existingEvent, result);
        }

        [Fact]
        public async Task CreateEventAsync_Should_Add_Event_And_Clear_Cache()
        {
            // Arrange
            var newEvent = new Event { Title = "New Event" };

            // Act
            await _eventService.CreateEventAsync(newEvent);

            // Assert
            _eventRepositoryMock.Verify(r => r.AddModelAsync(newEvent), Times.Once);
            _memoryCacheMock.Verify(c => c.Remove("events"), Times.Once);
        }

        [Fact]
        public async Task AddParticipantToEventAsync_GivenInviteIsAccepted_Should_Add_Participant()
        {
            // Arrange
            var eventId = 1;
            var userId = 1;
            var existingEvent = new Event { Id = eventId, Title = "Event 1" };
         
            existingEvent.AddInvitation(new Invitation { EventId = eventId, UserId = userId, Accepted = true });
            var @params = new AddParticipantParams { eventid = eventId, userid = userId };
            _eventRepositoryMock.Setup(r => r.GetByPrimaryKeyAsync(@params.eventid, It.IsAny<Expression<Func<Event, object>>[]>()))
    .ReturnsAsync(existingEvent);

            // Act
            await _eventService.AddParticipantToEventAsync(@params);

            // Assert
            _eventRepositoryMock.Verify(r => r.UpdateModelAsync(existingEvent), Times.Once);
            
            Assert.Single(existingEvent.Participants);
            Assert.Equal(userId, existingEvent.Participants.First().UserId);
            Assert.Equal(existingEvent, existingEvent.Participants.First().Event);
        }

        [Fact]
        public async Task AddParticipantToEventAsync_GivenInviteIsNotAccepted_Should_Not_Add_Participant()
        {
            // Arrange
            var eventId = 1;
            var userId = 1;
            var existingEvent = new Event { Id = eventId, Title = "Event 1" };

            existingEvent.AddInvitation(new Invitation { EventId = eventId, UserId = userId, Accepted = false });
            var @params = new AddParticipantParams { eventid = eventId, userid = userId };
            _eventRepositoryMock.Setup(r => r.GetByPrimaryKeyAsync(@params.eventid, It.IsAny<Expression<Func<Event, object>>[]>()))
    .ReturnsAsync(existingEvent);

            // Act
            await Assert.ThrowsAsync<InvalidAddOperationException<Participant>>(() => _eventService.AddParticipantToEventAsync(@params));
       
            // Assert
            _eventRepositoryMock.Verify(r => r.UpdateModelAsync(existingEvent), Times.Never);
            Assert.Empty(existingEvent.Participants);
        }
    }

    
}
