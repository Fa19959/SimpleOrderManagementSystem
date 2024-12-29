using System.ComponentModel.DataAnnotations;

namespace SimpleOrderManagementSystem.Models
{
    public class Product
    {
        [Key]
        public int PId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
