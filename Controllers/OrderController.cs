using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleOrderManagementSystem.DTOs;
using SimpleOrderManagementSystem.Helpers;
using SimpleOrderManagementSystem.Services;

namespace SimpleOrderManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICompoudedServices _compoudedServices;

        public OrderController(IOrderService orderService, ICompoudedServices compoudedServices)
        {
            _orderService = orderService;
            _compoudedServices = compoudedServices;
        }

        [Authorize]
        [HttpPost]
        public IActionResult PlaceOrder(List<OrderProductInputDTO> orderItems)
        {
            string token = JwtHelper.ExtractToken(Request);
            int userID = int.Parse( JwtHelper.GetClaimValue(token,"sub") );

            int orderId =  _compoudedServices.PlaceOrder(orderItems,userID);
            return Ok(orderId);

        }
    }
}
