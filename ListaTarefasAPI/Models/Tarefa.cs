namespace ListaTarefasAPI.Models
{
    public class Tarefa
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public DateTime? DataCriacao { get; set; } = DateTime.Now;

        public DateTime? DataVencimento { get; set; }

        public string Prioridade { get; set; }

        public int UsuarioId { get; set; }

        public int CategoriaId { get; set; }
    }
}
