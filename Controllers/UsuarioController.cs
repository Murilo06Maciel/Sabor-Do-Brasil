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

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest login)
    {
        // Procura o usuário pelo nickname
        var usuario = await _userManager.FindByNameAsync(login.Nickname);
        if (usuario == null)
            return Unauthorized(new { message = "Usuário não encontrado." });

        // Verifica a senha
        var senhaCorreta = await _userManager.CheckPasswordAsync(usuario, login.Senha);
        if (!senhaCorreta)
            return Unauthorized(new { message = "Senha incorreta." });

        // Retorna dados do usuário (ajuste conforme necessário)
        return Ok(new { nickname = usuario.Nickname, nome = usuario.Nome });
    }

    // Classe auxiliar para receber o login
    public class LoginRequest
    {
        public string Nickname { get; set; }
        public string Senha { get; set; }
    }
}