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

app.Run();
