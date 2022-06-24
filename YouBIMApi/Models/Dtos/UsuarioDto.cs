using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YouBIMApi.Models.Dtos
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        [Required]
        public string NombreUsuario { get; set; }
        public bool Activo { get; set; }
        [Required]
        public DateTime FechaNac { get; set; }
    }
}
