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
    
    public partial class PresupuestosClientesDetalleAgrupados
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PresupuestosClientesDetalleAgrupados()
        {
            this.PresupuestosClientesDetalle = new HashSet<PresupuestosClientesDetalle>();
        }
    
        public int Id { get; set; }
        public int PresupuestoId { get; set; }
        public string Descripcion { get; set; }
        public string Detalle { get; set; }
        public double Cantidad { get; set; }
    
        public virtual PresupuestosClientes PresupuestosClientes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PresupuestosClientesDetalle> PresupuestosClientesDetalle { get; set; }
    }
}
