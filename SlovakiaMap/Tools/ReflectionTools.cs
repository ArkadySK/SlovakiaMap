using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlovakiaMap.Tools
{
    public class ReflectionTools
    {
        public static object? GetPropValue(object? src, string propName)
        {
            Type? type = src?.GetType();
            if (type == null)
                throw new ArgumentException("source object is null");
            if (string.IsNullOrEmpty(propName))
                throw new ArgumentException("source object or property name is wrong!");

            return type.GetProperty(propName)?.GetValue(src, null);
        }
    }
}
