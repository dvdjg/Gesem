using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebGestion.Models
{
    [MetadataType(typeof(ProveedoresMetadata))]
    public partial class Proveedores : IIdRecord
    {
    }

    public class ProveedoresBase : CopyRecord, IIdRecord
    {
        public ProveedoresBase() { }
        public ProveedoresBase(Proveedores source)
        {
            CopyFrom(source);
        }

        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Seleccione: Persona")]
        [Display(Name = "Persona")]
        [AmbientValue(typeof(Personas), "Id")]
        [Description("Persona")]
        [DefaultValue(1)]
        public int PersonaId { get; set; }

        [Display(Name = "Teléfono FAX")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(100)]
        public string TelefonoFAX { get; set; }

        [Display(Name = "CuentaBancaria")]
        [MaxLength(200)]
        public string CuentaBancaria { get; set; }

        [Display(Name = "Contacto")]
        [MaxLength(200)]
        public string Contacto { get; set; }

        [Display(Name = "Web")]
        [MaxLength(100)]
        public string Web { get; set; }

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

    public class ProveedoresVM : ProveedoresBase, IIdRecord, ICopyRecord
    {
        public ProveedoresVM() { }
        public ProveedoresVM(Proveedores source) : base(source) { }
    }

    public partial class ProveedoresMetadata : ProveedoresBase
    {
        public ProveedoresMetadata() { }
        public ProveedoresMetadata(Proveedores source) : base(source) { }

        [Display(Name = "Estados")]
        public Estados Estados { get; set; }

        [Display(Name = "PedidosProveedores")]
        public PedidosProveedores PedidosProveedores { get; set; }

        [Display(Name = "Personas")]
        public Personas Personas { get; set; }

    }
}
