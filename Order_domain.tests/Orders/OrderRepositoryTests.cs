using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Order_domain.Data;
using Order_domain.Orders;
using Xunit;

namespace Order_domain.tests.Orders
{
    public class OrderRepositoryTests
    {
       
        

        private static DbContextOptions<OrderDbContext> CreateNewInMemoryDatabase()
        {
            return new DbContextOptionsBuilder<OrderDbContext>()
                .UseInMemoryDatabase("OrderDbContext" + Guid.NewGuid().ToString("N")).Options;
        }

        //public OrderRepositoryTests()
        //{
        //    var context = new OrderDbContext(CreateNewInMemoryDatabase());
        //    _orderRepository = new OrderRepository(context);
        //}

        [Fact]
        public void GetOrdersForCustomer()
        {
            var context = new OrderDbContext(CreateNewInMemoryDatabase());
            OrderRepository _orderRepository = new OrderRepository(context);

            Guid customerId = Guid.NewGuid();
            Guid otherCustomerId = Guid.NewGuid();

            Order order1 = OrderTestBuilder.AnOrder().WithCustomerId(customerId).WithId(Guid.NewGuid()).Build();
            Order order2 = OrderTestBuilder.AnOrder().WithCustomerId(otherCustomerId).WithId(Guid.NewGuid()).Build();
            Order order3 = OrderTestBuilder.AnOrder().WithCustomerId(customerId).WithId(Guid.NewGuid()).Build();

            _orderRepository.Save(order1);
            _orderRepository.Save(order2);
            _orderRepository.Save(order3);

            List<Order> ordersForCustomer = _orderRepository.GetOrdersForCustomer(customerId).ToList();

            Assert.Contains(order1, ordersForCustomer);
            Assert.Contains(order3, ordersForCustomer);
        }
    }
}
