using Microsoft.AspNetCore.Mvc;
using PalTracker.ConfigurationEntities;

namespace PalTracker.Controllers
{
    [Route("/env")]
    public class EnvironmentController : ControllerBase
    {
        private CloudFoundryEnvironment _cloudFoundryEnvironment;

        public EnvironmentController(CloudFoundryEnvironment cloudFoundryEnvironment)
        {
            _cloudFoundryEnvironment = cloudFoundryEnvironment;
        }

        public CloudFoundryEnvironment Get()
        {
            return _cloudFoundryEnvironment;
        }
    }
}
