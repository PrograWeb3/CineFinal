using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ensayo.Models.Servicios
{
    public abstract class ServicioDias
    {

        static public List<Dias> solapamientoDiasReservas(int IdVersiones, int IdPelicula, int IdSedes)
        {
            CineContext db = new CineContext();

            var ObtenerCartelera = ServicioCarteleras.BuscarCarteleraReserva(IdVersiones, IdPelicula, IdSedes);

            List<Dias> ListaDias = new List<Dias>();

            DateTime start = ObtenerCartelera.FechaInicio;
            DateTime finish = ObtenerCartelera.FechaFin;
           
            for (DateTime x = start; x <= finish; x = x.AddDays(1))
            {

                    string txtHora = x.ToString("dd-MM-yyyy");
                    ListaDias.Add(new Dias() { value = txtHora, dias = txtHora });

            }

            return ListaDias; 

        }
    }
}