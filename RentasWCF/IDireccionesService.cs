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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDireccionesService" in both code and config file together.
    [ServiceContract]
    public interface IDireccionesService
    {
        [OperationContract]
        string AgregarDireccion(string calle, int numero, string colonia, int cp, string municipio, string ciudad, string estado);

        [OperationContract]
        List<DireccionesVO> GetListDirecciones();

        [OperationContract]
        string EliminarDireccion(int id);

        [OperationContract]
        string ActualizarDireccion(int id, string calle, int numero, string colonia, int cp, string municipio, string ciudad, string estado);

        [OperationContract]
        DireccionesVO GetDireccionById(int id);
    }
}
