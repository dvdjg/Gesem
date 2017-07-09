using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebGestion.Utils;

namespace WebGestion.Models
{
    [MetadataType(typeof(PaisesMetadata))]
    public partial class Paises : IIdRecord
    {
    }

    public partial class PaisesBase : CopyRecord, IIdRecord
    {
        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        [Order]
        public int Id { get; set; }

        [Required(ErrorMessage = "Introduzca: Pais")]
        [Display(Name = "País")]
        [MaxLength(100)]
        [Order]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Introduzca: CodPais")]
        [Display(Name = "CodPaís")]
        [Order]
        public int CodPais { get; set; }

        [Display(Name = "Idioma")]
        [Required(ErrorMessage = "Seleccione: Idioma")]
        [AmbientValue(typeof(Idiomas), "Id")]
        [Description("Idioma")]
        [DefaultValue(1)]
        [Order]
        public int IdiomaId { get; set; }

        [Required(ErrorMessage = "Introduzca: X")]
        [Display(Name = "X")]
        [Order]
        public float X { get; set; }

        [Required(ErrorMessage = "Introduzca: Y")]
        [Display(Name = "Y")]
        [Order]
        public float Y { get; set; }

        public PaisesBase() { }

        public PaisesBase(Paises source)
        {
            CopyFrom(source);
        }

        //public string Descripcion
        //{
        //    get
        //    {
        //        return Nombre;
        //    }

        //    set
        //    {
        //        ;
        //    }
        //}
    }

    public class PaisesVM : PaisesBase, ICopyRecord
    {
        public String Idioma { get; set; }

        public PaisesVM() : base()
        {
        }
        public PaisesVM(Paises source) : base(source)
        {
        }
        public override ICopyRecord CopyFrom<T>(T t, int deep = 1)
        {
            base.CopyFrom(t);
            var source = t as Paises;
            if (source != null)
            {
                Idioma = source.Idiomas.Codigo;
            }
            return this;
        }
    }

    public partial class PaisesMetadata : PaisesBase
    {
        PaisesMetadata(Paises source) : base(source)
        {
        }

        [Display(Name = "Idiomas")]
        public Idiomas Idiomas { get; set; }

        [Display(Name = "Provincias")]
        public Provincias Provincias { get; set; }

    }
}
