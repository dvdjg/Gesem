using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using WebGestion.Controllers;

namespace WebGestion.Models
{
    [MetadataType(typeof(EmpresasMetadata))]
    public partial class Empresas : IIdRecord
    {
    }

    public class EmpresasBase : CopyRecord, IIdRecord
    {
        public EmpresasBase() { }
        public EmpresasBase(Empresas source)
        {
            CopyFrom(source);
        }

        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        [Order]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(100)]
        [Order]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Introduzca: NIF")]
        [Display(Name = "NIF")]
        [MaxLength(20)]
        [Order]
        public string NIF { get; set; }

        [Display(Name = "Cuenta Bancaria")]
        [MaxLength(100)]
        [Order]
        public string CuentaBancaria { get; set; }

        [Display(Name = "Observaciones")]
        [MaxLength(200)]
        [Order]
        public string Observaciones { get; set; }

        [Required(ErrorMessage = "Seleccione: Estado")]
        [Display(Name = "Estado")]
        [AmbientValue(typeof(Estados), "Id")]
        [Description("Estado del usuario")]
        [DefaultValue(1)]
        [Order]
        public int EstadoId { get; set; }

        [Display(Name = "Fecha de Alta")]
        [DataType(DataType.DateTime)]
        [Order]
        public DateTime FechaAlta { get; set; }

        [Display(Name = "Fecha de Baja")]
        [DataType(DataType.DateTime)]
        [Order]
        public DateTime? FechaBaja { get; set; }
    }

    public class EmpresasVM : EmpresasBase, IIdRecord, ICopyRecord
    {
        public EmpresasVM() { }
        public EmpresasVM(Empresas source) : base(source) { }

        [Display(Name = "Locales")]
        [AmbientValue(typeof(Locales), "Id")]
        [Description("Locales")]
        [Order]
        public virtual IEnumerable<int> LocalesId { get; set; }

        public override ICopyRecord CopyTo<T>(DbContext context, T t, int deep = 1)
        {
            base.CopyTo(context, t);
            var source = t as Empresas;
            if (source != null)
            {
                var ctx = context as Models.GesemEntities;
                source.Locales.Clear();
                if (LocalesId != null)
                    LocalesId.Each(id => source.Locales.AddNotNull(ctx.Locales.Find(id)));
            }
            return this;
        }

        public override ICopyRecord CopyFrom<T>(T t, int deep = 1)
        {
            base.CopyFrom(t);
            var source = t as Empresas;
            if (source != null)
            {
                LocalesId = source.Locales.Select(v => v.Id).ToList();
            }
            return this;
        }
    }

    public partial class EmpresasMetadata : EmpresasBase
    {
        public EmpresasMetadata() { }
        public EmpresasMetadata(Empresas source) : base(source) { }
    }
}
