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

        // Primer formulario de reservas
        public ActionResult Reserva(int IdPelicula)
        {
            var pelicula = ServicioCarteleras.BuscarCartelera(IdPelicula);
            var listaVersiones = ServicioVersiones.listaVersionesReserva(IdPelicula);
            ViewBag.Versiones = listaVersiones;
            return View();
        }

        [HttpPost]
        public ActionResult Reserva(FormCollection reservas)
        {
            
            int IdPelicula = int.Parse(Request.Form["IdPelicula"]);
            int IdVersion = int.Parse(Request.Form["IdVersion"]);
            int sede = int.Parse(Request.Form["IdSede"]);
            string dia = Request.Form["DiasReservas"];
            string hora = Request.Form["HorasReservas"];

            var reserva = ServicioReservas.cargarReserva(IdPelicula, IdVersion, sede, dia, hora);

            return RedirectToAction("ConfirmarReserva", reserva);
           
        }



        public ActionResult ConfirmarReserva(Reservas reserva)
        {

            ModelView model = new ModelView();
            var cartelera = ServicioCarteleras.BuscarCarteleraReserva(reserva.IdVersion, reserva.IdPelicula, reserva.IdSede);
            var tipoDni = ServicioTipoDocumento.listatipoDocumentos();
            ViewBag.tipoDNI = tipoDni;

            model.Reservas = reserva;
            model.Carteleras = cartelera;

            return View(model);
        }



        [HttpPost]
        public ActionResult ConfirmarReserva(ModelView model)
        {
            if (ModelState.IsValid)
            {
                Reservas reserva = model.Reservas;
                ServicioReservas.guardarReserva(reserva);
                return RedirectToAction("ReservaCompleta", reserva);
            }
            var cartelera = ServicioCarteleras.BuscarCarteleraReserva(model.Reservas.IdVersion, model.Reservas.IdPelicula, model.Reservas.IdSede);
            model.Carteleras = cartelera;

            var tipoDni = ServicioTipoDocumento.listatipoDocumentos();
            ViewBag.tipoDNI = tipoDni;

            return View(model);
        }




        public ActionResult ReservaCompleta(Reservas reserva)
        {
            var sedePrecio = ServicioSedes.buscarSedeReserva(reserva);

            decimal total = sedePrecio.PrecioGeneral * reserva.CantidadEntradas;
            ViewBag.precioTotal = total;
            return View(reserva);
        }




        //public ActionResult ConfirmarReserva(Reservas reserva)
        //{
        //    ModelView model = new ModelView();
        //    var cartelera = ServicioCarteleras.BuscarCartelera(reserva.IdPelicula);
        //    var tipoDni = ServicioTipoDocumento.listatipoDocumentos();
        //    ViewBag.tipoDNI = tipoDni;

        //    model.Carteleras = cartelera;
        //    model.Reservas = reserva;

        //    return View(model);
        //}


        //[HttpPost]
        //public ActionResult ConfirmarReserva2(ModelView ModelView, int? id)
        //{

        //    ModelView.Reservas.FechaCarga = DateTime.Now;
        //    ModelView.Reservas.FechaHoraInicio = DateTime.Now;

        //    db.Reservas.Add(ModelView.Reservas);
        //    db.SaveChanges();

        //    return View("");
        //}



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


        public JsonResult GetDIasList(int IdVersion, int IdPelicula, int IdSede)
        {
            if (IdVersion == 0 || IdPelicula == 0 || IdSede == 0)
            {
                List<Dias> DiasVacia = new List<Dias>();
                return Json(DiasVacia, JsonRequestBehavior.AllowGet);
            }
            var NuevaHoras = ServicioDias.solapamientoDiasReservas(IdVersion, IdPelicula, IdSede);
            return Json(NuevaHoras, JsonRequestBehavior.AllowGet);


        }



        public JsonResult GetHorasList(int IdVersion, int IdPelicula, int IdSede)
        {
            if (IdVersion == 0 || IdPelicula == 0 || IdSede == 0)
            {
                List<Horas> HoraVacia = new List<Horas>();
                return Json(HoraVacia, JsonRequestBehavior.AllowGet);
            }
            var NuevaHoras = ServicioHoras.solapamientoHorasReservas(IdVersion, IdPelicula, IdSede);
            return Json(NuevaHoras, JsonRequestBehavior.AllowGet);
        }

    }
}
