﻿using Microsoft.AspNetCore.TestHost;
using PalTracker;

namespace PalTrackerTests
{
    public static class IntegrationTestServer
    {
        public static TestServer Start()
        {
            return new TestServer(Program.CreateWebHostBuilder(new string[] { }));
        }
    }
}
