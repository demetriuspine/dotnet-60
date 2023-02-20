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
        public IActionResult Get([FromServices] AppDbContext ctx) // também chamada de Action
        {
            var todoList = Ok(ctx.Todos.ToList());

            return todoList;
        }

        [HttpGet]
        [Route("{id:int}")] // http://localhost:5236/v1/2
        public IActionResult GetById([FromServices] AppDbContext ctx, [FromRoute] int id) // também chamada de Action
        {
            var todo = ctx.Todos.FirstOrDefault(x => x.Id == id);

            if (todo == null)
            {
                return NotFound();
            }
            return Ok(todo);
        }

        [HttpPost]
        [Route("")]
        public IActionResult Post([FromServices] AppDbContext ctx, [FromBody] TodoModel todo)
        {
            ctx.Todos.Add(todo);

            ctx.SaveChanges();

            var createdTodo = ctx.Todos.FirstOrDefault(x => x.Id == todo.Id);

            if (createdTodo == null)
            {
                return BadRequest();
            }

            return Created($"{createdTodo.Id}", createdTodo);
        }

        [HttpPut]
        [Route("{id:int}")]
        public ActionResult Put([FromServices] AppDbContext ctx, [FromBody] TodoModel todo, [FromRoute] int id)
        {
            var model = ctx.Todos.FirstOrDefault(x => x.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            model.Title = todo.Title;
            model.Done = todo.Done;

            ctx.Todos.Update(model);

            ctx.SaveChanges();

            return Ok(model);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromServices] AppDbContext ctx, [FromRoute] int id)
        {
            var model = ctx.Todos.FirstOrDefault(x => x.Id == id);

            if (model == null)
            {
                return NotFound();
            }

            ctx.Todos.Remove(model);
            ctx.SaveChanges();

            return Ok(model);
        }
    }
}