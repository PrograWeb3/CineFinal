using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Ensayo.Models
{
    public class CartelerasMetadata

    {
        [Required]
        [Display(Name = "Sede")]
        public int IdSede { get; set; }

        [Required]
        [Display(Name = "Pelicula")]
        public int IdPelicula { get; set; }

        //[Range(15,24,ErrorMessage ="El Horario de inicio debe ser despues de las 15hs ")]
        [Required]
        [Display(Name ="Hora de Inicio")]
        public int HoraInicio { get; set; }

        [Required]
        [Display(Name= "Fecha de Inico")]
        public System.DateTime FechaInicio { get; set; }

        [Required]
        [Display(Name = "Fecha de Fin")]
        public System.DateTime FechaFin { get; set; }

        [Required]
        [Display(Name = "Nuemero de sala")]
        public int NumeroSala { get; set; }

        [Required]
        [Display(Name = "Version")]
        public int IdVersion { get; set; }

        public bool Lunes { get; set; }
        public bool Martes { get; set; }
        public bool Miercoles { get; set; }
        public bool Jueves { get; set; }
        public bool Viernes { get; set; }
        public bool Sabado { get; set; }
        public bool Domingo { get; set; }
        public System.DateTime FechaCarga { get; set; }


    }
}