using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using WebGestion.Controllers;

namespace WebGestion.Models
{
    [MetadataType(typeof(EmpleadosMetadata))]
    public partial class Empleados : IIdRecord
    {
    }

    public class EmpleadosBase : CopyRecord, IIdRecord
    {
        public EmpleadosBase() { }
        public EmpleadosBase(Empleados source)
        {
            CopyFrom(source);
        }

        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Persona")]
        [Required(ErrorMessage = "Seleccione:  Persona")]
        [AmbientValue(typeof(Personas), "Id")]
        [Description("Persona")]
        [DefaultValue(1)]
        public int PersonaId { get; set; }

        [Required(ErrorMessage = "Introduzca: CodEmpleado")]
        [Display(Name = "CodEmpleado", Prompt = "_nombre")]
        [MaxLength(20)]
        public string CodEmpleado { get; set; }

        [Display(Name = "Lugar")]
        [MaxLength(100)]
        public string Lugar { get; set; }

        [Display(Name = "Local")]
        [Required(ErrorMessage = "Seleccione:  Local")]
        [AmbientValue(typeof(Locales), "Id")]
        [Description("Local habitual del Empleado")]
        [DefaultValue(1)]
        public int LocalId { get; set; }

        [Display(Name = "Coordinador")]
        [Required(ErrorMessage = "Seleccione:  Coordinador")]
        [AmbientValue(typeof(Empleados), "Id")]
        [Description("Coordinador del Empleado")]
        [DefaultValue(1)]
        public int CoordinadorId { get; set; }

        [Required(ErrorMessage = "Seleccione: Estado")]
        [Display(Name = "Estado")]
        [AmbientValue(typeof(Estados), "Id")]
        [Description("Estado del usuario")]
        [DefaultValue(1)]
        public int EstadoId { get; set; }

        [Display(Name = "Fecha de Alta")]
        [DataType(DataType.DateTime)]
        public DateTime FechaAlta { get; set; }

        [Display(Name = "Fecha de Baja")]
        [DataType(DataType.DateTime)]
        public DateTime? FechaBaja { get; set; }
    }

    public class EmpleadosVM : EmpleadosBase, IIdRecord, ICopyRecord
    {
        public EmpleadosVM() { }
        public EmpleadosVM(Empleados source) : base(source) { }

        [Display(Name = "Cargos")]
        [AmbientValue(typeof(Cargos), "Id")]
        [Description("Cargos")]
        [Order]
        public virtual IEnumerable<int> CargosIds { get; set; }

        [Display(Name = "Recursos")]
        [AmbientValue(typeof(Recursos), "Id")]
        [Description("Miembro de Recursos")]
        [Order]
        public virtual IEnumerable<int> RecursosIds { get; set; }

        public override ICopyRecord CopyTo<T>(DbContext context, T t, int deep = 1)
        {
            base.CopyTo(context, t);
            var source = t as Empleados;
            if (source != null)
            {
                var ctx = context as Models.GesemEntities;
                source.Cargos.Clear();
                if (CargosIds != null)
                    CargosIds.Each(id => source.Cargos.AddNotNull(ctx.Cargos.Find(id)));
                source.Recursos.Clear();
                if (RecursosIds != null)
                    RecursosIds.Each(id => source.Recursos.AddNotNull(ctx.Recursos.Find(id)));
            }
            return this;
        }

        public override ICopyRecord CopyFrom<T>(T t, int deep = 1)
        {
            base.CopyFrom(t);
            var source = t as Empleados;
            if (source != null)
            {
                CargosIds = source.Cargos.Select(v => v.Id);
                RecursosIds = source.Recursos.Select(v => v.Id);
            }
            return this;
        }
    }

    public partial class EmpleadosMetadata : EmpleadosBase
    {
        public EmpleadosMetadata() { }
        public EmpleadosMetadata(Empleados source) : base(source) { }


        [Display(Name = "Empleados1")]
        public Empleados Empleados1 { get; set; }

        [Display(Name = "Empleados2")]
        public Empleados Empleados2 { get; set; }

        [Display(Name = "Estados")]
        public Estados Estados { get; set; }

        [Display(Name = "Locales")]
        public Locales Locales { get; set; }

        [Display(Name = "Personas")]
        public virtual Personas Personas { get; set; }

        [Display(Name = "FacturasClientes")]
        public FacturasClientes FacturasClientes { get; set; }

        [Display(Name = "Inventario")]
        public FacturasClientes Inventario { get; set; }

        [Display(Name = "Cargos")]
        public Cargos Cargos { get; set; }

        public virtual ICollection<Recursos> Recursos { get; set; }
    }
}
