using System;

namespace Sabor_Do_Brasil.Models
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Texto { get; set; }
        public DateTime Data { get; set; }

        // Relacionamento com usuário
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        // Relacionamento com receita
        public int ReceitaId { get; set; }
        public Receita Receita { get; set; }
    }
}