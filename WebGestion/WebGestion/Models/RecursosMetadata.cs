using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using WebGestion.Controllers;

namespace WebGestion.Models
{
    [MetadataType(typeof(RecursosMetadata))]
    public partial class Recursos : IIdRecord
    {
    }

    public class RecursosBase : CopyRecord, IIdRecord
    {
        public RecursosBase() { }
        public RecursosBase(Recursos source)
        {
            CopyFrom(source);
        }

        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Introduzca: Nombre")]
        [Display(Name = "Nombre")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(200)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Seleccione: Estado")]
        [Display(Name = "Estado")]
        [AmbientValue(typeof(Estados), "Id")]
        [Description("Estado del usuario")]
        [DefaultValue(1)]
        public int EstadoId { get; set; }

        [Display(Name = "Fecha de Alta")]
        [DataType(DataType.DateTime)]
        [Order]
        public System.DateTime FechaAlta { get; set; }

        [Display(Name = "Fecha de Baja")]
        [DataType(DataType.DateTime)]
        [Order]
        public DateTime? FechaBaja { get; set; }
    }

    public class RecursosVM : RecursosBase, IIdRecord, ICopyRecord
    {
        public RecursosVM() { }
        public RecursosVM(Recursos source) : base(source) { }

        [Required(ErrorMessage = "Seleccione: Inventario")]
        [Display(Name = "Inventario")]
        [AmbientValue(typeof(Bienes), "Id")]
        [Description("Inventario")]
        [DefaultValue(1)]
        public IEnumerable<int> InventarioIds { get; set; }

        [Required(ErrorMessage = "Seleccione: Empleados")]
        [Display(Name = "Empleados")]
        [AmbientValue(typeof(Empleados), "Id")]
        [Description("Empleados")]
        [DefaultValue(1)]
        public IEnumerable<int> EmpleadosIds { get; set; }

        public override ICopyRecord CopyTo<T>(DbContext context, T t, int deep = 1)
        {
            base.CopyTo(context, t);
            var source = t as Recursos;
            if (source != null)
            {
                var ctx = context as Models.GesemEntities;
                source.Inventario.Clear();
                if (InventarioIds != null)
                    InventarioIds.Each(id => source.Inventario.AddNotNull(ctx.Inventario.Find(id)));
                source.Empleados.Clear();
                if (EmpleadosIds != null)
                    EmpleadosIds.Each(id => source.Empleados.AddNotNull(ctx.Empleados.Find(id)));
            }
            return this;
        }

        public override ICopyRecord CopyFrom<T>(T t, int deep = 1)
        {
            base.CopyFrom(t);
            var source = t as Recursos;
            if (source != null)
            {
                InventarioIds = source.Inventario.Select(v => v.Id);
                EmpleadosIds = source.Empleados.Select(v => v.Id);
            }
            return this;
        }
    }

    public partial class RecursosMetadata : RecursosBase
    {
        public RecursosMetadata() { }
        public RecursosMetadata(Recursos source) : base(source) { }

        [Display(Name = "Bienes")]
        public virtual ICollection<Bienes> Bienes { get; set; }

        [Display(Name = "Empleados")]
        public virtual ICollection<Empleados> Empleados { get; set; }

        [Display(Name = "Estados")]
        public virtual Estados Estados { get; set; }

    }
}
