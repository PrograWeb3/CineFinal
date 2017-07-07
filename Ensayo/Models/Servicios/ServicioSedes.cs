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
        public static void EditarSede(Sedes sede)
        {
            CineContext db = new CineContext();
            var sede_a_editar = from x in db.Sedes where x.IdSede == sede.IdSede select x;
            foreach (Sedes item in sede_a_editar)
            {
                item.Nombre = sede.Nombre;
                item.Direccion = sede.Direccion;
                item.PrecioGeneral = sede.PrecioGeneral;
            }
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

        static public Sedes buscarSedeReserva(Reservas reserva)
        {
            CineContext db = new CineContext();
            var res = (from Reservas in db.Reservas
                       from Sedes in db.Sedes
                       where Reservas.IdSede == Sedes.IdSede && Sedes.IdSede == reserva.IdSede
                       select Sedes).FirstOrDefault();
            return res;
        }
    }
}