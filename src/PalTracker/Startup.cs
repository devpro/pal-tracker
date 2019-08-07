﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PalTracker.ConfigurationEntities;
using PalTracker.Repositories;

namespace PalTracker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton(x => new WelcomeMessage(
                GetConfigurationValue("WELCOME_MESSAGE")
            ));
            services.AddSingleton(x => new CloudFoundryEnvironment(
                GetConfigurationValue("PORT"),
                GetConfigurationValue("MEMORY_LIMIT"),
                GetConfigurationValue("CF_INSTANCE_INDEX"),
                GetConfigurationValue("CF_INSTANCE_ADDR")
            ));

            services.AddSingleton<ITimeEntryRepository, InMemoryTimeEntryRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private string GetConfigurationValue(string key)
        {
            return Configuration.GetValue<string>(key, $"{key} not configured.");
        }
    }
}
