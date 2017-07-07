using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ensayo.Models.Servicios;
using System.Web.Script.Serialization;

namespace Ensayo.Controllers
{
    public class AdministracionController : Controller
    {

        // GET: Administracion
        public ActionResult Inicio()
        {
            return View();
        }

        public ActionResult Peliculas()
        {
            List<Peliculas> peliculas = ServicioPeliculas.ListarPeliculas();
            
            return View(peliculas);
        }
        /*Metodos controller para las peliculas*/
        public ActionResult NuevaPelicula()
        {
            ViewBag.Clasificaciones = ServicioPeliculas.ObtenerClasificaciones();
            ViewBag.Generos = ServicioPeliculas.ObtenerGeneros();
            return View();
        }
        [HttpPost]
        public ActionResult NuevaPelicula(Peliculas nueva_pelicula, HttpPostedFileBase Imagen)
        {
            try
            {              
                ServicioPeliculas.InsertarImagen(nueva_pelicula, Imagen);             
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "No se ha seleccionado un portada para la película.");
            }
            if (ModelState.IsValid)
            {
                int DuracionPermitida = ServicioPeliculas.ValidarDuracionPelicula(nueva_pelicula);
                if (DuracionPermitida > 0)
                {
                    ModelState.AddModelError("", "La duracion de la pelicula no puede superar los 90 minutos.");
                }
            }
            if (ModelState.IsValid)
            {
                ServicioPeliculas.CrearPelicula(nueva_pelicula);
                return RedirectToAction("Peliculas");
            }
            ViewBag.Clasificaciones = ServicioPeliculas.ObtenerClasificaciones();
            ViewBag.Generos = ServicioPeliculas.ObtenerGeneros();
            return View(nueva_pelicula);
        }
        
        public ActionResult EditarPelicula(Int32 Id)
        {
            Peliculas p = ServicioPeliculas.MostrarPeliculaSeleccionada(Id);
            ViewBag.Clasificaciones = ServicioPeliculas.ObtenerClasificaciones();
            ViewBag.Generos = ServicioPeliculas.ObtenerGeneros();
            return View(p);
        }

