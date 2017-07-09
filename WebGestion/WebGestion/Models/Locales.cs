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
    
    public partial class Locales
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Locales()
        {
            this.Empleados = new HashSet<Empleados>();
            this.Inventario = new HashSet<Inventario>();
        }
    
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public int OrdenDelLocal { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string CP { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public string Pais { get; set; }
        public Nullable<int> LocalidadId { get; set; }
        public string TelefonoFijo { get; set; }
        public string TelefonoMovil { get; set; }
        public string TelefonoFAX { get; set; }
        public string Email { get; set; }
        public Nullable<int> IdiomaId { get; set; }
        public string Observaciones { get; set; }
        public int EstadoId { get; set; }
        public System.DateTime FechaAlta { get; set; }
        public Nullable<System.DateTime> FechaBaja { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Empleados> Empleados { get; set; }
        public virtual Empresas Empresas { get; set; }
        public virtual Estados Estados { get; set; }
        public virtual Idiomas Idiomas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inventario> Inventario { get; set; }
        public virtual Localidades Localidades { get; set; }
    }
}