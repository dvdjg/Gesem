using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebGestion.Models
{
    [MetadataType(typeof(PresupuestosClientesDetalleAgrupadosMetadata))]
    public partial class PresupuestosClientesDetalleAgrupados : IIdRecord
    {
    }

    public class PresupuestosClientesDetalleAgrupadosBase : CopyRecord, IIdRecord
    {
        public PresupuestosClientesDetalleAgrupadosBase() { }
        public PresupuestosClientesDetalleAgrupadosBase(PresupuestosClientesDetalleAgrupados source)
        {
            CopyFrom(source);
        }

        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Presupuesto")]
        [Required(ErrorMessage = "Seleccione: Presupuesto")]
        [AmbientValue(typeof(PresupuestosClientes), "Id")]
        [Description("Presupuesto de Cliente")]
        [DefaultValue(1)]
        public int PresupuestoId { get; set; }

        [Display(Name = "Descripción", Prompt = "_nombre")]
        public string Descripcion { get; set; }

        [Display(Name = "Detalle")]
        [MaxLength(100)]
        public string Detalle { get; set; }

        [Display(Name = "Cantidad")]
        public double Cantidad { get; set; }
    }

    public class PresupuestosClientesDetalleAgrupadosVM : PresupuestosClientesDetalleAgrupadosBase, IIdRecord, ICopyRecord
    {
        public PresupuestosClientesDetalleAgrupadosVM() { }
        public PresupuestosClientesDetalleAgrupadosVM(PresupuestosClientesDetalleAgrupados source) : base(source) { }
    }

    public partial class PresupuestosClientesDetalleAgrupadosMetadata : PresupuestosClientesDetalleAgrupadosBase
    {
        public PresupuestosClientesDetalleAgrupadosMetadata() { }
        public PresupuestosClientesDetalleAgrupadosMetadata(PresupuestosClientesDetalleAgrupados source) : base(source) { }

        [Display(Name = "PresupuestosClientes")]
        public PresupuestosClientes PresupuestosClientes { get; set; }

        [Display(Name = "PresupuestosClientesDetalle")]
        public PresupuestosClientesDetalle PresupuestosClientesDetalle { get; set; }

    }
}
