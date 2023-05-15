using EventAPI.Controllers;

namespace TestEventApi
{
    public class EventControllerTests
        {
            [Fact]
            public void RegisterUserForEvent_ExistingEvent_AddsUserToRegisteredUsers()
            {
                // Arrange
                var dbContext = new EventDbContext(/* Mock or use an in-memory SQLite connection */);
                var controller = new EventController(dbContext);

                var eventId = 1;
                var userName = "John Doe";

                // Act
                controller.RegisterUserForEvent(eventId, userName);
                var @event = controller.GetEventById(eventId);

                // Assert
                Assert.NotNull(@event);
                Assert.Contains(userName, @event.RegisteredUsers);
            }
        }
    
}