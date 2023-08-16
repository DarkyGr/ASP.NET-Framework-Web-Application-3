using MVC_Proyecto_GRM.Models.ViewModels.Direcciones;
using MVC_Proyecto_GRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Proyecto_GRM.Models.ViewModels.Clientes;
using static MVC_Proyecto_GRM.Models.Enum;

namespace MVC_Proyecto_GRM.Controllers
{
    public class ClientesController : Controller
    {
        // GET: Clientes
        public ActionResult Index()
        {
            List<ClientesListar> lista = new List<ClientesListar>();

            // Entity framework
            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                // LinQ
                lista = (from vc in db.View_Clientes
                         select new ClientesListar
                         {
                             ClienteId = vc.ClienteId,
                             DireccionId = vc.DireccionId,
                             Dirección = vc.Dirección,
                             Nombre = vc.Nombre,
                             ApellidoP = vc.ApellidoP,
                             ApellidoM = vc.ApellidoM,
                             Telefono = vc.Telefono,
                             NumLicencia = vc.NumLicencia,
                             FechaVencimientoLicencia = vc.FechaVencimientoLicencia,
                         }).ToList();
            }

            return View(lista);
        }

        public ActionResult ClienteAgregar()
        {
            return View();
        }

        [HttpPost] // Data validator
        public ActionResult ClienteAgregar(ClienteAgregar model)
        {
            try
            {
                // Validar si modelo es correcto
                if (ModelState.IsValid)
                {
                    using (RentaCarrosEntities db = new RentaCarrosEntities())
                    {
                        var cliente = new Clientes();

                        cliente.DireccionId = model.DireccionId;
                        cliente.Nombre = model.Nombre;
                        cliente.ApellidoP = model.ApellidoP;
                        cliente.ApellidoM = model.ApellidoM;
                        cliente.Telefono = model.Telefono;
                        cliente.NumLicencia = model.NumLicencia;
                        cliente.FechaVencimientoLicencia = model.FechaVencimientoLicencia;

                        // Se agrega el nuevo camion
                        db.Clientes.Add(cliente);
                        db.SaveChanges();

                        Alert("El Cliente ha sido agregado", NoticationType.success);
                    }
                    // Redirecciona a la lista de camiones (Controller)
                    return Redirect("~/Clientes");
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

        public ActionResult ClienteEditar(int id)
        {
            Clientes cliente = new Clientes();

            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                //camion = db.Camiones.Where(x => x.CamionId == camionId).OrderBy(x => x.Matricula).FirstOrDefault();
                cliente = db.Clientes.Where(x => x.ClienteId == id).FirstOrDefault();
            }

            ViewBag.Title = "Editando cliente con ID: " + cliente.ClienteId;

            return View(cliente);
        }

        [HttpPost]
        public ActionResult ClienteEditar(Clientes model)
        {
            try
            {
                // Validar si modelo es correcto
                if (ModelState.IsValid)
                {
                    using (RentaCarrosEntities db = new RentaCarrosEntities())
                    {
                        var cliente = new Clientes();

                        cliente.ClienteId = model.ClienteId;
                        cliente.DireccionId = model.DireccionId;
                        cliente.Nombre = model.Nombre;
                        cliente.ApellidoP = model.ApellidoP;
                        cliente.ApellidoM = model.ApellidoM;
                        cliente.Telefono = model.Telefono;
                        cliente.NumLicencia = model.NumLicencia;
                        cliente.FechaVencimientoLicencia = model.FechaVencimientoLicencia;

                        // Se agrega el nuevo camion                        
                        db.Entry(cliente).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        Alert("El Cliente ha sido agregado", NoticationType.success);
                    }
                    // Redirecciona a la lista de camiones
                    return Redirect("~/Clientes");
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
        public ActionResult ClienteEliminar(int id)
        {
            Clientes cliente = new Clientes();
            try
            {
                using (RentaCarrosEntities db = new RentaCarrosEntities())
                {
                    cliente = db.Clientes.Where(x => x.ClienteId == id).FirstOrDefault();
                    db.Clientes.Remove(cliente);
                    db.SaveChanges();
                }
                Alert("Cliente Eliminado con éxito.", NoticationType.success);
                return Redirect("~/Clientes");
            }
            catch (Exception ex)
            {
                Alert("Error: " + ex.Message, NoticationType.error);
                return Redirect("~/Clientes");
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