using System.Linq;
using Cobilas.Collections;

namespace Cobilas.Numeric.Convert {
    public static class String_Hexadecimal_Extension {
        public static bool IsHexadecimal(this string str)
            => str.ToUpper().All((c) => {
                switch (c) {
                    case '0': case '1': case '2':
                    case '3': case '4': case '5':
                    case '6': case '7': case '8':
                    case '9': case 'A': case 'B':
                    case 'C': case 'D': case 'E':
                    case 'F': case 'X': return true;
                    default: return false;
                }
            });

        public static decimal HexToDecimal(this string str) {
            char[] carac = str.ToUpper().Replace("0X", "").Remove(0, 1).ToCharArray();
            ArrayManipulation.Reverse(carac);
            decimal res = 0;
            for (int I = 0; I < carac.Length; I++)
                res += HexToByte(carac[I]) * BitArray_Binary_Extension.Pow(16, I);
            return res;
        }

        private static byte HexToByte(char c) {
            switch (c) {
                case 'A': case 'a': return 10;
                case 'B': case 'b': return 11;
                case 'C': case 'c': return 12;
                case 'D': case 'd': return 13;
                case 'E': case 'e': return 14;
                case 'F': case 'f': return 15;
                default: return byte.Parse(c.ToString());
            }
        }
    }
}
