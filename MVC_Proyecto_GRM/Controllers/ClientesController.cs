using MVC_Proyecto_GRM.Models.ViewModels.Direcciones;
using MVC_Proyecto_GRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Proyecto_GRM.Models.ViewModels.Clientes;

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
                lista = (from cliente in db.Clientes
                         select new ClientesListar
                         {
                             ClienteId = cliente.ClienteId,
                             DireccionId = cliente.DireccionId,
                             Nombre = cliente.Nombre,
                             ApellidoP = cliente.ApellidoP,
                             ApellidoM = cliente.ApellidoM,
                             Telefono = cliente.Telefono,
                             NumLicencia = cliente.NumLicencia,
                             FechaVencimientoLicencia = (DateTime)cliente.FechaVencimientoLicencia,
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
                    }
                    // Redirecciona a la lista de camiones (Controller)
                    return Redirect("~/Clientes");
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