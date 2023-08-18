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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IEmpleadosService" in both code and config file together.
    [ServiceContract]
    public interface IEmpleadosService
    {
        [OperationContract]
        string AgregarEmpleado(int direccionId, string nombre, string apellidoP, string apellidoM, float salario, string puesto);

        [OperationContract]
        string ActualizarEmpleado(int id, int direccionId, string nombre, string apellidoP, string apellidoM, float salario, string puesto);

        [OperationContract]
        string EliminarEmpleado(int id);

        [OperationContract]
        List<EmpleadosVO> GetListEmpleados();

        [OperationContract]
        EmpleadosVO GetEmpleadoById(int id);
    }
}
