using System;
using System.Collections.Generic;

namespace Order_domain.Orders
{
    public interface IOrderRepository
    {
        Order Save(Order entity);

        Order Update(Order entity);

        IEnumerable<Order> GetAll();

        Order Get(Guid entityId);

        IEnumerable<Order> GetOrdersForCustomer(Guid customerId);
    }
}