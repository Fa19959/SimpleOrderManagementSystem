using SimpleOrderManagementSystem.Models;

namespace SimpleOrderManagementSystem.Repositories
{
    public interface IOrderRepository
    {
        int AddOrder(Order order);
    }
}