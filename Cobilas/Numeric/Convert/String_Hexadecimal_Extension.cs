using System.Linq;
using Cobilas.Collections;

namespace Cobilas.Numeric.Convert;
/// <summary>This extension allows <seealso cref="string"/> to Hexadecimal conversion.</summary>
public static class String_Hexadecimal_Extension {

    /// <summary>Checks whether the text value is a hexadecimal value.</summary>
    public static bool IsHexadecimal(this string str)
        => str.ToUpper().All((c) => {
            return c switch {
                '0' or '1'
                or '2' or 
                '3' or '4' 
                or '5' or 
                '6' or '7' 
                or '8' or 
                '9' or 'A' 
                or 'B' or 
                'C' or 'D' 
                or 'E' or 
                'F' or 'X' => true,
                _ => false,
            };
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
        return c switch {
            'A' or 'a' => 10,
            'B' or 'b' => 11,
            'C' or 'c' => 12,
            'D' or 'd' => 13,
            'E' or 'e' => 14,
            'F' or 'f' => 15,
            _ => byte.Parse(c.ToString()),
        };
    }
}
