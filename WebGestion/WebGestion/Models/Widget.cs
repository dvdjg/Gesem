using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGestion.Models
{
    public class Widget
    {
        public int ID { get; set; }
        public string Color { get; set; }
        public string Shape { get; set; }

        public override string ToString()
        {
            return string.Format("ID {0}: {1} {2}", ID, Color, Shape);
        }
    }
}
