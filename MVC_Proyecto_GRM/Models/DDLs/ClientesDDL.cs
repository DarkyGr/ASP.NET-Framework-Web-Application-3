using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MVC_Proyecto_GRM.Models.DDLs
{
    public class ClientesDDL
    {
        public int ClienteId { get; set; }

        [Required]  // Data validator
        [Display(Name = "Nombre del Cliente")]   // Manda el name de lo que falta
        public string Nombre { get; set; }
    }
}