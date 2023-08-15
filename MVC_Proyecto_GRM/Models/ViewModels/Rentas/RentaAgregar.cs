using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MVC_Proyecto_GRM.Models.ViewModels.Rentas
{
    public class RentaAgregar
    {
        public int RentaId { get; set; }

        [Required]  // Data validator
        [Display(Name = "ID Vehiculo")]   // Manda el name de lo que falta
        public int VehiculoId { get; set; }

        [Required]  // Data validator
        [Display(Name = "ID Cliente")]   // Manda el name de lo que falta
        public int ClienteId { get; set; }

        [Required]  // Data validator
        [Display(Name = "ID Empleado")]   // Manda el name de lo que falta
        public int EmpleadoId { get; set; }

        [Required]  // Data validator
        [Display(Name = "Costo")]   // Manda el name de lo que falta
        public float Costo { get; set; }

        [Required]  // Data validator
        [Display(Name = "Fecha de Renta")]   // Manda el name de lo que falta
        public DateTime FechaRenta { get; set; }

        [Required]  // Data validator
        [Display(Name = "Fecha de Renta Fin")]   // Manda el name de lo que falta
        public DateTime FechaRentaFin { get; set; }
    }
}