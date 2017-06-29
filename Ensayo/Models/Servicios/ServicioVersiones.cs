using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ensayo.Models.Servicios
{
    public abstract class ServicioVersiones
    {
        public static List<Versiones> ListarVersiones()
        {
            CineContext db = new CineContext();
            List<Versiones> versiones = db.Versiones.ToList();
            return versiones;
        }


        static public List<Versiones> listaVersionesReserva(int IdPeliculas)
        {
            CineContext db = new CineContext();
            List<Versiones> lista = (from Versiones in db.Versiones
                                     from Carteleras in db.Carteleras
                                     where Carteleras.IdVersion == Versiones.IdVersion && Carteleras.IdPelicula == IdPeliculas
                                     select Versiones).ToList();
            return lista;
        }
    }
}