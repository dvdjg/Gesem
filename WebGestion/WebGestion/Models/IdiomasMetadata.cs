using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebGestion.Models
{
    [MetadataType(typeof(IdiomasMetadata))]
    public partial class Idiomas : IIdRecord
    {
    }

    public class IdiomasBase : CopyRecord, IIdRecord
    {
        public IdiomasBase() { }
        public IdiomasBase(Idiomas source)
        {
            CopyFrom(source);
        }

        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Introduzca: Código")]
        [Display(Name = "Código", Prompt = "_nombre")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Introduzca: Español")]
        [Display(Name = "Español")]
        public string Espannol { get; set; }

        [Required(ErrorMessage = "Introduzca: Inglés")]
        [Display(Name = "Inglés")]
        public string Ingles { get; set; }
    }

    public class IdiomasVM : IdiomasBase, IIdRecord, ICopyRecord
    {
        public IdiomasVM() { }
        public IdiomasVM(Idiomas source) : base(source) { }
    }

    public partial class IdiomasMetadata : IdiomasBase
    {
        public IdiomasMetadata() { }
        public IdiomasMetadata(Idiomas source) : base(source) { }

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
