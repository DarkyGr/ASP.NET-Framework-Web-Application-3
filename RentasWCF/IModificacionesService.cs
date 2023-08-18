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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IModificacionesService" in both code and config file together.
    [ServiceContract]
    public interface IModificacionesService
    {
        [OperationContract]
        string AgregarMantenimiento(int vehiculoId, string nota, DateTime fecha);

        [OperationContract]
        string ActualizarMantenimiento(int id, int vehiculoId, string nota, DateTime fecha);

        [OperationContract]
        string EliminarMantenimiento(int id);

        [OperationContract]
        List<MantenimientosVO> GetListMantenimientos();

        [OperationContract]
        MantenimientosVO GetMantenimientoById(int id);
    }
}
