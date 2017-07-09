using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebGestion.Controllers
{
    public static class Common
    {
        /// <summary>
        /// Clones any object and returns the new cloned object.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <param name="source">The original object.</param>
        /// <returns>The clone of the object.</returns>
        public static T Clone<T>(this T source)
        {
            var dcs = new DataContractSerializer(typeof(T));
            using (var ms = new System.IO.MemoryStream())
            {
                dcs.WriteObject(ms, source);
                ms.Seek(0, System.IO.SeekOrigin.Begin);
                return (T)dcs.ReadObject(ms);
            }
        }

        public static void Each<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
                action(item);
        }
        public static void AddNotNull<T>(this ICollection<T> items, T item)
        {
            if (item != null)
                items.Add(item);
        }
    }

    static class ObjectExtensions
    {
        public static TResult GetByName<TResult>(this object @this, string propertyName)
        {
            return (TResult)@this.GetType().GetProperty(propertyName).GetValue(@this, null);
        }

        public static TResult GetByIndex<TResult>(this object @this, int index)
        {
            return (TResult)@this.GetType().GetProperties()[index].GetValue(@this, null);
        }
        public static string CSharpName(this Type type)
        {
            var sb = new StringBuilder();
            var name = type.Name;
            if (!type.IsGenericType) return name;
            sb.Append(name.Substring(0, name.IndexOf('`')));
            sb.Append("<");
            sb.Append(string.Join(", ", type.GetGenericArguments()
                                            .Select(t => t.CSharpName())));
            sb.Append(">");
            return sb.ToString();
        }
    }
}
