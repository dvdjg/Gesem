using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebGestion.Models
{
    [MetadataType(typeof(CambiosLogMetadata))]
    public partial class CambiosLog : IIdRecord
    {
    }

    public class CambiosLogBase : CopyRecord, IIdRecord
    {
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        [Order]
        public int Id { get; set; }

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Seleccione:  Usuario")]
        [AmbientValue(typeof(AspNetUsers), "Id")]
        [Description("Usuario")]
        [DefaultValue(1)]
        [Order]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Introduzca: Query")]
        [Display(Name = "Query", Prompt = "_nombre")]
        [Order]
        public string Query { get; set; }

        [Display(Name = "Fecha")]
        [DataType(DataType.DateTime)]
        [Order]
        public DateTime Fecha { get; set; }

        public CambiosLogBase() { }
        public CambiosLogBase(CambiosLog source)
        {
            CopyFrom(source);
        }
    }

    public class CambiosLogVM : CambiosLogBase, IIdRecord, ICopyRecord
    {
        public CambiosLogVM() : base()
        {
        }
        public CambiosLogVM(CambiosLog source) : base(source)
        {
        }
    }

    public partial class CambiosLogMetadata : CambiosLogBase
    {
        public CambiosLogMetadata() { }
        public CambiosLogMetadata(CambiosLog source) : base(source) { }

        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}
