using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Order_domain.Data;

namespace Order_domain.Items
{
    public class ItemRepository : IRepository<Item>
    {
        private readonly OrderDbContext _context;

        public ItemRepository(OrderDbContext dBContext)
        {
            _context = dBContext;
        }

        public Item Save(Item entity)
        {
            _context.Items.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public Item Update(Item entity)
        {
            _context.Items.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public Item Get(Guid entityId)
        {
            return _context.Items
                .AsNoTracking()
                .Single(item => item.Id.Equals(entityId));
        }

        public IList<Item> GetAll()
        {
            return _context.Items
                .AsNoTracking()
                .ToList();
        }
    }
}
