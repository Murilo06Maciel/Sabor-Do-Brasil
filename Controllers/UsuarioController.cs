using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Sabor_Do_Brasil;
using Sabor_Do_Brasil.Models;
using System.Threading.Tasks;

[ApiController]
[Route("api")]
public class UsuarioController : Controller
{
    private readonly UserManager<Usuario> _userManager;

    public UsuarioController(UserManager<Usuario> userManager)
    {
        _userManager = userManager;
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

    [HttpGet("cadastro")]
    public IActionResult Cadastro()
    {
        return View();
    }

    [HttpPost("cadastro")]
    public async Task<IActionResult> Cadastro(CadastroRequest cadastro)
    {
        if (!ModelState.IsValid)
            return View(cadastro);

        var novoUsuario = new Usuario
        {
            UserName = cadastro.UserName,
            Email = cadastro.Email,
            Nome = cadastro.Nome,
            Nickname = cadastro.Nickname
        };

        var result = await _userManager.CreateAsync(novoUsuario, cadastro.PasswordHash);

        if (result.Succeeded)
        {
            ViewBag.Message = "Usuário cadastrado com sucesso!";
            return View();
        }
        else
        {
            ViewBag.Message = string.Join("; ", result.Errors.Select(e => e.Description));
            return View(cadastro);
        }
    }
}