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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IVehiculosService" in both code and config file together.
    [ServiceContract]
    public interface IVehiculosService
    {
        [OperationContract]
        string AgregarVehiculo(string matricula, string marca, string modelo, int capacidad, int kilometraje);

        [OperationContract]
        string ActualizarVehiculo(int id, string matricula, string marca, string modelo, int capacidad, int kilometraje);

        [OperationContract]
        string EliminarVehiculo(int id);

        [OperationContract]
        List<VehiculosVO> GetListVehiculos();

        [OperationContract]
        VehiculosVO GetVehiculoById(int id);
    }
}
