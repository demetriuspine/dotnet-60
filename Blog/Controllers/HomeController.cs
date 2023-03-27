using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")] // health check, para ver se a api está online
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
