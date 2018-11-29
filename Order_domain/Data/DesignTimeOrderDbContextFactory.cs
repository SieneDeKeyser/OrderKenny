using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Order_domain.Data
{
    public class DesignTimeOrderDbContextFactory : IDesignTimeDbContextFactory<OrderDbContext>
    {
        public readonly ILoggerFactory efLoggerFactory
          = new LoggerFactory(new[] { new ConsoleLoggerProvider((category, level) => category.Contains("Command") && level == LogLevel.Information, true), });

        public OrderDbContext CreateDbContext(string[] args)
        {
            IConfiguration Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            var options = new DbContextOptionsBuilder<OrderDbContext>()
                .UseSqlServer(Configuration.GetConnectionString("OrderDataBase"))
                .EnableSensitiveDataLogging()
                .Options;
            return new OrderDbContext(options);
        }

        public DesignTimeOrderDbContextFactory(ILoggerFactory efLoggerFactory)
        {
            this.efLoggerFactory = efLoggerFactory;
        }

        public DesignTimeOrderDbContextFactory()
        {
        }
    }
}
