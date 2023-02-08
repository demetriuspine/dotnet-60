var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () =>
{
    return Results.Ok("Hello World!!"); // retorna status 200
});

app.MapGet("/say-hello/{name}", (string name) =>
{
    return Results.Ok($"Hello {name}");
});

app.MapPost("/", (UserExample user) => // o asp.net ja faz a serialização e deserialização do json enviado pelo body ...
{
    return Results.Ok(user); //e do objeto enviado
});

app.Run();
