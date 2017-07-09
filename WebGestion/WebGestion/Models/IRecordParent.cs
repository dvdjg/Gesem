using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGestion.Models
{
    public interface IRecordParent : IIdRecord
    {
        int PadreId { get; set; }
    }
}
