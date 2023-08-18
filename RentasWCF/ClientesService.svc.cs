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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ClientesService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ClientesService.svc or ClientesService.svc.cs at the Solution Explorer and start debugging.
    public class ClientesService : IClientesService
    {
        public string ActualizarCliente(int id, int direccionId, string nombre, string apellidoP, string apellidoM, string telefono, int numLicencia, DateTime fechaVencimientoLicencia)
        {
            string respuesta = "";
            Clientes cliente = new Clientes()
            {
                ClienteId = id,
                DireccionId = direccionId,
                Nombre = nombre,
                ApellidoP = apellidoP,
                ApellidoM = apellidoM,
                Telefono = telefono,
                NumLicencia = numLicencia,
                FechaVencimientoLicencia = fechaVencimientoLicencia,
            };

            try
            {
                using (RentaCarrosEntities db = new RentaCarrosEntities())
                {
                    db.Entry(cliente).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    respuesta = "Cliente Actualizado con éxito";
                }
            }
            catch (Exception ex)
            {
                respuesta = "Error" + ex.Message;
            }
            return respuesta;
        }

        public string AgregarCliente(int direccionId, string nombre, string apellidoP, string apellidoM, string telefono, int numLicencia, DateTime fechaVencimientoLicencia)
        {
            string respuesta = "";
            Clientes cliente = new Clientes()
            {
                DireccionId = direccionId,
                Nombre = nombre,
                ApellidoP = apellidoP,
                ApellidoM = apellidoM,
                Telefono = telefono,
                NumLicencia = numLicencia,
                FechaVencimientoLicencia = fechaVencimientoLicencia,
            };

            try
            {
                using (RentaCarrosEntities db = new RentaCarrosEntities())
                {
                    db.Clientes.Add(cliente);
                    db.SaveChanges();
                    respuesta = "Cliente Agregado con éxito";
                }
            }
            catch (Exception ex)
            {
                respuesta = "Error" + ex.Message;
            }
            return respuesta;
        }

        public string EliminarCliente(int id)
        {
            string respuesta = "";

            try
            {
                using (RentaCarrosEntities db = new RentaCarrosEntities())
                {
                    Clientes cli = (from c in db.Clientes
                                    where c.ClienteId == id
                                    select c).FirstOrDefault();
                    db.Clientes.Remove(cli);
                    db.SaveChanges();
                }

                return respuesta = "Cliente Eliminado con éxito";
            }
            catch (Exception ex)
            {
                return respuesta = "Error: " + ex.Message;
            }
        }

        public ClientesVO GetClienteById(int id)
        {
            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                return (from c in db.Clientes
                        where c.ClienteId == id
                        select new ClientesVO
                        {
                            ClienteId = c.ClienteId,
                            DireccionId = c.DireccionId,
                            Nombre = c.Nombre,
                            ApellidoP = c.ApellidoP,
                            ApellidoM = c.ApellidoM,
                            Telefono = c.Telefono,
                            NumLicencia = c.NumLicencia,
                            FechaVencimientoLicencia = c.FechaVencimientoLicencia,
                        }).SingleOrDefault();
            }
        }

        public List<ClientesVO> GetListClientes()
        {
            List<ClientesVO> listaVO = new List<ClientesVO>();

            using (RentaCarrosEntities db = new RentaCarrosEntities())
            {
                foreach (var cliente in db.Clientes)
                {
                    ClientesVO clientesVO = new ClientesVO();

                    clientesVO.ClienteId = cliente.ClienteId;
                    clientesVO.DireccionId = cliente.DireccionId;
                    clientesVO.Nombre = cliente.Nombre;
                    clientesVO.ApellidoP = cliente.ApellidoP;
                    clientesVO.ApellidoM = cliente.ApellidoM;
                    clientesVO.Telefono = cliente.Telefono;
                    clientesVO.NumLicencia = cliente.NumLicencia;
                    clientesVO.FechaVencimientoLicencia = (DateTime)cliente.FechaVencimientoLicencia;

                    listaVO.Add(clientesVO);
                }

                return listaVO;
            }
        }
    }
}
