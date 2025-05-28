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

    public class CadastroRequest
    {
        public string Nome { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Nickname { get; set; }
        public string PasswordHash { get; set; } // Aqui vem a senha do frontend
    }

    [HttpPost("cadastrar")]
    public async Task<IActionResult> Cadastrar([FromBody] CadastroRequest cadastro)
    {
        var novoUsuario = new Usuario
        {
            UserName = cadastro.UserName,
            Email = cadastro.Email,
            Nome = cadastro.Nome,
            Nickname = cadastro.Nickname
        };

        var result = await _userManager.CreateAsync(novoUsuario, cadastro.PasswordHash);

        if (result.Succeeded)
            return Ok(new { message = "Usuário cadastrado com sucesso!" });
        else
            return BadRequest(new { message = string.Join("; ", result.Errors.Select(e => e.Description)) });
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