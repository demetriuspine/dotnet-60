using Blog.Data;
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
    }
}
