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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRentasService" in both code and config file together.
    [ServiceContract]
    public interface IRentasService
    {
        [OperationContract]
        string AgregarRenta(int vehiculoId, int clienteId, int empleadoId, float costo, DateTime fechaRenta, DateTime fechaRentaFin);

        [OperationContract]
        string ActualizarRenta(int id, int vehiculoId, int clienteId, int empleadoId, float costo, DateTime fechaRenta, DateTime fechaRentaFin);

        [OperationContract]
        string EliminarRenta(int id);

        [OperationContract]
        List<RentasVO> GetListRentas();

        [OperationContract]
        RentasVO GetRentaById(int id);
    }
}
