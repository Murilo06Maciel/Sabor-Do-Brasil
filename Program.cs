using Microsoft.EntityFrameworkCore;
using Sabor_Do_Brasil;
using Microsoft.AspNetCore.Cors;
using BCrypt.Net;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;

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

// Configuração do logging (ADICIONADO AQUI)
builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // Log no console
builder.Logging.AddDebug();   // Log no debug (Visual Studio Output)
// builder.Logging.AddFile("Logs/myapp-{Date}.txt"); // Para logs em arquivo (instale o pacote Serilog.Extensions.Logging.File)

var app = builder.Build();

app.UseCors("AllowAll");
app.UseDefaultFiles();
app.UseStaticFiles();

// Endpoint de cadastro
app.MapPost("/api/cadastrar", async (AppDbContext db, Usuario usuario) => 
{
    // Validação básica
    if (string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Senha))
    {
        return Results.BadRequest("E-mail e senha são obrigatórios");
    }

    // Verifica se usuário já existe
    if (await db.Usuarios.AnyAsync(u => u.Email == usuario.Email))
    {
        return Results.Conflict("E-mail já cadastrado");
    }

    // Cria hash da senha
    usuario.Senha = Convert.ToBase64String(
        Rfc2898DeriveBytes.Pbkdf2(
            password: usuario.Senha,
            salt: RandomNumberGenerator.GetBytes(32),
            iterations: 100_000,
            hashAlgorithm: HashAlgorithmName.SHA256,
            outputLength: 32
        )
    );

    // Salva no banco
    db.Usuarios.Add(usuario);
    await db.SaveChangesAsync();

    return Results.Ok(new { 
        success = true,
        message = "Usuário cadastrado com sucesso!" 
    });
})
.RequireCors("AllowAll");

// Endpoint de login
app.MapPost("/api/login", async (AppDbContext db, Usuario loginRequest) =>
{
    // Validação básica
    if (string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Senha))
    {
        return Results.BadRequest("E-mail e senha são obrigatórios");
    }

    var usuario = await db.Usuarios
        .FirstOrDefaultAsync(u => u.Email == loginRequest.Email);

    if (usuario == null)
    {
        return Results.Unauthorized();
    }

    // Extrai hash e salt do banco
    var parts = usuario.Senha.Split('|');
    if (parts.Length != 2)
    {
        return Results.Unauthorized();
    }

    byte[] storedHash = Convert.FromBase64String(parts[0]);
    byte[] storedSalt = Convert.FromBase64String(parts[1]);

    // Verifica a senha
    var pbkdf2 = new Rfc2898DeriveBytes(
        loginRequest.Senha,
        storedSalt,
        100000,
        HashAlgorithmName.SHA256
    );

    byte[] testHash = pbkdf2.GetBytes(32);

    if (!CryptographicOperations.FixedTimeEquals(storedHash, testHash))
    {
        return Results.Unauthorized();
    }

    return Results.Ok(new {
        success = true,
        usuario = new {
            usuario.Id,
            usuario.Nome,
            usuario.Email
        }
    });
});

app.Run();