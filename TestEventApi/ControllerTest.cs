using EventAPI.Controllers;
using EventAPI.DomainModel;
using EventAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EventAPI.Tests
{
    public class EventControllerTests
    {
        private readonly Mock<IEventService> _eventServiceMock;
        private readonly EventController _eventController;

        public EventControllerTests()
        {
            _eventServiceMock = new Mock<IEventService>();
            _eventController = new EventController(_eventServiceMock.Object);
        }

        [Fact]
        public async Task GetEventsWithPaginationAsync_ReturnsOkResult()
        {
            // Arrange
            var expectedPage = 1;
            var expectedPageSize = 10;
            var expectedResult = new ResultSet
            {
                Page = expectedPage,
                PageSize = expectedPageSize,
                Results = new List<Event> { new Event { Id = 1, Title = "Event 1" } }
            };
        
            _eventServiceMock.Setup(x => x.GetEventsWithPaginationAsync(expectedPage, expectedPageSize))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _eventController.GetEventsWithPaginationAsync(expectedPage, expectedPageSize);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResult, okResult.Value);
        }

        [Fact]
        public async Task GetEventById_ReturnsOkResult()
        {
            // Arrange
            var expectedId = 1;
            var expectedResult = new Event { Id = 1, Title = "Event 1" };

            _eventServiceMock.Setup(x => x.GetEventByIdAsync(expectedId))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _eventController.GetEventById(expectedId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResult, okResult.Value);
        }

        [Fact]
        public async Task AddParticipantToEvent_ReturnsOkResult()
        {
            // Arrange
            var expectedParams = new AddParticipantParams { eventid = 1, userid = 1 };

            // Mock the method call and handle exceptions if necessary

            // Act
            var result = await _eventController.AddParticipantToEvent(expectedParams);

            // Assert
            Assert.IsType<OkResult>(result);
        }
    }
}
