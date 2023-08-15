using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MVC_Proyecto_GRM.Models.ViewModels.Direcciones
{
    public class DireccionAgregar
    {   
        public int DireccionId { get; set; }

        [Required]  // Data validator
        [Display(Name = "Calle")]   // Manda el name de lo que falta
        public string Calle { get; set; }

        [Required]  // Data validator
        [Display(Name = "Número")]   // Manda el name de lo que falta
        public int Numero { get; set; }

        [Required]  // Data validator
        [Display(Name = "Colonia")]   // Manda el name de lo que falta
        public string Colonia { get; set; }

        [Required]  // Data validator
        [Display(Name = "CP")]   // Manda el name de lo que falta
        public int CP { get; set; }

        [Required]  // Data validator
        [Display(Name = "Municipio")]   // Manda el name de lo que falta
        public string Municipio { get; set; }

        [Required]  // Data validator
        [Display(Name = "Ciudad")]   // Manda el name de lo que falta
        public string Ciudad { get; set; }

        [Required]  // Data validator
        [Display(Name = "Estado")]   // Manda el name de lo que falta
        public string Estado { get; set; }
    }
}