using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcTables.Configuration;
using WebGestion.Models;

namespace WebGestion.App_Start
{
    class ProvinciasTable : MvcTable<Provincias>
    {
        public override void Configure(IStaticTableConfiguration<Provincias> config)
        {
            config
                .SetAction("ListInvoices", "Gesem")
                .SetCssClass("table table-striped")
                .SetDefaultSortColumn(m => m.Nombre, true)
                .DisplayForColumn(m => m.Nombre)
                .DisplayForColumn(m => m.Paises.Nombre)
                .DisplayForColumn(m => m.Idiomas.Codigo)
                .ConfigurePagingControl(p => p.SetContainerCssClass("pagination").SetPageSizes(100, 500, 1000));
        }
    }
}
