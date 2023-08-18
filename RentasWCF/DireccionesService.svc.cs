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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DireccionesService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DireccionesService.svc or DireccionesService.svc.cs at the Solution Explorer and start debugging.
    public class DireccionesService : IDireccionesService
    {
        public string ActualizarDireccion(int id, string calle, int numero, string colonia, int cp, string municipio, string ciudad, string estado)
        {
            string respuesta = "";
            Direcciones direccion = new Direcciones()
            {
                DireccionId = id,
                Calle = calle,
                Numero = numero,
                Colonia = colonia,
                CP = cp,
                Municipio = municipio,
                Ciudad = ciudad,
                Estado = estado,
            };

            try
            {
                using (RentaCarrosEntities db = new RentaCarrosEntities())
                {
                    db.Entry(direccion).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    respuesta = "Dirección Actualizada con éxito";
                }
            }
            catch (Exception ex)
            {
                respuesta = "Error" + ex.Message;
            }
            return respuesta;
        }

        public string AgregarDireccion(string calle, int numero, string colonia, int cp, string municipio, string ciudad, string estado)
        {
            string respuesta = "";
            Direcciones direccion = new Direcciones()
            {
                Calle = calle,
                Numero = numero,
                Colonia = colonia,
                CP = cp,
                Municipio = municipio,
                Ciudad = ciudad,
                Estado = estado,
            };

            try
            {
                using (RentaCarrosEntities db = new RentaCarrosEntities())
                {
                    db.Direcciones.Add(direccion);
                    db.SaveChanges();
                    respuesta = "Dirección Agregada con éxito";
                }
            }
            catch (Exception ex)
            {
                respuesta = "Error" + ex.Message;
            }
            return respuesta;
        }

        public string EliminarDireccion(int id)
        {
            string respuesta = "";

            try
            {
                using (RentaCarrosEntities db = new RentaCarrosEntities())
                {
                    Direcciones direc = (from d in db.Direcciones
                                         where d.DireccionId == id
                                         select d).FirstOrDefault();
                    db.Direcciones.Remove(direc);
                    db.SaveChanges();
                }

                return respuesta = "Dirección Eliminada con éxito";
            }
            catch (Exception ex)
            {
                return respuesta = "Error: " + ex.Message;
            }
        }

        public DireccionesVO GetDireccionById(int id)
        {
            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                return (from d in db.Direcciones
                        where d.DireccionId == id
                        select new DireccionesVO
                        {
                            DireccionId = d.DireccionId,
                            Calle = d.Calle,
                            Numero = d.Numero,
                            Colonia = d.Colonia,
                            CP = d.CP,
                            Municipio = d.Municipio,
                            Ciudad = d.Ciudad,
                            Estado = d.Estado,
                        }).SingleOrDefault();
            }
        }

        public List<DireccionesVO> GetListDirecciones()
        {
            List<DireccionesVO> listaVO = new List<DireccionesVO>();

            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                foreach (var direccion in db.Direcciones)
                {
                    DireccionesVO direccionVO = new DireccionesVO();

                    direccionVO.DireccionId = direccion.DireccionId;
                    direccionVO.Calle = direccion.Calle;
                    direccionVO.Numero = (int)direccion.Numero;
                    direccionVO.Colonia = direccion.Colonia;
                    direccionVO.CP = direccion.CP;
                    direccionVO.Municipio = direccion.Municipio;
                    direccionVO.Ciudad = direccion.Ciudad;
                    direccionVO.Estado = direccion.Estado;

                    listaVO.Add(direccionVO);
                }

                return listaVO;
            }
        }
    }
}
