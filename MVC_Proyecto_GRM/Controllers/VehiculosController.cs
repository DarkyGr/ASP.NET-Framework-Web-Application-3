using MVC_Proyecto_GRM.Models.ViewModels.Direcciones;
using MVC_Proyecto_GRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Proyecto_GRM.Models.ViewModels.Vehiculos;

namespace MVC_Proyecto_GRM.Controllers
{
    public class VehiculosController : Controller
    {
        // GET: Vehiculos
        public ActionResult Index()
        {
            List<VehiculosListar> lista = new List<VehiculosListar>();

            // Entity framework
            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                // LinQ
                lista = (from vehiculo in db.Vehiculos
                         select new VehiculosListar
                         {
                             VehiculoId = vehiculo.VehiculoId,
                             Matricula = vehiculo.Matricula,
                             Marca = vehiculo.Marca,
                             Modelo = vehiculo.Modelo,
                             Capacidad = vehiculo.Capacidad,
                             Kilometraje = (float)vehiculo.Kilometraje,
                         }).ToList();
            }

            return View(lista);
        }

        public ActionResult VehiculoAgregar()
        {
            return View();
        }

        [HttpPost] // Data validator
        public ActionResult VehiculoAgregar(VehiculoAgregar model)
        {
            try
            {
                // Validar si modelo es correcto
                if (ModelState.IsValid)
                {
                    using (RentaCarrosEntities db = new RentaCarrosEntities())
                    {
                        var vehiculo = new Vehiculos();
                        
                        vehiculo.Matricula = model.Matricula;
                        vehiculo.Marca = model.Marca;
                        vehiculo.Modelo = model.Modelo;
                        vehiculo.Capacidad = model.Capacidad;
                        vehiculo.Kilometraje = model.Kilometraje;

                        // Se agrega el nuevo camion
                        db.Vehiculos.Add(vehiculo);
                        db.SaveChanges();
                    }
                    // Redirecciona a la lista de camiones (Controller)
                    return Redirect("~/Vehiculos");
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