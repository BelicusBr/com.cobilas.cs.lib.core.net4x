namespace System.Xml; 
/// <summary>
/// Represents the value of a tag.
/// </summary>
public struct XMLIRWValue : IDisposable, IEquatable<XMLIRWValue>, IConvertible {
    private object value;

    /// <summary>
    /// Represents an empty value.
    /// </summary>
    public readonly static XMLIRWValue Empty = new(default!);

#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    public XMLIRWValue(object value) {
        this.value = value;
    }

    public void Dispose() {
        value = default!;
    }

    public bool Equals(XMLIRWValue other) => other.value == value;

    public override string ToString()
        => value is null ? string.Empty : value.ToString();

    public string ToString(IFormatProvider provider)
        => Convert.ToString(value, provider);
        
    public override bool Equals(object obj)
        => obj is XMLIRWValue V && Equals(V);
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
    public override int GetHashCode() => base.GetHashCode();

    TypeCode IConvertible.GetTypeCode()
        => Convert.GetTypeCode(value);

    bool IConvertible.ToBoolean(IFormatProvider provider)
        => Convert.ToBoolean(value, provider);

    byte IConvertible.ToByte(IFormatProvider provider)
        => Convert.ToByte(value, provider);

    char IConvertible.ToChar(IFormatProvider provider)
        => Convert.ToChar(value, provider);

    DateTime IConvertible.ToDateTime(IFormatProvider provider)
        => Convert.ToDateTime(value, provider);

    decimal IConvertible.ToDecimal(IFormatProvider provider)
        => Convert.ToDecimal(value, provider);

    double IConvertible.ToDouble(IFormatProvider provider)
        => Convert.ToDouble(value, provider);

    short IConvertible.ToInt16(IFormatProvider provider)
        => Convert.ToInt16(value, provider);

    int IConvertible.ToInt32(IFormatProvider provider)
        => Convert.ToInt32(value, provider);

    long IConvertible.ToInt64(IFormatProvider provider)
        => Convert.ToInt64(value, provider);

    sbyte IConvertible.ToSByte(IFormatProvider provider)
        => Convert.ToSByte(value, provider);

    float IConvertible.ToSingle(IFormatProvider provider)
        => Convert.ToSingle(value, provider);

    object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        => Convert.ChangeType(value, conversionType, provider);

    ushort IConvertible.ToUInt16(IFormatProvider provider)
        => Convert.ToUInt16(value, provider);

    uint IConvertible.ToUInt32(IFormatProvider provider)
        => Convert.ToUInt32(value, provider);

    ulong IConvertible.ToUInt64(IFormatProvider provider)
        => Convert.ToUInt64(value, provider);

#pragma warning disable CS1591
    public static bool operator ==(XMLIRWValue left, XMLIRWValue right) => left.Equals(right);
    public static bool operator !=(XMLIRWValue left, XMLIRWValue right) => !(left == right);

    public static explicit operator string(XMLIRWValue V) 
        => V == Empty ? string.Empty : Convert.ToString(V);
#pragma warning restore
}