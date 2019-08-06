using Microsoft.AspNetCore.Mvc;
using PalTracker.ConfigurationEntities;

namespace PalTracker.Controllers
{
    [Route("/")]
    public class WelcomeController : ControllerBase
    {
        private readonly WelcomeMessage _welcomeMessage;

        public WelcomeController(WelcomeMessage welcomeMessage)
        {
            _welcomeMessage = welcomeMessage;
        }

        [HttpGet]
        public string SayHello()
        {
            return _welcomeMessage.Message;
        }
    }
}
