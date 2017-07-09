using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using WebGestion.Controllers;

namespace WebGestion.Models
{
    [MetadataType(typeof(ClientesMetadata))]
    public partial class Clientes : IIdRecord
    {
    }

    public class ClientesBase : CopyRecord, IIdRecord
    {
        [Key]
        //[ReadOnly(true)]
        [Display(Name = "Persona")]
        [AmbientValue(typeof(Personas), "Id")]
        [Description("Persona")]
        [DefaultValue(1)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Seleccione: Persona")]
        [Display(Name = "Persona")]
        [AmbientValue(typeof(Personas), "Id")]
        [Description("Persona")]
        [DefaultValue(1)]
        public int PersonaId { get; set; }

        [Display(Name = "Descuento")]
        public decimal Descuento { get; set; }

        [Required(ErrorMessage = "Seleccione: Estado")]
        [Display(Name = "Estado")]
        [AmbientValue(typeof(Estados), "Id")]
        [Description("Estado del cliente")]
        [DefaultValue(1)]
        public int EstadoId { get; set; }

        [Display(Name = "Fecha de Alta")]
        [DataType(DataType.DateTime)]
        public DateTime FechaAlta { get; set; }

        [Display(Name = "Fecha de Baja")]
        [DataType(DataType.DateTime)]
        public DateTime? FechaBaja { get; set; }

        public ClientesBase() { }
        public ClientesBase(Clientes source)
        {
            CopyFrom(source);
        }
    }

    public class ClientesVM : ClientesBase, IIdRecord, ICopyRecord
    {
        public ClientesVM() { }
        public ClientesVM(Clientes source) : base(source) { }

        [Required(ErrorMessage = "Seleccione: Direcciones de Facturacion")]
        [Display(Name = "Estado")]
        [AmbientValue(typeof(DireccionesFacturacion), "Id")]
        [Description("Direcciones de Facturacion")]
        [DefaultValue(1)]
        public IEnumerable<int> DireccionesFacturacionIds { get; set; }

        public override ICopyRecord CopyTo<T>(DbContext context, T t, int deep = 1)
        {
            base.CopyTo(context, t);
            var source = t as Clientes;
            if (source != null)
            {
                var ctx = context as Models.GesemEntities;
                source.DireccionesFacturacion.Clear();
                if (DireccionesFacturacionIds != null)
                    DireccionesFacturacionIds.Each(id => source.DireccionesFacturacion.AddNotNull(ctx.DireccionesFacturacion.Find(id)));
            }
            return this;
        }

        public override ICopyRecord CopyFrom<T>(T t, int deep = 1)
        {
            base.CopyFrom(t);
            var source = t as Clientes;
            if (source != null)
            {
                DireccionesFacturacionIds = source.DireccionesFacturacion.Select(v => v.Id);
            }
            return this;
        }
    }

    public partial class ClientesMetadata : ClientesBase
    {
        public ClientesMetadata() { }
        public ClientesMetadata(Clientes source) : base(source) { }

        [Display(Name = "Estados")]
        public Estados Estados { get; set; }

        [Display(Name = "DireccionesFacturacion")]
        public DireccionesFacturacion DireccionesFacturacion { get; set; }

        [Display(Name = "Personas")]
        public Personas Personas { get; set; }

        [Display(Name = "PresupuestosClientes")]
        public PresupuestosClientes PresupuestosClientes { get; set; }

    }
}
