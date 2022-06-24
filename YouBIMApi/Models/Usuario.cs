using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YouBIMApi.Models
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        
        public DateTime FechaNac { get; set; }
        public bool Activo { get; set; }
    }
}
