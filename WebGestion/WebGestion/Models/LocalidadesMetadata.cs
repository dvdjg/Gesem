using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebGestion.Utils;

namespace WebGestion.Models
{
    [MetadataType(typeof(LocalidadesMetadata))]
    public partial class Localidades : IIdRecord
    {
    }

    public class LocalidadesBase : CopyRecord, IIdRecord
    {
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Introduzca: CodLocalidad")]
        [Display(Name = "CodLocalidad")]
        public int CodLocalidad { get; set; }

        [Required(ErrorMessage = "Seleccione: Provincia")]
        [Display(Name = "Provincia")]
        [AmbientValue(typeof(Provincias), "Id")]
        [Description("Provincia")]
        [DefaultValue(1)]
        public int ProvinciaId { get; set; }

        [Display(Name = "Idioma")]
        [Required(ErrorMessage = "Seleccione: Idioma")]
        [AmbientValue(typeof(Idiomas), "Id")]
        [Description("Idioma")]
        [DefaultValue(1)]
        public int IdiomaId { get; set; }

        [Required(ErrorMessage = "Introduzca: Localidad")]
        [Display(Name = "Localidad")]
        [MaxLength(150)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Introduzca: X")]
        [Display(Name = "X")]
        public float X { get; set; }

        [Required(ErrorMessage = "Introduzca: Y")]
        [Display(Name = "Y")]
        public float Y { get; set; }

        [Required(ErrorMessage = "Introduzca: Exacto")]
        [Display(Name = "Exacto")]
        public bool Exacto { get; set; }

        public LocalidadesBase() { }
        public LocalidadesBase(Localidades source)
        {
            CopyFrom(source);
        }

        //private System.Collections.Generic.IEnumerable<PropertyInfos> m_resultsTo;
        //public virtual ICopyRecord CopyTo<T>(T t)
        //{
        //    m_resultsTo = this.CopyProperties(t, m_resultsTo);
        //    //var dest = t as Localidades;
        //    //if (dest != null)
        //    //{
        //    //    dest.Id = Id;
        //    //    dest.CodLocalidad = CodLocalidad;
        //    //    dest.ProvinciaId = ProvinciaId;
        //    //    dest.IdiomaId = IdiomaId;
        //    //    dest.Nombre = Nombre;
        //    //    dest.X = X;
        //    //    dest.Y = Y;
        //    //    dest.Exacto = Exacto;
        //    //}
        //    return this;
        //}

        //private System.Collections.Generic.IEnumerable<PropertyInfos> m_resultsFrom;
        //public virtual ICopyRecord CopyFrom<T>(T t)
        //{
        //    m_resultsFrom = t.CopyProperties(this, m_resultsFrom);
        //    //var source = t as Localidades;
        //    //if (source != null)
        //    //{
        //    //    Id = source.Id;
        //    //    CodLocalidad = source.CodLocalidad;
        //    //    ProvinciaId = source.ProvinciaId;
        //    //    IdiomaId = source.IdiomaId;
        //    //    Nombre = source.Nombre;
        //    //    X = source.X;
        //    //    Y = source.Y;
        //    //    Exacto = source.Exacto;
        //    //}
        //    return this;
        //}
    }
    // [NonSerialized]

    public class LocalidadesVM : LocalidadesBase, ICopyRecord
    {
        public String Pais { get; set; }
        public String Provincia { get; set; }
        public String Idioma { get; set; }

        public LocalidadesVM() : base()
        {
        }
        public LocalidadesVM(Localidades source) : base(source)
        {
        }
        public override ICopyRecord CopyFrom<T>(T t, int deep = 1) 
        {
            base.CopyFrom(t);
            var source = t as Localidades;
            if (source != null)
            {
                Pais = source.Provincias.Paises.Nombre;
                Provincia = source.Provincias.Nombre;
                Idioma = source.Idiomas.Codigo;
            }
            return this;
        }
    }

    public partial class LocalidadesMetadata : LocalidadesBase
    {
        public LocalidadesMetadata(Localidades source) : base(source)
        {
        }

        [Display(Name = "DireccionesFacturacion")]
        public DireccionesFacturacion DireccionesFacturacion { get; set; }

        [Display(Name = "Idiomas")]
        public Idiomas Idiomas { get; set; }

        [Display(Name = "Locales")]
        public Locales Locales { get; set; }

        [Display(Name = "Provincias")]
        public Provincias Provincias { get; set; }

        [Display(Name = "Personas")]
        public Personas Personas { get; set; }
    }
}
