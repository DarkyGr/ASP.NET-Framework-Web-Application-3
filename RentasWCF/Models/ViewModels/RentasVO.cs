using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentasWCF.Models.ViewModels
{
    public class RentasVO
    {
        public int RentaId { get; set; }
        public int VehiculoId { get; set; }
        public int ClienteId { get; set; }
        public int EmpleadoId { get; set; }
        public double Costo { get; set; }
        public System.DateTime FechaRenta { get; set; }
        public System.DateTime FechaRentaFin { get; set; }
    }
}