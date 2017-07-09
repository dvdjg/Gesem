using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using WebGestion.Controllers;

namespace WebGestion.Models
{
    public class ApplicationGroupsBase : CopyRecord, IRecord
    {
        [Key]
        [ReadOnly(true)]
        [DefaultValue(-1)]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Introduzca el nombre del Estado")]
        [Display(Name = "Grupo")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(200)]
        public string Descripcion { get; set; }

        public ApplicationGroupsBase() {}
        public ApplicationGroupsBase(ApplicationGroups source)
        {
            CopyFrom(source);
        }
    }

    public class ApplicationGroupsVM : ApplicationGroupsBase, IRecord, ICopyRecord
    {
        public ApplicationGroupsVM() : base()
        {
        }
        public ApplicationGroupsVM(ApplicationGroups source) : base(source)
        {
        }

        [Display(Name = "Roles del grupo")]
        [AmbientValue(typeof(AspNetRoles), "Id")]
        [Description("Roles que componen el grupo")]
        public virtual IEnumerable<int> AspNetRolesIds { get; set; }

        [Display(Name = "Usuarios del grupo")]
        [AmbientValue(typeof(AspNetUsers), "Id")]
        [Description("Roles que componen el grupo")]
        public virtual IEnumerable<int> AspNetUsersIds { get; set; }

        public override ICopyRecord CopyTo<T>(DbContext context, T t, int deep = 1)
        {
            base.CopyTo(context, t);
            var source = t as ApplicationGroups;
            if (source != null)
            {
                var ctx = context as Models.GesemEntities;
                source.AspNetRoles.Clear();
                if (AspNetRolesIds != null)
                    AspNetRolesIds.Each(id => source.AspNetRoles.AddNotNull(ctx.AspNetRoles.Find(id)));
                source.AspNetUsers.Clear();
                if (AspNetUsersIds != null)
                    AspNetUsersIds.Each(id => source.AspNetUsers.AddNotNull(ctx.AspNetUsers.Find(id)));
            }
            return this;
        }

        public override ICopyRecord CopyFrom<T>(T t, int deep = 1)
        {
            base.CopyFrom(t);
            var source = t as ApplicationGroups;
            if (source != null)
            {
                AspNetRolesIds = source.AspNetRoles.Select(v => v.Id);
                AspNetUsersIds = source.AspNetUsers.Select(v => v.Id);
            }
            return this;
        }
    }

    [MetadataType(typeof(ApplicationGroupsMetadata))]
    public partial class ApplicationGroups : IIdRecord
    {
    }

    public partial class ApplicationGroupsMetadata : ApplicationGroupsBase
    {
        [Display(Name = "AspNetRoles")]
        public AspNetRoles AspNetRoles { get; set; }

        [Display(Name = "AspNetUsers")]
        public AspNetUsers AspNetUsers { get; set; }
    }
}
