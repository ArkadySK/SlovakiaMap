using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlovakiaMap.Tools
{
    public class ReflectionTools
    {
        public static object GetPropValue(object src, string propName)
        {
            if (src is null || string.IsNullOrEmpty(propName))
                throw new ArgumentException("source object or property name is wrong!");

            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
