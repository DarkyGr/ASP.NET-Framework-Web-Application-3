using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentasWCF.Models.ViewModels
{
    public class VehiculosVO
    {
        public int VehiculoId { get; set; }
        public string Matricula { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Capacidad { get; set; }
        public double Kilometraje { get; set; }
    }
}