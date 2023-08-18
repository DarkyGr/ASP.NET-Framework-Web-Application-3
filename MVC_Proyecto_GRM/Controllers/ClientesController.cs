using MVC_Proyecto_GRM.Models.ViewModels.Direcciones;
using MVC_Proyecto_GRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Proyecto_GRM.Models.ViewModels.Clientes;
using static MVC_Proyecto_GRM.Models.Enum;
using MVC_Proyecto_GRM.Models.ViewModels.Rentas;
using MVC_Proyecto_GRM.Models.DDLs;

namespace MVC_Proyecto_GRM.Controllers
{
    public class ClientesController : Controller
    {
        // ********************** Metodo Sencillo APROBADO *********************** 
        // GET: Clientes
        public ActionResult Index()
        {
            List<ClientesListar> lista = new List<ClientesListar>();

            // Entity framework
            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                // LinQ
                lista = (from c in db.Clientes
                         join direc in db.Direcciones on c.DireccionId equals direc.DireccionId
                         select new ClientesListar
                         {
                             ClienteId = c.ClienteId,
                             DireccionId = c.DireccionId,
                             Dirección = "Calle " + direc.Calle + " #" + direc.Numero + " Col. " + direc.Colonia + " C.P. " + direc.CP,
                             Nombre = c.Nombre,
                             ApellidoP = c.ApellidoP,
                             ApellidoM = c.ApellidoM,
                             Telefono = c.Telefono,
                             NumLicencia = c.NumLicencia,
                             FechaVencimientoLicencia = c.FechaVencimientoLicencia,
                         }).ToList();
            }

            return View(lista);
        }

        public ActionResult ClienteAgregar()
        {
            CargarDDL();

            return View();
        }

        [HttpPost] // Data validator
        public ActionResult ClienteAgregar(ClienteAgregar model)
        {
            try
            {
                // Validar si modelo es correcto
                if (ModelState.IsValid && model.DireccionId != 0)
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
                CargarDDL();
                return View(model);
            }
            catch (Exception ex)
            {
                Alert("Verificar la información", NoticationType.warning);
                CargarDDL();
                return View(model);
            }
        }

        public ActionResult ClienteEditar(int id)
        {            
            CargarDDL();
            Clientes cliente = new Clientes();

            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                //camion = db.Camiones.Where(x => x.CamionId == camionId).OrderBy(x => x.Matricula).FirstOrDefault();
                cliente = db.Clientes.Where(x => x.ClienteId == id).FirstOrDefault();
            }

            return View(cliente);
        }

        [HttpPost]
        public ActionResult ClienteEditar(Clientes model)
        {
            try
            {
                // Validar si modelo es correcto
                if (ModelState.IsValid && model.DireccionId != 0)
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
                CargarDDL();
                return View(model);
            }
            catch (Exception ex)
            {
                Alert("Verificar la información", NoticationType.warning);
                CargarDDL();
                return View(model);
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


        // ********************** Metodos APROVADOS *********************** 
        
        public void Alert(string message, NoticationType noticationType)
        {
            var msg = "<script language='javascript'>Swal.fire('" + noticationType.ToString().ToUpper() + "', '" + message +
                "','" + noticationType + "')" + "</script>";

            TempData["notification"] = msg;
        }

        public void CargarDDL()
        {
            List<DireccionesDDL> listaD = new List<DireccionesDDL>();
            listaD.Insert(0, new DireccionesDDL { DireccionId = 0, Direccion = "Seleccione una dirección." });

            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                foreach (var d in db.Direcciones)
                {
                    DireccionesDDL aux = new DireccionesDDL();
                    aux.DireccionId = d.DireccionId;
                    aux.Direccion = "Calle " + d.Calle + " #" + d.Numero + " Col. " + d.Colonia + " C.P. " + d.CP;

                    listaD.Add(aux);
                }
            }

            ViewBag.ListaDirecciones = listaD;
        }
    }
}