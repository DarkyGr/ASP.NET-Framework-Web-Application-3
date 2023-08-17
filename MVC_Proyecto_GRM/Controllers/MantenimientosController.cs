using MVC_Proyecto_GRM.Models.ViewModels.Direcciones;
using MVC_Proyecto_GRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Proyecto_GRM.Models.ViewModels.Mantenimientos;
using static MVC_Proyecto_GRM.Models.Enum;
using MVC_Proyecto_GRM.Models.ViewModels.Clientes;
using MVC_Proyecto_GRM.Models.DDLs;

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
                lista = (from c in db.Mantenimientos
                         join ve in db.Vehiculos on c.VehiculoId equals ve.VehiculoId
                         select new MantenimientosListar
                         {
                             MantenimientoId = c.MantenimientoId,
                             VehiculoId = c.VehiculoId,
                             Vehiculo = "Marca: " + ve.Marca + " Modelo: " + ve.Modelo + " Matrícula: " + ve.Matricula,
                             Nota = c.Nota,
                             Fecha = c.Fecha,
                         }).ToList();
            }

            return View(lista);
        }

        public ActionResult MantenimientoAgregar()
        {
            CargarDDL();

            return View();
        }

        [HttpPost] // Data validator
        public ActionResult MantenimientoAgregar(MantenimientoAgregar model)
        {
            try
            {
                // Validar si modelo es correcto
                if (ModelState.IsValid && model.VehiculoId != 0)
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
                        Alert("El Mantenimiento ha sido agregado", NoticationType.success);
                    }
                    // Redirecciona a la lista de camiones (Controller)
                    return Redirect("~/Mantenimientos");
                }
                Alert("Verificar la información", NoticationType.warning);
                CargarDDL();
                return View(model);
            }
            catch (Exception ex)
            {
                Alert("Error: " + ex.Message, NoticationType.error);
                CargarDDL();
                return View(model);
            }
        }

        public ActionResult MantenimientoEditar(int id)
        {
            Mantenimientos mantenimiento = new Mantenimientos();
            CargarDDL();

            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                //camion = db.Camiones.Where(x => x.CamionId == camionId).OrderBy(x => x.Matricula).FirstOrDefault();
                mantenimiento = db.Mantenimientos.Where(x => x.MantenimientoId == id).FirstOrDefault();
            }

            ViewBag.Title = "Editando mantenimiento con ID: " + mantenimiento.MantenimientoId;

            return View(mantenimiento);
        }

        [HttpPost]
        public ActionResult MantenimientoEditar(Mantenimientos model)
        {
            try
            {
                // Validar si modelo es correcto
                if (ModelState.IsValid && model.VehiculoId != 0)
                {
                    using (RentaCarrosEntities db = new RentaCarrosEntities())
                    {
                        var mantenimiento = new Mantenimientos();

                        mantenimiento.MantenimientoId = model.MantenimientoId;
                        mantenimiento.VehiculoId = model.VehiculoId;
                        mantenimiento.Nota = model.Nota;
                        mantenimiento.Fecha = model.Fecha;

                        // Se agrega el nuevo camion                        
                        db.Entry(mantenimiento).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        Alert("El Mantenimiento ha sido agregado", NoticationType.success);
                    }
                    // Redirecciona a la lista de camiones
                    return Redirect("~/Mantenimientos");
                }
                Alert("Verificar la información", NoticationType.warning);
                CargarDDL();
                return View(model);
            }
            catch (Exception ex)
            {
                Alert("Error: " + ex.Message, NoticationType.error);
                CargarDDL();
                return View(model);
                //throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult MantenimientoEliminar(int id)
        {
            Mantenimientos mantenimiento = new Mantenimientos();
            try
            {
                using (RentaCarrosEntities db = new RentaCarrosEntities())
                {
                    mantenimiento = db.Mantenimientos.Where(x => x.MantenimientoId == id).FirstOrDefault();
                    db.Mantenimientos.Remove(mantenimiento);
                    db.SaveChanges();
                }
                Alert("Mantenimiento Eliminado con éxito.", NoticationType.success);
                return Redirect("~/Mantenimientos");
            }
            catch (Exception ex)
            {
                Alert("Error: " + ex.Message, NoticationType.error);
                return Redirect("~/Mantenimientos");
            }
        }

        public void Alert(string message, NoticationType noticationType)
        {
            var msg = "<script language='javascript'>Swal.fire('" + noticationType.ToString().ToUpper() + "', '" + message +
                "','" + noticationType + "')" + "</script>";

            TempData["notification"] = msg;
        }

        public void CargarDDL()
        {
            List<VehiculosDDL> listaV = new List<VehiculosDDL>();
            listaV.Insert(0, new VehiculosDDL { VehiculoId = 0, Info = "Seleccione un vehículo." });

            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                foreach (var d in db.Vehiculos)
                {
                    VehiculosDDL aux = new VehiculosDDL();
                    aux.VehiculoId = d.VehiculoId;
                    aux.Info = "Marca: " + d.Marca + " Modelo: " + d.Modelo + " Matrícula: " + d.Matricula;

                    listaV.Add(aux);
                }
            }

            ViewBag.ListaVehiculos = listaV;
        }
    }
}