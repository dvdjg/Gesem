using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebGestion.Models
{
    public abstract class BaseModel
    {
        abstract public void Copy<T>(T source);
        abstract public void Update<T>(out T dest);
    }
}
