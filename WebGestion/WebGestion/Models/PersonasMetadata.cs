using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace WebGestion.Models
{
    [MetadataType(typeof(PersonasMetadata))]
    public partial class Personas : IIdRecord
    {
    }

    public class PersonasBase : CopyRecord, IIdRecord
    {
        public PersonasBase() { }
        public PersonasBase(Personas source)
        {
            CopyFrom(source);
        }

        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Introduzca: Nombre")]
        [Display(Name = "Nombre", Prompt = "_nombre")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Display(Name = "Apellidos", Prompt = "_nombre")]
        [MaxLength(100)]
        public string Apellidos { get; set; }

        [Display(Name = "NIF")]
        [MaxLength(10)]
        public string NIF { get; set; }

        [Display(Name = "Direccion")]
        [MaxLength(200)]
        public string Direccion { get; set; }

        [Display(Name = "Localidad")]
        [MaxLength(150)]
        public string Localidad { get; set; }

        [Display(Name = "Provincia")]
        [MaxLength(150)]
        public string Provincia { get; set; }

        [Display(Name = "País")]
        [MaxLength(150)]
        public string Pais { get; set; }

        [Display(Name = "CP")]
        [DataType(DataType.PostalCode)]
        [MaxLength(20)]
        public string CP { get; set; }

        [Required(ErrorMessage = "Seleccione: Localidad")]
        [Display(Name = "Localidad")]
        [AmbientValue(typeof(Localidades), "Id")]
        [Association("Localidades", "LocalidadId", "Id")]
        [Description("Localidad")]
        [DefaultValue(1)]
        public int? LocalidadId { get; set; }

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Seleccione: Usuario")]
        [AmbientValue(typeof(AspNetUsers), "Id")]
        [Description("Usuario")]
        [DefaultValue(1)]
        public int? UserId { get; set; }

        [Display(Name = "Idioma")]
        [Required(ErrorMessage = "Seleccione: Idioma")]
        [AmbientValue(typeof(Idiomas), "Id")]
        [Description("Idioma")]
        [DefaultValue(1)]
        public int? IdiomaId { get; set; }

        [Display(Name = "Teléfono Fijo")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(100)]
        public string TelefonoFijo { get; set; }

        [Display(Name = "Teléfono Móvil")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(100)]
        public string TelefonoMovil { get; set; }

        [Display(Name = "e-mail")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(100)]
        public string Email { get; set; }

        [Display(Name = "Observaciones")]
        public string Observaciones { get; set; }

        [Required(ErrorMessage = "Seleccione: Estado")]
        [Display(Name = "Estado")]
        [AmbientValue(typeof(Estados), "Id")]
        [Description("Estado del usuario")]
        [DefaultValue(1)]
        public int EstadoId { get; set; }
    }

    public class PersonasVM : PersonasBase, IIdRecord, ICopyRecord
    {
        public PersonasVM() { }
        public PersonasVM(Personas source) : base(source) { }

        //[Display(Name = "Usuario")]
        //[AmbientValue(typeof(AspNetUsers), "Id")]
        //[Description("Usuario")]
        //public virtual int? AspNetUsersId { get; set; }

        //public override ICopyRecord CopyTo<T>(DbContext context, T t, int deep = 1)
        //{
        //    base.CopyTo(context, t);
        //    var source = t as Personas;
        //    if (source != null)
        //    {
        //        var ctx = context as Models.GesemEntities;
        //        if (AspNetUsersId != null)
        //        {
        //            var user = ctx.AspNetUsers.Find(AspNetUsersId);
        //            source.AspNetUsers = user;
        //        }
        //        else
        //        {
        //            source.AspNetUsers = null;
        //        }
        //    }
        //    return this;
        //}

        //public override ICopyRecord CopyFrom<T>(T t, int deep = 1)
        //{
        //    base.CopyFrom(t);
        //    var source = t as Personas;
        //    if (source != null)
        //    {
        //        AspNetUsersId = (source.AspNetUsers == null) ? null : (int?)source.AspNetUsers.Id;
        //    }
        //    return this;
        //}
    }

    public partial class PersonasMetadata : PersonasBase
    {
        public PersonasMetadata() { }
        public PersonasMetadata(Personas source) : base(source) { }

        [Display(Name = "Usuarios")]
        public virtual AspNetUsers AspNetUsers { get; set; }

        [Display(Name = "Clientes")]
        public virtual Clientes Clientes { get; set; }

        [Display(Name = "Empleados")]
        public virtual ICollection<Empleados> Empleados { get; set; }
        
        public virtual Estados Estados { get; set; }

        [Display(Name = "Idiomas")]
        public virtual Idiomas Idiomas { get; set; }

        [Display(Name = "Localidades")]
        public virtual Localidades Localidades { get; set; }

        [Display(Name = "Proveedores")]
        public virtual ICollection<Proveedores> Proveedores { get; set; }

        [Display(Name = "Historicos")]
        public virtual ICollection<Historicos> Historicos { get; set; }

    }
}
