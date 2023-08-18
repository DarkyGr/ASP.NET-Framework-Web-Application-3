using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentasWCF.Models.ViewModels
{
    public class ClientesVO
    {
        public int ClienteId { get; set; }
        public int DireccionId { get; set; }
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public string Telefono { get; set; }
        public int NumLicencia { get; set; }
        public Nullable<System.DateTime> FechaVencimientoLicencia { get; set; }
    }
}