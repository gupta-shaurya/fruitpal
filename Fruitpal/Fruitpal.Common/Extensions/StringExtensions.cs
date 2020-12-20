using System.Collections.Generic;
using System.Linq;

namespace Fruitpal.Common.Extensions
{
    public static class StringExtensions
    {
        #region Public Methods

        public static string Replace(this string source, Dictionary<string, object> values)
        {
            return values.Aggregate(
                source,
                (current, parameter) => current
                    .Replace($"{{{parameter.Key}}}", parameter.Value.ToString()));
        }

        #endregion Public Methods
    }
}