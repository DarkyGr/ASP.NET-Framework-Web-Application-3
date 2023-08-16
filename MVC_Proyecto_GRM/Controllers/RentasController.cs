using MVC_Proyecto_GRM.Models.ViewModels.Direcciones;
using MVC_Proyecto_GRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Proyecto_GRM.Models.ViewModels.Rentas;
using static MVC_Proyecto_GRM.Models.Enum;

namespace MVC_Proyecto_GRM.Controllers
{
    public class RentasController : Controller
    {
        // GET: Rentas
        public ActionResult Index()
        {
            List<RentasListar> lista = new List<RentasListar>();

            // Entity framework
            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                // LinQ
                lista = (from renta in db.Rentas
                         select new RentasListar
                         {
                             RentaId = renta.RentaId,
                             VehiculoId = renta.VehiculoId,
                             ClienteId = renta.ClienteId,
                             EmpleadoId = renta.EmpleadoId,
                             Costo = (float)renta.Costo,
                             FechaRenta = renta.FechaRenta,
                             FechaRentaFin = renta.FechaRentaFin,
                         }).ToList();
            }

            return View(lista);
        }

        public ActionResult RentaAgregar()
        {
            return View();
        }

        [HttpPost] // Data validator
        public ActionResult RentaAgregar(RentaAgregar model)
        {
            try
            {
                // Validar si modelo es correcto
                if (ModelState.IsValid)
                {
                    using (RentaCarrosEntities db = new RentaCarrosEntities())
                    {
                        var renta = new Rentas();

                        renta.VehiculoId = model.VehiculoId; 
                        renta.ClienteId = model.ClienteId;
                        renta.EmpleadoId = model.EmpleadoId;
                        renta.Costo = model.Costo;
                        renta.FechaRenta = model.FechaRenta;
                        renta.FechaRentaFin = model.FechaRentaFin;

                        // Se agrega el nuevo camion
                        db.Rentas.Add(renta);
                        db.SaveChanges();
                        Alert("La Renta ha sido agregada", NoticationType.success);
                    }
                    // Redirecciona a la lista de camiones (Controller)
                    return Redirect("~/Rentas");
                }
                Alert("Verificar la información", NoticationType.warning);
                return View(model);
            }
            catch (Exception ex)
            {
                Alert("Error: " + ex.Message, NoticationType.error);
                return View(model);
            }
        }

        public ActionResult RentaEditar(int id)
        {
            Rentas renta = new Rentas();

            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                //camion = db.Camiones.Where(x => x.CamionId == camionId).OrderBy(x => x.Matricula).FirstOrDefault();
                renta = db.Rentas.Where(x => x.RentaId == id).FirstOrDefault();
            }

            ViewBag.Title = "Editando renta con ID: " + renta.RentaId;

            return View(renta);
        }

        [HttpPost]
        public ActionResult RentaEditar(Rentas model)
        {
            try
            {
                // Validar si modelo es correcto
                if (ModelState.IsValid)
                {
                    using (RentaCarrosEntities db = new RentaCarrosEntities())
                    {
                        var renta = new Rentas();

                        renta.RentaId = model.RentaId;
                        renta.VehiculoId = model.VehiculoId;
                        renta.ClienteId = model.ClienteId;
                        renta.EmpleadoId = model.EmpleadoId;
                        renta.Costo = model.Costo;
                        renta.FechaRenta = model.FechaRenta;
                        renta.FechaRentaFin = model.FechaRentaFin;

                        // Se agrega el nuevo camion                        
                        db.Entry(renta).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        Alert("La Renta ha sido agregada", NoticationType.success);
                    }
                    // Redirecciona a la lista de camiones
                    return Redirect("~/Rentas");
                }
                Alert("Verificar la información", NoticationType.warning);
                return View(model);
            }
            catch (Exception ex)
            {
                Alert("Error: " + ex.Message, NoticationType.error);
                return View(model);
                //throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult RentaEliminar(int id)
        {
            Rentas renta = new Rentas();
            try
            {
                using (RentaCarrosEntities db = new RentaCarrosEntities())
                {
                    renta = db.Rentas.Where(x => x.RentaId == id).FirstOrDefault();
                    db.Rentas.Remove(renta);
                    db.SaveChanges();
                }
                Alert("Renta Eliminada con éxito.", NoticationType.success);
                return Redirect("~/Rentas");
            }
            catch (Exception ex)
            {
                Alert("Error: " + ex.Message, NoticationType.error);
                return Redirect("~/Rentas");
            }
        }

        public void Alert(string message, NoticationType noticationType)
        {
            var msg = "<script language='javascript'>Swal.fire('" + noticationType.ToString().ToUpper() + "', '" + message +
                "','" + noticationType + "')" + "</script>";

            TempData["notification"] = msg;
        }
    }
}