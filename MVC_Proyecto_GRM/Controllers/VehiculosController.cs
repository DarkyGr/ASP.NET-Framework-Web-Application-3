using MVC_Proyecto_GRM.Models.ViewModels.Direcciones;
using MVC_Proyecto_GRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Proyecto_GRM.Models.ViewModels.Vehiculos;
using static MVC_Proyecto_GRM.Models.Enum;

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
                        foreach (var item in db.Vehiculos)
                        {
                            if (item.Matricula == model.Matricula)
                            {
                                Alert("Ya existe un vehículo con la misma matrícula.", NoticationType.error);
                                return View(model);
                            }
                        }

                        var vehiculo = new Vehiculos();

                        vehiculo.Matricula = model.Matricula;
                        vehiculo.Marca = model.Marca;
                        vehiculo.Modelo = model.Modelo;
                        vehiculo.Capacidad = model.Capacidad;
                        vehiculo.Kilometraje = model.Kilometraje;

                        // Se agrega el nuevo camion
                        db.Vehiculos.Add(vehiculo);
                        db.SaveChanges();

                        Alert("El Vehículo ha sido agregado", NoticationType.success);

                    }
                    // Redirecciona a la lista de camiones (Controller)
                    return Redirect("~/Vehiculos");
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

        public ActionResult VehiculoEditar(int id)
        {
            Vehiculos vehiculo = new Vehiculos();

            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                //camion = db.Camiones.Where(x => x.CamionId == camionId).OrderBy(x => x.Matricula).FirstOrDefault();
                vehiculo = db.Vehiculos.Where(x => x.VehiculoId == id).FirstOrDefault();
            }

            return View(vehiculo);
        }

        [HttpPost]
        public ActionResult VehiculoEditar(Vehiculos model)
        {
            try
            {
                // Validar si modelo es correcto
                if (ModelState.IsValid)
                {
                    using (RentaCarrosEntities db = new RentaCarrosEntities())
                    {
                        var vehiculo = new Vehiculos();

                        vehiculo.VehiculoId = model.VehiculoId;
                        vehiculo.Matricula = model.Matricula;
                        vehiculo.Marca = model.Marca;
                        vehiculo.Modelo = model.Modelo;
                        vehiculo.Capacidad = model.Capacidad;
                        vehiculo.Kilometraje = model.Kilometraje;

                        // Se agrega el nuevo camion                        
                        db.Entry(vehiculo).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        Alert("El Vehículo ha sido actualizado", NoticationType.success);
                    }
                    // Redirecciona a la lista de camiones
                    return Redirect("~/Vehiculos");
                }
                Alert("El kilometraje no puede estar vacío y debe ser mayor o igual al que tenía", NoticationType.warning);
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
        public ActionResult VehiculoEliminar(int id)
        {
            Vehiculos vehiculo = new Vehiculos();
            try
            {
                using (RentaCarrosEntities db = new RentaCarrosEntities())
                {
                    vehiculo = db.Vehiculos.Where(x => x.VehiculoId == id).FirstOrDefault();
                    db.Vehiculos.Remove(vehiculo);
                    db.SaveChanges();
                }
                Alert("Vehículo Eliminado con éxito.", NoticationType.success);
                return Redirect("~/Vehiculos");
            }
            catch (Exception ex)
            {
                Alert("El Vehículo no puede ser eliminado porque posee mantenimiento(s) o renta(s).", NoticationType.error);
                return Redirect("~/Vehiculos");
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