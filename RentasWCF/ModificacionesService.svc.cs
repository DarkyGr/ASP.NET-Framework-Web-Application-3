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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ModificacionesService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ModificacionesService.svc or ModificacionesService.svc.cs at the Solution Explorer and start debugging.
    public class ModificacionesService : IModificacionesService
    {
        public string EliminarMantenimiento(int id)
        {
            string respuesta = "";

            try
            {
                using (RentaCarrosEntities db = new RentaCarrosEntities())
                {
                    Mantenimientos man = (from m in db.Mantenimientos
                                     where m.MantenimientoId == id
                                     select m).FirstOrDefault();
                    db.Mantenimientos.Remove(man);
                    db.SaveChanges();
                }

                return respuesta = "Mantenimiento Eliminado con éxito";
            }
            catch (Exception ex)
            {
                return respuesta = "Error: " + ex.Message;
            }
        }

        public List<MantenimientosVO> GetListMantenimientos()
        {
            List<MantenimientosVO> listaVO = new List<MantenimientosVO>();

            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                foreach (var mantenimiento in db.Mantenimientos)
                {
                    MantenimientosVO mantenimientoVO = new MantenimientosVO();

                    mantenimientoVO.MantenimientoId = mantenimiento.MantenimientoId;
                    mantenimientoVO.VehiculoId = mantenimiento.VehiculoId;
                    mantenimientoVO.Nota = mantenimiento.Nota;
                    mantenimientoVO.Fecha = mantenimiento.Fecha;

                    listaVO.Add(mantenimientoVO);
                }

                return listaVO;
            }
        }

        public MantenimientosVO GetMantenimientoById(int id)
        {
            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                return (from m in db.Mantenimientos
                        where m.MantenimientoId == id
                        select new MantenimientosVO
                        {
                            MantenimientoId = m.MantenimientoId,
                            VehiculoId = m.VehiculoId,
                            Nota = m.Nota,
                            Fecha = m.Fecha,
                        }).SingleOrDefault();
            }
        }

        public string AgregarMantenimiento(int vehiculoId, string nota, DateTime fecha)
        {
            string respuesta = "";
            Mantenimientos mantenimiento = new Mantenimientos()
            {
                VehiculoId = vehiculoId,
                Nota = nota,
                Fecha = fecha,
            };

            try
            {
                using (RentaCarrosEntities db = new RentaCarrosEntities())
                {
                    db.Mantenimientos.Add(mantenimiento);
                    db.SaveChanges();
                    respuesta = " Agregado con éxito";
                }
            }
            catch (Exception ex)
            {
                respuesta = "Error" + ex.Message;
            }
            return respuesta;
        }

        public string ActualizarMantenimiento(int id, int vehiculoId, string nota, DateTime fecha)
        {
            string respuesta = "";
            Mantenimientos mantenimiento = new Mantenimientos()
            {
                MantenimientoId = id,
                VehiculoId = vehiculoId,
                Nota = nota,
                Fecha = fecha,
            };

            try
            {
                using (RentaCarrosEntities db = new RentaCarrosEntities())
                {
                    db.Entry(mantenimiento).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    respuesta = "Mantenimiento Actualizado con éxito";
                }
            }
            catch (Exception ex)
            {
                respuesta = "Error" + ex.Message;
            }
            return respuesta;
        }
    }
}
