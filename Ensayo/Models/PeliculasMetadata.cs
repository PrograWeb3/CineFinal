using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Ensayo.Models
{
    public class PeliculasMetadata
    {
        [Required]
        [Display(Name = "Ede")]

        public int IdPelicula { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public string Imagen { get; set; }

        [Required]
        public int IdCalificacion { get; set; }

        [Required]        
        public int IdGenero { get; set; }

        [Required]       
        public int Duracion { get; set; }        
    }
}