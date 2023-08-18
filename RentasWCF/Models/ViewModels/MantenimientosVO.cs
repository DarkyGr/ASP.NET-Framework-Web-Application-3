using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentasWCF.Models.ViewModels
{
    public class MantenimientosVO
    {
        public int MantenimientoId { get; set; }
        public int VehiculoId { get; set; }
        public string Nota { get; set; }
        public System.DateTime Fecha { get; set; }
    }
}