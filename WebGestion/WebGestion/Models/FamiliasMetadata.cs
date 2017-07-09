using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebGestion.Models
{
    [MetadataType(typeof(FamiliasMetadata))]
    public partial class Familias : IIdRecord
    {
    }

    public class FamiliasBase : CopyRecord, IIdRecord
    {
        public FamiliasBase() { }
        public FamiliasBase(Familias source)
        {
            CopyFrom(source);
        }

        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Introduzca: CodFamilia")]
        [Display(Name = "CodFamilia", Prompt = "_nombre")]
        [MaxLength(20)]
        public string CodFamilia { get; set; }

        [Required(ErrorMessage = "Introduzca: Familia")]
        [Display(Name = "Nombre", Prompt = "_nombre")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(200)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Seleccione: Estado")]
        [Display(Name = "Estado")]
        [AmbientValue(typeof(Estados), "Id")]
        [Description("Estado del usuario")]
        [DefaultValue(1)]
        public int EstadoId { get; set; }

        [Display(Name = "Padre")]
        [Required(ErrorMessage = "Seleccione: Padre")]
        [AmbientValue(typeof(Familias), "Id")]
        [Description("Padre")]
        [DefaultValue(1)]
        public int PadreId { get; set; }
    }

    public class FamiliasVM : FamiliasBase, IIdRecord, ICopyRecord
    {
        public FamiliasVM() { }
        public FamiliasVM(Familias source) : base(source) { }
    }

    public partial class FamiliasMetadata : FamiliasBase
    {
        public FamiliasMetadata() { }
        public FamiliasMetadata(Familias source) : base(source) { }

        [Display(Name = "Bienes")]
        public Bienes Bienes { get; set; }

        [Display(Name = "Estados")]
        public Estados Estados { get; set; }

        [Display(Name = "Familias1")]
        public Familias Familias1 { get; set; }

        [Display(Name = "Familias2")]
        public Familias Familias2 { get; set; }

        public virtual Historicos Historicos { get; set; }
    }
}
