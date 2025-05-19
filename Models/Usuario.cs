using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Sabor_Do_Brasil
{
    public class Usuario : IdentityUser
    {
        
        [Required]
        public string Nome { get; set; }
        
        [Required]
        public string Nickname { get; set; }


    }
}