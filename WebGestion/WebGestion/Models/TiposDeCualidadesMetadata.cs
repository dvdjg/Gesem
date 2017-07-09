using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebGestion.Utils;

namespace WebGestion.Models
{
    public class TiposDeCualidadesBase : CopyRecord, IRecordParent, IRecord
    {
        [Key]
        [ReadOnly(true)]
        [DefaultValue(-1)]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Introduzca el nombre del Estado")]
        [Display(Name = "Estado")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Padre")]
        [AmbientValue(typeof(TiposDeCualidades), "Id")]
        [Description("Estado Padre")]
        [DefaultValue(1)]
        public int PadreId { get; set; }

        public TiposDeCualidadesBase() { PadreId = 1; }
        public TiposDeCualidadesBase(TiposDeCualidades source)
        {
            CopyFrom(source);
        }
    }

    public class TiposDeCualidadesVM : TiposDeCualidadesBase, ICopyRecord
    {
        public TiposDeCualidadesVM() :base()
        {

        }
        public TiposDeCualidadesVM(TiposDeCualidades source) :base(source)
        {
        }

    }

    [MetadataType(typeof(TiposDeCualidadesMetadata))]
    public partial class TiposDeCualidades : IIdRecord
    {
    }

    public partial class TiposDeCualidadesMetadata : TiposDeCualidadesBase
    {
        public TiposDeCualidadesMetadata(TiposDeCualidades source) :base(source)
        {
        }

        public virtual Cualidades Cualidades { get; set; }
    }
}
