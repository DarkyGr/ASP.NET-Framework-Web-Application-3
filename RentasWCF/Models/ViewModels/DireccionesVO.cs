using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentasWCF.Models.ViewModels
{
    public class DireccionesVO
    {
        public int DireccionId { get; set; }
        public string Calle { get; set; }
        public Nullable<int> Numero { get; set; }
        public string Colonia { get; set; }
        public int CP { get; set; }
        public string Municipio { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
    }
}