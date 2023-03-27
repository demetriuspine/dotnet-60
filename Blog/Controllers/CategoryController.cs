using Blog.Data;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [ApiController]
    [Route("v1")]
    public class CategoryController : ControllerBase
    {
        [HttpGet("categories")] //localhost:PORT/v1/categories
        public IActionResult Get([FromServices]BlogDataContext ctx)
        {
            var categories = ctx.Categories.ToList();
            return Ok(categories);
        }
    }
}
