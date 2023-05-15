using Blog.Data;
using Blog.Extensions;
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
                return Ok(new ResultViewModel<List<Category>>(categories));
            }
            catch (Exception e)
            {
                return BadRequest(new ResultViewModel<string>(e.Message));
            }
        }

        [HttpGet("categories/{id:int}")] //localhost:PORT/v1/categories/1
        public async Task<IActionResult> GetByIdAsync([FromServices] BlogDataContext ctx, [FromRoute] int id)
        {
            try
            {
                var category = await ctx.Categories.FirstOrDefaultAsync(x=>x.Id == id);

                if (category == null)
                    return NotFound(new ResultViewModel<string>("Category not found"));

                return Ok(new ResultViewModel<Category>(category));
            }
            catch (Exception e)
            {
                return BadRequest(new ResultViewModel<string>(e.Message));
            }
        }

        [HttpPost("categories")] //localhost:PORT/v1/categories
        public async Task<IActionResult> PostAsync(
            [FromServices] BlogDataContext ctx,
            [FromBody] EditorCategoryViewModel model)
        {
            if (!ModelState.IsValid) // por padrão o ASP.NET já faz isso
            {
                //return BadRequest(ModelState.Values.FirstOrDefault()!.Errors); // desse jeito, me retorna uma lista de de objetos, mas eu preciso de uma lista de strings
                return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));
            }
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

                return Created($"v1/categories/{category.Id}", new ResultViewModel<Category>(category));
            } 
            catch (DbUpdateException e)
            {
                return BadRequest(new ResultViewModel<string>(e.Message));
            } 
            
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<string>(e.Message));
            }

        }

        [HttpPut("categories/{id:int}")] //localhost:PORT/v1/categories/1
        public async Task<IActionResult> PutAsync(
            [FromServices] BlogDataContext ctx,
            [FromRoute] int id, 
            [FromBody] EditorCategoryViewModel model)
        {
            try
            {
                var category = await ctx.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (category == null)
                    return NotFound(new ResultViewModel<string>("Category not found"));

                category.Name = model.Name;
                category.Slug = model.Slug;

                ctx.Categories.Update(category);

                await ctx.SaveChangesAsync();

                //return Ok(new ResultViewModel<EditorCategoryViewModel>(model));
                return Ok(new ResultViewModel<Category>(category));
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
                    return NotFound(new ResultViewModel<string>("Category not found"));

                ctx.Categories.Remove(category);

                await ctx.SaveChangesAsync();

                return Ok(new ResultViewModel<Category>(category));
            }
            catch (Exception e)
            {
                return BadRequest(new ResultViewModel<string>(e.Message));
            }
        }

    }
}
