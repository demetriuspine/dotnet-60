using Blog.Data;
using Blog.Models;
using Blog.ViewModels;
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
            try
            {
                var categories = await ctx.Categories.ToListAsync();
                return Ok(categories);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("categories/{id:int}")] //localhost:PORT/v1/categories/1
        public async Task<IActionResult> GetByIdAsync([FromServices] BlogDataContext ctx, [FromRoute] int id)
        {
            try
            {
                var category = await ctx.Categories.FirstOrDefaultAsync(x=>x.Id == id);

                if (category == null)
                    return NotFound();

                return Ok(category);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("categories")] //localhost:PORT/v1/categories
        public async Task<IActionResult> PostAsync(
            [FromServices] BlogDataContext ctx,
            [FromBody] CreateCategoryViewModel model)
        {
            try
            {
                var category = new Category
                {
                    Id = 0,
                    Name = model.Name,
                    Slug = model.Slug.ToLower(),
                };

                await ctx.Categories.AddAsync(category);

                await ctx.SaveChangesAsync();

                return Created($"v1/categories/{category.Id}", category);
            } 
            catch (DbUpdateException e)
            {
                return BadRequest(e.Message);
            } 
            
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut("categories/{id:int}")] //localhost:PORT/v1/categories/1
        public async Task<IActionResult> PutAsync([FromServices] BlogDataContext ctx, [FromRoute] int id, [FromBody] Category model)
        {
            try
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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("categories/{id:int}")] //localhost:PORT/v1/categories/1
        public async Task<IActionResult> DeleteAsync([FromServices] BlogDataContext ctx, [FromRoute] int id)
        {
            try
            {
                var category = await ctx.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound();

                ctx.Categories.Remove(category);

                await ctx.SaveChangesAsync();

                return Ok(category);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
