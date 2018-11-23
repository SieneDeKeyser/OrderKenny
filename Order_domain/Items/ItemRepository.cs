using Microsoft.EntityFrameworkCore;
using Order_domain.Data;

namespace Order_domain.Items
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(OrderDbContext context) : base(context)
        {
        }
    }
}
