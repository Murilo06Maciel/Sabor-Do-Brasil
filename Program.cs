using Microsoft.EntityFrameworkCore;
using Sabor_Do_Brasil;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Configuração do MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

builder.Services.AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseCors();
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Endpoint de login pode ser implementado com SignInManager (opcional)

app.Run();

// Classe para receber os dados do cadastro
public class CadastroViewModel
{
    public string Nome { get; set; }
    public string Nickname { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; } // <-- é esse nome que o backend espera
}