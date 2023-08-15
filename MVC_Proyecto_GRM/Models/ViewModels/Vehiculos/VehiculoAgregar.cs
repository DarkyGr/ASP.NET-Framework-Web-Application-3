using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MVC_Proyecto_GRM.Models.ViewModels.Vehiculos
{
    public class VehiculoAgregar
    {
        public int VehiculoId { get; set; }

        [Required]  // Data validator
        [Display(Name = "Matrícula")]   // Manda el name de lo que falta
        public string Matricula { get; set; }

        [Required]  // Data validator
        [Display(Name = "Marca")]   // Manda el name de lo que falta
        public string Marca { get; set; }

        [Required]  // Data validator
        [Display(Name = "Modelo")]   // Manda el name de lo que falta
        public string Modelo { get; set; }

        [Required]  // Data validator
        [Display(Name = "Capacidad")]   // Manda el name de lo que falta
        public int Capacidad { get; set; }

        [Required]  // Data validator
        [Display(Name = "Kilometraje")]   // Manda el name de lo que falta
        public float Kilometraje { get; set; }
    }
}