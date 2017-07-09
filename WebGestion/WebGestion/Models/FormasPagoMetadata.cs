using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebGestion.Models
{
    [MetadataType(typeof(FormasPagoMetadata))]
    public partial class FormasPago : IIdRecord
    {
    }

    public class FormasPagoBase : CopyRecord, IIdRecord
    {
        public FormasPagoBase() { }
        public FormasPagoBase(FormasPago source)
        {
            CopyFrom(source);
        }

        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Introduzca: FormaPago")]
        [Display(Name = "FormaPago", Prompt = "_nombre")]
        public string FormaPago { get; set; }

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

    public class FormasPagoVM : FormasPagoBase, IIdRecord, ICopyRecord
    {
        public FormasPagoVM() { }
        public FormasPagoVM(FormasPago source) : base(source) { }
    }

    public partial class FormasPagoMetadata : FormasPagoBase
    {
        public FormasPagoMetadata() { }
        public FormasPagoMetadata(FormasPago source) : base(source) { }

        [Display(Name = "Estados")]
        public Estados Estados { get; set; }

        [Display(Name = "FacturasProveedores")]
        public FacturasProveedores FacturasProveedores { get; set; }

        [Display(Name = "PedidosClientes")]
        public PedidosClientes PedidosClientes { get; set; }

    }
}
