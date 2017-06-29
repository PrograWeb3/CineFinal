using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ensayo.Models.Servicios
{
    public abstract class ServicioCarteleras
    {
        public static List<Carteleras> ListarCarteleras()
        {
            CineContext db = new CineContext();
            List<Carteleras> carteleras = (from c in db.Carteleras select c).ToList();            
            return carteleras;
        }
        //Sobrecarga para filtrar por salas
        public static List<Carteleras> ListarCarteleras(Int32 Id)
        {
            CineContext db = new CineContext();
            List<Carteleras> carteleras = (from c in db.Carteleras where c.NumeroSala == Id select c).ToList();
            return carteleras;
        }
        public static void CrearCartelera(Carteleras cartelera)
        {
            CineContext db = new CineContext();

            //Tratamiento de HoraInicio
            string hora_texto = cartelera.HoraInicio.ToString();
            int hora_en_segundos = Int32.Parse(hora_texto.Substring(0, 2)) * 3600;
            int minutos_en_segundos = Int32.Parse(hora_texto.Substring(2, 2)) * 60;
            Console.WriteLine("{0}:{1}", hora_en_segundos, minutos_en_segundos);
            //Formato de persistencia en la bd.
            int segundostotales = (hora_en_segundos + minutos_en_segundos);
            cartelera.HoraInicio = segundostotales;
            db.Carteleras.Add(cartelera);
            db.SaveChanges();
        }
        public static void EditarCartelera(Carteleras cd)
        {
            CineContext db = new CineContext();
            Carteleras c = db.Carteleras.ToList().Find(x => x.IdCartelera == cd.IdCartelera);

            c.IdPelicula = cd.IdPelicula;
            c.FechaInicio = cd.FechaInicio;
            c.FechaFin = cd.FechaFin;
            c.NumeroSala = cd.NumeroSala;
            c.IdVersion = cd.IdVersion;
            c.Lunes = cd.Lunes;
            c.Martes = cd.Martes;
            c.Miercoles = cd.Miercoles;
            c.Jueves = cd.Jueves;
            c.Viernes = cd.Viernes;
            c.Sabado = cd.Sabado;
            c.Domingo = cd.Domingo;
            db.SaveChanges();


        }
        public static void EliminarCartelera(Int32 Id)
        {
            CineContext db = new CineContext();
            var cartelera_eliminada = from c in db.Carteleras where c.IdCartelera == Id select c;
            db.Carteleras.Remove(cartelera_eliminada.First());
            db.SaveChanges();
        }
        
        public static void ValidarIntervalo30(Carteleras cartelera)
        {
            

            
        }

        public static int ValidarExistencia(Carteleras cartelera)
        {
            CineContext db = new CineContext();
            var CarteleraExistente = (from Carteleras in db.Carteleras
                                      where Carteleras.IdPelicula == cartelera.IdPelicula && Carteleras.IdSede == cartelera.IdSede && Carteleras.IdVersion == cartelera.IdVersion
                                      select Carteleras).Count();

            return CarteleraExistente;
        }
        public static int ValidarSolapamientoFechas(DateTime fecha_inicio, DateTime fecha_fin)
        {
            CineContext db = new CineContext();
            int solapamiento = 0;
            List<Carteleras> carteleras = db.Carteleras.ToList();
            foreach (var x in carteleras)
            {
                if (fecha_inicio >= x.FechaInicio && fecha_inicio < x.FechaFin)
                {
                    solapamiento = 1;
                }
            }
            return solapamiento;
        }
        public static Carteleras MostrarCarteleraSeleccionada(Int32 Id)
        {
            CineContext db = new CineContext();
            Carteleras c = db.Carteleras.ToList().Find(x => x.IdCartelera == Id);
            return c;
        }



        public static Carteleras BuscarCartelera(int codigo)
        {
            CineContext db = new CineContext();
            //Peliculas p = db.Peliculas.ToList().Find(pelicula => pelicula.IdPelicula == codigo);
            var cartelera = (from Carteleras in db.Carteleras
                             where Carteleras.IdPelicula == codigo
                             select Carteleras).FirstOrDefault();
            return cartelera;


        }
        //public List<Carteleras> listaCarteleras()
        //{
        //    List<Carteleras> cartelera = (from Carteleras in cine.Cartelerass
        //                                  select cartelera).ToList();
        //    return cartelera;
        //}

        //public List<Sedes> listaSede()
        //{
        //    List<Sedes> sedes = (from Sedes in cine.Sedess
        //                         select Sedes).ToList();
        //    return sedes;
        //}

    }
}