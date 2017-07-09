using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebGestion.Models
{
    [MetadataType(typeof(HistoricosMetadata))]
    public partial class Historicos : IIdRecord
    {
    }

    public class HistoricosBase : CopyRecord, IIdRecord
    {
        public HistoricosBase() { }
        public HistoricosBase(Historicos source)
        {
            CopyFrom(source);
        }

        [Key]
        [ReadOnly(true)]
        [Required(ErrorMessage = "Introduzca: Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Nombre", Prompt = "_nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Archivo")]
        public byte[] Archivo { get; set; }

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Seleccione: Usuario")]
        [AmbientValue(typeof(AspNetUsers), "Id")]
        [Description("Usuario")]
        [DefaultValue(1)]
        public int UsuarioId { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "Seleccione: Tipo")]
        [AmbientValue(typeof(TiposPropiedades), "Id")]
        [Description("Tipo")]
        [DefaultValue(1)]
        public int TipoId { get; set; }

        [Required(ErrorMessage = "Seleccione: Estado")]
        [Display(Name = "Estado")]
        [AmbientValue(typeof(Estados), "Id")]
        [Description("Estado del usuario")]
        [DefaultValue(1)]
        public int EstadoId { get; set; }

        [Display(Name = "Fecha")]
        [DataType(DataType.DateTime)]
        public DateTime Fecha { get; set; }

        [Display(Name = "Fecha Fin")]
        [DataType(DataType.DateTime)]
        public DateTime? FechaFin { get; set; }
    }

    public class HistoricosVM : HistoricosBase, IIdRecord, ICopyRecord
    {
        public HistoricosVM() { }
        public HistoricosVM(Historicos source) : base(source) { }
    }

    public partial class HistoricosMetadata : HistoricosBase
    {
        public HistoricosMetadata() { }
        public HistoricosMetadata(Historicos source) : base(source) { }

        [Display(Name = "Estados")]
        public Estados Estados { get; set; }

        [Display(Name = "TiposPropiedades")]
        public TiposPropiedades TiposPropiedades { get; set; }

        public virtual ICollection<Bienes> Bienes { get; set; }

        public virtual ICollection<FacturasClientes> FacturasClientes { get; set; }

        public virtual ICollection<FacturasProveedores> FacturasProveedores { get; set; }

        public virtual ICollection<Familias> Familias { get; set; }

        public virtual ICollection<PedidosClientes> PedidosClientes { get; set; }

        public virtual ICollection<PedidosProveedores> PedidosProveedores { get; set; }

        public virtual ICollection<Personas> Personas { get; set; }

        public virtual ICollection<PresupuestosClientes> PresupuestosClientes { get; set; }
    }
}
