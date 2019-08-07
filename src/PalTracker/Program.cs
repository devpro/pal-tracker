using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using Steeltoe.Extensions.Logging;

namespace PalTracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return  WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(config => config.AddCloudFoundry())
                // to make Steeltoe 2.2 work but deactivated as we need to fallback to 2.1
                // .ConfigureLogging((builderContext, loggingBuilder) =>
                // {
                //     loggingBuilder.AddDynamicConsole();
                // })
                .UseStartup<Startup>();
        }
    }
}
