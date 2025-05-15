using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Sabor_Do_Brasil
{
    public class Usuario : IdentityUser
    {
        public int Id { get; set; }
        
        [Required]
        public string Nome { get; set; }
        
        [Required, EmailAddress]
        public string Email { get; set; }
        
        [Required]
        public string Nickname { get; set; }
        
        [Required]
        public string Senha { get; set; }

    }
}