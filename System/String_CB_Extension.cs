﻿using Cobilas.Collections;
using System.Globalization;

namespace System;
/// <summary>Grants more methods to <seealso cref="string"/> class.</summary>
public static class String_CB_Extension {
    /// <summary>Returns a value indicating whether a specified substring occurs within this string.</summary>
    public static bool Contains(this string S, params char[] value) {
        for (int I = 0; I < ArrayManipulation.ArrayLength(value); I++)
            if (S.Contains(value[I].ToString()))
                return true;
        return false;
    }
    /// <inheritdoc cref="Contains(string, char[])"/>
    public static bool Contains(this string S, params string[] values) {
        for (int I = 0; I < ArrayManipulation.ArrayLength(values); I++)
            if (S.Contains(values[I]))
                return true;
        return false;
    }
    /// <summary>Converts a <seealso cref="string"/> to a <seealso cref="Guid"/>.</summary>
    /// <param name="S">Receives a string that will be converted to a GUID.</param>
    /// <returns>Returns a string already converted to GUID.</returns>
    public static Guid ToGuid(this string S) {
        byte[] bytes = new byte[16];
        for (int I = 0; I < S.Length; I++)
            bytes[I % 16] ^= (byte)S[I];
        return new(bytes);
    }
#pragma warning disable CS1591
    public static sbyte ToSByte(this string S, NumberStyles style, IFormatProvider formatProvider)
        => sbyte.Parse(S, style, formatProvider);

    public static sbyte ToSByte(this string S, IFormatProvider formatProvider)
        => sbyte.Parse(S, formatProvider);

    public static sbyte ToSByte(this string S)
        => sbyte.Parse(S);

    public static short ToShort(this string S, NumberStyles style, IFormatProvider formatProvider)
        => short.Parse(S, style, formatProvider);

    public static short ToShort(this string S, IFormatProvider formatProvider)
        => short.Parse(S, formatProvider);

    public static short ToShort(this string S)
        => short.Parse(S);

    public static int ToInt(this string S, NumberStyles style, IFormatProvider formatProvider)
        => int.Parse(S, style, formatProvider);

    public static int ToInt(this string S, IFormatProvider formatProvider)
        => int.Parse(S, formatProvider);

    public static int ToInt(this string S)
        => int.Parse(S);

    public static long ToLong(this string S, NumberStyles style, IFormatProvider formatProvider)
        => long.Parse(S, style, formatProvider);

    public static long ToLong(this string S, IFormatProvider formatProvider)
        => long.Parse(S, formatProvider);

    public static long ToLong(this string S)
        => long.Parse(S);

    public static byte ToByte(this string S, NumberStyles style, IFormatProvider formatProvider)
        => byte.Parse(S, style, formatProvider);

    public static byte ToByte(this string S, IFormatProvider formatProvider)
        => byte.Parse(S, formatProvider);

    public static byte ToByte(this string S)
        => byte.Parse(S);

    public static ushort ToUShort(this string S, NumberStyles style, IFormatProvider formatProvider)
        => ushort.Parse(S, style, formatProvider);

    public static ushort ToUShort(this string S, IFormatProvider formatProvider)
        => ushort.Parse(S, formatProvider);

    public static ushort ToUShort(this string S)
        => ushort.Parse(S);

    public static uint ToUInt(this string S, NumberStyles style, IFormatProvider formatProvider)
        => uint.Parse(S, style, formatProvider);

    public static uint ToUInt(this string S, IFormatProvider formatProvider)
        => uint.Parse(S, formatProvider);

    public static uint ToUInt(this string S)
        => uint.Parse(S);

    public static ulong ToULong(this string S, NumberStyles style, IFormatProvider formatProvider)
        => ulong.Parse(S, style, formatProvider);

    public static ulong ToULong(this string S, IFormatProvider formatProvider)
        => ulong.Parse(S, formatProvider);

    public static ulong ToULong(this string S)
        => ulong.Parse(S);

    public static float ToFloat(this string S, NumberStyles style, IFormatProvider formatProvider)
        => float.Parse(S, style, formatProvider);

    public static float ToFloat(this string S, IFormatProvider formatProvider)
        => float.Parse(S, formatProvider);

    public static float ToFloat(this string S)
        => float.Parse(S);

    public static double ToDouble(this string S, NumberStyles style, IFormatProvider formatProvider)
        => double.Parse(S, style, formatProvider);

    public static double ToDouble(this string S, IFormatProvider formatProvider)
        => double.Parse(S, formatProvider);

    public static double ToDouble(this string S)
        => double.Parse(S);

    public static decimal ToDecimal(this string S, NumberStyles style, IFormatProvider formatProvider)
        => decimal.Parse(S, style, formatProvider);

    public static decimal ToDecimal(this string S, IFormatProvider formatProvider)
        => decimal.Parse(S, formatProvider);

    public static decimal ToDecimal(this string S)
        => decimal.Parse(S);
#pragma warning restore CS1591
}
