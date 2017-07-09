using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using WebGestion.Controllers;

namespace WebGestion.Models
{
    [MetadataType(typeof(BienesMetadata))]
    public partial class Bienes : IIdRecord
    {
    }

    public class BienesBase : CopyRecord, IIdRecord
    {
        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Introduzca: CodBien")]
        [Display(Name = "CodBien", Prompt = "_nombre")]
        [MaxLength(16)]
        public string CodBien { get; set; }

        [Required(ErrorMessage = "Introduzca: Bien")]
        [Display(Name = "Nombre", Prompt = "_nombre")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(200)]
        public string Descripcion { get; set; }

        [Display(Name = "Familia")]
        [Required(ErrorMessage = "Introduzca: Familia")]
        [AmbientValue(typeof(Familias), "Id")]
        [Description("Familia del Bien")]
        [DefaultValue(1)]
        public int FamiliaId { get; set; }

        [Display(Name = "IVA")]
        [Required(ErrorMessage = "Introduzca: IVA")]
        [AmbientValue(typeof(IVAs), "Id")]
        [Description("IVA del Bien")]
        [DefaultValue(1)]
        public int IVAId { get; set; }

        [Display(Name = "Precio")]
        [Description("Precio del Bien")]
        [DefaultValue(1)]
        public double Precio { get; set; }

        [Display(Name = "Precio Base")]
        [Description("Precio Base del Bien")]
        [DefaultValue(0)]
        public double PrecioBase { get; set; }

        [Display(Name = "Descuento")]
        [Description("Descuento del Bien")]
        [DefaultValue(1)]
        public double Descuento { get; set; }
        
        [Required(ErrorMessage = "Seleccione: Estado")]
        [Display(Name = "Estado")]
        [AmbientValue(typeof(Estados), "Id")]
        [Description("Estado del usuario")]
        [DefaultValue(1)]
        public int EstadoId { get; set; }

        public BienesBase() { }

        public BienesBase(Bienes source)
        {
            CopyFrom(source);
        }
    }

    public class BienesVM : BienesBase, IIdRecord, ICopyRecord
    {
        public BienesVM() : base()
        {
        }
        public BienesVM(Bienes source) : base(source)
        {
        }

        [Display(Name = "Cualidades")]
        [AmbientValue(typeof(ApplicationGroups), "Id")]
        [Description("Cualidades")]
        public virtual IEnumerable<int> CualidadesIds { get; set; }

        public override ICopyRecord CopyTo<T>(DbContext context, T t, int deep = 1)
        {
            base.CopyTo(context, t);
            var source = t as Bienes;
            if (source != null)
            {
                var ctx = context as Models.GesemEntities;
                source.Cualidades.Clear();
                if (CualidadesIds != null)
                    CualidadesIds.Each(id => source.Cualidades.AddNotNull(ctx.Cualidades.Find(id)));
            }
            return this;
        }

        public override ICopyRecord CopyFrom<T>(T t, int deep = 1)
        {
            base.CopyFrom(t);
            var source = t as Bienes;
            if (source != null)
            {
                CualidadesIds = source.Cualidades.Select(v => v.Id);
            }
            return this;
        }
    }

    public partial class BienesMetadata : BienesBase
    {
        public BienesMetadata(Bienes source) : base(source)
        {
        }

        [Display(Name = "Estados")]
        public virtual Estados Estados { get; set; }
        public virtual Familias Familias { get; set; }
        public virtual IVAs IVAs { get; set; }
        public virtual ICollection<BienesCompuestos> BienesCompuestos { get; set; }
        public virtual ICollection<BienesCompuestos> BienesCompuestos1 { get; set; }
        public virtual ICollection<Inventario> Inventario { get; set; }
        public virtual ICollection<PedidosProveedoresDetalle> PedidosProveedoresDetalle { get; set; }
        public virtual ICollection<PresupuestosClientesDetalle> PresupuestosClientesDetalle { get; set; }
        public virtual ICollection<Cualidades> Cualidades { get; set; }
        public virtual ICollection<Historicos> Historicos { get; set; }
        public virtual ICollection<Recursos> Recursos { get; set; }

    }
}
