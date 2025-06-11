namespace Sabor_Do_Brasil.Models
{
    public class ReceitaCategoria
    {
        public int ReceitaId { get; set; }
        public Receita Receita { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}