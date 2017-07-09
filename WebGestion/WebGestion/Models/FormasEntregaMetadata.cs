using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebGestion.Models
{
    [MetadataType(typeof(FormasEntregaMetadata))]
    public partial class FormasEntrega : IIdRecord
    {
    }

    public class FormasEntregaBase : CopyRecord, IIdRecord
    {
        public FormasEntregaBase() { }
        public FormasEntregaBase(FormasEntrega source)
        {
            CopyFrom(source);
        }

        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Introduzca: FormaEntrega")]
        [Display(Name = "FormaEntrega", Prompt = "_nombre")]
        public string FormaEntrega { get; set; }

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

    public class FormasEntregaVM : FormasEntregaBase, IIdRecord, ICopyRecord
    {
        public FormasEntregaVM() { }
        public FormasEntregaVM(FormasEntrega source) : base(source) { }
    }

    public partial class FormasEntregaMetadata : FormasEntregaBase
    {
        public FormasEntregaMetadata() { }
        public FormasEntregaMetadata(FormasEntrega source) : base(source) { }

        [Display(Name = "Estados")]
        public Estados Estados { get; set; }

        [Display(Name = "FacturasProveedores")]
        public FacturasProveedores FacturasProveedores { get; set; }

        [Display(Name = "PedidosClientes")]
        public PedidosClientes PedidosClientes { get; set; }

    }
}
