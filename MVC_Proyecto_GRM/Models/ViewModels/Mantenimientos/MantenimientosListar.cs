using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MVC_Proyecto_GRM.Models.ViewModels.Mantenimientos
{
    public class MantenimientosListar
    {
        public int MantenimientoId { get; set; }        
        public int VehiculoId { get; set; }        
        public string Nota { get; set; }        
        public DateTime Fecha { get; set; }
    }
}