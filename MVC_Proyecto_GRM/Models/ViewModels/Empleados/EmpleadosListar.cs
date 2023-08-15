using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Proyecto_GRM.Models.ViewModels.Empleados
{
    public class EmpleadosListar
    {
        public int EmpleadoId { get; set; }
        public int DireccionId { get; set; }
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public float Salario { get; set; }
        public string Puesto { get; set; }
    }
}