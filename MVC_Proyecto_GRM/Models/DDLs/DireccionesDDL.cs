using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Proyecto_GRM.Models.DDLs
{
    public class DireccionesDDL
    {
        public int DireccionId { get; set; }

        [Required]  // Data validator
        [Display(Name = "Dirección")]   // Manda el name de lo que falta
        public string Direccion { get; set; }
    }
}