using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGestion.Models
{
    public class EstadosModel
    {
        public EstadosModel()
        {
            ItemToAdd = new Item();
        }
        public Item ItemToAdd { get; set; }
        public List<Item> Items { get; set; }

        public void AddItem()
        {
            Items.Add(ItemToAdd);
            ItemToAdd = new Item();
        }
    }
}
