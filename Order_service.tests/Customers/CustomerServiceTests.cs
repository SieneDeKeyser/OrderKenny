using System;
using NSubstitute;
using Order_domain;
using Order_domain.Customers;
using Order_domain.tests.Customers;
using Order_service.Customers;
using Xunit;

namespace Order_service.tests.Customers
{
    public class CustomerServiceTests
    {
        private readonly CustomerService _customerService;
        private readonly IRepository<Customer> _customerRepository;

        public CustomerServiceTests()
        {
            _customerRepository = Substitute.For<IRepository<Customer>>();
            _customerService = new CustomerService(_customerRepository, new CustomerValidator());
        }

        [Fact]
        public void CreateCustomer_HappyPath()
        {
            Customer customer = CustomerTestBuilder.ACustomer().Build();
            _customerRepository.Save(customer).Returns(customer);
            Customer createdCustomer = _customerService.CreateCustomer(customer);

            Assert.NotNull(createdCustomer);
        }

        [Fact]
        public void CreateCustomer_GivenCustomerThatIsNotValidForCreation_ThenThrowException()
        {
            Customer customer = CustomerTestBuilder.AnEmptyCustomer().Build();

            Exception ex = Assert.Throws<InvalidOperationException>(() => _customerService.CreateCustomer(customer));
            Assert.Contains("Invalid Customer provided for creation", ex.Message);
        }
    }
}
