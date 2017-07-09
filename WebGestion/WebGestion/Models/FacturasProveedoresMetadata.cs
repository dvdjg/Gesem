using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using WebGestion.Controllers;

namespace WebGestion.Models
{
    [MetadataType(typeof(FacturasProveedoresMetadata))]
    public partial class FacturasProveedores : IIdRecord
    {
    }

    public class FacturasProveedoresBase : CopyRecord, IIdRecord
    {
        public FacturasProveedoresBase() { }
        public FacturasProveedoresBase(FacturasProveedores source)
        {
            CopyFrom(source);
        }

        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Introduzca: TipoFacturaId")]
        [Display(Name = "TipoFacturaId")]
        public int TipoFacturaId { get; set; }

        [Required(ErrorMessage = "Introduzca: CodFactura")]
        [Display(Name = "CodFactura", Prompt = "_nombre")]
        [MaxLength(20)]
        public string CodFactura { get; set; }

        [Display(Name = "Forma de Pago")]
        [Required(ErrorMessage = "Seleccione:  Forma de Pago")]
        [AmbientValue(typeof(FormasPago), "Id")]
        [Description("Forma de Pago para la Factura")]
        [DefaultValue(1)]
        public int FormaPagoId { get; set; }

        [Display(Name = "Forma de Entrega")]
        [Required(ErrorMessage = "Seleccione: Forma de Entrega")]
        [AmbientValue(typeof(FormasEntrega), "Id")]
        [Description("Forma de Entrega para la Factura")]
        [DefaultValue(1)]
        public int FormaEntregaId { get; set; }

        [Required(ErrorMessage = "Seleccione: Estado")]
        [Display(Name = "Estado")]
        [AmbientValue(typeof(Estados), "Id")]
        [Description("Estado del usuario")]
        [DefaultValue(1)]
        public int EstadoId { get; set; }

        [Display(Name = "Fecha de Alta")]
        [DataType(DataType.DateTime)]
        public System.DateTime FechaAlta { get; set; }

    }

    public class FacturasProveedoresVM : FacturasProveedoresBase, IIdRecord, ICopyRecord
    {
        public FacturasProveedoresVM() { }
        public FacturasProveedoresVM(FacturasProveedores source) : base(source) { }

        [Required(ErrorMessage = "Seleccione: Pedidos de Proveedores")]
        [Display(Name = "Pedidos de Proveedores")]
        [AmbientValue(typeof(PedidosProveedores), "Id")]
        [Description("Pedidos de Proveedores")]
        [DefaultValue(1)]
        public IEnumerable<int> PedidosProveedoresIds { get; set; }

        public override ICopyRecord CopyTo<T>(DbContext context, T t, int deep = 1)
        {
            base.CopyTo(context, t);
            var source = t as FacturasProveedores;
            if (source != null)
            {
                var ctx = context as Models.GesemEntities;
                source.PedidosProveedores.Clear();
                if (PedidosProveedoresIds != null)
                    PedidosProveedoresIds.Each(id => source.PedidosProveedores.AddNotNull(ctx.PedidosProveedores.Find(id)));
            }
            return this;
        }

        public override ICopyRecord CopyFrom<T>(T t, int deep = 1)
        {
            base.CopyFrom(t);
            var source = t as FacturasProveedores;
            if (source != null)
            {
                PedidosProveedoresIds = source.PedidosProveedores.Select(v => v.Id);
            }
            return this;
        }
    }

    public partial class FacturasProveedoresMetadata : FacturasProveedoresBase
    {
        public FacturasProveedoresMetadata() { }
        public FacturasProveedoresMetadata(FacturasProveedores source) : base(source) { }

        [Display(Name = "Estados")]
        public Estados Estados { get; set; }

        [Display(Name = "FormasEntrega")]
        public FormasEntrega FormasEntrega { get; set; }

        [Display(Name = "FormasPago")]
        public FormasPago FormasPago { get; set; }

        [Display(Name = "TiposFacturas")]
        public TiposFacturas TiposFacturas { get; set; }

        [Display(Name = "Historicos")]
        public Historicos Historicos { get; set; }

        [Display(Name = "PedidosProveedores")]
        public PedidosProveedores PedidosProveedores { get; set; }

    }
}
