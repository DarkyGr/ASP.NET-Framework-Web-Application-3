//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC_Proyecto_GRM.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class View_Clientes
    {
        public int ClienteId { get; set; }
        public int DireccionId { get; set; }
        public string Dirección { get; set; }
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public string Telefono { get; set; }
        public int NumLicencia { get; set; }
        public Nullable<System.DateTime> FechaVencimientoLicencia { get; set; }
    }
}
