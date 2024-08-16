using System.ComponentModel.DataAnnotations;

namespace ListaTarefasAPI.ViewModels
{
    public class CreateTarefaViewModel
    {
        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descricao { get; set; }

        public string? Prioridade { get; set; }

        public int? UsuarioId { get; set; }

        public int? CategoriaId { get; set; }

    }
}
