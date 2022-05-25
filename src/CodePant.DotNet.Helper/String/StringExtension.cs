using System;
using Newtonsoft.Json.Serialization;

namespace CodePant.DotNet.Helper.String
{
    public static class StringExtension
    {
        private static readonly SnakeCaseNamingStrategy SnakeCaseNamingStrategy = new();
        public static string ToSnakeCase(this string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(paramName: nameof(text));
            }

            return SnakeCaseNamingStrategy.GetPropertyName(text, false);
        }
    }
}
