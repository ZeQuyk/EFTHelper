using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeFromTarkov.Utility.Extensions
{
    public static class StringExtensions
    {
        public static string ToSentence(this string Input)
        {
            // https://stackoverflow.com/questions/272633/add-spaces-before-capital-letters
            return new string(Input.SelectMany((c, i) => i > 0 && char.IsUpper(c) ? new[] { ' ', c } : new[] { c }).ToArray()).TrimStart(' ');
        }
    }
}