        [HttpPost]
        public ActionResult EditarPelicula(Peliculas pd, HttpPostedFileBase portada_pelicula)
        {
            try
            {
                ServicioPeliculas.EditarPelicula(pd, portada_pelicula);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "No se ha seleccionado un portada para la película.");
            }
            ViewBag.numero = pd.Nombre;
            ViewBag.duracion = pd.Duracion;
            return RedirectToAction("Peliculas");
        }
        /*Metodos controller para las sedes*/

        public ActionResult Sedes()
        {
            List<Sedes> sedes = ServicioSedes.ListarSedes();
            return View(sedes);
        }
        public ActionResult NuevaSedes()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NuevaSedes(Sedes nueva_sede)
        {
            if (ModelState.IsValid)
            {
                ServicioSedes.CrearSede(nueva_sede);
                return RedirectToAction("Sedes");
            }
            return View(nueva_sede);
        }
        public ActionResult EditarSede(Int32 Id)
        {
            Sedes sede = ServicioSedes.MostrarSedeSeleccionada(Id);
            return View(sede);
        }
        [HttpPost]
        public ActionResult EditarSede(Sedes sede)
        {
            if (ModelState.IsValid)
            {
                ServicioSedes.EditarSede(sede);
                return RedirectToAction("Sedes");
            }
            return View(sede);
        }
        /*Metodos controller para las carteleras*/

        public ActionResult Carteleras()
        {
            List<Carteleras> carteleras = ServicioCarteleras.ListarCarteleras();
            return View(carteleras);
        }

        public ActionResult NuevaCartelera()
        {
            List<Versiones> ListVersiones = ServicioVersiones.ListarVersiones();
            ViewBag.versiones = ListVersiones;

            List<Sedes> sedes = ServicioSedes.ListarSedes();
            ViewBag.sedes = sedes;

            List<Peliculas> listaPeliculas = ServicioPeliculas.ListarPeliculas(); ;
            ViewBag.peliculas = listaPeliculas;

            return View();
        }
        [HttpPost]
        public ActionResult NuevaCartelera(Carteleras cartelera)
        {
            //validar que no exista una cartelera con misma pelicula, sede y sala
            var CarteleraExistente = ServicioCarteleras.ValidarExistencia(cartelera);
            var FechaSolapada = ServicioCarteleras.ValidarSolapamientoFechas(cartelera);
            var HoraInicioPermitida = ServicioCarteleras.ValidarHoraInicioPermitida(cartelera);
            //var Intervalo30 = ServicioCarteleras.ValidarIntervalo30(cartelera);

            if (ModelState.IsValid)
            {
                if (CarteleraExistente > 0)
                {
                    ModelState.AddModelError("", "Ya existe una cartelera con misma sede, pelicula y misma version");
                }
                if (FechaSolapada > 0)
                {
                    ModelState.AddModelError("", "La fecha indicada no esta disponible");
                }
                if (HoraInicioPermitida > 0)
                {
                    ModelState.AddModelError("", "La hora de inicio debe ser luego de las 15:00 hs.");
                }
            }
            if (ModelState.IsValid)
            {
                cartelera.FechaCarga = DateTime.Now;
                ServicioCarteleras.CrearCartelera(cartelera);                
                return RedirectToAction("Carteleras");
            }

            List<Versiones> ListVersiones = ServicioVersiones.ListarVersiones();
            ViewBag.versiones = ListVersiones;

            List<Sedes> sedes = ServicioSedes.ListarSedes();
            ViewBag.sedes = sedes;

            List<Peliculas> listaPeliculas = ServicioPeliculas.ListarPeliculas();
            ViewBag.peliculas = listaPeliculas;

            /*Funcion para convertir int a formato horas*/

            //int fromTime = cartelera.HoraInicio;
            //TimeSpan hora1 = TimeSpan.FromHours(fromTime);

            //int fromtime2 =  90;
            //TimeSpan hora2 = TimeSpan.FromHours(fromtime2);

            // var suma = hora1 + hora2;

            //ViewBag.hora = hora1;

            //ViewBag.hora2 = suma;

            return View("nuevaCartelera");

        }
        public ActionResult EditarCartelera(Int32 Id)
        {
            Carteleras c = ServicioCarteleras.MostrarCarteleraSeleccionada(Id);
            ViewBag.Versiones = ServicioVersiones.ListarVersiones();
            ViewBag.peliculas = ServicioPeliculas.ListarPeliculas();
            ViewBag.sedes = ServicioSedes.ListarSedes();

            return View(c);        
        }
        [HttpPost]
        public ActionResult EditarCartelera(Carteleras cd)
        {
            ServicioCarteleras.EditarCartelera(cd);
            return RedirectToAction("Carteleras");
        }
        public ActionResult EliminarCartelera(Int32 Id)
        {
            ServicioCarteleras.EliminarCartelera(Id);
            return RedirectToAction("Carteleras");
        }
        public ActionResult Funciones(Int32 Id)
        {

            Carteleras cartelera = ServicioCarteleras.MostrarCarteleraSeleccionada(Id);
            //carteleras.OrderBy(x => x.HoraInicio);            
            return View(cartelera);
        }
        /*public string FuncionesPorSala(Int32 Id)
        {
            List<Carteleras> carteleras = ServicioCarteleras.ListarCarteleras(Id); //Filtra mediante ajax.
            var JSON = new JavaScriptSerializer();
            string objeto_json = JSON.Serialize(carteleras);
            return objeto_json;
        }*/
		public ActionResult ReporteReservas()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CrearReporte(DateTime FechaI, DateTime FechaF)
        {
            List<Reservas> reservas = ServicioReservas.CrearReporte(FechaI, FechaF);
            //Compruebo si el periodo consultado tiene mas de 30 días.
            int cantidad_dias = (FechaF - FechaI).Days;
            if(cantidad_dias > 30)
            {
                ViewBag.error = "El periodo de consulta no puede superar los 30 días";
            }
            //Verifico que la fecha de inicio no supere la de fin.
            if (cantidad_dias < 0)
            {
                ViewBag.error = "La fecha de inicio no puede ser mayor a la fecha de fin.";
            }
            //Verifico que la fecha de inicio no supere la de fin.
            if (reservas.Count == 0)
            {
                ViewBag.error = "No existe reservas realizadas en ese periodo.";
            }
            return View("ReporteReservas", reservas);
        }
    }
}

