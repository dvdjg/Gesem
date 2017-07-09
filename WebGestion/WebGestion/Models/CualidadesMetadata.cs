using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using WebGestion.Controllers;

namespace WebGestion.Models
{
    [MetadataType(typeof(CualidadesMetadata))]
    public partial class Cualidades : IIdRecord
    {
    }

    public class CualidadesBase : CopyRecord, IIdRecord
    {
        public CualidadesBase() { }
        public CualidadesBase(Cualidades source)
        {
            CopyFrom(source);
        }

        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Seleccione: Tipo de Cualidad")]
        [Display(Name = "Tipo de Cualidad")]
        [AmbientValue(typeof(Estados), "Id")]
        [Description("Tipo de Cualidad")]
        [DefaultValue(1)]
        public int TiposDeCualidadesId { get; set; }

        public double? Cantidad { get; set; }
        
        [Display(Name = "Descripción", Prompt = "_nombre")]
        [MaxLength(200)]
        public string Descripcion { get; set; }
    }

    public class CualidadesVM : CualidadesBase, IIdRecord, ICopyRecord
    {
        public CualidadesVM() { }
        public CualidadesVM(Cualidades source) : base(source) { }

        [Display(Name = "Recursos")]
        [AmbientValue(typeof(Recursos), "Id")]
        [Description("Miembro de Recursos")]
        [Order]
        public virtual IEnumerable<int> BienesIds { get; set; }

        public override ICopyRecord CopyTo<T>(DbContext context, T t, int deep = 1)
        {
            base.CopyTo(context, t);
            var source = t as Cualidades;
            if (source != null)
            {
                var ctx = context as Models.GesemEntities;
                source.Bienes.Clear();
                if (BienesIds != null)
                    BienesIds.Each(id => source.Bienes.AddNotNull(ctx.Bienes.Find(id)));
            }
            return this;
        }

        public override ICopyRecord CopyFrom<T>(T t, int deep = 1)
        {
            base.CopyFrom(t);
            var source = t as Cualidades;
            if (source != null)
            {
                BienesIds = source.Bienes.Select(v => v.Id);
            }
            return this;
        }
    }

    public partial class CualidadesMetadata : CualidadesBase
    {
        public CualidadesMetadata() { }
        public CualidadesMetadata(Cualidades source) : base(source) { }

        public virtual TiposDeCualidades TiposDeCualidades { get; set; }
        public virtual Bienes Bienes { get; set; }


    }
}
