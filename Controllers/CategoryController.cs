using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Data; // Make sure this is included
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CategoryController(AppDbContext db)
        {
            _db = db;
        }


        ///get all Product list
        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var category = await _db.Categories.ToListAsync(); 
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category == null) return NotFound("Category not found.");
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category updatedCategory)
        {
            if (id != updatedCategory.Id)
                return BadRequest("Category ID mismatch");

            var existingCategory = await _db.Categories.FindAsync(id);
            if (existingCategory == null)
                return NotFound();

            existingCategory.Name = updatedCategory.Name;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]    
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _db.Categories.FindAsync(id);
            if (category == null) return NotFound();

            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}