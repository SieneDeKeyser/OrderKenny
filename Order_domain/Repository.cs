using Microsoft.EntityFrameworkCore;
using Order_domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Order_domain
{
    public abstract class Repository<T>
        where T : Entity
    {
        private readonly OrderDbContext _context;

        public Repository(OrderDbContext context)
        {
            _context = context;
        }
        public Repository()
        {

        }
        public T Save(T entity)
        {
            entity.GenerateId();
            _context.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            var entityFromDb = Get(entity.Id);
            _context.Attach(entityFromDb);

            entityFromDb = entity;
            
            _context.SaveChanges();
            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T Get(Guid entityId)
        {
            return _context.Set<T>().SingleOrDefault(x => x.Id == entityId);
        }

        /**
         * Since we don't use transactions yet, we need a way to reset the database
         * in the tests. We'll use this method. Obviously this is a method that should
         * never be available in production...
         */
    }
}
