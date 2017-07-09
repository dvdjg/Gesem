using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGestion.Models
{
    /// <summary>
    /// Clase para almacenar modelos genéricos
    /// ╔══════════════════╦══════════════╤════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗
    /// ║ Type             ║ Name         │ Description                                                                                                                    ║
    /// ╠══════════════════╬══════════════╪════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╣
    /// ║ Boolean          ║ active       │ (Initialization only, but will not be stored with the node.)                                                                   ║
    /// ╟──────────────────╫──────────────┼────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────╢
    /// ║ NodeData[]       ║ children     │ Optional array of child nodes.                                                                                                 ║
    /// ╟──────────────────╫──────────────┼────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────╢
    /// ║ object           ║ data         │ All unknown properties from constructor will be copied to `node.data`.                                                         ║
    /// ╟──────────────────╫──────────────┼────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────╢
    /// ║ Boolean          ║ expanded     │ Initial expansion state. Use `node.setExpanded()` or `node.isExpanded()` to access.                                            ║
    /// ╟──────────────────╫──────────────┼────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────╢
    /// ║ String           ║ extraClasses │ Class names added to the node markup (separate with space).                                                                    ║
    /// ╟──────────────────╫──────────────┼────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────╢
    /// ║ Boolean          ║ focus        │ (Initialization only, but will not be stored with the node.)                                                                   ║
    /// ╟──────────────────╫──────────────┼────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────╢
    /// ║ Boolean          ║ folder       │ Folders have different default icons and honor the `clickFolderMode` option.                                                   ║
    /// ╟──────────────────╫──────────────┼────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────╢
    /// ║ Boolean          ║ hideCheckbox │ Remove checkbox for this node.                                                                                                 ║
    /// ╟──────────────────╫──────────────┼────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────╢
    /// ║ Boolean | String ║ icon         │ Boolean value overrides the tree option of the same name. A string value is used as `src` attribute for a <img> tag.           ║
    /// ╟──────────────────╫──────────────┼────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────╢
    /// ║ String           ║ iconclass    │ Class names added to the node icon markup to allow custom icons or glyphs (separate with space, e.g. "ui-icon ui-icon-heart"). ║
    /// ║                  ║              │ If specified, an additonal `fancytree-custom-icon` class is added instead of `fancytree-icon`.                                 ║
    /// ╟──────────────────╫──────────────┼────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────╢
    /// ║ String           ║ key          │ Unique key for this node (auto-generated if omitted)                                                                           ║
    /// ╟──────────────────╫──────────────┼────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────╢
    /// ║ Boolean          ║ lazy         │ Lazy folders call the `lazyLoad` on first expand to load their children.                                                       ║
    /// ╟──────────────────╫──────────────┼────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────╢
    /// ║ String           ║ refKey       │ (Reserved, used by 'clones' extension.)                                                                                        ║
    /// ╟──────────────────╫──────────────┼────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────╢
    /// ║ Boolean          ║ selected     │ Initial selection state. Use `node.setSelected()` or `node.isSelected()` to access.                                            ║
    /// ╟──────────────────╫──────────────┼────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────╢
    /// ║ String           ║ title        │ Node text (may contain HTML tags). Use `node.setTitle()` to modify.                                                            ║
    /// ╟──────────────────╫──────────────┼────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────╢
    /// ║ String           ║ tooltip      │ Will be added as `title` attribute, thus enabling a tooltip.                                                                   ║
    /// ╟──────────────────╫──────────────┼────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────╢
    /// ║ Boolean          ║ unselectable │ Prevent selection using mouse or keyboard.                                                                                     ║
    /// ╟──────────────────╫──────────────┼────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────────╢
    /// ║ any              ║ OTHER        │ Attributes other than listed above will be copied to `node.data`.                                                              ║
    /// ╚══════════════════╩══════════════╧════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝
    /// </summary>
    public class Item
    {
        public Item()
        {
            Id = default(int);
            Nombre = "Nombre";
            Desc = "Descripción";
            Numero = default(double);
            PadreId = default(int);
            FechaInicio = DateTime.Now;
            FechaFin = default(DateTime);
        }
        public int Id { get; set; } // key
        public string Nombre { get; set; } // title
        public string Desc { get; set; } // tooltip
        public double Numero { get; set; }
        public int PadreId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
    //public class Item
    //{
    //    public Item()
    //    {
    //        Id = default(int);
    //        Nombre = "Nombre";
    //        Desc = "Descripción";
    //        Numero = default(double);
    //        PadreId = default(int);
    //        FechaInicio = DateTime.Now;
    //        FechaFin = default(DateTime);
    //    }
    //    public int Id { get; set; }
    //    public string Nombre { get; set; }
    //    public string Desc { get; set; }
    //    public double Numero { get; set; }
    //    public int PadreId { get; set; }
    //    public DateTime FechaInicio { get; set; }
    //    public DateTime FechaFin { get; set; }
    //}
}
