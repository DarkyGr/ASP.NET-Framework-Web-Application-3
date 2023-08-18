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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IClientesService" in both code and config file together.
    [ServiceContract]
    public interface IClientesService
    {
        [OperationContract]
        string AgregarCliente(int direccionId, string nombre, string apellidoP, string apellidoM, string telefono, int numLicencia, DateTime fechaVencimientoLicencia);

        [OperationContract]
        string ActualizarCliente(int id, int direccionId, string nombre, string apellidoP, string apellidoM, string telefono, int numLicencia, DateTime fechaVencimientoLicencia);

        [OperationContract]
        string EliminarCliente(int id);

        [OperationContract]
        List<ClientesVO> GetListClientes();

        [OperationContract]
        ClientesVO GetClienteById(int id);
    }
}
