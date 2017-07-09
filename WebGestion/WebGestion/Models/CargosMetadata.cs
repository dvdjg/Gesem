using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebGestion.Models
{
    [MetadataType(typeof(CargosMetadata))]
    public partial class Cargos : IIdRecord
    {
    }

    public class CargosBase : CopyRecord, IIdRecord
    {
        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Introduzca el nombre")]
        [Display(Name = "Cargo")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

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

        public CargosBase() { }
        public CargosBase(Cargos source)
        {
            CopyFrom(source);
        }
    }

    public class CargosVM : CargosBase, IIdRecord, ICopyRecord
    {
        public CargosVM() { }
        public CargosVM(Cargos source) : base(source) { }
    }

    public partial class CargosMetadata : CargosBase
    {
        public CargosMetadata() { }
        public CargosMetadata(Cargos source) : base(source) { }

        [Display(Name = "Estados")]
        public Estados Estados { get; set; }

        [Display(Name = "Empleados")]
        public ICollection<Empleados> Empleados { get; set; }
        
    }
}
