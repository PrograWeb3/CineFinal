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



        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuarios usuario)
        {
            CineContext db = new CineContext();


            //Buscasr usuario
            var LogUsuario = ServicioUsuarios.buscarUsuario(usuario);

            if (LogUsuario != null)
            {

                Session["UserID"] = LogUsuario.IdUsuario.ToString();
                Session["UserName"] = LogUsuario.NombreUsuario.ToString();

                return RedirectToAction("Inicio", "Administracion");
            }
            else
            {
                ModelState.AddModelError("", "Usuario o Password incorrecto");
            }

            return View();
        }


        public ActionResult Logout()//Cerrar Sesion
        {
            Session.Clear();
            return RedirectToAction("Index");
        }


        public ActionResult VistaPreviaPelicula(Int32 id)
        {
            Peliculas p = ServicioPeliculas.MostrarPeliculaSeleccionada(id);
            return View(p);
        }


      

    }
}
