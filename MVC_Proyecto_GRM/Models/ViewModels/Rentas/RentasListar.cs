using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Proyecto_GRM.Models.ViewModels.Rentas
{
    public class RentasListar
    {
        public int RentaId { get; set; }
        public int VehiculoId { get; set; }
        public int ClienteId { get; set; }
        public int EmpleadoId { get; set; }
        public float Costo { get; set; }
        public DateTime FechaRenta { get; set; }
        public DateTime FechaRentaFin { get; set; }
    }
}