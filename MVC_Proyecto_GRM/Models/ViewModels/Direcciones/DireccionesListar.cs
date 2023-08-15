using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Proyecto_GRM.Models.ViewModels.Direcciones
{
    public class DireccionesListar
    {
        public int DireccionId { get; set; }
        public string Calle { get; set; }
        public int Numero { get; set; }
        public string Colonia { get; set; }
        public int CP { get; set; }
        public string Municipio { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
    }
}