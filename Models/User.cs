using System.ComponentModel.DataAnnotations;

namespace SimpleOrderManagementSystem.Models
{
    public class User
    {
        [Key]
        public int UId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string role { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
