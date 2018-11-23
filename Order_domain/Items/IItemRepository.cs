using System;
using System.Collections.Generic;

namespace Order_domain.Items
{
    public interface IItemRepository
    {
        Item Save(Item entity);

        Item Update(Item entity);

        IEnumerable<Item> GetAll();

        Item Get(Guid entityId);
    }
}