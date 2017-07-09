using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using WebGestion.Controllers;

namespace WebGestion.Models
{
    [MetadataType(typeof(PedidosProveedoresMetadata))]
    public partial class PedidosProveedores : IIdRecord
    {
    }

    public class PedidosProveedoresBase : CopyRecord, IIdRecord
    {
        public PedidosProveedoresBase() { }
        public PedidosProveedoresBase(PedidosProveedores source)
        {
            CopyFrom(source);
        }

        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Introduzca: CodPedido")]
        [Display(Name = "CodPedido", Prompt = "_nombre")]
        [MaxLength(20)]
        public string CodPedido { get; set; }

        [Display(Name = "Proveedor")]
        [Required(ErrorMessage = "Seleccione: Proveedor")]
        [AmbientValue(typeof(Proveedores), "Id")]
        [Description("Proveedor")]
        [DefaultValue(1)]
        public int ProveedorId { get; set; }

        [Required(ErrorMessage = "Seleccione: Estado")]
        [Display(Name = "Estado")]
        [AmbientValue(typeof(Estados), "Id")]
        [Description("Estado del usuario")]
        [DefaultValue(1)]
        public int EstadoId { get; set; }

        [Display(Name = "Fecha")]
        [DataType(DataType.DateTime)]
        public System.DateTime FechaAlta { get; set; }
    }

    public class PedidosProveedoresVM : PedidosProveedoresBase, IIdRecord, ICopyRecord
    {
        public PedidosProveedoresVM() { }
        public PedidosProveedoresVM(PedidosProveedores source) : base(source) { }

        [Required(ErrorMessage = "Seleccione: Factura de Proveedores")]
        [Display(Name = "Factura de Proveedores")]
        [AmbientValue(typeof(FacturasProveedores), "Id")]
        [Description("Factura de Proveedores")]
        [DefaultValue(1)]
        public IEnumerable<int> FacturasProveedoresIds { get; set; }

        public override ICopyRecord CopyTo<T>(DbContext context, T t, int deep = 1)
        {
            base.CopyTo(context, t);
            var source = t as PedidosProveedores;
            if (source != null)
            {
                var ctx = context as Models.GesemEntities;
                source.FacturasProveedores.Clear();
                if (FacturasProveedoresIds != null)
                    FacturasProveedoresIds.Each(id => source.FacturasProveedores.AddNotNull(ctx.FacturasProveedores.Find(id)));
            }
            return this;
        }

        public override ICopyRecord CopyFrom<T>(T t, int deep = 1)
        {
            base.CopyFrom(t);
            var source = t as PedidosProveedores;
            if (source != null)
            {
                FacturasProveedoresIds = source.FacturasProveedores.Select(v => v.Id);
            }
            return this;
        }
    }

    public partial class PedidosProveedoresMetadata : PedidosProveedoresBase
    {
        public PedidosProveedoresMetadata() { }
        public PedidosProveedoresMetadata(PedidosProveedores source) : base(source) { }

        [Display(Name = "Estados")]
        public Estados Estados { get; set; }

        [Display(Name = "Proveedores")]
        public Proveedores Proveedores { get; set; }

        [Display(Name = "PedidosProveedoresDetalle")]
        public PedidosProveedoresDetalle PedidosProveedoresDetalle { get; set; }

        [Display(Name = "Historicos")]
        public Historicos Historicos { get; set; }

        [Display(Name = "FacturasProveedores")]
        public FacturasProveedores FacturasProveedores { get; set; }

    }
}
