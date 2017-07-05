using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ensayo.Models.Servicios
{
    public abstract class ServicioReservas
    {



        static public void guardarReserva(Reservas reserva)
        {
            CineContext db = new CineContext();

            reserva.FechaCarga = DateTime.Now;
            db.Reservas.Add(reserva);
            db.SaveChanges();

        }

        static public Reservas cargarReserva(int IdPelicula, int IdVersion, int sede, string dia, string hora)
        {
            CineContext db = new CineContext();

            Reservas reserva = new Reservas();
            reserva.IdPelicula = IdPelicula;
            reserva.IdVersion = IdVersion;
            reserva.IdSede = sede;

            string FechaReservaCompleta = dia + " " + hora;

            DateTime DiaReserva = Convert.ToDateTime(FechaReservaCompleta);
            reserva.FechaHoraInicio = DiaReserva;

            return reserva;
        }
    }
}