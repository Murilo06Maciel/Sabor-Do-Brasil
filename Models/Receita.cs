using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sabor_Do_Brasil.Models
{
    public class Receita
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string Ingredientes { get; set; }

        public string ModoPreparo { get; set; }

        public string ImagemUrl { get; set; }

        // Relacionamento com usuário
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        // Relacionamento com comentários
        public ICollection<Comentario> Comentarios { get; set; }
        public ICollection<ReceitaCategoria> ReceitaCategorias { get; set; }
    }
}