using Microsoft.AspNetCore.Mvc;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("v1/")] // prefixo de rota
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("")] // mesma coisa que [HttpGet("/")], caso o prefixo de rota esteja em uso, não pode usar o 
        public string Get() // também chamada de Action
        {
            return "Hello World !";
        }
    }
}