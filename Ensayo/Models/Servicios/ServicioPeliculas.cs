using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ensayo.Models.Servicios
{
    public abstract class ServicioPeliculas
    {
        public static void CrearPelicula(Peliculas nueva_pelicula, HttpPostedFileBase portada_pelicula)
        {
            CineContext db = new CineContext();
            if (portada_pelicula.ContentLength > 0)
            {
                string ruta = Path.Combine("~/Content/imagenes/peliculas", nueva_pelicula.Nombre);
                nueva_pelicula.Imagen = ruta;
            }
            nueva_pelicula.FechaCarga = DateTime.Now;
            db.Peliculas.Add(nueva_pelicula);
            db.SaveChanges();
        }
        public static void EditarPelicula(Peliculas pelicula_editada, HttpPostedFileBase nueva_portada_pelicula)
        {
            CineContext db = new CineContext();
            var pe = from p in db.Peliculas where p.IdPelicula == pelicula_editada.IdPelicula select p;
            foreach (Peliculas x in pe)
            {                
                x.Nombre = pelicula_editada.Nombre;
                x.Descripcion = pelicula_editada.Descripcion;
                x.IdCalificacion = pelicula_editada.IdCalificacion;
                x.IdGenero = pelicula_editada.IdGenero;
                x.Duracion = pelicula_editada.Duracion;
                //Si existe una image para cambiar.
                if (nueva_portada_pelicula.ContentLength > 0)
                {
                    string ruta = Path.Combine("~/Content/imagenes/peliculas", pelicula_editada.Nombre);
                    x.Imagen = ruta;
                }
            }            
            db.SaveChanges();            
        }
        public static void EditarPelicula(Peliculas pd)
        {
            CineContext db = new CineContext();
            Peliculas p = db.Peliculas.ToList().Find(pelicula => pelicula.IdPelicula == pd.IdPelicula);
            p.Nombre = pd.Nombre;
            p.Descripcion = pd.Descripcion;
            p.Duracion = pd.Duracion;
            p.IdGenero = pd.IdGenero;
            p.IdCalificacion = pd.IdCalificacion;
            p.Imagen = pd.Imagen;
            db.SaveChanges();
        }
        public static Peliculas MostrarPeliculaSeleccionada(Int32 codigo)
        {
            CineContext db = new CineContext();
            Peliculas p = db.Peliculas.ToList().Find(pelicula => pelicula.IdPelicula == codigo);
            return p;
        }
        public static List<Peliculas> ListarPeliculas()
        {
            CineContext db = new CineContext();
            List<Peliculas> peliculas = (from Peliculas in db.Peliculas
                                         select Peliculas).ToList();          
	        return(peliculas);
        }
        public static List<SelectListItem> ObtenerClasificaciones()
        {
            CineContext db = new CineContext();         
            List<SelectListItem> clasificaciones = new List<SelectListItem>();
            foreach(var x in db.Calificaciones.ToList())
            {
                SelectListItem item = new SelectListItem();
                item.Value = x.IdCalificacion.ToString();
                item.Text = x.Nombre;
                clasificaciones.Add(item);                
            }
            return clasificaciones;
        }
        public static List<SelectListItem> ObtenerGeneros()
        {
            CineContext db = new CineContext();
            List<SelectListItem> generos = new List<SelectListItem>();
            foreach (var x in db.Generos.ToList())
            {
                SelectListItem item = new SelectListItem();
                item.Value = x.IdGenero.ToString();
                item.Text = x.Nombre;
                generos.Add(item);
            }
            return generos;
        }
    }
}