using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGestion.Models.Hubs
{
    class PantallaPaises
    {
        public string NuevoPais { get; set; }
        //public Idiomas Idioma { get; set; }
        public Idiomas CodIdioma { get; set; }
        public List<Idiomas> CodIdiomas { get; set; }
        public Paises Pais { get; set; }
    }
}
