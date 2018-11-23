using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Order_domain.Data;

namespace Order_domain.Orders
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(OrderDbContext context) : base(context)
        {
        }

        public Order Save(Order entity)
        {
            Order savedOrder = base.Save(entity);
            return savedOrder;
        }

        public IEnumerable<Order> GetOrdersForCustomer(Guid customerId)
        {
            return GetAll().Where(order => order.CustomerId == customerId).Select(order => order).ToList();
        }
    }
}
