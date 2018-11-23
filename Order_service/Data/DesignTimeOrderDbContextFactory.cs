using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Order_service.Data
{
    public class DesignTimeOrderDbContextFactory : IDesignTimeDbContextFactory<OrderDbContext>
    {
        public OrderDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<OrderDbContext>()
                .UseSqlServer($"Data Source=.\\SQLExpress;Initial Catalog=ParkShark;Integrated Security=True;")
                .EnableSensitiveDataLogging()
                .Options;
            return new OrderDbContext(options);
        }
    }
}
