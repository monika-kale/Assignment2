using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Data; // Make sure this is included
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProductController(AppDbContext db)
        {
            _db = db;
        }


        ///get all Product list
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var product = await _db.Products.ToListAsync(); 
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound("Product not found.");
            return Ok(product);
        }

        /// <summary>
        /// Create new Product
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            // Ensure EF doesn't try to insert the category again
            _db.Entry(product).Reference(p => p.Category).IsModified = false;
            _db.Products.Add(product);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        /// update Product
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            if (id != updatedProduct.Id)
                return BadRequest("Product ID mismatch");

            var existingProduct = await _db.Products.FindAsync(id);
            if (existingProduct == null)
                return NotFound();

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.CategoryId = updatedProduct.CategoryId;

            _db.Products.Update(existingProduct);
            await _db.SaveChangesAsync();

            return Ok(existingProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound("Product not found.");

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return Ok("Product deleted successfully.");
        }
       
    }

    
}
