using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPCReaderWriter
{
    public static class HexGenerator
    {
        private static readonly string[] _hexadecimalValues = new string[] {"1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "A", "B", "C", "D", "E", "F"};

        public static string GetNewHexadecimalValue()
        {
            Random random = new Random();
            var newHexadecimalOrder = _hexadecimalValues.OrderBy(x => random.Next());
            var prefix = DateTime.Now.ToString("yyyyMMdd");
            var newHexadecimalOrderString = string.Join(string.Empty, newHexadecimalOrder.ToArray());

            return prefix + newHexadecimalOrderString.Substring(0, 12);
        }
    }
}