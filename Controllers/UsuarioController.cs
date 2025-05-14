using Microsoft.AspNetCore.Mvc;
using Sabor_Do_Brasil;

[ApiController]
[Route("api")]
public class UsuarioController : ControllerBase
{
    private readonly AppDbContext _context;

    public UsuarioController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar([FromBody] Usuario usuario)
    {
        _context.Usuarios.Add(usuario); // Adiciona o usuário
        _context.SaveChanges();         // Salva no banco

        return Ok(new { message = "Usuário cadastrado com sucesso!" });
    }
}