using MVC_Proyecto_GRM.Models.ViewModels.Direcciones;
using MVC_Proyecto_GRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Proyecto_GRM.Models.ViewModels.Rentas;

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
                    }
                    // Redirecciona a la lista de camiones (Controller)
                    return Redirect("~/Rentas");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}