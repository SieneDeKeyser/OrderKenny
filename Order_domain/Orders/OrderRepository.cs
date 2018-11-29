using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Order_domain.Data;

namespace Order_domain.Orders
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly OrderDbContext _Context;

        public OrderRepository(OrderDbContext dBContext)
        {
            _Context = dBContext;
        }

        public Order Save(Order entity)
        {
            _Context.Orders.Add(entity);
            _Context.SaveChanges();
            return entity;
        }

        public Order Update(Order entity)
        {
            _Context.SaveChanges();
            return entity;
        }

        public Order Get(Guid entityId)
        {
            return _Context.Orders
                .Include(order => order.Customer)
                .Include(order => order.OrderItems)
                .Single(item => item.Id.Equals(entityId));
        }

        public IList<Order> GetAll()
        {
            return _Context.Orders
                .Include(order => order.Customer)
                .Include(order => order.OrderItems)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<Order> GetOrdersForCustomer(Guid customerId)
        {
            return _Context.Orders
                .Where(order => order.Customer.Id.Equals(customerId))
                .ToList();
        }
    }
}
