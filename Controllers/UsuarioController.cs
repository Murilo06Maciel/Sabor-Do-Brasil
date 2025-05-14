using Microsoft.AspNetCore.Mvc;
using Sabor_Do_Brasil;

[ApiController]
[Route("api")]
public class UsuarioController : ControllerBase
{
    [HttpPost("cadastrar")]
    public IActionResult Cadastrar([FromBody] Usuario usuario)
    {
        // Aqui você salva o usuário no banco de dados
        // Exemplo: _context.Usuarios.Add(usuario); _context.SaveChanges();

        return Ok(new { message = "Usuário cadastrado com sucesso!" });
    }
}