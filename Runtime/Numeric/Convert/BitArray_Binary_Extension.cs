using System.Collections;

namespace Cobilas.Numeric.Convert {
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    public static class BitArray_Binary_Extension {
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente

        /// <summary>Transforms a <see cref="BitArray"/> into a <see cref="byte"/> list.</summary>
        public static byte[] GetBinaryArray(this BitArray array) {
            byte[] res = new byte[array.Length];
            for (int I = 0; I < array.Length; I++)
                res[I] = (byte)(array.Get(I) ? 1 : 0);
            return res;
        }

        /// <summary>Defines a list of bytes in the <see cref="BitArray"/> object.</summary>
        public static void SetBinaryArray(this BitArray array, byte[] binaryArray) {
            for (int I = 0; I < binaryArray.Length; I++)
                array.Set(I, binaryArray[I] == 1);
        }

        /// <summary>
        /// Convert <see cref="BitArray"/> to <see cref="decimal"/>.
        /// </summary>
        public static decimal BitToDecimal(this BitArray array) {
            byte[] bits = GetBinaryArray(array);
            decimal res = 0;
            for (int I = 0; I < bits.Length; I++)
                res += bits[I] * Pow(2, I);
            return res;
        }

        internal static decimal Pow(decimal x, decimal y) {
            if (y <= 0) return 1;
            decimal I_Res = x;
            for (int I = 1; I < y; I++)
                I_Res *= x;
            return I_Res;
        }
    }
}
