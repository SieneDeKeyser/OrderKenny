using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Order_domain.Customers;
using Order_domain.Data;
using Order_domain.tests.Customers;
using Xunit;

namespace Order_domain.tests
{
    public class RepositoryIntegrationTest 
    {
        private static DbContextOptions<OrderDbContext> CreateNewInMemoryDatabase()
        {
            return new DbContextOptionsBuilder<OrderDbContext>()
                .UseInMemoryDatabase("OrderDbContext" + Guid.NewGuid().ToString("N")).Options;
        }

        [Fact]
        public void Save()
        {
            var context = new OrderDbContext(CreateNewInMemoryDatabase());
            IRepository<Customer> _repository = new CustomerRepository(context);
            var customerToSave = CustomerTestBuilder.ACustomer().Build();

            var savedCustomer = _repository.Save(customerToSave);

            Assert.NotEqual(Guid.Empty, savedCustomer.Id);
        }

        [Fact]
        public void Update()
        {
            var context = new OrderDbContext(CreateNewInMemoryDatabase());
            IRepository<Customer> _repository = new CustomerRepository(context);

            var customerToSave = CustomerTestBuilder.ACustomer()
                .WithFirstname("Jo")
                .WithLastname("Jorissen")
                .Build();

            var savedCustomer = _repository.Save(customerToSave);

            var updatedCustomer = _repository.Update(CustomerTestBuilder.ACustomer()
                    .WithId(savedCustomer.Id)
                    .WithFirstname("Joske")
                    .WithLastname("Jorissen")
                    .Build());

            Assert.NotEqual(Guid.Empty, savedCustomer.Id);
            Assert.Equal(savedCustomer.Id, updatedCustomer.Id);
            Assert.Equal("Joske", updatedCustomer.FirstName);
            Assert.Equal("Jorissen", updatedCustomer.LastName);
            Assert.Single(_repository.GetAll());
        }

        [Fact]
        public void Get()
        {
            var context = new OrderDbContext(CreateNewInMemoryDatabase());
            IRepository<Customer> _repository = new CustomerRepository(context);
            Customer savedCustomer = (CustomerTestBuilder.ACustomer().Build());
            _repository.Save(savedCustomer);
            Customer actualCustomer = _repository.Get(savedCustomer.Id);

            Assert.Equal(actualCustomer.Id, savedCustomer.Id);
        }

        [Fact]
        public void GetAll()
        {
            var context = new OrderDbContext(CreateNewInMemoryDatabase());
            IRepository<Customer> _repository = new CustomerRepository(context);

            var customer1 = _repository.Save(CustomerTestBuilder.ACustomer().Build());
            var customer2 = _repository.Save(CustomerTestBuilder.ACustomer().Build());

            var customers = _repository.GetAll();

            Assert.Equal(2, customers.ToArray().Length);
        }
    }
}
