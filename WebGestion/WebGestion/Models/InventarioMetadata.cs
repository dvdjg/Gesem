using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using WebGestion.Controllers;

namespace WebGestion.Models
{
    [MetadataType(typeof(InventarioMetadata))]
    public partial class Inventario : IIdRecord
    {
    }

    public class InventarioBase : CopyRecord, IIdRecord
    {
        public InventarioBase() { }
        public InventarioBase(Inventario source)
        {
            CopyFrom(source);
        }

        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Introduzca: Código único")]
        [Display(Name = "Código", Prompt = "_nombre")]
        public string CodUnico { get; set; }

        [Required(ErrorMessage = "Seleccione: Bien")]
        [Display(Name = "Bien")]
        [AmbientValue(typeof(Bienes), "Id")]
        [Description("Bien")]
        [DefaultValue(1)]
        public string BienId { get; set; }

        [Required(ErrorMessage = "Seleccione: Local")]
        [Display(Name = "Local")]
        [AmbientValue(typeof(Locales), "Id")]
        [Description("Local")]
        [DefaultValue(1)]
        public string LocalId { get; set; }

        [Required(ErrorMessage = "Seleccione: Empleado")]
        [Display(Name = "Empleado")]
        [AmbientValue(typeof(Empleados), "Id")]
        [Description("Empleado")]
        [DefaultValue(1)]
        public string EmpleadoId { get; set; }

        public double Stock { get; set; }

        [Display(Name = "Fecha de Alta")]
        [DataType(DataType.DateTime)]
        public System.DateTime FechaAlta { get; set; }

        [Display(Name = "Fecha de Baja")]
        [DataType(DataType.DateTime)]
        public DateTime? FechaBaja { get; set; }
    }

    public class InventarioVM : InventarioBase, IIdRecord, ICopyRecord
    {
        public InventarioVM() { }
        public InventarioVM(Inventario source) : base(source) { }

        [Display(Name = "Recursos")]
        [AmbientValue(typeof(Recursos), "Id")]
        [Description("Recursos")]
        [Order]
        public virtual IEnumerable<int> RecursosIds { get; set; }

        public override ICopyRecord CopyTo<T>(DbContext context, T t, int deep = 1)
        {
            base.CopyTo(context, t);
            var source = t as Inventario;
            if (source != null)
            {
                var ctx = context as Models.GesemEntities;
                source.Recursos.Clear();
                if (RecursosIds != null)
                    RecursosIds.Each(id => source.Recursos.AddNotNull(ctx.Recursos.Find(id)));
            }
            return this;
        }

        public override ICopyRecord CopyFrom<T>(T t, int deep = 1)
        {
            base.CopyFrom(t);
            var source = t as Inventario;
            if (source != null)
            {
                RecursosIds = source.Recursos.Select(v => v.Id);
            }
            return this;
        }
    }

    public partial class InventarioMetadata : InventarioBase
    {
        public InventarioMetadata() { }
        public InventarioMetadata(Inventario source) : base(source) { }

        [Display(Name = "DireccionesFacturacion")]
        public virtual ICollection<DireccionesFacturacion> DireccionesFacturacion { get; set; }

        [Display(Name = "Locales")]
        public virtual ICollection<Locales> Locales { get; set; }

        [Display(Name = "Provincias")]
        public virtual ICollection<Provincias> Provincias { get; set; }

        [Display(Name = "Localidades")]
        public virtual ICollection<Localidades> Localidades { get; set; }

        [Display(Name = "Paises")]
        public virtual ICollection<Paises> Paises { get; set; }

        [Display(Name = "Personas")]
        public virtual ICollection<Personas> Personas { get; set; }

    }
}
