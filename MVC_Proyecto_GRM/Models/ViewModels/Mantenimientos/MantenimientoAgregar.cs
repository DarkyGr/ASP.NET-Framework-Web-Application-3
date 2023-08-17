using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MVC_Proyecto_GRM.Models.ViewModels.Mantenimientos
{
    public class MantenimientoAgregar
    {
        public int MantenimientoId { get; set; }

        [Required]  // Data validator
        [Display(Name = "ID Vehículo")]   // Manda el name de lo que falta
        public int VehiculoId { get; set; }

        [Required]  // Data validator
        [Display(Name = "Nota")]   // Manda el name de lo que falta
        public string Nota { get; set; }

        [Required]  // Data validator
        [Display(Name = "Fecha")]   // Manda el name de lo que falta
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
    }
}