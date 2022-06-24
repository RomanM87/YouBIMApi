using System.ComponentModel.DataAnnotations;

namespace YouBIMApi.Models.Dtos
{
    public class UsuarioUpdateDto
    {
        [Required]
        public int IdUsuario { get; set; }
    }
}
