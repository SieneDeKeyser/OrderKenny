using Microsoft.EntityFrameworkCore;
using Order_domain.Data;

namespace Order_domain.Customers
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(OrderDbContext context) : base(context)
        {
        }
    }
}
