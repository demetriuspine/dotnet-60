using Microsoft.AspNetCore.Mvc;
using MyApi.Data;
using MyApi.Models;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("v1/")] // prefixo de rota
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("")] // mesma coisa que [HttpGet("/")], caso o prefixo de rota esteja em uso, não pode usar o 
        public List<TodoModel> Get([FromServices] AppDbContext ctx) // também chamada de Action
        {
            var todoList = ctx.Todos.ToList();

            return todoList;
        }

        [HttpPost]
        [Route("")]
        public TodoModel? Post([FromServices] AppDbContext ctx, TodoModel todo)
        {
            todo.CreatedAt = DateTime.Now;
            ctx.Todos.Add(todo);

            ctx.SaveChanges();

            return ctx.Todos.FirstOrDefault(x => x.Id == todo.Id);
        }
    }
}