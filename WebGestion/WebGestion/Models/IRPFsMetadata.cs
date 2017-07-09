using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebGestion.Models
{
    [MetadataType(typeof(IRPFsMetadata))]
    public partial class IRPFs : IIdRecord
    {
    }

    public class IRPFsBase : CopyRecord, IIdRecord
    {
        public IRPFsBase() { }
        public IRPFsBase(IRPFs source)
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

        [Display(Name = "IRPF")]
        public double IRPF { get; set; }

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

    public class IRPFsVM : IRPFsBase, IIdRecord, ICopyRecord
    {
        public IRPFsVM() { }
        public IRPFsVM(IRPFs source) : base(source) { }
    }

    public partial class IRPFsMetadata : IRPFsBase
    {
        public IRPFsMetadata() { }
        public IRPFsMetadata(IRPFs source) : base(source) { }

        [Display(Name = "Estados")]
        public Estados Estados { get; set; }

        [Display(Name = "FacturasClientes")]
        public FacturasClientes FacturasClientes { get; set; }

    }
}
