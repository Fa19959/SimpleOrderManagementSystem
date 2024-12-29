using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleOrderManagementSystem.Models
{
    [PrimaryKey(nameof(OrderId), nameof(ProductId))]
    public class OrderProduct
    {
       
        [ForeignKey("order")]
        public int OrderId { get; set; }
        public Order order { get; set; }

        [ForeignKey("product")]
        public int ProductId { get; set; }
        public Product product { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
