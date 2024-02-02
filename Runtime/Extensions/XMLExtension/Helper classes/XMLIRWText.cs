namespace System.Xml {
    public sealed class XMLIRWText : XMLIRW, IConvertible {
        private object textValue;

        public object Value => textValue;
        public bool IsNull => textValue is null;
        public override string Name { get; set; }
        public override XMLIRW Parent { get; set; }
        public override XmlNodeType Type { get; set; }

        public static XMLIRWText Empty => new XMLIRWText(null);

        public XMLIRWText(XMLIRW parent, object textValue) : base(parent, "#text", XmlNodeType.Text) {
            this.textValue = textValue;
        }

        public XMLIRWText(object textValue) : this(null, textValue) {}

        public override void Dispose() {
            textValue = Name = default;
            Parent = default;
            Type = default;
        }

        public string ToString(IFormatProvider provider)
            => Convert.ToString(textValue, provider);

        TypeCode IConvertible.GetTypeCode()
            => Convert.GetTypeCode(textValue);

        bool IConvertible.ToBoolean(IFormatProvider provider)
            => Convert.ToBoolean(textValue, provider);

        byte IConvertible.ToByte(IFormatProvider provider)
            => Convert.ToByte(textValue, provider);

        char IConvertible.ToChar(IFormatProvider provider)
            => Convert.ToChar(textValue, provider);

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
            => Convert.ToDateTime(textValue, provider);

        decimal IConvertible.ToDecimal(IFormatProvider provider)
            => Convert.ToDecimal(textValue, provider);

        double IConvertible.ToDouble(IFormatProvider provider)
            => Convert.ToDouble(textValue, provider);

        short IConvertible.ToInt16(IFormatProvider provider)
            => Convert.ToInt16(textValue, provider);

        int IConvertible.ToInt32(IFormatProvider provider)
            => Convert.ToInt32(textValue, provider);

        long IConvertible.ToInt64(IFormatProvider provider)
            => Convert.ToInt64(textValue, provider);

        sbyte IConvertible.ToSByte(IFormatProvider provider)
            => Convert.ToSByte(textValue, provider);

        float IConvertible.ToSingle(IFormatProvider provider)
            => Convert.ToSingle(textValue, provider);

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
            => Convert.ChangeType(textValue, conversionType, provider);

        ushort IConvertible.ToUInt16(IFormatProvider provider)
            => Convert.ToUInt16(textValue, provider);

        uint IConvertible.ToUInt32(IFormatProvider provider)
            => Convert.ToUInt32(textValue, provider);

        ulong IConvertible.ToUInt64(IFormatProvider provider)
            => Convert.ToUInt64(textValue, provider);

        public static explicit operator string(XMLIRWText text) => Convert.ToString(text);
        public static explicit operator char[](XMLIRWText text) => Convert.ToString(text).ToCharArray();

        public static explicit operator float(XMLIRWText text) => Convert.ToSingle(text);
        public static explicit operator double(XMLIRWText text) => Convert.ToDouble(text);
        public static explicit operator decimal(XMLIRWText text) => Convert.ToDecimal(text);

        public static explicit operator sbyte(XMLIRWText text) => Convert.ToSByte(text);
        public static explicit operator short(XMLIRWText text) => Convert.ToInt16(text);
        public static explicit operator int(XMLIRWText text) => Convert.ToInt32(text);
        public static explicit operator long(XMLIRWText text) => Convert.ToInt64(text);

        public static explicit operator byte(XMLIRWText text) => Convert.ToByte(text);
        public static explicit operator ushort(XMLIRWText text) => Convert.ToUInt16(text);
        public static explicit operator uint(XMLIRWText text) => Convert.ToUInt32(text);
        public static explicit operator ulong(XMLIRWText text) => Convert.ToUInt64(text);

        public static explicit operator DateTime(XMLIRWText text) => Convert.ToDateTime(text);

        public static explicit operator bool(XMLIRWText text) => Convert.ToBoolean(text);

        public static implicit operator XMLIRWText(string text) => new XMLIRWText(text);
        public static implicit operator XMLIRWText(char[] text) => new XMLIRWText(new string(text));

        public static implicit operator XMLIRWText(float text) => new XMLIRWText(text);
        public static implicit operator XMLIRWText(double text) => new XMLIRWText(text);
        public static implicit operator XMLIRWText(decimal text) => new XMLIRWText(text);

        public static implicit operator XMLIRWText(sbyte text) => new XMLIRWText(text);
        public static implicit operator XMLIRWText(short text) => new XMLIRWText(text);
        public static implicit operator XMLIRWText(int text) => new XMLIRWText(text);
        public static implicit operator XMLIRWText(long text) => new XMLIRWText(text);

        public static implicit operator XMLIRWText(byte text) => new XMLIRWText(text);
        public static implicit operator XMLIRWText(ushort text) => new XMLIRWText(text);
        public static implicit operator XMLIRWText(uint text) => new XMLIRWText(text);
        public static implicit operator XMLIRWText(ulong text) => new XMLIRWText(text);

        public static implicit operator XMLIRWText(DateTime text) => new XMLIRWText(text);

        public static implicit operator XMLIRWText(bool text) => new XMLIRWText(text);
    }
}