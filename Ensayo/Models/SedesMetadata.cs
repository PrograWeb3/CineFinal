using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Ensayo.Models
{
    public class SedesMetadata
    {
        [Required]
        public string Nombre { get; set; }

        [Display(Name = "Dirección")]
        [Required]
        public string Direccion { get; set; }

        [Display(Name = "Precio General")]
        [Required]
        public decimal PrecioGeneral { get; set; }
    }
}