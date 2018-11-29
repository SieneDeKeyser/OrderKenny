using Microsoft.EntityFrameworkCore;
using Order_domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Order_domain.Customers
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly OrderDbContext _context;

        public CustomerRepository(OrderDbContext dBContext)
        {
            _context = dBContext;
        }

        public Customer Save(Customer entity)
        {
            _context.Customers.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Customer Update(Customer entity)
        {
            _context.SaveChanges();
            return entity;
        }

        public Customer Get(Guid entityId)
        {
            return _context.Customers
                .AsNoTracking()
                .Single(item => item.Id.Equals(entityId));
        }

        public IList<Customer> GetAll()
        {
            return _context.Customers
                .AsNoTracking()
                .ToList();
        }
    }
}
