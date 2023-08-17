using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MVC_Proyecto_GRM.Models.ViewModels.Clientes
{
    public class ClienteAgregar
    {
        public int ClienteId { get; set; }

        [Required]  // Data validator
        [Display(Name = "Dirección")]   // Manda el name de lo que falta
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
        [Display(Name = "Teléfono")]   // Manda el name de lo que falta
        public string Telefono { get; set; }

        [Required]  // Data validator
        [Display(Name = "Número de Licencia")]   // Manda el name de lo que falta
        public int NumLicencia { get; set; }

        [Required]  // Data validator
        [Display(Name = "Fecha de Vencimiento de la Licencia")]   // Manda el name de lo que falta
        [DataType(DataType.Date)]
        public DateTime FechaVencimientoLicencia { get; set; }
    }
}