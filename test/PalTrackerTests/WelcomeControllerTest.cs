﻿using PalTracker.ConfigurationEntities;
using PalTracker.Controllers;
using Xunit;

namespace PalTrackerTests
{
    public class WelcomeControllerTest
    {
        [Fact]
        public void Get()
        {
            var message = new WelcomeMessage("hello from test");

            var controller = new WelcomeController(message);

            Assert.Equal("hello from test", controller.SayHello());
        }
    }
}
