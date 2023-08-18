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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "VehiculosService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select VehiculosService.svc or VehiculosService.svc.cs at the Solution Explorer and start debugging.
    public class VehiculosService : IVehiculosService
    {
        public string ActualizarVehiculo(int id, string matricula, string marca, string modelo, int capacidad, int kilometraje)
        {
            string respuesta = "";
            Vehiculos vehiculo = new Vehiculos()
            {
                VehiculoId = id,
                Matricula = matricula,
                Marca = marca,
                Modelo = modelo,
                Capacidad = capacidad,
                Kilometraje = kilometraje,
            };

            try
            {
                using (RentaCarrosEntities db = new RentaCarrosEntities())
                {
                    db.Entry(vehiculo).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    respuesta = "Vehículo Actualizado con éxito";
                }
            }
            catch (Exception ex)
            {
                respuesta = "Error" + ex.Message;
            }
            return respuesta;
        }

        public string AgregarVehiculo(string matricula, string marca, string modelo, int capacidad, int kilometraje)
        {
            string respuesta = "";
            Vehiculos vehiculo = new Vehiculos()
            {
                Matricula = matricula,
                Marca = marca,
                Modelo = modelo,
                Capacidad = capacidad,
                Kilometraje = kilometraje,                
            };

            try
            {
                using (RentaCarrosEntities db = new RentaCarrosEntities())
                {
                    db.Vehiculos.Add(vehiculo);
                    db.SaveChanges();
                    respuesta = "Vehículo Agregado con éxito";
                }
            }
            catch (Exception ex)
            {
                respuesta = "Error" + ex.Message;
            }
            return respuesta;
        }

        public string EliminarVehiculo(int id)
        {
            string respuesta = "";

            try
            {
                using (RentaCarrosEntities db = new RentaCarrosEntities())
                {
                    Vehiculos vehi = (from v in db.Vehiculos
                                         where v.VehiculoId == id
                                         select v).FirstOrDefault();
                    db.Vehiculos.Remove(vehi);
                    db.SaveChanges();
                }

                return respuesta = "Vehículo Eliminado con éxito";
            }
            catch (Exception ex)
            {
                return respuesta = "Error: " + ex.Message;
            }
        }

        public List<VehiculosVO> GetListVehiculos()
        {
            List<VehiculosVO> listaVO = new List<VehiculosVO>();

            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                foreach (var vehiculo in db.Vehiculos)
                {
                    VehiculosVO vehiculoVO = new VehiculosVO();

                    vehiculoVO.VehiculoId = vehiculo.VehiculoId;
                    vehiculoVO.Matricula = vehiculo.Matricula;
                    vehiculoVO.Marca = vehiculo.Marca;
                    vehiculoVO.Modelo = vehiculo.Modelo;
                    vehiculoVO.Capacidad = vehiculo.Capacidad;
                    vehiculoVO.Kilometraje = vehiculo.Kilometraje;

                    listaVO.Add(vehiculoVO);
                }

                return listaVO;
            }
        }

        public VehiculosVO GetVehiculoById(int id)
        {
            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                return (from v in db.Vehiculos
                        where v.VehiculoId == id
                        select new VehiculosVO
                        {
                            VehiculoId = v.VehiculoId,
                            Matricula = v.Matricula,
                            Marca = v.Marca,
                            Modelo = v.Modelo,
                            Capacidad = v.Capacidad,
                            Kilometraje = v.Kilometraje,
                        }).SingleOrDefault();
            }
        }
    }
}
