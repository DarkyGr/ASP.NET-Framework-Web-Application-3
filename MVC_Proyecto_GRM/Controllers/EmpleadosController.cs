using MVC_Proyecto_GRM.Models.ViewModels.Direcciones;
using MVC_Proyecto_GRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Proyecto_GRM.Models.ViewModels.Empleados;
using static MVC_Proyecto_GRM.Models.Enum;

namespace MVC_Proyecto_GRM.Controllers
{
    public class EmpleadosController : Controller
    {
        // GET: Empleados
        public ActionResult Index()
        {
            List<EmpleadosListar> lista = new List<EmpleadosListar>();

            // Entity framework
            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                // LinQ
                lista = (from empleado in db.Empleados
                         select new EmpleadosListar
                         {
                             EmpleadoId = empleado.EmpleadoId,
                             DireccionId = empleado.DireccionId,
                             Nombre = empleado.Nombre,
                             ApellidoP = empleado.ApellidoP,
                             ApellidoM = empleado.ApellidoM,
                             Salario = (float)empleado.Salario,
                             Puesto = empleado.Puesto,
                         }).ToList();
            }

            return View(lista);
        }

        public ActionResult EmpleadoAgregar()
        {
            return View();
        }

        [HttpPost] // Data validator
        public ActionResult EmpleadoAgregar(EmpleadoAgregar model)
        {
            try
            {
                // Validar si modelo es correcto
                if (ModelState.IsValid)
                {
                    using (RentaCarrosEntities db = new RentaCarrosEntities())
                    {
                        var empleado = new Empleados();

                        empleado.EmpleadoId = model.EmpleadoId;
                        empleado.DireccionId= model.DireccionId;
                        empleado.Nombre = model.Nombre;
                        empleado.ApellidoP = model.ApellidoP;
                        empleado.ApellidoM = model.ApellidoM;
                        empleado.Salario = model.Salario;
                        empleado.Puesto = model.Puesto;

                        // Se agrega el nuevo camion
                        db.Empleados.Add(empleado);
                        db.SaveChanges();
                        Alert("El Empleado ha sido agregado", NoticationType.success);
                    }
                    // Redirecciona a la lista de camiones (Controller)
                    return Redirect("~/Empleados");
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

        public ActionResult EmpleadoEditar(int id)
        {
            Empleados empleado = new Empleados();

            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                //camion = db.Camiones.Where(x => x.CamionId == camionId).OrderBy(x => x.Matricula).FirstOrDefault();
                empleado = db.Empleados.Where(x => x.EmpleadoId == id).FirstOrDefault();
            }

            ViewBag.Title = "Editando empleado con ID: " + empleado.EmpleadoId;

            return View(empleado);
        }

        [HttpPost]
        public ActionResult EmpleadoEditar(Empleados model)
        {
            try
            {
                // Validar si modelo es correcto
                if (ModelState.IsValid)
                {
                    using (RentaCarrosEntities db = new RentaCarrosEntities())
                    {
                        var empleado = new Empleados();

                        empleado.EmpleadoId = model.EmpleadoId;
                        empleado.DireccionId = model.DireccionId;
                        empleado.Nombre = model.Nombre;
                        empleado.ApellidoP = model.ApellidoP;
                        empleado.ApellidoM = model.ApellidoM;
                        empleado.Salario = model.Salario;
                        empleado.Puesto = model.Puesto;

                        // Se agrega el nuevo camion                        
                        db.Entry(empleado).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        Alert("El Empleado ha sido agregado", NoticationType.success);
                    }
                    // Redirecciona a la lista de camiones
                    return Redirect("~/Empleados");
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
        public ActionResult EmpleadoEliminar(int id)
        {
            Empleados empleado = new Empleados();
            try
            {
                using (RentaCarrosEntities db = new RentaCarrosEntities())
                {
                    empleado = db.Empleados.Where(x => x.EmpleadoId == id).FirstOrDefault();
                    db.Empleados.Remove(empleado);
                    db.SaveChanges();
                }
                Alert("Empleado Eliminado con éxito.", NoticationType.success);
                return Redirect("~/Empleados");
            }
            catch (Exception ex)
            {
                Alert("Error: " + ex.Message, NoticationType.error);
                return Redirect("~/Empleados");
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