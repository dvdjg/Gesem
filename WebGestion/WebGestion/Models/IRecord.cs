using System.Data.Entity;
using WebGestion.Utils;

namespace WebGestion.Models
{
    public interface IIdRecord
    {
        int Id { get; set;  }
    }

    public interface ICopyRecord
    {
        ICopyRecord CopyTo<T>(DbContext context, T t, int deep = 1);
        ICopyRecord CopyFrom<T>(T t, int deep = 1);
    }

    public interface IRecord : IIdRecord
    {
        string Nombre { get; set; }
        string Descripcion { get; set; }
    }

    public class CopyRecord : ICopyRecord
    {
        private System.Collections.Generic.IEnumerable<PropertyInfos> m_resultsTo;
        public virtual ICopyRecord CopyTo<T>(DbContext context, T t, int deep = 1)
        {
            m_resultsTo = this.CopyProperties(t, m_resultsTo);
            return this;
        }

        private System.Collections.Generic.IEnumerable<PropertyInfos> m_resultsFrom;
        public virtual ICopyRecord CopyFrom<T>(T t, int deep = 1)
        {
            m_resultsFrom = t.CopyProperties(this, m_resultsFrom);
            return this;
        }
    }
}
