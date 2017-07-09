using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebGestion.Models
{
    [MetadataType(typeof(IVAsMetadata))]
    public partial class IVAs : IIdRecord
    {
    }

    public class IVAsBase : CopyRecord, IIdRecord
    {
        public IVAsBase() { }
        public IVAsBase(IVAs source)
        {
            CopyFrom(source);
        }

        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Descripción", Prompt = "_nombre")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Introduzca: IVA")]
        [Display(Name = "IVA")]
        public double IVA { get; set; }

        [Required(ErrorMessage = "Seleccione: Estado")]
        [Display(Name = "Estado")]
        [AmbientValue(typeof(Estados), "Id")]
        [Description("Estado del usuario")]
        [DefaultValue(1)]
        public int EstadoId { get; set; }

        [Display(Name = "Fecha de Alta")]
        [DataType(DataType.DateTime)]
        public DateTime FechaAlta { get; set; }

        [Display(Name = "Fecha de Baja")]
        [DataType(DataType.DateTime)]
        public DateTime? FechaBaja { get; set; }
    }

    public class IVAsVM : IVAsBase, IIdRecord, ICopyRecord
    {
        public IVAsVM() { }
        public IVAsVM(IVAs source) : base(source) { }
    }

    public partial class IVAsMetadata : IVAsBase
    {
        public IVAsMetadata() { }
        public IVAsMetadata(IVAs source) : base(source) { }

        [Display(Name = "Bienes")]
        public Bienes Bienes { get; set; }

        [Display(Name = "Estados")]
        public Estados Estados { get; set; }

        [Display(Name = "PedidosProveedoresDetalle")]
        public PedidosProveedoresDetalle PedidosProveedoresDetalle { get; set; }

        [Display(Name = "PresupuestosClientesDetalle")]
        public PresupuestosClientesDetalle PresupuestosClientesDetalle { get; set; }

    }
}
