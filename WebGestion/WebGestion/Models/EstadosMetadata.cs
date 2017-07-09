using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebGestion.Utils;

namespace WebGestion.Models
{
    public class EstadosBase : CopyRecord, IRecordParent, IRecord
    {
        //[Required(ErrorMessage = "Introduzca: Id")]
        [Key]
        [ReadOnly(true)]
        [DefaultValue(-1)]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Introduzca el nombre del Estado")]
        [Display(Name = "Estado")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Padre")]
        [AmbientValue(typeof(Estados), "Id")]
        [Description("Estado Padre")]
        [DefaultValue(1)]
        public int PadreId { get; set; }

        public EstadosBase() { PadreId = 1; }
        public EstadosBase(Estados source)
        {
            CopyFrom(source);
        }
    }
    // [NonSerialized]

    public class EstadosVM : EstadosBase, ICopyRecord
    {
        public EstadosVM() :base() { }
        public EstadosVM(Estados source) :base(source) { }
    }

    [MetadataType(typeof(EstadosMetadata))]
    public partial class Estados : IIdRecord
    {
    }

    public partial class EstadosMetadata : EstadosBase
    {
        public EstadosMetadata(Estados source) :base(source)
        {
        }

        [Display(Name = "Bienes")]
        public Bienes Bienes { get; set; }

        [Display(Name = "AspNetRoles")]
        public AspNetRoles AspNetRoles { get; set; }

        [Display(Name = "AspNetUsers")]
        public AspNetUsers AspNetUsers { get; set; }

        [Display(Name = "Cargos")]
        public Cargos Cargos { get; set; }

        [Display(Name = "Clientes")]
        public Clientes Clientes { get; set; }

        [Display(Name = "DireccionesFacturacion")]
        public DireccionesFacturacion DireccionesFacturacion { get; set; }

        [Display(Name = "Empleados")]
        public Empleados Empleados { get; set; }

        [Display(Name = "Empresas")]
        public Empresas Empresas { get; set; }

        [Display(Name = "FacturasClientes")]
        public FacturasClientes FacturasClientes { get; set; }

        [Display(Name = "FacturasProveedores")]
        public FacturasProveedores FacturasProveedores { get; set; }

        [Display(Name = "Familias")]
        public Familias Familias { get; set; }

        [Display(Name = "FormasEntrega")]
        public FormasEntrega FormasEntrega { get; set; }

        [Display(Name = "FormasPago")]
        public FormasPago FormasPago { get; set; }

        [Display(Name = "Historicos")]
        public Historicos Historicos { get; set; }

        [Display(Name = "IRPFs")]
        public IRPFs IRPFs { get; set; }

        [Display(Name = "IVAs")]
        public IVAs IVAs { get; set; }

        [Display(Name = "Locales")]
        public Locales Locales { get; set; }

        [Display(Name = "PedidosClientes")]
        public PedidosClientes PedidosClientes { get; set; }

        [Display(Name = "PedidosProveedores")]
        public PedidosProveedores PedidosProveedores { get; set; }

        [Display(Name = "Personas")]
        public Personas Personas { get; set; }

        [Display(Name = "PresupuestosClientes")]
        public PresupuestosClientes PresupuestosClientes { get; set; }

        [Display(Name = "Proveedores")]
        public Proveedores Proveedores { get; set; }

        [Display(Name = "Estados1")]
        public Estados Estados1 { get; set; }

        [Display(Name = "Estados2")]
        public Estados Estados2 { get; set; }

    }
}
