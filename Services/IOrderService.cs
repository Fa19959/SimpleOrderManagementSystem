using SimpleOrderManagementSystem.Models;

namespace SimpleOrderManagementSystem.Services
{
    public interface IOrderService
    {
        int AddOrder(Order order);
    }
}