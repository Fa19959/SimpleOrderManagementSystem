using SimpleOrderManagementSystem.Models;
using SimpleOrderManagementSystem.Repositories;

namespace SimpleOrderManagementSystem.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public int AddOrder(Order order)
        {
            return _orderRepository.AddOrder(order);
        }

    }
}
