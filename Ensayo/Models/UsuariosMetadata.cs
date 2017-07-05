using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Ensayo.Models
{
    public class UsuariosMetadata
    {
        [Display(Name = "Nombre Usuario")]
        [Required]
        public string NombreUsuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}