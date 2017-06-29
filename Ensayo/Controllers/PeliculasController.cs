using Ensayo.Models;
using Ensayo.Models.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ensayo.Controllers
{
    public class PeliculasController : Controller
    {

        private CineContext db = new CineContext();

        public ActionResult Reserva(int IdPelicula)
        {
            var pelicula = ServicioCarteleras.BuscarCartelera(IdPelicula);
            var listaVersiones = ServicioVersiones.listaVersionesReserva(IdPelicula);
            ViewBag.Versiones = listaVersiones;
            return View();
        }


        public ActionResult ConfirmarReserva(Reservas reserva)
        {
            ModelView model = new ModelView();
            var cartelera = ServicioCarteleras.BuscarCartelera(reserva.IdPelicula);
            var tipoDni = ServicioTipoDocumento.listatipoDocumentos();
            ViewBag.tipoDNI = tipoDni;

            model.Carteleras = cartelera;
            model.Reservas = reserva;

            return View(model);
        }


        //[HttpPost]
        public ActionResult ConfirmarReserva2(ModelView ModelView, int? id)
        {

            ModelView.Reservas.FechaCarga = DateTime.Now;
            ModelView.Reservas.FechaHoraInicio = DateTime.Now;

            db.Reservas.Add(ModelView.Reservas);
            db.SaveChanges();

            return View("");
        }



        /*Metodos para manejar ajax */

        public JsonResult GetSedesList(int IdVersion, int IdPelicula)
        {


            db.Configuration.ProxyCreationEnabled = false;

            List<Sedes> listaSede = (from Sedes in db.Sedes
                                     from Carteleras in db.Carteleras
                                     where Sedes.IdSede == Carteleras.IdSede && Carteleras.IdVersion == IdVersion && Carteleras.IdPelicula == IdPelicula
                                     select Sedes).ToList();

            return Json(listaSede, JsonRequestBehavior.AllowGet);
        }


    }
}
