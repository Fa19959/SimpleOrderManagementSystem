using System.ComponentModel.DataAnnotations;

namespace SimpleOrderManagementSystem.DTOs
{
    public class UserInputDTO
    {
        [Required]
        public string name {  get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long, include at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string password { get; set; }

        [Required]
        [RegularExpression(@"^(admin|user)$", ErrorMessage = "Role must be either 'admin' or 'user'.")]
        public string role { get; set; }

    }
}
