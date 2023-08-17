using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MVC_Proyecto_GRM.Models.DDLs
{
    public class EmpleadosDDL
    {
        public int EmpleadoId { get; set; }

        [Required]  // Data validator
        [Display(Name = "Nombre del Empleado")]   // Manda el name de lo que falta
        public string Nombre { get; set; }
    }
}