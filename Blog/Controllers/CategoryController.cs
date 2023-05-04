using Blog.Data;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [ApiController]
    [Route("v1")]
    public class CategoryController : ControllerBase
    {
        [HttpGet("categories")] //localhost:PORT/v1/categories
        public async Task<IActionResult> GetAsync([FromServices]BlogDataContext ctx)
        {
            var categories = await ctx.Categories.ToListAsync();
            return Ok(categories);
        }

        [HttpGet("categories/{id:int}")] //localhost:PORT/v1/categories/1
        public async Task<IActionResult> GetByIdAsync([FromServices] BlogDataContext ctx, [FromRoute] int id)
        {
            var category = await ctx.Categories.FirstOrDefaultAsync(x=>x.Id == id);

            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost("categories")] //localhost:PORT/v1/categories
        public async Task<IActionResult> PostAsync([FromServices] BlogDataContext ctx, [FromBody] Category model)
        {
            try
            {
                await ctx.Categories.AddAsync(model);

                await ctx.SaveChangesAsync();

                return Created($"v1/categories/{model.Id}", model);
            } 
            catch (DbUpdateException e)
            {
                return BadRequest(e.Message);
            } 
            
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        [HttpPut("categories/{id:int}")] //localhost:PORT/v1/categories/1
        public async Task<IActionResult> PutAsync([FromServices] BlogDataContext ctx, [FromRoute] int id, [FromBody] Category model)
        {
            var category = await ctx.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound();

            category.Name = model.Name;
            category.Slug = model.Slug;

            ctx.Categories.Update(category);

            await ctx.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete("categories/{id:int}")] //localhost:PORT/v1/categories/1
        public async Task<IActionResult> DeleteAsync([FromServices] BlogDataContext ctx, [FromRoute] int id)
        {
            var category = await ctx.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound();

            ctx.Categories.Remove(category);

            await ctx.SaveChangesAsync();

            return Ok(category);
        }

    }
}
