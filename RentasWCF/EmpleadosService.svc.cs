using RentasWCF.Models;
using RentasWCF.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RentasWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "EmpleadosService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select EmpleadosService.svc or EmpleadosService.svc.cs at the Solution Explorer and start debugging.
    public class EmpleadosService : IEmpleadosService
    {
        public string ActualizarEmpleado(int id, int direccionId, string nombre, string apellidoP, string apellidoM, float salario, string puesto)
        {
            string respuesta = "";
            Empleados empleado = new Empleados()
            {
                EmpleadoId = id,
                DireccionId = direccionId,
                Nombre = nombre,
                ApellidoP = apellidoP,
                ApellidoM = apellidoM,
                Salario = salario,
                Puesto = puesto,
            };

            try
            {
                using (RentaCarrosEntities db = new RentaCarrosEntities())
                {
                    db.Entry(empleado).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    respuesta = "Empleado Actualizado con éxito";
                }
            }
            catch (Exception ex)
            {
                respuesta = "Error" + ex.Message;
            }
            return respuesta;
        }

        public string AgregarEmpleado(int direccionId, string nombre, string apellidoP, string apellidoM, float salario, string puesto)
        {
            string respuesta = "";
            Empleados empleado = new Empleados()
            {
                DireccionId = direccionId,
                Nombre = nombre,
                ApellidoP = apellidoP,
                ApellidoM = apellidoM,
                Salario = salario,
                Puesto = puesto,
            };

            try
            {
                using (RentaCarrosEntities db = new RentaCarrosEntities())
                {
                    db.Empleados.Add(empleado);
                    db.SaveChanges();
                    respuesta = "Empleado Agregado con éxito";
                }
            }
            catch (Exception ex)
            {
                respuesta = "Error" + ex.Message;
            }
            return respuesta;
        }

        public string EliminarEmpleado(int id)
        {
            string respuesta = "";

            try
            {
                using (RentaCarrosEntities db = new RentaCarrosEntities())
                {
                    Empleados emp = (from e in db.Empleados
                                         where e.EmpleadoId == id
                                         select e).FirstOrDefault();
                    db.Empleados.Remove(emp);
                    db.SaveChanges();
                }

                return respuesta = "Empleado Eliminado con éxito";
            }
            catch (Exception ex)
            {
                return respuesta = "Error: " + ex.Message;
            }
        }

        public EmpleadosVO GetEmpleadoById(int id)
        {
            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                return (from e in db.Empleados
                        where e.EmpleadoId == id
                        select new EmpleadosVO
                        {
                            EmpleadoId = e.EmpleadoId,
                            DireccionId = e.DireccionId,
                            Nombre = e.Nombre,
                            ApellidoP = e.ApellidoP,
                            ApellidoM = e.ApellidoM,
                            Salario = e.Salario,
                            Puesto = e.Puesto,
                        }).SingleOrDefault();
            }
        }

        public List<EmpleadosVO> GetListEmpleados()
        {
            List<EmpleadosVO> listaVO = new List<EmpleadosVO>();

            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                foreach (var empleado in db.Empleados)
                {
                    EmpleadosVO empleadoVO = new EmpleadosVO();

                    empleadoVO.EmpleadoId = empleado.EmpleadoId;
                    empleadoVO.DireccionId = empleado.DireccionId;
                    empleadoVO.Nombre = empleado.Nombre;
                    empleadoVO.ApellidoP = empleado.ApellidoP;
                    empleadoVO.ApellidoM = empleado.ApellidoM;
                    empleadoVO.Salario = empleado.Salario;
                    empleadoVO.Puesto = empleado.Puesto;

                    listaVO.Add(empleadoVO);
                }

                return listaVO;
            }
        }
    }
}
