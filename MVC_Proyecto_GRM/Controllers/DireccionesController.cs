using MVC_Proyecto_GRM.Models;
using MVC_Proyecto_GRM.Models.ViewModels.Direcciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static MVC_Proyecto_GRM.Models.Enum;

namespace MVC_Proyecto_GRM.Controllers
{
    public class DireccionesController : Controller
    {
        // GET: Direcciones
        public ActionResult Index()
        {
            List<DireccionesListar> lista = new List<DireccionesListar>();

            // Entity framework
            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                // LinQ
                lista = (from direccion in db.Direcciones
                         select new DireccionesListar
                         {
                             DireccionId = direccion.DireccionId,
                             Calle = direccion.Calle,
                             Numero = (int)direccion.Numero,
                             Colonia = direccion.Colonia,
                             CP = direccion.CP,
                             Municipio = direccion.Municipio,
                             Ciudad = direccion.Ciudad,
                             Estado = direccion.Estado,
                         }).ToList();
            }

            return View(lista);
        }

        public ActionResult DireccionAgregar()
        {
            return View();
        }

        [HttpPost] // Data validator
        public ActionResult DireccionAgregar(DireccionAgregar model)
        {
            try
            {
                // Validar si modelo es correcto
                if (ModelState.IsValid)
                {
                    using (RentaCarrosEntities db = new RentaCarrosEntities())
                    {
                        var direccion = new Direcciones();

                        direccion.DireccionId = model.DireccionId;
                        direccion.Calle = model.Calle;
                        direccion.Numero = model.Numero;
                        direccion.Colonia = model.Colonia;
                        direccion.CP = model.CP;
                        direccion.Municipio = model.Municipio;
                        direccion.Ciudad = model.Ciudad;
                        direccion.Estado = model.Estado;

                        // Se agrega el nuevo camion
                        db.Direcciones.Add(direccion);
                        db.SaveChanges();
                        Alert("La Dirección ha sido agregada", NoticationType.success);
                    }
                    // Redirecciona a la lista de camiones (Controller)
                    return Redirect("~/Direcciones");
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

        public ActionResult DireccionEditar(int id)
        {
            Direcciones direccion = new Direcciones();

            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                //camion = db.Camiones.Where(x => x.CamionId == camionId).OrderBy(x => x.Matricula).FirstOrDefault();
                direccion = db.Direcciones.Where(x => x.DireccionId == id).FirstOrDefault();
            }

            return View(direccion);
        }

        [HttpPost]
        public ActionResult DireccionEditar(Direcciones model)
        {
            try
            {
                // Validar si modelo es correcto
                if (ModelState.IsValid)
                {
                    using (RentaCarrosEntities db = new RentaCarrosEntities())
                    {
                        var direccion = new Direcciones();

                        direccion.DireccionId = model.DireccionId;
                        direccion.Calle = model.Calle;
                        direccion.Numero = model.Numero;
                        direccion.Colonia = model.Colonia;
                        direccion.CP = model.CP;
                        direccion.Municipio = model.Municipio;
                        direccion.Ciudad = model.Ciudad;
                        direccion.Estado = model.Estado;

                        // Se agrega el nuevo camion                        
                        db.Entry(direccion).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        Alert("La Dirección ha sido agregado", NoticationType.success);
                    }
                    // Redirecciona a la lista de camiones
                    return Redirect("~/Direcciones");
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
        public ActionResult DireccionEliminar(int id)
        {
            Direcciones direccion = new Direcciones();
            try
            {
                using (RentaCarrosEntities db = new RentaCarrosEntities())
                {
                    direccion = db.Direcciones.Where(x => x.DireccionId == id).FirstOrDefault();
                    db.Direcciones.Remove(direccion);
                    db.SaveChanges();
                }
                Alert("Dirección Eliminada con éxito.", NoticationType.success);
                return Redirect("~/Direcciones");
            }
            catch (Exception ex)
            {
                Alert("No se puede eliminar la dirección porque la posee uno o varios clientes/empleados.", NoticationType.error);
                return Redirect("~/Direcciones");
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