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
            int segundostotales = ConversorDeHoras.HoraASegundos(cartelera.HoraInicio);
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
        
        public static int ValidarIntervalo30(Carteleras cartelera)
        {
            CineContext db = new CineContext();            
            // segundos totales representa la hora de inicio de la cartelera entrante (Nueva).
            int hora_inicio_entrante = ConversorDeHoras.HoraASegundos(cartelera.HoraInicio);            
            int conflicto_intervalo = 0;
            List<Carteleras> carteleras = db.Carteleras.ToList();
            Peliculas pelicula_de_cartelera;
            int duracion_pelicula;
            int fin_de_funcion_segundos;

            foreach (var c in carteleras)
            {
                // Calculo la hora de fin de función.
                pelicula_de_cartelera = db.Peliculas.ToList().Find(x => x.IdPelicula == c.IdPelicula);
                duracion_pelicula = pelicula_de_cartelera.Duracion;
                fin_de_funcion_segundos = (duracion_pelicula * 60) + c.HoraInicio;

                if (fin_de_funcion_segundos + 1800 >= hora_inicio_entrante)
                {
                    conflicto_intervalo = 1;
                }
            }
            return conflicto_intervalo;
        }

        public static int ValidarExistencia(Carteleras cartelera)
        {
            CineContext db = new CineContext();
            var CarteleraExistente = (from Carteleras in db.Carteleras
                                      where Carteleras.IdPelicula == cartelera.IdPelicula && Carteleras.IdSede == cartelera.IdSede && Carteleras.IdVersion == cartelera.IdVersion
                                      select Carteleras).Count();

            return CarteleraExistente;
        }
        //Consigna
        /* En Carteleras  no se pueden solapar (fecha hora inicio - fecha hora fin) 
         * para una misma Sede y nro de Sala. Usen el sentido común para esto y que no se puedan solapar,
         * verifiquen tanto los dias como los horarios. */
        public static int ValidarSolapamientoFechas(Carteleras cartelera)
        {
            CineContext db = new CineContext();
            int solapamiento = 0;
            List<Carteleras> carteleras = db.Carteleras.ToList();
            foreach (var x in carteleras)
            {
                if (cartelera.FechaInicio >= x.FechaInicio && cartelera.FechaInicio < x.FechaFin && cartelera.NumeroSala == x.NumeroSala && cartelera.IdSede == x.IdSede)
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

        public static Carteleras BuscarCarteleraReserva(int IdVersiones, int IdPelicula, int IdSedes)
        {
            CineContext db = new CineContext();
            var cartelera = (from Carteleras c in db.Carteleras
                             where c.IdVersion == IdVersiones && c.IdPelicula == IdPelicula && c.IdSede == IdSedes
                             select c).FirstOrDefault();
            return cartelera;

        }





    }
}