using Microsoft.EntityFrameworkCore;
using SimpleOrderManagementSystem.DTOs;
using SimpleOrderManagementSystem.Models;

namespace SimpleOrderManagementSystem.Services
{
    public class CompoudedServices : ICompoudedServices
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly ApplicationDbContext _context;
        public CompoudedServices(IUserService userService, IProductService productService,
                                  IOrderService orderService, ApplicationDbContext context)
        {
            _userService = userService;
            _productService = productService;
            _orderService = orderService;
            _context = context;
        }

        public int PlaceOrder(List<OrderProductInputDTO> orderProducts, int userId)
        {
            if (orderProducts == null || orderProducts.Count == 0)
            {
                throw new ArgumentException("OrderProducts cannot be null or empty.");
            }

            Product product = null;
            decimal totalAmount = 0;
            List<(int productId, int quantity)> orderItems = new List<(int productId, int quantity)>();
            List<Product> itemsToUpdateStock = new List<Product>();

            foreach (var orderProduct in orderProducts)
            {
                // Validate product name
                if (string.IsNullOrWhiteSpace(orderProduct.ProductName))
                {
                    throw new ArgumentException("Product name cannot be null or empty.");
                }

                // Fetch the product details
                product = _productService.GetProductByName(orderProduct.ProductName);

                if (product == null)
                {
                    throw new InvalidOperationException($"Product '{orderProduct.ProductName}' not found.");
                }

                // Check stock availability
                if (product.Stock >= orderProduct.quantity)
                {
                    totalAmount += product.Price * orderProduct.quantity;
                    orderItems.Add((product.PId, orderProduct.quantity));
                    product.Stock -= orderProduct.quantity;
                    itemsToUpdateStock.Add(product);
                }
                else
                {
                    throw new InvalidOperationException($"Insufficient stock for product '{product.Name}'. Available stock: {product.Stock}.");
                }
            }

            // Create the order
            Order order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount,
                OrderProducts = new List<OrderProduct>()
            };

            foreach (var item in orderItems)
            {
                order.OrderProducts.Add(new OrderProduct
                {
                    ProductId = item.productId,
                    Quantity = item.quantity
                });
            }


            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    int orderId = _orderService.AddOrder(order);
                    _productService.UpdateStocks(itemsToUpdateStock);

                    transaction.Commit();
                    return orderId;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

    }
}
