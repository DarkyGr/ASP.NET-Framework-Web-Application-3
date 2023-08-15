using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MVC_Proyecto_GRM.Models.ViewModels.Empleados
{
    public class EmpleadoAgregar
    {
        public int EmpleadoId { get; set; }

        [Required]  // Data validator
        [Display(Name = "ID Dirección")]   // Manda el name de lo que falta
        public int DireccionId { get; set; }

        [Required]  // Data validator
        [Display(Name = "Nombre")]   // Manda el name de lo que falta
        public string Nombre { get; set; }

        [Required]  // Data validator
        [Display(Name = "Apellido Paterno")]   // Manda el name de lo que falta
        public string ApellidoP { get; set; }

        [Required]  // Data validator
        [Display(Name = "Apellido Materno")]   // Manda el name de lo que falta
        public string ApellidoM { get; set; }

        [Required]  // Data validator
        [Display(Name = "Salario")]   // Manda el name de lo que falta
        public float Salario { get; set; }

        [Required]  // Data validator
        [Display(Name = "Puesto")]   // Manda el name de lo que falta
        public string Puesto { get; set; }
    }
}