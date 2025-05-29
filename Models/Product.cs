using System.ComponentModel.DataAnnotations;
namespace WebApplication2.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Product name cannot be longer than 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Product name can only contain letters, numbers, and spaces.")]
        public string Name { get; set; }
        [Required]
        [Range(1.00, 100000.0, ErrorMessage = "Price must be between 1.00 and 100000.")]
        public float Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public int Quantity { get; set; } = 0; // Default to 0 if not specified
        public DateTime CreatedAt { get; set; }

        public Category? Category { get; set; }
    }

}