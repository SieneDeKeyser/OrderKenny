using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NJsonSchema;
using NSwag.AspNetCore;
using Oder_infrastructure.Exceptions;
using Oder_infrastructure.Logging;
using Order_api.Controllers.Customers;
using Order_api.Controllers.Customers.Addresses;
using Order_api.Controllers.Customers.Emails;
using Order_api.Controllers.Customers.PhoneNumbers;
using Order_api.Controllers.Items;
using Order_api.Controllers.Orders;
using Order_domain.Customers;
using Order_domain.Items;
using Order_domain.Orders;
using Order_service.Customers;
using Order_service.Data;
using Order_service.Items;
using Order_service.Orders;

namespace Order_api
{
    public class Startup
    {
        public Startup(ILoggerFactory logFactory, IConfiguration configuration)
        {
            ApplicationLogging.LoggerFactory = logFactory;

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        protected virtual void ConfigureOrderServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //services.AddScoped(ConfigureDbContext());

            services.AddSingleton<AddressMapper>();
            services.AddSingleton<EmailMapper>();
            services.AddSingleton<PhoneNumberMapper>();
            services.AddSingleton<CustomerMapper>();
            services.AddSingleton<ItemMapper>();
            services.AddSingleton<OrderMapper>();
            services.AddSingleton<OrderItemMapper>();

            services.AddScoped<CustomerValidator>();
            services.AddScoped<ItemValidator>();
            services.AddScoped<OrderValidator>();

            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddSingleton<ICustomerRepository, CustomerRepository>();

            services.AddSingleton<IItemService, ItemService>();
            services.AddSingleton<IItemRepository, ItemRepository>();

            services.AddSingleton<IOrderService, OrderService>();
            services.AddSingleton<IOrderRepository, OrderRepository>();

            services.AddDbContext<OrderDbContext>(option =>
                option.UseSqlServer("Data Source=.\\SQLExpress;Initial Catalog=OrderDatabase;Integrated Security=True;")
            );
            //services.AddScoped<OrderDbContext>();
            services.AddSwagger();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureOrderServices(services);
        }



        protected virtual DbContextOptions<OrderDbContext> ConfigureDbContext()
        {
            return new DbContextOptionsBuilder<OrderDbContext>()
                .UseSqlServer($"Data Source=.\\SQLExpress;Initial Catalog=OrderDatabase;Integrated Security=True;")
                .Options;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwaggerUi3WithApiExplorer(settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling =
                    PropertyNameHandling.CamelCase;
            });

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();
        }
    }
}
