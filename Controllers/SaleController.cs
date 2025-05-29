using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Data; // Make sure this is included
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly AppDbContext _db;

        public SaleController(AppDbContext db)
        {
            _db = db;
        }


        ///get all Product list
        [HttpGet]
        public async Task<IActionResult> GetAllSale()
        {
            var sale = await _db.Sales.ToListAsync(); 
            if (sale == null) return NotFound();
            return Ok(sale);
        }

       [HttpPost]
        public async Task<IActionResult> CreateSale([FromBody] Sale sale)
        {
            var existingProductSale = await _db.Sales.FirstOrDefaultAsync(s => s.ProductId == sale.ProductId);

            var product = await _db.Products.FindAsync(sale.ProductId);
            if (product == null)
                return NotFound("Product not found.");

            if (sale.Quantity <= 0)
                return BadRequest("Sale quantity must be greater than 0.");

            if (product.Quantity < sale.Quantity)
                return BadRequest("Not enough product quantity available.");

            if (existingProductSale == null)
            {
                // New sale
                product.Quantity -= sale.Quantity;
                sale.Date = DateTime.Now;

                _db.Sales.Add(sale);
            }
            else
            {
                // Update existing sale
                product.Quantity -= sale.Quantity; // subtract only the new quantity sold

                existingProductSale.Quantity += sale.Quantity;
                existingProductSale.Date = DateTime.Now;
            }

            await _db.SaveChangesAsync(); // <--- Save all changes
            return Ok("Sale recorded successfully");
        }

    }
}