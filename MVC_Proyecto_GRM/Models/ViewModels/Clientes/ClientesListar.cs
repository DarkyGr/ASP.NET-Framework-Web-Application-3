using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Proyecto_GRM.Models.ViewModels.Clientes
{
    public class ClientesListar
    {
        public int ClienteId { get; set; }
        public int DireccionId { get; set; }
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public string Telefono { get; set; }
        public int NumLicencia { get; set; }
        public DateTime FechaVencimientoLicencia { get; set; }
    }
}