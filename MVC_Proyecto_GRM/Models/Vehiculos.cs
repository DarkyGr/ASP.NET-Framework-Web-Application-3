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
    using System.ComponentModel.DataAnnotations;

    public partial class Vehiculos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vehiculos()
        {
            this.Mantenimientos = new HashSet<Mantenimientos>();
            this.Rentas = new HashSet<Rentas>();
        }
    
        public int VehiculoId { get; set; }

        [Required]  // Data validator
        [Display(Name = "Matrícula")]   // Manda el name de lo que falta
        public string Matricula { get; set; }

        [Required]  // Data validator
        [Display(Name = "Marca")]   // Manda el name de lo que falta
        public string Marca { get; set; }

        [Required]  // Data validator
        [Display(Name = "Modelo")]   // Manda el name de lo que falta
        public string Modelo { get; set; }

        [Required]  // Data validator
        [Display(Name = "Capacidad")]   // Manda el name de lo que falta
        public int Capacidad { get; set; }

        [Required]  // Data validator
        [Display(Name = "Kilometraje")]   // Manda el name de lo que falta
        public double Kilometraje { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mantenimientos> Mantenimientos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rentas> Rentas { get; set; }
    }
}
