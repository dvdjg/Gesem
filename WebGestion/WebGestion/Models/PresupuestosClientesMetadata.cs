using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using WebGestion.Controllers;

namespace WebGestion.Models
{
    [MetadataType(typeof(PresupuestosClientesMetadata))]
    public partial class PresupuestosClientes : IIdRecord
    {
    }

    public class PresupuestosClientesBase : CopyRecord, IIdRecord
    {
        public PresupuestosClientesBase() { }
        public PresupuestosClientesBase(PresupuestosClientes source)
        {
            CopyFrom(source);
        }

        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Introduzca: CodPresupuesto")]
        [Display(Name = "CodPresupuesto", Prompt = "_nombre")]
        [MaxLength(20)]
        public string CodPresupuesto { get; set; }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Seleccione: Cliente")]
        [AmbientValue(typeof(Clientes), "Id")]
        [Description("Cliente")]
        [DefaultValue(1)]
        public int ClienteId { get; set; }

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

    public class PresupuestosClientesVM : PresupuestosClientesBase, IIdRecord, ICopyRecord
    {
        public PresupuestosClientesVM() { }
        public PresupuestosClientesVM(PresupuestosClientes source) : base(source) { }

        [Required(ErrorMessage = "Seleccione: Presupuestos de Clientes Detalle Agrupados")]
        [Display(Name = "Presupuestos de Clientes Detalle Agrupados")]
        [AmbientValue(typeof(PresupuestosClientesDetalleAgrupados), "Id")]
        [Description("Presupuestos de Clientes Detalle Agrupados")]
        [DefaultValue(1)]
        public IEnumerable<int> PresupuestosClientesDetalleAgrupadosIds { get; set; }

        public override ICopyRecord CopyTo<T>(DbContext context, T t, int deep = 1)
        {
            base.CopyTo(context, t);
            var source = t as PresupuestosClientes;
            if (source != null)
            {
                var ctx = context as Models.GesemEntities;
                source.PresupuestosClientesDetalleAgrupados.Clear();
                if (PresupuestosClientesDetalleAgrupadosIds != null)
                    PresupuestosClientesDetalleAgrupadosIds.Each(id => source.PresupuestosClientesDetalleAgrupados.AddNotNull(ctx.PresupuestosClientesDetalleAgrupados.Find(id)));
            }
            return this;
        }

        public override ICopyRecord CopyFrom<T>(T t, int deep = 1)
        {
            base.CopyFrom(t);
            var source = t as PresupuestosClientes;
            if (source != null)
            {
                PresupuestosClientesDetalleAgrupadosIds = source.PresupuestosClientesDetalleAgrupados.Select(v => v.Id);
            }
            return this;
        }
    }

    public partial class PresupuestosClientesMetadata : PresupuestosClientesBase
    {
        public PresupuestosClientesMetadata() { }
        public PresupuestosClientesMetadata(PresupuestosClientes source) : base(source) { }

        public virtual AspNetUsers AspNetUsers { get; set; }

        [Display(Name = "Clientes")]
        public Clientes Clientes { get; set; }

        [Display(Name = "Estados")]
        public Estados Estados { get; set; }

        [Display(Name = "PedidosClientes")]
        public PedidosClientes PedidosClientes { get; set; }

        [Display(Name = "Presupuestos de Clientes Detalle Agrupados")]
        public PresupuestosClientesDetalleAgrupados PresupuestosClientesDetalleAgrupados { get; set; }

        [Display(Name = "Historicos")]
        public Historicos Historicos { get; set; }

    }
}
