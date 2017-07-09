using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebGestion.Models
{
    [MetadataType(typeof(TiposPropiedadesMetadata))]
    public partial class TiposPropiedades : IIdRecord
    {
    }

    public class TiposPropiedadesBase : CopyRecord, IIdRecord
    {
        public TiposPropiedadesBase() { }
        public TiposPropiedadesBase(TiposPropiedades source)
        {
            CopyFrom(source);
        }

        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Introduzca: Nombre")]
        [Display(Name = "Nombre")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(200)]
        public string Descripcion { get; set; }

        [Display(Name = "Padre")]
        [Required(ErrorMessage = "Seleccione: Padre")]
        [AmbientValue(typeof(TiposPropiedades), "Id")]
        [Description("Padre")]
        [DefaultValue(1)]
        public int PadreId { get; set; }
    }

    public class TiposPropiedadesVM : TiposPropiedadesBase, IIdRecord, ICopyRecord
    {
        public TiposPropiedadesVM() { }
        public TiposPropiedadesVM(TiposPropiedades source) : base(source) { }
    }

    public partial class TiposPropiedadesMetadata : TiposPropiedadesBase
    {
        public TiposPropiedadesMetadata() { }
        public TiposPropiedadesMetadata(TiposPropiedades source) : base(source) { }

        [Display(Name = "Historicos")]
        public Historicos Historicos { get; set; }

        [Display(Name = "TiposPropiedades1")]
        public TiposPropiedades TiposPropiedades1 { get; set; }

        [Display(Name = "TiposPropiedades2")]
        public TiposPropiedades TiposPropiedades2 { get; set; }

    }
}
