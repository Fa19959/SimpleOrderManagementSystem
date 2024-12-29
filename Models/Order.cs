using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleOrderManagementSystem.Models
{
    public class Order
    {
        [Key]
        public int OId { get; set; }

        [ForeignKey("user")]
        public int UserId { get; set; }
        public User user { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
