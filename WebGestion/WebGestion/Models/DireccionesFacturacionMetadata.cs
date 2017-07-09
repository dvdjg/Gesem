using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebGestion.Models
{
    [MetadataType(typeof(DireccionesFacturacionMetadata))]
    public partial class DireccionesFacturacion : IIdRecord
    {
    }

    public class DireccionesFacturacionBase : CopyRecord, IIdRecord
    {
        public DireccionesFacturacionBase() { }
        public DireccionesFacturacionBase(DireccionesFacturacion source)
        {
            CopyFrom(source);
        }

        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Seleccione:  Cliente")]
        [AmbientValue(typeof(Clientes), "Id")]
        [Description("Cliente de Facturación")]
        [DefaultValue(1)]
        public int ClienteId { get; set; }

        [Display(Name = "Activa")]
        public int Activa { get; set; }

        [Required(ErrorMessage = "Introduzca: Nombre")]
        [Display(Name = "Nombre")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Display(Name = "Apellidos")]
        [MaxLength(100)]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Introduzca: Direccion")]
        [Display(Name = "Direccion")]
        [MaxLength(200)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Introduzca: CP")]
        [Display(Name = "CP")]
        [DataType(DataType.PostalCode)]
        [MaxLength(20)]
        public string CP { get; set; }

        [Required(ErrorMessage = "Introduzca: Localidad")]
        [Display(Name = "Localidad")]
        [MaxLength(100)]
        public string Localidad { get; set; }

        [Required(ErrorMessage = "Introduzca: Provincia")]
        [Display(Name = "Provincia")]
        [MaxLength(100)]
        public string Provincia { get; set; }

        [Required(ErrorMessage = "Introduzca: Pais")]
        [Display(Name = "País")]
        [MaxLength(100)]
        public string Pais { get; set; }

        [Display(Name = "Localidad")]
        [Required(ErrorMessage = "Seleccione:  Localidad")]
        [AmbientValue(typeof(AspNetUsers), "Id")]
        [Description("Localidad de Facturación")]
        [DefaultValue(1)]
        public int LocalidadId { get; set; }

        [Required(ErrorMessage = "Seleccione: Idioma")]
        [Display(Name = "Idioma")]
        [AmbientValue(typeof(Idiomas), "Id")]
        [Description("Idioma")]
        [DefaultValue(1)]
        public int IdiomaId { get; set; }

        [Required(ErrorMessage = "Seleccione: Estado")]
        [Display(Name = "Estado")]
        [AmbientValue(typeof(Estados), "Id")]
        [Description("Estado del usuario")]
        [DefaultValue(1)]
        public int EstadoId { get; set; }

        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }

        [Display(Name = "Fecha de Alta")]
        [DataType(DataType.DateTime)]
        public DateTime FechaAlta { get; set; }

        [Display(Name = "Fecha de Baja")]
        [DataType(DataType.DateTime)]
        public DateTime? FechaBaja { get; set; }
    }

    public class DireccionesFacturacionVM : DireccionesFacturacionBase, IIdRecord, ICopyRecord
    {
        public DireccionesFacturacionVM() { }
        public DireccionesFacturacionVM(DireccionesFacturacion source) : base(source) { }
    }

    public partial class DireccionesFacturacionMetadata : DireccionesFacturacionBase
    {
        public DireccionesFacturacionMetadata() { }
        public DireccionesFacturacionMetadata(DireccionesFacturacion source) : base(source) { }

        [Display(Name = "Clientes")]
        public Clientes Clientes { get; set; }

        [Display(Name = "Estados")]
        public Estados Estados { get; set; }

        [Display(Name = "Localidades")]
        public Localidades Localidades { get; set; }

        public virtual Idiomas Idiomas { get; set; }
    }
}
