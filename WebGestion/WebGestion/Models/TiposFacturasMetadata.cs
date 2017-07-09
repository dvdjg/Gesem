using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebGestion.Models
{
    [MetadataType(typeof(TiposFacturasMetadata))]
    public partial class TiposFacturas : IIdRecord
    {
    }

    public class TiposFacturasBase : CopyRecord, IIdRecord
    {
        public TiposFacturasBase() { }
        public TiposFacturasBase(TiposFacturas source)
        {
            CopyFrom(source);
        }

        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Introduzca: Prefijo")]
        [Display(Name = "Prefijo", Prompt = "_nombre")]
        [MaxLength(4)]
        public string Prefijo { get; set; }

        [Display(Name = "Descripción", Prompt = "_nombre")]
        [MaxLength(200)]
        public string Descripcion { get; set; }
    }

    public class TiposFacturasVM : TiposFacturasBase, IIdRecord, ICopyRecord
    {
        public TiposFacturasVM() { }
        public TiposFacturasVM(TiposFacturas source) : base(source) { }
    }

    public partial class TiposFacturasMetadata : TiposFacturasBase
    {
        public TiposFacturasMetadata() { }
        public TiposFacturasMetadata(TiposFacturas source) : base(source) { }

        [Display(Name = "FacturasClientes")]
        public FacturasClientes FacturasClientes { get; set; }

        [Display(Name = "FacturasProveedores")]
        public FacturasProveedores FacturasProveedores { get; set; }

    }
}
