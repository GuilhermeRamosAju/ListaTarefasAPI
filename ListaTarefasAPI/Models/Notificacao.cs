namespace ListaTarefasAPI.Models
{
    public class Notificacao
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public int TarefaId { get; set; }

        public string Mensagem { get; set; }

        public DateTime? DataCriacao { get; set; } = DateTime.Now;

    }
}
