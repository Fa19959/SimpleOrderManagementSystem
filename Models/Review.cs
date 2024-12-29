using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleOrderManagementSystem.Models
{
    public class Review
    {
        [Key]
        public int RId { get; set; }

        [ForeignKey("user")]
        public int UserId { get; set; }
        public User user { get; set; }

        [ForeignKey("product")]
        public int ProductId { get; set; }
        public Product product { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public string Comment { get; set; }

        public DateTime ReviewDate { get; set; }
    }
}
