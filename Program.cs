var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/weatherforecast", () =>
{
    return "s";
}); 

app.Run();