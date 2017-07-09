//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebGestion.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Localidades
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Localidades()
        {
            this.DireccionesFacturacion = new HashSet<DireccionesFacturacion>();
            this.Locales = new HashSet<Locales>();
            this.Personas = new HashSet<Personas>();
        }
    
        public int Id { get; set; }
        public int CodLocalidad { get; set; }
        public int CodProvincia { get; set; }
        public int ProvinciaId { get; set; }
        public int IdiomaId { get; set; }
        public string Nombre { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public bool Exacto { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DireccionesFacturacion> DireccionesFacturacion { get; set; }
        public virtual Idiomas Idiomas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Locales> Locales { get; set; }
        public virtual Provincias Provincias { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Personas> Personas { get; set; }
    }
}
