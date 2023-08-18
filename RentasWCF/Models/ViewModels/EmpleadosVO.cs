using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentasWCF.Models.ViewModels
{
    public class EmpleadosVO
    {
        public int EmpleadoId { get; set; }
        public int DireccionId { get; set; }
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public double Salario { get; set; }
        public string Puesto { get; set; }
    }
}