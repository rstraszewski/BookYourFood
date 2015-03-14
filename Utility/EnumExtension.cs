using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class EnumExtension
    {

        public static string GetDescription(this Enum value)
        {
            var attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttribute<DescriptionAttribute>();

            
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
