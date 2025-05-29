using System.ComponentModel.DataAnnotations;
namespace WebApplication2.Models
{
    public class User
    {
        [Key] // Optional if property is named "Id"
        public int Id { get; set; }
        
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        [MaxLength(100)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one number.")]
        [Display(Name = "Password")]

        public string Password { get; set; }

        public int Isdelete { get; set; }  = 0; // explicitly setting default to 0
    }
}