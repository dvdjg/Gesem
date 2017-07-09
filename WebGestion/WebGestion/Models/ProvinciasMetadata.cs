using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebGestion.Utils;

namespace WebGestion.Models
{
    [MetadataType(typeof(ProvinciasMetadata))]
    public partial class Provincias : IIdRecord
    {
    }

    public partial class ProvinciasBase : CopyRecord, IIdRecord
    {
        [Display(Name = "Id")]
        [Order]
        public int Id { get; set; }

        [Required(ErrorMessage = "Introduzca: CodProvincia")]
        [Display(Name = "CodProvincia")]
        [Order]
        public int CodProvincia { get; set; }

        [Required(ErrorMessage = "Introduzca: Provincia")]
        [Display(Name = "Provincia")]
        [MaxLength(150)]
        [Order]
        public string Nombre { get; set; }

        [Display(Name = "País")]
        [Required(ErrorMessage = "Seleccione: Pais")]
        [AmbientValue(typeof(Paises), "Id")]
        [Description("Pais")]
        [DefaultValue(1)]
        [Order]
        public int PaisId { get; set; }

        [Required(ErrorMessage = "Introduzca: IdiomaId")]
        [Display(Name = "IdiomaId")]
        [Order]
        public int IdiomaId { get; set; }

        [Display(Name = "X")]
        [Order]
        public float X { get; set; }

        [Display(Name = "Y")]
        [Order]
        public float Y { get; set; }

        [Display(Name = "Exacto")]
        [Order]
        public bool Exacto { get; set; }

        public ProvinciasBase() { }
        public ProvinciasBase(Provincias source)
        {
            CopyFrom(source);
        }
    }

    public class ProvinciasVM : ProvinciasBase, ICopyRecord, IIdRecord
    {
        public String Pais { get; set; }
        public String Idioma { get; set; }

        public ProvinciasVM() : base()
        {
        }
        public ProvinciasVM(Provincias source) : base(source)
        {
        }
        public override ICopyRecord CopyFrom<T>(T t, int deep = 1)
        {
            base.CopyFrom(t);
            var source = t as Provincias;
            if (source != null)
            {
                Pais = source.Paises.Nombre;
                Idioma = source.Idiomas.Codigo;
            }
            return this;
        }
    }

    public partial class ProvinciasMetadata : ProvinciasBase
    {
        [Display(Name = "Idiomas")]
        public Idiomas Idiomas { get; set; }

        [Display(Name = "Localidades")]
        public Localidades Localidades { get; set; }

        [Display(Name = "Paises")]
        public Paises Paises { get; set; }

    }
}
