using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebGestion.Models
{
    [MetadataType(typeof(PresupuestosClientesDetalleMetadata))]
    public partial class PresupuestosClientesDetalle : IIdRecord
    {
    }

    public class PresupuestosClientesDetalleBase : CopyRecord, IIdRecord
    {
        public PresupuestosClientesDetalleBase() { }
        public PresupuestosClientesDetalleBase(PresupuestosClientesDetalle source)
        {
            CopyFrom(source);
        }

        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Agrupado")]
        [Required(ErrorMessage = "Seleccione: Agrupado")]
        [AmbientValue(typeof(PresupuestosClientesDetalleAgrupados), "Id")]
        [Description("Agrupado")]
        [DefaultValue(1)]
        public int AgrupadoId { get; set; }

        [Display(Name = "Bien")]
        [Required(ErrorMessage = "Seleccione: Bien")]
        [AmbientValue(typeof(Bienes), "Id")]
        [Description("Bien")]
        [DefaultValue(1)]
        public int BienId { get; set; }

        [Required(ErrorMessage = "Introduzca: Cantidad")]
        [Display(Name = "Cantidad")]
        public double Cantidad { get; set; }

        [Display(Name = "IVA")]
        [Required(ErrorMessage = "Seleccione: IVA")]
        [AmbientValue(typeof(IVAs), "Id")]
        [Description("IVA")]
        [DefaultValue(1)]
        public int IVAId { get; set; }

        [Display(Name = "Precio")]
        public double Precio { get; set; }
    }

    public class PresupuestosClientesDetalleVM : PresupuestosClientesDetalleBase, IIdRecord, ICopyRecord
    {
        public PresupuestosClientesDetalleVM() { }
        public PresupuestosClientesDetalleVM(PresupuestosClientesDetalle source) : base(source) { }
    }

    public partial class PresupuestosClientesDetalleMetadata : PresupuestosClientesDetalleBase
    {
        public PresupuestosClientesDetalleMetadata() { }
        public PresupuestosClientesDetalleMetadata(PresupuestosClientesDetalle source) : base(source) { }

        [Display(Name = "Bienes")]
        public Bienes Bienes { get; set; }

        [Display(Name = "IVAs")]
        public IVAs IVAs { get; set; }

        [Display(Name = "PresupuestosClientesDetalleAgrupados")]
        public PresupuestosClientesDetalleAgrupados PresupuestosClientesDetalleAgrupados { get; set; }

    }
}
