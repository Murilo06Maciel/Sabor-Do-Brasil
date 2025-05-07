using Microsoft.EntityFrameworkCore;
using Sabor_Do_Brasil; // Adicione esta linha

var builder = WebApplication.CreateBuilder(args);

// Configuração do MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/index", async context =>
{
    await context.Response.SendFileAsync("wwwroot/index.html");
});

app.Run();