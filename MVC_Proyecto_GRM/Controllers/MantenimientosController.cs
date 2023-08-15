using MVC_Proyecto_GRM.Models.ViewModels.Direcciones;
using MVC_Proyecto_GRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Proyecto_GRM.Models.ViewModels.Mantenimientos;

namespace MVC_Proyecto_GRM.Controllers
{
    public class MantenimientosController : Controller
    {
        // GET: Mantenimientos
        public ActionResult Index()
        {
            List<MantenimientosListar> lista = new List<MantenimientosListar>();

            // Entity framework
            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                // LinQ
                lista = (from manteni in db.Mantenimientos
                         select new MantenimientosListar
                         {
                             MantenimientoId =  manteni.MantenimientoId,
                             VehiculoId = manteni.VehiculoId,
                             Nota = manteni.Nota,
                             Fecha = manteni.Fecha,
                         }).ToList();
            }

            return View(lista);
        }

        public ActionResult MantenimientoAgregar()
        {
            return View();
        }

        [HttpPost] // Data validator
        public ActionResult MantenimientoAgregar(MantenimientoAgregar model)
        {
            try
            {
                // Validar si modelo es correcto
                if (ModelState.IsValid)
                {
                    using (RentaCarrosEntities db = new RentaCarrosEntities())
                    {
                        var manteni = new Mantenimientos();

                        manteni.VehiculoId = model.VehiculoId;
                        manteni.Nota = model.Nota;
                        manteni.Fecha = model.Fecha;

                        // Se agrega el nuevo camion
                        db.Mantenimientos.Add(manteni);
                        db.SaveChanges();
                    }
                    // Redirecciona a la lista de camiones (Controller)
                    return Redirect("~/Mantenimientos");
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