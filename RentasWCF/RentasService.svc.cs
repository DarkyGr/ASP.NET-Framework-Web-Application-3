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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RentasService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select RentasService.svc or RentasService.svc.cs at the Solution Explorer and start debugging.
    public class RentasService : IRentasService
    {
        public string ActualizarRenta(int id, int vehiculoId, int clienteId, int empleadoId, float costo, DateTime fechaRenta, DateTime fechaRentaFin)
        {
            string respuesta = "";
            Rentas renta = new Rentas()
            {
                RentaId = id,
                VehiculoId = vehiculoId,
                ClienteId = clienteId,
                EmpleadoId = empleadoId,
                Costo = costo,
                FechaRenta = fechaRenta,
                FechaRentaFin = fechaRentaFin,
            };

            try
            {
                using (RentaCarrosEntities db = new RentaCarrosEntities())
                {
                    db.Entry(renta).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    respuesta = "Renta Actualizada con éxito";
                }
            }
            catch (Exception ex)
            {
                respuesta = "Error" + ex.Message;
            }
            return respuesta;
        }

        public string AgregarRenta(int vehiculoId, int clienteId, int empleadoId, float costo, DateTime fechaRenta, DateTime fechaRentaFin)
        {
            string respuesta = "";
            Rentas renta = new Rentas()
            {
                VehiculoId = vehiculoId,
                ClienteId = clienteId,
                EmpleadoId = empleadoId,
                Costo = costo,
                FechaRenta = fechaRenta,
                FechaRentaFin = fechaRentaFin,
            };

            try
            {
                using (RentaCarrosEntities db = new RentaCarrosEntities())
                {
                    db.Rentas.Add(renta);
                    db.SaveChanges();
                    respuesta = "Renta Agregada con éxito";
                }
            }
            catch (Exception ex)
            {
                respuesta = "Error" + ex.Message;
            }
            return respuesta;
        }

        public string EliminarRenta(int id)
        {
            string respuesta = "";

            try
            {
                using (RentaCarrosEntities db = new RentaCarrosEntities())
                {
                    Rentas ren = (from r in db.Rentas
                                  where r.RentaId == id
                                  select r).FirstOrDefault();
                    db.Rentas.Remove(ren);
                    db.SaveChanges();
                }

                return respuesta = "Renta Eliminada con éxito";
            }
            catch (Exception ex)
            {
                return respuesta = "Error: " + ex.Message;
            }
        }

        public List<RentasVO> GetListRentas()
        {
            List<RentasVO> listaVO = new List<RentasVO>();

            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                foreach (var renta in db.Rentas)
                {
                    RentasVO rentaVO = new RentasVO();

                    rentaVO.RentaId = renta.RentaId;
                    rentaVO.VehiculoId = renta.VehiculoId;
                    rentaVO.ClienteId = renta.ClienteId;
                    rentaVO.EmpleadoId = renta.EmpleadoId;
                    rentaVO.Costo = renta.Costo;
                    rentaVO.FechaRenta = renta.FechaRenta;
                    rentaVO.FechaRentaFin = renta.FechaRentaFin;

                    listaVO.Add(rentaVO);
                }

                return listaVO;
            }
        }

        public RentasVO GetRentaById(int id)
        {
            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                return (from r in db.Rentas
                        where r.RentaId == id
                        select new RentasVO
                        {
                            RentaId = r.RentaId,
                            VehiculoId = r.VehiculoId,
                            ClienteId = r.ClienteId,
                            EmpleadoId = r.EmpleadoId,
                            Costo = r.Costo,
                            FechaRenta = r.FechaRenta,
                            FechaRentaFin = r.FechaRentaFin,
                        }).SingleOrDefault();
            }
        }
    }
}
