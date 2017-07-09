using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebGestion.Models
{
    [MetadataType(typeof(LocalesMetadata))]
    public partial class Locales : IIdRecord
    {
    }

    public class LocalesBase : CopyRecord, IIdRecord
    {
        public LocalesBase() { }
        public LocalesBase(Locales source)
        {
            CopyFrom(source);
        }

        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Seleccione: Empresa")]
        [Display(Name = "Empresa")]
        [AmbientValue(typeof(Empresas), "Id")]
        [Description("Empresa")]
        [DefaultValue(1)]
        public int EmpresaId { get; set; }

        [Required(ErrorMessage = "Introduzca: Orden Del Local")]
        [Display(Name = "Orden Del Local")]
        public int OrdenDelLocal { get; set; }

        [Required(ErrorMessage = "Introduzca: Nombre")]
        [Display(Name = "Nombre")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(200)]
        public string Direccion { get; set; }

        [Display(Name = "CP")]
        [DataType(DataType.PostalCode)]
        [MaxLength(20)]
        public string CP { get; set; }

        [Display(Name = "Localidad")]
        [MaxLength(100)]
        public string Localidad { get; set; }

        [Display(Name = "Provincia")]
        [MaxLength(100)]
        public string Provincia { get; set; }

        [Display(Name = "País")]
        [MaxLength(100)]
        public string Pais { get; set; }

        [Required(ErrorMessage = "Seleccione: Localidad")]
        [Display(Name = "Localidad")]
        [AmbientValue(typeof(Localidades), "Id")]
        [Description("Localidad")]
        [DefaultValue(1)]
        public int LocalidadId { get; set; }

        [Display(Name = "Teléfono Fijo")]
        [DataType(DataType.PhoneNumber)]
        public string TelefonoFijo { get; set; }

        [Display(Name = "Teléfono Móvil")]
        [DataType(DataType.PhoneNumber)]
        public string TelefonoMovil { get; set; }

        [Display(Name = "Teléfono FAX")]
        [DataType(DataType.PhoneNumber)]
        public string TelefonoFAX { get; set; }

        [Display(Name = "e-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }

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

    public class LocalesVM : LocalesBase, IIdRecord, ICopyRecord
    {
        public LocalesVM() { }
        public LocalesVM(Locales source) : base(source) { }
    }

    public partial class LocalesMetadata : LocalesBase
    {
        public LocalesMetadata() { }
        public LocalesMetadata(Locales source) : base(source) { }

        [Display(Name = "Empleados")]
        public Empleados Empleados { get; set; }

        [Display(Name = "Empresas")]
        public Empresas Empresas { get; set; }

        [Display(Name = "Estados")]
        public Estados Estados { get; set; }

        [Display(Name = "Localidades")]
        public Localidades Localidades { get; set; }

        [Display(Name = "Inventario")]
        public Inventario Inventario { get; set; }

    }
}
