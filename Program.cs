using Microsoft.EntityFrameworkCore;
using Sabor_Do_Brasil;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Configuração do MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configuração do logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Configuração do Identity
builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

app.UseCors("AllowAll");
app.UseDefaultFiles();
app.UseStaticFiles();

// Endpoint de cadastro usando Identity (correto)
app.MapPost("/api/cadastrar", async (UserManager<Usuario> _userManager, CadastroViewModel model) =>
{
    var usuario = new Usuario
    {
        UserName = model.Email,
        Email = model.Email,
        Nome = model.Nome,
        Nickname = model.Nickname
    };

    var result = await _userManager.CreateAsync(usuario, model.Senha);

    if (result.Succeeded)
        return Results.Ok(new { message = "Usuário cadastrado com sucesso!" });
    else
        return Results.BadRequest(result.Errors);
})
.RequireCors("AllowAll");

// Endpoint de login pode ser implementado com SignInManager (opcional)

app.Run();

// Classe para receber os dados do cadastro
public class CadastroViewModel
{
    public string Nome { get; set; }
    public string Nickname { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
}