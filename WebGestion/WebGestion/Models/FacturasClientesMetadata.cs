using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using WebGestion.Controllers;

namespace WebGestion.Models
{
    [MetadataType(typeof(FacturasClientesMetadata))]
    public partial class FacturasClientes : IIdRecord
    {
    }

    public class FacturasClientesBase : CopyRecord, IIdRecord
    {
        public FacturasClientesBase() { }
        public FacturasClientesBase(FacturasClientes source)
        {
            CopyFrom(source);
        }

        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Tipo de Factura")]
        [Required(ErrorMessage = "Seleccione:  Tipo de Factura")]
        [AmbientValue(typeof(TiposFacturas), "Id")]
        [Description("Tipo de Factura")]
        [DefaultValue(1)]
        public int TipoFacturaId { get; set; }

        [Required(ErrorMessage = "Introduzca: CodFactura")]
        [Display(Name = "CodFactura", Prompt = "_nombre")]
        [MaxLength(20)]
        public string CodFactura { get; set; }

        [Display(Name = "IRPFId")]
        public int IRPFId { get; set; }

        [Required(ErrorMessage = "Introduzca: Empleado")]
        [Display(Name = "EmpleadoId")]
        [AmbientValue(typeof(Empleados), "Id")]
        [Description("Empleado")]
        [DefaultValue(1)]
        public int EmpleadoId { get; set; }

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

    public class FacturasClientesVM : FacturasClientesBase, IIdRecord, ICopyRecord
    {
        public FacturasClientesVM() { }
        public FacturasClientesVM(FacturasClientes source) : base(source) { }

        [Required(ErrorMessage = "Seleccione: Pedidos de Clientes")]
        [Display(Name = "Pedidos de Clientes")]
        [AmbientValue(typeof(PedidosClientes), "Id")]
        [Description("Pedidos de Clientes")]
        [DefaultValue(1)]
        public IEnumerable<int> PedidosClientesIds { get; set; }

        public override ICopyRecord CopyTo<T>(DbContext context, T t, int deep = 1)
        {
            base.CopyTo(context, t);
            var source = t as FacturasClientes;
            if (source != null)
            {
                var ctx = context as Models.GesemEntities;
                source.PedidosClientes.Clear();
                if (PedidosClientesIds != null)
                    PedidosClientesIds.Each(id => source.PedidosClientes.AddNotNull(ctx.PedidosClientes.Find(id)));
            }
            return this;
        }

        public override ICopyRecord CopyFrom<T>(T t, int deep = 1)
        {
            base.CopyFrom(t);
            var source = t as FacturasClientes;
            if (source != null)
            {
                PedidosClientesIds = source.PedidosClientes.Select(v => v.Id);
            }
            return this;
        }
    }

    public partial class FacturasClientesMetadata : FacturasClientesBase
    {
        public FacturasClientesMetadata() { }
        public FacturasClientesMetadata(FacturasClientes source) : base(source) { }

        [Display(Name = "Empleados")]
        public Empleados Empleados { get; set; }

        [Display(Name = "Estados")]
        public Estados Estados { get; set; }

        [Display(Name = "IRPFs")]
        public IRPFs IRPFs { get; set; }

        [Display(Name = "TiposFacturas")]
        public TiposFacturas TiposFacturas { get; set; }

        [Display(Name = "Historicos")]
        public Historicos Historicos { get; set; }

        [Display(Name = "PedidosClientes")]
        public PedidosClientes PedidosClientes { get; set; }

    }
}
