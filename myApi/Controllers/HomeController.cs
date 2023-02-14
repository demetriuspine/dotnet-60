using Microsoft.AspNetCore.Mvc;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("v1")] // prefixo de rota
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("/")] // mesma coisa que [HttpGet("/")]
        public string Get() // também chamada de Action
        {
            return "Hello World";
        }
    }
}