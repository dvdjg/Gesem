using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WebGestion.Utils
{
    public class PropertyInfos
    {
        public PropertyInfo sourceProperty, targetProperty;
    }

    /// <summary>
    /// A static class for reflection type functions
    /// </summary>
    public static class Reflection
    {
        /// <summary>
        /// Extension for 'Object' that copies the properties to a destination object.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        public static System.Collections.Generic.IEnumerable<PropertyInfos>
            CopyProperties(this object source, object destination, System.Collections.Generic.IEnumerable<PropertyInfos> results = null)
        {
            // If any this null throw an exception
            if (source == null || destination == null)
                throw new Exception("Source or/and Destination Objects are null");
            if (results == null)
            {
                // Getting the Types of the objects
                Type typeDest = destination.GetType();
                Type typeSrc = source.GetType();
                // Collect all the valid properties to map
                results = from srcProp in typeSrc.GetProperties()
                          let targetProperty = typeDest.GetProperty(srcProp.Name)
                          where srcProp.CanRead
                          && targetProperty != null
                          && (targetProperty.GetSetMethod(true) != null && !targetProperty.GetSetMethod(true).IsPrivate)
                          && (targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) == 0
                          && targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType)
                          select new PropertyInfos { sourceProperty = srcProp, targetProperty = targetProperty };
            }
            //map the properties
            foreach (var props in results)
            {
                props.targetProperty.SetValue(destination, props.sourceProperty.GetValue(source, null), null);
            }
            return results;
        }

        public static Type GetEnumerableType(Type type)
        {
            if (type == null)
                return null; // throw new ArgumentNullException("type");

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                return type.GetGenericArguments()[0];

            var iface = (from i in type.GetInterfaces()
                         where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                         select i).FirstOrDefault();

            if (iface == null)
                return null; // throw new ArgumentException("Does not represent an enumerable type.", "type");

            return GetEnumerableType(iface);
        }
    }
}
