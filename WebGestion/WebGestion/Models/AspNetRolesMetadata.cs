using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using WebGestion.Controllers;

namespace WebGestion.Models
{
    [MetadataType(typeof(AspNetRolesMetadata))]
    public partial class AspNetRoles : IIdRecord
    {
    }

    public class AspNetRolesBase : CopyRecord, IIdRecord
    {
        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Introduzca: Name")]
        [Display(Name = "Name", Prompt = "_nombre")]
        [MaxLength(256)]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [MaxLength(256)]
        public string Description { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Seleccione: Estado")]
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

        public AspNetRolesBase() { }
        public AspNetRolesBase(AspNetRoles source)
        {
            CopyFrom(source);
        }
    }

    public class AspNetRolesVM : AspNetRolesBase, IIdRecord, ICopyRecord
    {
        public AspNetRolesVM() : base()
        {
        }
        public AspNetRolesVM(AspNetRoles source) : base(source)
        {
        }

        [Required(ErrorMessage = "Introduzca loa grupos que comparte el rol")]
        [Display(Name = "Grupos de roles de usuarios")]
        [AmbientValue(typeof(ApplicationGroups), "Id")]
        [Description("Grupos a los que pertenece el usuario")]
        public virtual IEnumerable<int> ApplicationGroupsIds { get; set; }

        public override ICopyRecord CopyTo<T>(DbContext context, T t, int deep = 1)
        {
            base.CopyTo(context, t);
            var source = t as AspNetRoles;
            if (source != null)
            {
                var ctx = context as Models.GesemEntities;
                source.ApplicationGroups.Clear();
                if (ApplicationGroupsIds != null)
                    ApplicationGroupsIds.Each(id => source.ApplicationGroups.AddNotNull(ctx.ApplicationGroups.Find(id)));
            }
            return this;
        }

        public override ICopyRecord CopyFrom<T>(T t, int deep = 1)
        {
            base.CopyFrom(t);
            var source = t as AspNetRoles;
            if (source != null)
            {
                ApplicationGroupsIds = source.ApplicationGroups.Select(v => v.Id);
            }
            return this;
        }

        //[ReadOnly(true)]
        //public string Nombre { get { return Name; } set { Name = value; } }
        //[ReadOnly(true)]
        //public string Descripcion { get { return Description; } set { Description = value; } }
    }

    public partial class AspNetRolesMetadata : AspNetRolesBase
    {
        public AspNetRolesMetadata(AspNetRoles source) :base(source)
        {
        }
        [Display(Name = "Estados")]
        public Estados Estados { get; set; }

        [Display(Name = "ApplicationGroups")]
        public ApplicationGroups ApplicationGroups { get; set; }

        [Display(Name = "AspNetUsers")]
        public AspNetUsers AspNetUsers { get; set; }

    }
}
