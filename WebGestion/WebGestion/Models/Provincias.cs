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
    
    public partial class Provincias
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Provincias()
        {
            this.Localidades = new HashSet<Localidades>();
        }
    
        public int Id { get; set; }
        public int CodProvincia { get; set; }
        public int CodPais { get; set; }
        public int PaisId { get; set; }
        public int IdiomaId { get; set; }
        public string Nombre { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public bool Exacto { get; set; }
    
        public virtual Idiomas Idiomas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Localidades> Localidades { get; set; }
        public virtual Paises Paises { get; set; }
    }
}
