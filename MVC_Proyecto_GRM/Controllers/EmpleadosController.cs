using MVC_Proyecto_GRM.Models.ViewModels.Direcciones;
using MVC_Proyecto_GRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Proyecto_GRM.Models.ViewModels.Empleados;

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
                    }
                    // Redirecciona a la lista de camiones (Controller)
                    return Redirect("~/Empleados");
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