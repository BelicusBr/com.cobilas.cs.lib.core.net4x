using Cobilas.Collections;
using System.Collections.Generic;

namespace System;
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
public static class Enum_CB_Extension {
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente

    /// <summary>
    /// Determines whether one or more bit fields are set in the current instance.
    /// </summary>
    /// <param name="E">The target enumerator.</param>
    /// <param name="flags">Comparison tags.</param>
    public static bool HasFlag(this Enum E, params Enum[] flags) {
        for (int I = 0; I < ArrayManipulation.ArrayLength(flags); I++)
            if (E.HasFlag(flags[I]))
                return true;
        return false;
    }

    /// <summary>
    /// Gets the tag with the current assigned value.
    /// </summary>
    /// <param name="E">The target enumerator.</param>
    public static KeyValuePair<string, int> GetEnumPair(this Enum E) {
        KeyValuePair<string, int>[] pairs = GetEnumPairs(E);
        if (E.ToString().Contains(',')) {
            string[] flags = E.ToString().Split(',');
            int res = 0;
            for (int A = 0; A < flags.Length; A++)
                for (int B = 0; B < pairs.Length; B++)
                    if (pairs[B].Key == flags[A].Trim())
                        res |= pairs[B].Value;
            return new KeyValuePair<string, int>(E.ToString(), res);
        } else {
            for (int I = 0; I < ArrayManipulation.ArrayLength(pairs); I++)
                if (pairs[I].Key == Enum.GetName(E.GetType(), E))
                    return pairs[I];
        }
        return default;
    }

    /// <summary>
    /// Gets a list of all tags along with their assigned values.
    /// </summary>
    /// <param name="E">The target enumerator.</param>
    /// <exception cref="System.ArgumentNullException">enumType is null.</exception>
    /// <returns>Returns a list of all tags along with their assigned values or an empty list.</returns>
    public static KeyValuePair<string, int>[] GetEnumPairs(this Enum E) {
        Array array = Enum.GetValues(E.GetType());
        KeyValuePair<string, int>[] Res = Array.Empty<KeyValuePair<string, int>>();
        for (int I = 0; I < ArrayManipulation.ArrayLength(array); I++)
            ArrayManipulation.Add(new KeyValuePair<string, int>(
                array.GetValue(I).ToString(),
                (int)array.GetValue(I)
                ), ref Res);
        return Res;
    }

    /// <summary>
    /// Retrieves the name of the constant in the specified enumeration that has the specified value.
    /// </summary>
    /// <param name="E">The target enumerator.</param>
    /// <param name="value">The value of a particular enumerated constant in terms of its underlying type.</param>
    /// <exception cref="System.ArgumentNullException">enumType is null.</exception>
    /// <exception cref="System.ArgumentException">enumType is not an <see cref="System.Enum"/>. -or- value is neither of type enumType nor does
    /// it have the same underlying type as enumType.
    /// </exception>
    /// <returns>A <see cref="string"/> containing the name of the enumerated constant in enumType whose value is value; or null if no such constant is found.</returns>
    public static string GetName(this Enum E, object value)
        => Enum.GetName(E.GetType(), value);

    /// <summary>
    /// Retrieves the name of the constant in the specified enumeration that has the specified value.
    /// </summary>
    /// <param name="E">The target enumerator.</param>
    /// <exception cref="System.ArgumentNullException">enumType is null.</exception>
    /// <exception cref="System.ArgumentException">enumType is not an <see cref="System.Enum"/>. -or- value is neither of type enumType nor does
    /// it have the same underlying type as enumType.
    /// </exception>
    /// <returns>A string containing the name of the enumerated constant in enumType whose value is value; or null if no such constant is found.</returns>
    public static string GetName(this Enum E)
        => GetName(E, E);

    /// <summary>
    /// Retrieves an array of the names of the constants in a specified enumeration.
    /// </summary>
    /// <param name="E">The target enumerator.</param>
    /// <exception cref="System.ArgumentNullException">enumType is null.</exception>
    /// <exception cref="System.ArgumentException">enumType parameter is not an <see cref="System.Enum"/>.</exception>
    /// <returns>A string array of the names of the constants in enumType.</returns>
    public static string[] GetNames(this Enum E)
        => Enum.GetNames(E.GetType());

    /// <summary>
    /// Converts the specified value of a specified enumerated type to its equivalent <see cref="string"/> representation according to the specified format.
    /// </summary>
    /// <param name="E">The target enumerator.</param>
    /// <param name="value">The value to convert.</param>
    /// <param name="format">The output format to use.</param>
    /// <exception cref="System.ArgumentNullException">The enumType, value, or format parameter is null.</exception>
    /// <exception cref="System.ArgumentException">The enumType parameter is not an <see cref="System.Enum"/> type. -or- The value is from an
    /// enumeration that differs in type from enumType. -or- The type of value is not
    /// an underlying type of enumType.
    /// </exception>
    /// <exception cref="System.FormatException">The format parameter contains an invalid value.</exception>
    /// <exception cref="System.InvalidOperationException">format equals "X", but the enumeration type is unknown.</exception>
    /// <returns>A string representation of value.</returns>
    public static string Format(this Enum E, object value, string format)
        => Enum.Format(E.GetType(), value, format);
}
