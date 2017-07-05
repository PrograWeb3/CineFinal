using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Ensayo.Models
{
    public class ReservasMetadata
    {
        [Display(Name = "Sede")]
        [Required]
        public int IdSede { get; set; }

        [Display(Name = "Version de Pelicula")]
        [Required]
        public int IdVersion { get; set; }
        [Required]
        public int IdPelicula { get; set; }
        [Required]
        public System.DateTime FechaHoraInicio { get; set; }
        [Required]
        public string Email { get; set; }

        [Display(Name = "Tipo de documento")]
        [Required]
        public int IdTipoDocumento { get; set; }

        [Display(Name = "Número de Documento ")]
        [Required]
        public string NumeroDocumento { get; set; }

        
        [Display(Name = "Cantidad de entradas")]
        [Required]
        public int CantidadEntradas { get; set; }
    }
}