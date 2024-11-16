using System.Text;

namespace System.Security.Cryptography; 
/// <summary>
/// The HashString structure can be used to transform a list of bytes with a minimum length of 16 into a 
/// <see cref="string"/> or <see cref="Guid"/>.
/// </summary>
public readonly struct HashString : IEquatable<HashString>, IComparable<HashString>, IEquatable<string>, IFormattable {
    private readonly byte[] hash;

    /// <summary>
    /// A read-only instance of type <see cref="HashString"/> whose value is all zero.
    /// </summary>
    public readonly static HashString Empty = new(new byte[16]);

    /// <summary>
    /// Initializes a <see cref="HashString"/> object using a list of bytes with a minimum length of 16.
    /// </summary>
    /// <param name="hash"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public HashString(byte[] hash) {
        if (hash is null) throw new ArgumentNullException("The hash parameter cannot be null.");
        else if (hash.Length < 16) throw new ArgumentException("The hash parameter cannot have a length shorter than 16.");
        this.hash = hash;
    }

    /// <summary>
    /// Indicates whether this instance and a specified object are equal.
    /// </summary>
    /// <param name="obj">The <see cref="object"/> to compare with the current instance.</param>
    /// <returns>true if obj and this instance are the same type and represent the same value; otherwise, false.</returns>
    public override bool Equals(object? obj)
        => (obj is HashString hs && Equals(hs)) ||
        (obj is string stg && Equals(stg));

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
    public override int GetHashCode() {
        int res = 0;
        foreach (byte item in hash)
            res ^= item;
        return res;
    }

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
    public bool Equals(HashString other)
        => ((Guid)this) == ((Guid)other);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.
    /// <para>Use <see cref="Guid"/>.ToString() or convert a list of bytes to a <see cref="string"/> using a <see cref="StringBuilder"/> or similar.</para>
    /// </param>
    /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
    public bool Equals(string? other) {
        StringBuilder builder = new();
        foreach (byte item in hash)
            builder.Append(item);
        return other == builder.ToString() || ToString() == other;
    }

    /// <summary>
    /// Returns a string representation of the value of this instance in registry format.
    /// </summary>
    /// <returns>The value of this <see cref="System.Guid"/>, formatted by using the "D" format specifier as
    /// follows: <c>xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx</c> where the value of the GUID is
    /// represented as a series of lowercase hexadecimal digits in groups of 8, 4, 4,
    /// 4, and 12 digits and separated by hyphens. An example of a return value is "382c74c3-721d-4f34-80e5-57657b6cbc27".
    /// To convert the hexadecimal digits from a through f to uppercase, call the System.String.ToUpper
    /// method on the returned string.
    /// </returns>
    public override string ToString()
        => ((Guid)this).ToString();

    /// <summary>
    /// Returns a string representation of the value of this <see cref="System.Guid"/> instance, according to the provided format specifier.
    /// </summary>
    /// <param name="format">A single format specifier that indicates how to format the value of this <see cref="System.Guid"/>.
    /// The format parameter can be "N", "D", "B", "P", or "X". If format is null or
    /// an empty string (""), "D" is used.
    /// </param>
    /// <exception cref="FormatException">The value of format is not null, an empty string (""), "N", "D", "B", "P", or "X".</exception>
    /// <returns>The value of this <see cref="System.Guid"/>, represented as a series of lowercase hexadecimal
    /// digits in the specified format.
    /// </returns>
    public string ToString(string format)
        => ((Guid)this).ToString(format);

    /// <summary>
    /// Returns a string representation of the value of this instance of the <see cref="System.Guid"/>
    /// class, according to the provided format specifier and culture-specific format
    /// information.
    /// </summary>
    /// <param name="format">A single format specifier that indicates how to format the value of this <see cref="System.Guid"/>.
    /// The format parameter can be "N", "D", "B", "P", or "X". If format is null or
    /// an empty string (""), "D" is used.
    /// </param>
    /// <param name="formatProvider">(Reserved) An object that supplies culture-specific formatting information.</param>
    /// <returns>The value of this <see cref="System.Guid"/>, represented as a series of lowercase hexadecimal
    /// digits in the specified format.
    /// </returns>
    public string ToString(string? format, IFormatProvider? formatProvider)
        => ((Guid)this).ToString(format, formatProvider);

    /// <summary>
    /// Compares this instance to a specified <see cref="HashString"/> object and returns an indication
    /// of their relative values.
    /// </summary>
    /// <param name="other">An object to compare to this instance.</param>
    /// <returns>A signed number indicating the relative values of this instance and value. Return
    /// value Description A negative integer This instance is less than value. Zero This
    /// instance is equal to value. A positive integer This instance is greater than
    /// value.</returns>
    public int CompareTo(HashString other)
        => ((Guid)this).CompareTo((Guid)other);

    /// <summary>
    /// Implicit conversion from <see cref="HashString"/> to <see cref="string"/> type.
    /// </summary>
    public static implicit operator string(HashString hash) => hash.ToString();

    /// <summary>
    /// Explicit conversion from <see cref="HashString"/> object to <see cref="Guid"/>.
    /// </summary>
    public static explicit operator Guid(HashString hash) {
        byte[] h16 = new byte[16];
        for (long I = 0, J = 0; I < hash.hash.LongLength; I++) {
            h16[J++] ^= hash.hash[I];
            J = J >= 16 ? 0 : J;
        }
        return new Guid(h16);
    }
}