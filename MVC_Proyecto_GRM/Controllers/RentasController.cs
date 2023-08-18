﻿using MVC_Proyecto_GRM.Models.ViewModels.Direcciones;
using MVC_Proyecto_GRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Proyecto_GRM.Models.ViewModels.Rentas;
using static MVC_Proyecto_GRM.Models.Enum;
using MVC_Proyecto_GRM.Models.ViewModels.Clientes;
using MVC_Proyecto_GRM.Models.DDLs;

namespace MVC_Proyecto_GRM.Controllers
{
    public class RentasController : Controller
    {
        // GET: Rentas
        public ActionResult Index()
        {
            List<RentasListar> lista = new List<RentasListar>();

            // Entity framework
            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                // LinQ
                lista = (from r in db.Rentas
                         join v in db.Vehiculos on r.VehiculoId equals v.VehiculoId
                         join c in db.Clientes on r.ClienteId equals c.ClienteId
                         join e in db.Empleados on r.EmpleadoId equals e.EmpleadoId
                         select new RentasListar
                         {
                             RentaId = r.RentaId,
                             VehiculoId = r.VehiculoId,
                             Vehiculo = "Marca: " + v.Marca + " Modelo: " + v.Modelo + " Matrícula " + v.Matricula,
                             ClienteId = r.ClienteId,
                             Cliente = c.Nombre + " " + c.ApellidoP + " " + c.ApellidoM,
                             EmpleadoId = r.EmpleadoId,
                             Empleado = e.Nombre + " " + e.ApellidoP + " " + e.ApellidoM,
                             Costo = (float)r.Costo,
                             FechaRenta = r.FechaRenta,
                             FechaRentaFin = r.FechaRentaFin
                         }).ToList();
            }

            return View(lista);
        }

        public ActionResult RentaAgregar()
        {
            CargarDDL();
            return View();
        }

        [HttpPost] // Data validator
        public ActionResult RentaAgregar(RentaAgregar model)
        {
            try
            {
                // Validar si modelo es correcto
                if (ModelState.IsValid && model.VehiculoId != 0 && model.ClienteId != 0 && model.EmpleadoId != 0)
                {
                    using (RentaCarrosEntities db = new RentaCarrosEntities())
                    {
                        var renta = new Rentas();

                        renta.VehiculoId = model.VehiculoId; 
                        renta.ClienteId = model.ClienteId;
                        renta.EmpleadoId = model.EmpleadoId;
                        renta.Costo = model.Costo;
                        renta.FechaRenta = model.FechaRenta;
                        renta.FechaRentaFin = model.FechaRentaFin;

                        // Se agrega el nuevo camion
                        db.Rentas.Add(renta);
                        db.SaveChanges();
                        Alert("La Renta ha sido agregada", NoticationType.success);
                    }
                    // Redirecciona a la lista de camiones (Controller)
                    return Redirect("~/Rentas");
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

        public ActionResult RentaEditar(int id)
        {
            Rentas renta = new Rentas();
            CargarDDL();

            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                //camion = db.Camiones.Where(x => x.CamionId == camionId).OrderBy(x => x.Matricula).FirstOrDefault();
                renta = db.Rentas.Where(x => x.RentaId == id).FirstOrDefault();
            }

            return View(renta);
        }

        [HttpPost]
        public ActionResult RentaEditar(Rentas model)
        {
            try
            {
                // Validar si modelo es correcto
                if (ModelState.IsValid && model.VehiculoId != 0 && model.ClienteId != 0 && model.EmpleadoId != 0)
                {
                    using (RentaCarrosEntities db = new RentaCarrosEntities())
                    {
                        var renta = new Rentas();

                        renta.RentaId = model.RentaId;
                        renta.VehiculoId = model.VehiculoId;
                        renta.ClienteId = model.ClienteId;
                        renta.EmpleadoId = model.EmpleadoId;
                        renta.Costo = model.Costo;
                        renta.FechaRenta = model.FechaRenta;
                        renta.FechaRentaFin = model.FechaRentaFin;

                        // Se agrega el nuevo camion                        
                        db.Entry(renta).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                        Alert("La Renta ha sido agregada", NoticationType.success);
                    }
                    // Redirecciona a la lista de camiones
                    return Redirect("~/Rentas");
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
        public ActionResult RentaEliminar(int id)
        {
            Rentas renta = new Rentas();
            try
            {
                using (RentaCarrosEntities db = new RentaCarrosEntities())
                {
                    renta = db.Rentas.Where(x => x.RentaId == id).FirstOrDefault();
                    db.Rentas.Remove(renta);
                    db.SaveChanges();
                }
                Alert("Renta Eliminada con éxito.", NoticationType.success);
                return Redirect("~/Rentas");
            }
            catch (Exception ex)
            {
                Alert("Error: " + ex.Message, NoticationType.error);
                return Redirect("~/Rentas");
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

            List<ClientesDDL> listaC = new List<ClientesDDL>();
            listaC.Insert(0, new ClientesDDL { ClienteId = 0, Nombre = "Seleccione un cliente." });

            List<EmpleadosDDL> listaE = new List<EmpleadosDDL>();
            listaE.Insert(0, new EmpleadosDDL { EmpleadoId = 0, Nombre = "Seleccione un empleado." });

            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                foreach (var v in db.Vehiculos)
                {
                    VehiculosDDL aux = new VehiculosDDL();
                    aux.VehiculoId = v.VehiculoId;
                    aux.Info = "Marca: " + v.Marca + " Modelo: " + v.Modelo + " Matrícula " + v.Matricula;

                    listaV.Add(aux);
                }

                foreach (var c in db.Clientes)
                {
                    ClientesDDL aux = new ClientesDDL();
                    aux.ClienteId = c.ClienteId;
                    aux.Nombre = c.Nombre + " " + c.ApellidoP + " " + c.ApellidoM;

                    listaC.Add(aux);
                }

                foreach (var e in db.Empleados)
                {
                    EmpleadosDDL aux = new EmpleadosDDL();
                    aux.EmpleadoId = e.EmpleadoId;
                    aux.Nombre = e.Nombre + " " + e.ApellidoP + " " + e.ApellidoM;

                    listaE.Add(aux);
                }
            }

            ViewBag.ListaVehiculos = listaV;
            ViewBag.ListaClientes = listaC;
            ViewBag.ListaEmpleados = listaE;
        }
    }
}