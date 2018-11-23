using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Order_domain.Data;

namespace Order_api.integrationTests
{
    public class TestStartup : Startup
    {
        public TestStartup(ILoggerFactory logFactory, IConfiguration configuration) : base(logFactory, configuration)
        {
        }

        protected DbContextOptions<OrderDbContext> ConfigureDbContext()
        {
            return new DbContextOptionsBuilder<OrderDbContext>()
                .UseInMemoryDatabase("ParkSharkDb" + Guid.NewGuid().ToString("N")).Options;
        }

        protected override void ConfigureOrderServices(IServiceCollection services)
        {
            base.ConfigureOrderServices(services);

            services.AddMvc()
                .AddApplicationPart(typeof(Startup).Assembly);
            services.AddSingleton(ConfigureDbContext());
        }

    }
}
