using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Sabor_Do_Brasil;
using System.Threading.Tasks;

[ApiController]
[Route("api")]
public class UsuarioController : ControllerBase
{
    private readonly UserManager<Usuario> _userManager;

    public UsuarioController(UserManager<Usuario> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("cadastrar")]
    public async Task<IActionResult> Cadastrar([FromBody] Usuario usuario)
    {
        // Receba a senha separadamente (por segurança)
        var senha = usuario.PasswordHash; // ou ajuste para receber a senha corretamente

        var novoUsuario = new Usuario
        {
            UserName = usuario.UserName,
            Email = usuario.Email,
            Nome = usuario.Nome,
            Nickname = usuario.Nickname
        };

        var result = await _userManager.CreateAsync(novoUsuario, senha);

        if (result.Succeeded)
            return Ok(new { message = "Usuário cadastrado com sucesso!" });
        else
            return BadRequest(result.Errors);
    }
}