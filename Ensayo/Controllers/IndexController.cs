using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ensayo.Models;
using Ensayo.Models.Servicios;

namespace Ensayo.Controllers
{
    public class IndexController : Controller
    {
        //
        // GET: /Index/

        public ActionResult Index()
        {
            List<Carteleras> cartelera = ServicioCarteleras.ListarCarteleras();
            return View(cartelera);
        }
        public ActionResult VistaPreviaPelicula(Int32 id)
        {
            Peliculas p = ServicioPeliculas.MostrarPeliculaSeleccionada(id);
            return View(p);
        }


        //Probando Login ---Cristian
        public ActionResult Login()
        {
            return View();
        }

    }
}
