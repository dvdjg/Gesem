using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using WebGestion.Controllers;

namespace WebGestion.Models
{
    [MetadataType(typeof(PedidosClientesMetadata))]
    public partial class PedidosClientes : IIdRecord
    {
    }

    public class PedidosClientesBase : CopyRecord, IIdRecord
    {
        public PedidosClientesBase() { }
        public PedidosClientesBase(PedidosClientes source)
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

        [Required(ErrorMessage = "Seleccione: Presupuesto")]
        [Display(Name = "Presupuesto")]
        [AmbientValue(typeof(PresupuestosClientes), "Id")]
        [Description("Presupuesto")]
        [DefaultValue(1)]
        public int PresupuestoId { get; set; }

        [Required(ErrorMessage = "Seleccione: Forma de Pago")]
        [Display(Name = "Forma de Pago")]
        [AmbientValue(typeof(FormasPago), "Id")]
        [Description("Forma de Pago")]
        [DefaultValue(1)]
        public int FormaPagoId { get; set; }

        [Required(ErrorMessage = "Seleccione: Forma de Entrega")]
        [Display(Name = "Forma de Entrega")]
        [AmbientValue(typeof(FormasEntrega), "Id")]
        [Description("Forma de Entrega")]
        [DefaultValue(1)]
        public int FormaEntregaId { get; set; }

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Seleccione: Usuario")]
        [AmbientValue(typeof(AspNetUsers), "Id")]
        [Description("Usuario")]
        [DefaultValue(1)]
        public int UsuarioId { get; set; }

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

    public class PedidosClientesVM : PedidosClientesBase, IIdRecord, ICopyRecord
    {
        public PedidosClientesVM() { }
        public PedidosClientesVM(PedidosClientes source) : base(source) { }

        [Required(ErrorMessage = "Seleccione: Factura de Clientes")]
        [Display(Name = "Factura de Clientes")]
        [AmbientValue(typeof(FacturasClientes), "Id")]
        [Description("Factura de Clientes")]
        [DefaultValue(1)]
        public IEnumerable<int> FacturasClientesIds { get; set; }

        public override ICopyRecord CopyTo<T>(DbContext context, T t, int deep = 1)
        {
            base.CopyTo(context, t);
            var source = t as PedidosClientes;
            if (source != null)
            {
                var ctx = context as Models.GesemEntities;
                source.FacturasClientes.Clear();
                if (FacturasClientesIds != null)
                    FacturasClientesIds.Each(id => source.FacturasClientes.AddNotNull(ctx.FacturasClientes.Find(id)));
            }
            return this;
        }

        public override ICopyRecord CopyFrom<T>(T t, int deep = 1)
        {
            base.CopyFrom(t);
            var source = t as PedidosClientes;
            if (source != null)
            {
                FacturasClientesIds = source.FacturasClientes.Select(v => v.Id);
            }
            return this;
        }
    }

    public partial class PedidosClientesMetadata : PedidosClientesBase
    {
        public PedidosClientesMetadata() { }
        public PedidosClientesMetadata(PedidosClientes source) : base(source) { }

        [Display(Name = "AspNetUsers")]
        public virtual AspNetUsers AspNetUsers { get; set; }

        [Display(Name = "Estados")]
        public Estados Estados { get; set; }

        [Display(Name = "FormasEntrega")]
        public FormasEntrega FormasEntrega { get; set; }

        [Display(Name = "FormasPago")]
        public FormasPago FormasPago { get; set; }

        [Display(Name = "PresupuestosClientes")]
        public PresupuestosClientes PresupuestosClientes { get; set; }

        [Display(Name = "Historicos")]
        public Historicos Historicos { get; set; }

        [Display(Name = "FacturasClientes")]
        public FacturasClientes FacturasClientes { get; set; }

    }
}
