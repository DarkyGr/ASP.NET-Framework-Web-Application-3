using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MVC_Proyecto_GRM.Models.DDLs
{
    public class VehiculosDDL
    {
        public int VehiculoId { get; set; }

        [Required]  // Data validator
        [Display(Name = "Vehículo")]   // Manda el name de lo que falta
        public string Info { get; set; }
    }
}