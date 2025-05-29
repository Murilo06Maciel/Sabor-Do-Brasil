using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Sabor_Do_Brasil;
using Sabor_Do_Brasil.Models;
using System.Threading.Tasks;

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

    [HttpGet]
    public IActionResult Cadastro()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Cadastro(CadastroRequest cadastro)
    {
        if (!ModelState.IsValid)
            return View(cadastro);

        var usuario = new Usuario
        {
            Nome = cadastro.Nome,
            UserName = cadastro.UserName,
            Email = cadastro.Email,
            Nickname = cadastro.Nickname
        };

        var result = await _userManager.CreateAsync(usuario, cadastro.Senha);

        if (result.Succeeded)
        {
            ViewBag.Message = "Usuário cadastrado com sucesso!";
            return View();
        }
        else
        {
            ViewBag.Message = "Erro: " + string.Join("; ", result.Errors.Select(e => e.Description));
            return View(cadastro);
        }
    }
}