using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("Enum is null");
            }

            FieldInfo? fi = value.GetType().GetField(value.ToString());
            if (null == fi)
                throw new ArgumentNullException("FieldInfo is null");

            DescriptionAttribute[] attributes =
                  (DescriptionAttribute[])fi.GetCustomAttributes(
                  typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }
    }
}
