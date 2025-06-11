using System.Collections.Generic;

namespace Sabor_Do_Brasil.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public ICollection<ReceitaCategoria> ReceitaCategorias { get; set; }
    }
}