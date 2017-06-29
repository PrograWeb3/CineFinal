using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ensayo.Models.Servicios
{
    public abstract class ServicioSedes
    {
        public static void CrearSede(Sedes nueva_sede)
        {
            CineContext db = new CineContext();
            db.Sedes.Add(nueva_sede);
            db.SaveChanges();
        }
        public static List<Sedes> ListarSedes()
        {
            CineContext db = new CineContext();
            List<Sedes> sedes = db.Sedes.ToList();
            return sedes;
        }
        public static Sedes MostrarSedeSeleccionada(Int32 codigo)
        {
            CineContext db = new CineContext();
            Sedes s = db.Sedes.ToList().Find(sede => sede.IdSede == codigo);
            return s;
        }
    }
}