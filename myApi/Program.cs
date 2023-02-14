var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); // adiciona suporte a controllers

var app = builder.Build();

// app.MapGet("/", () =>
// {
//     return Results.Ok("Hello World!!"); // retorna status 200
// });

// app.MapGet("/say-hello/{name}", (string name) =>
// {
//     return Results.Ok($"Hello {name}");
// });

// app.MapPost("/", (UserExample user) => // o asp.net ja faz a serialização e deserialização do json enviado pelo body ...
// {
//     return Results.Ok(user); //e do objeto enviado
// });


app.MapControllers(); // busca todos os controllers dentrod o arquivo

// app.UsePathBase("/v1"); // mesmo que [Route("v1")] no HomeController

app.Run();
