using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebGestion.Models
{
    [MetadataType(typeof(AspNetUserClaimsMetadata))]
    public partial class AspNetUserClaims : IIdRecord
    {
    }

    public class AspNetUserClaimsBase : CopyRecord, IIdRecord
    {
        public AspNetUserClaimsBase() { }
        public AspNetUserClaimsBase(AspNetUserClaims source)
        {
            CopyFrom(source);
        }

        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Seleccione:  Usuario")]
        [AmbientValue(typeof(AspNetUsers), "Id")]
        [Description("Usuario")]
        [DefaultValue(1)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Introduzca: ClaimType")]
        [Display(Name = "Claim Type", Prompt = "_nombre")]
        public string ClaimType { get; set; }

        [Required(ErrorMessage = "Introduzca: ClaimValue")]
        [Display(Name = "Claim Value", Prompt = "_nombre")]
        public string ClaimValue { get; set; }
    }

    public class AspNetUserClaimsVM : AspNetUserClaimsBase, IIdRecord, ICopyRecord
    {
        public AspNetUserClaimsVM() { }
        public AspNetUserClaimsVM(AspNetUserClaims source) : base(source) { }
    }

    public partial class AspNetUserClaimsMetadata : AspNetUserClaimsBase
    {
        public AspNetUserClaimsMetadata() { }
        public AspNetUserClaimsMetadata(AspNetUserClaims source) : base(source) { }

        [Display(Name = "AspNetUsers")]
        public AspNetUsers AspNetUsers { get; set; }

    }
}
