using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using WebGestion.Controllers;
using WebGestion.Utils;

namespace WebGestion.Models
{
    [MetadataType(typeof(AspNetUsersMetadata))]
    public partial class AspNetUsers : IIdRecord
    {
    }

    public class AspNetUsersBase : CopyRecord, IIdRecord
    {
        public AspNetUsersBase() { }
        public AspNetUsersBase(AspNetUsers source)
        {
            CopyFrom(source);
        }

        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        [Order]
        public int Id { get; set; }

        [Required(ErrorMessage = "Introduzca: Nombre de Usuario")]
        [Display(Name = "Nombre de Usuario", Prompt = "_nombre")]
        [MaxLength(256)]
        [Order]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Introduzca: Email")]
        [Display(Name = "e-mail")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(256)]
        [Order]
        public string Email { get; set; }

        [Display(Name = "e-mail confirmado")]
        [Order]
        public bool EmailConfirmed { get; set; }

        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        [Order]
        public string PhoneNumber { get; set; }

        [Display(Name = "Teléfono confirmado")]
        [DataType(DataType.PhoneNumber)]
        [Order]
        public bool PhoneNumberConfirmed { get; set; }

        [Display(Name = "Password Hash")]
        [DataType(DataType.Password)]
        [Order]
        public string PasswordHash { get; set; }

        [Display(Name = "Security Stamp")]
        [DataType(DataType.Password)]
        [Order]
        public string SecurityStamp { get; set; }

        [Display(Name = "Two Factor Enabled")]
        [Order]
        public bool TwoFactorEnabled { get; set; }

        [Display(Name = "Lockout End Date Utc")]
        [Order]
        public DateTime? LockoutEndDateUtc { get; set; }

        [Display(Name = "Lockout Enabled")]
        [Order]
        public bool LockoutEnabled { get; set; }

        [Display(Name = "Access Failed Count")]
        [Order]
        public int AccessFailedCount { get; set; }

        [Required(ErrorMessage = "Seleccione: Estado")]
        [Display(Name = "Estado")]
        [AmbientValue(typeof(Estados), "Id")]
        [Description("Estado del usuario")]
        [DefaultValue(1)]
        [Order]
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

    public class AspNetUsersVM : AspNetUsersBase, IIdRecord, ICopyRecord
    {
        public AspNetUsersVM() { }
        public AspNetUsersVM(AspNetUsers source) : base(source) { }

        [Required(ErrorMessage = "Introduzca los grupos a los que pertenece el usuario")]
        [Display(Name = "Grupos")]
        [AmbientValue(typeof(ApplicationGroups), "Id")]
        [Description("Grupos a los que pertenece el usuario")]
        [Order]
        public virtual IEnumerable<int> ApplicationGroupsIds { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Roles")]
        [AmbientValue(typeof(AspNetRoles), "Id")]
        [Description("Roles que contempla el usuario")]
        [Order]
        public virtual IEnumerable<int> AspNetRolesIds { get; set; }

        [Display(Name = "Info de Usuario")]
        [AmbientValue(typeof(AspNetUserClaims), "Id")]
        [Description("Info de Usuario")]
        [Order]
        public virtual IEnumerable<int> AspNetUserClaimsIds { get; set; }

        [Display(Name = "Logins de Usuario")]
        [Order]
        public virtual ICollection<string> AspNetUserLogins { get; set; }

        [Display(Name = "Persona")]
        [AmbientValue(typeof(Personas), "Id")]
        [Description("Persona")]
        [Order]
        public virtual int? PersonasId { get; set; }

        public override ICopyRecord CopyTo<T>(DbContext context, T t, int deep = 1)
        {
            base.CopyTo(context, t);
            var source = t as AspNetUsers;
            if (source != null)
            {
                var ctx = context as Models.GesemEntities;
                source.ApplicationGroups.Clear();
                if (ApplicationGroupsIds != null)
                    ApplicationGroupsIds.Each(id => source.ApplicationGroups.AddNotNull(ctx.ApplicationGroups.Find(id)));
                source.Personas.Clear();
                if(PersonasId != null)
                {
                    var persona = ctx.Personas.Find(PersonasId);
                    source.Personas.AddNotNull(persona);
                }
            }
            return this;
        }

        public override ICopyRecord CopyFrom<T>(T t, int deep = 1)
        {
            base.CopyFrom(t);
            var source = t as AspNetUsers;
            if (source != null)
            {
                ApplicationGroupsIds = source.ApplicationGroups.Select(v => v.Id);
                AspNetRolesIds = source.AspNetRoles.Select(v => v.Id);
                AspNetUserClaimsIds = source.AspNetUserClaims.Select(v => v.Id);
                AspNetUserLogins = source.AspNetUserLogins.Select(v => v.LoginProvider).ToList();
                PersonasId = (source.Personas == null || source.Personas.Count == 0) ? null : (int?) source.Personas.First().Id;
            }
            return this;
        }
    }

    public partial class AspNetUsersMetadata : AspNetUsersBase
    {
        public AspNetUsersMetadata() { }
        public AspNetUsersMetadata(AspNetUsers source) : base(source) { }

        [Display(Name = "AspNetUserClaims")]
        public AspNetUserClaims AspNetUserClaims { get; set; }

        [Display(Name = "AspNetUserLogins")]
        public AspNetUserLogins AspNetUserLogins { get; set; }

        [Display(Name = "Estados")]
        public Estados Estados { get; set; }

        [Display(Name = "PedidosClientes")]
        public PedidosClientes PedidosClientes { get; set; }

        [Display(Name = "Personas")]
        public Personas Personas { get; set; }

        [Display(Name = "PresupuestosClientes")]
        public PresupuestosClientes PresupuestosClientes { get; set; }

        [Display(Name = "CambiosLog")]
        public CambiosLog CambiosLog { get; set; }

        [Display(Name = "ApplicationGroups")]
        public ApplicationGroups ApplicationGroups { get; set; }

        [Display(Name = "AspNetRoles")]
        public AspNetRoles AspNetRoles { get; set; }

    }
}
