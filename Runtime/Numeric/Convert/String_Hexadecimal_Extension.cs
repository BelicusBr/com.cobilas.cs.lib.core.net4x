using System.Linq;
using Cobilas.Collections;

namespace Cobilas.Numeric.Convert {
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    public static class String_Hexadecimal_Extension {
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente

        /// <summary>Checks whether the text value is a hexadecimal value.</summary>
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

        /// <summary>Converts hexadecimal value to decimal.</summary>
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
