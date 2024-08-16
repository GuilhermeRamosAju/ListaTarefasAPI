using System.ComponentModel.DataAnnotations;

namespace ListaTarefasAPI.ViewModels
{
    public class CreateCategoriaViewModel
    {
        [Required]
        public string Nome { get; set; }

        public string? Descricao { get; set; }

    }
}
