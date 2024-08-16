namespace ListaTarefasAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public string Perfil { get; set; }
    }
}
