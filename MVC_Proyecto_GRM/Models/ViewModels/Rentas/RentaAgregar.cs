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
        [Display(Name = "Vehiculo")]   // Manda el name de lo que falta
        public int VehiculoId { get; set; }

        [Required]  // Data validator
        [Display(Name = "Cliente")]   // Manda el name de lo que falta
        public int ClienteId { get; set; }

        [Required]  // Data validator
        [Display(Name = "Empleado")]   // Manda el name de lo que falta
        public int EmpleadoId { get; set; }

        [Required]  // Data validator
        [Display(Name = "Costo")]   // Manda el name de lo que falta
        public float Costo { get; set; }

        [Required]  // Data validator
        [Display(Name = "Fecha de Renta")]   // Manda el name de lo que falta
        [DataType(DataType.Date)]
        public DateTime FechaRenta { get; set; }

        [Required]  // Data validator
        [Display(Name = "Fecha de Renta Fin")]   // Manda el name de lo que falta
        [DataType(DataType.Date)]
        public DateTime FechaRentaFin { get; set; }
    }
}