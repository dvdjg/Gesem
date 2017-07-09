using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebGestion.Models
{
    [MetadataType(typeof(AspNetUserLoginsMetadata))]
    public partial class AspNetUserLogins
    {
    }

    public class AspNetUserLoginsBase : CopyRecord
    {
        public AspNetUserLoginsBase() { }
        public AspNetUserLoginsBase(AspNetUserLogins source)
        {
            CopyFrom(source);
        }

        [Key]
        [Required(ErrorMessage = "Introduzca: Login Provider")]
        [Display(Name = "Login Provider", Prompt = "_nombre")]
        [MaxLength(128)]
        public string LoginProvider { get; set; }

        [Key]
        [Required(ErrorMessage = "Introduzca: Login Provider")]
        [Display(Name = "Login Provider")]
        [MaxLength(128)]
        public string ProviderKey { get; set; }

        [Key]
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Seleccione:  Usuario")]
        [AmbientValue(typeof(AspNetUsers), "Id")]
        [Description("Usuario")]
        [DefaultValue(1)]
        public int UserId { get; set; }
    }

    public class AspNetUserLoginsVM : AspNetUserLoginsBase, ICopyRecord
    {
        public AspNetUserLoginsVM() { }
        public AspNetUserLoginsVM(AspNetUserLogins source) : base(source) { }
    }

    public partial class AspNetUserLoginsMetadata : AspNetUserLoginsBase
    {
        public AspNetUserLoginsMetadata() { }
        public AspNetUserLoginsMetadata(AspNetUserLogins source) : base(source) { }
    }
}
