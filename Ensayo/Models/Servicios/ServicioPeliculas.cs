using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Hosting;

namespace Ensayo.Models.Servicios
{
    public abstract class ServicioPeliculas
    {
        public static void CrearPelicula(Peliculas nueva_pelicula)
        {
            CineContext db = new CineContext();
            nueva_pelicula.FechaCarga = DateTime.Now;
            db.Peliculas.Add(nueva_pelicula);
            db.SaveChanges();
        }
        public static void InsertarImagen(Peliculas nueva_pelicula, HttpPostedFileBase Imagen)
        {
            if (Imagen.ContentLength > 0)
            {
                string carpetaImagenes = System.Configuration.ConfigurationManager.AppSettings["CarpetaImagenes"];
                string pathDestino = System.Web.Hosting.HostingEnvironment.MapPath("~") + carpetaImagenes;
                //Creamos la carpeta en caso de que no exista
                if (!System.IO.Directory.Exists(pathDestino))
                {
                    System.IO.Directory.CreateDirectory(pathDestino);
                }

                string nombreArchivoFinal = nueva_pelicula.Nombre;
                nombreArchivoFinal = string.Concat(nombreArchivoFinal, Path.GetExtension(Imagen.FileName));
                Imagen.SaveAs(string.Concat(pathDestino, nombreArchivoFinal));
                nueva_pelicula.Imagen = string.Concat(carpetaImagenes, nombreArchivoFinal);
            }
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
            return (peliculas);
        }
        public static List<SelectListItem> ObtenerClasificaciones()
        {
            CineContext db = new CineContext();
            List<SelectListItem> clasificaciones = new List<SelectListItem>();
            foreach (var x in db.Calificaciones.ToList())
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
            List<Generos> g = db.Generos.ToList();
            foreach (var x in g)
            {
                SelectListItem item = new SelectListItem();
                item.Value = x.IdGenero.ToString();
                item.Text = x.Nombre;
                generos.Add(item);
            }
            return generos;
        }
        public static int ValidarDuracionPelicula(Peliculas pelicula)
        {
            int DuracionProhibida = 0;
            if (pelicula.Duracion > 90)
            {
                DuracionProhibida = 1;
            }
            return DuracionProhibida;
        }        
    }
}