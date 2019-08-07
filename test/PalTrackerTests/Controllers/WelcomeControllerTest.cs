using FluentAssertions;
using PalTracker.ConfigurationEntities;
using PalTracker.Controllers;
using Xunit;

namespace PalTrackerTests.Controllers
{
    public class WelcomeControllerTest
    {
        [Fact]
        public void Get()
        {
            // Arrange
            var message = new WelcomeMessage("hello from test");

            // Act
            var controller = new WelcomeController(message);

            // Assert
            controller.SayHello().Should().Be("hello from test");
        }
    }
}
