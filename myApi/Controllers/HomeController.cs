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
        [Route("")] // mesma coisa que [HttpGet("/")], caso o prefixo de rota esteja em uso, não pode usar o /
        public List<TodoModel> Get([FromServices] AppDbContext ctx) // também chamada de Action
        {
            var todoList = ctx.Todos.ToList();

            return todoList;
        }

        [HttpGet]
        [Route("{id:int}")] // http://localhost:5236/v1/2
        public TodoModel? GetById([FromServices] AppDbContext ctx, [FromRoute] int id) // também chamada de Action
        {
            var todo = ctx.Todos.FirstOrDefault(x => x.Id == id);

            return todo;
        }

        [HttpPost]
        [Route("")]
        public TodoModel? Post([FromServices] AppDbContext ctx, [FromBody] TodoModel todo)
        {
            ctx.Todos.Add(todo);

            ctx.SaveChanges();

            return ctx.Todos.FirstOrDefault(x => x.Id == todo.Id);
        }

        [HttpPut]
        [Route("{id:int}")]
        public TodoModel? Put([FromServices] AppDbContext ctx, [FromBody] TodoModel todo, [FromRoute] int id)
        {
            var model = ctx.Todos.FirstOrDefault(x => x.Id == id);

            if (model == null)
            {
                return null;
            }

            model.Title = todo.Title;
            model.Done = todo.Done;

            ctx.Todos.Update(model);

            ctx.SaveChanges();

            return model;
        }

        [HttpDelete]
        [Route("{id:int}")]
        public TodoModel? Delete([FromServices] AppDbContext ctx, [FromRoute] int id)
        {
            var model = ctx.Todos.FirstOrDefault(x => x.Id == id);

            if (model == null)
            {
                return null;
            }

            ctx.Todos.Remove(model);
            ctx.SaveChanges();

            return model;
        }
    }
}