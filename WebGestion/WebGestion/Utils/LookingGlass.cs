using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;
using System.Reflection;

namespace WebGestion.Utils
{
    // http://mattmc3.blogspot.com.es/2011/03/fun-with-dynamicobject-and-making-net.html
    public class LookingGlass : DynamicObject
    {
        public dynamic TheObject { get; private set; }
        public Type TheObjectType;

        public LookingGlass(dynamic theObject)
        {
            this.TheObject = theObject;
            this.TheObjectType = this.TheObject.GetType();
        }

        public virtual object GetIndex(object[] indexes)
        {
            object result;
            TryGetIndex(null, indexes, out result);
            return result;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            result = null;
            int index = (int)indexes[0];
            var value = this.TheObject[index];
            result = new LookingGlass(value);
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return TryGetMember(binder.Name, out result);
        }

        public virtual object GetMember(string name)
        {
            object result;
            TryGetMember(name, out result);
            return result;
        }

        public virtual object GetMemberValue(string name)
        {
            var field = GetField(name);
            if (field != null)
            {
                return field.GetValue(this.TheObject);
            }

            var prop = GetProperty(name);
            if (prop != null)
            {
                return prop.GetValue(this.TheObject, null);
            }
            return null;
        }

        public virtual bool TryGetMember(string name, out object result)
        {
            result = null;

            var field = GetField(name);
            if (field != null)
            {
                var value = field.GetValue(this.TheObject);
                result = new LookingGlass(value);
                return true;
            }

            var prop = GetProperty(name);
            if (prop != null)
            {
                var value = prop.GetValue(this.TheObject, null);
                result = new LookingGlass(value);
                return true;
            }

            return false;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            return TrySetMember(binder.Name, value);
        }

        public virtual void SetMember(string name, object value)
        {
            TrySetMember(name, value);
        }

        public virtual bool TrySetMember(string name, object value)
        {
            var field = GetField(name);
            if (field != null)
            {
                field.SetValue(this.TheObject, value);
                return true;
            }

            var prop = GetProperty(name);
            if (prop != null)
            {
                prop.SetValue(this.TheObject, value);
                return true;
            }

            return false;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            return TryInvokeMember(binder.Name, args, out result);
        }

        public virtual object InvokeMember(string name, object[] args)
        {
            object result;
            TryInvokeMember(name, args, out result);
            return result;
        }
        
        public virtual bool TryInvokeMember(string name, object[] args, out object result)
        {
            result = null;
            var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod;
            var value = this.TheObjectType.InvokeMember(name, flags, Type.DefaultBinder, this.TheObject, args);
            if (value != null)
            {
                result = new LookingGlass(value);
            }
            return true;
        }

        private FieldInfo GetField(string fieldName)
        {
            var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var field = this.TheObjectType.GetField(fieldName, flags);
            return field;
        }

        private PropertyInfo GetProperty(string propertyName)
        {
            var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var prop = this.TheObjectType.GetProperty(propertyName, flags);
            return prop;
        }

        public override string ToString()
        {
            return this.TheObject.ToString();
        }
    }
}
