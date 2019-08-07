using FluentAssertions;
using PalTracker.ConfigurationEntities;
using PalTracker.Controllers;
using Xunit;

namespace PalTrackerTests.Controllers
{
    public class EnvironmentControllerTest
    {
        [Fact]
        public void Get()
        {
            // Arrange
            var cloudFoundryInfo = new CloudFoundryEnvironment(
                "8080",
                "512M",
                "1",
                "127.0.0.1"
            );

            // Act
            var response = new EnvironmentController(cloudFoundryInfo).Get();

            // Assert
            response.Port.Should().Be("8080");
            response.MemoryLimit.Should().Be("512M");
            response.CfInstanceIndex.Should().Be("1");
            response.CfInstanceAddr.Should().Be("127.0.0.1");
        }
    }
}
