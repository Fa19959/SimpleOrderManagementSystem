using SimpleOrderManagementSystem.DTOs;

namespace SimpleOrderManagementSystem.Services
{
    public interface ICompoudedServices
    {
        int PlaceOrder(List<OrderProductInputDTO> orderProducts, int userId);
    }
}