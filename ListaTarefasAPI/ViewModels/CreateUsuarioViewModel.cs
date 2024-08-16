using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations;

namespace ListaTarefasAPI.ViewModels
{
    public class CreateUsuarioViewModel
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}
