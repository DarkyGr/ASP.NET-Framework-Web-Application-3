using MVC_Proyecto_GRM.Models;
using MVC_Proyecto_GRM.Models.ViewModels.Direcciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                    }
                    // Redirecciona a la lista de camiones (Controller)
                    return Redirect("~/Direcciones");
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