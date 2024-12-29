using SimpleOrderManagementSystem.Models;

namespace SimpleOrderManagementSystem.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int AddOrder(Order order)
        {

            _context.Orders.Add(order);
            _context.SaveChanges();
            return order.OId;
        }
    }
}
