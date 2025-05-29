using System.ComponentModel.DataAnnotations;
namespace WebApplication2.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        public Product? Product { get; set; }
    }
}