using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace CodePant.DotNet.Helper.EnumHelper
{
    public static class EnumExtension
    {

        /// <summary>
        /// Provides Description Attribute Value from Enum
        ///  <seealso href="https://codereview.stackexchange.com/a/157981">Code Answer From t3chb0t</seealso>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            return
                value
                    .GetType()
                    .GetMember(value.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<DescriptionAttribute>()
                    ?.Description
                ?? value.ToString();
        }
    }
}
