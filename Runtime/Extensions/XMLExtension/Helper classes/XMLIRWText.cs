using System.Globalization;

namespace System.Xml {
    /// <summary>Represents XML text.</summary>
    public class XMLIRWText : XMLIRW, IConvertible {
        /// <summary>The value of the XML text.</summary>
        protected object textValue;

        /// <summary>The value of the XML text.</summary>
        public object Value => textValue;
        /// <summary>Check if the value is null.</summary>
        public bool IsNull => textValue is null;
        /// <inheritdoc/>
        public override string Name { get; set; }
        /// <inheritdoc/>
        public override XMLIRW Parent { get; set; }
        /// <inheritdoc/>
        public override XmlNodeType Type { get; set; }

        /// <summary>Creates an empty instance.</summary>
        public static XMLIRWText Empty => new XMLIRWText(null);

        /// <summary>Creates a new instance of the XMLIRW element.</summary>
        public XMLIRWText(XMLIRW parent, object textValue) : base(parent, "#text", XmlNodeType.Text) {
            this.textValue = textValue;
        }

        /// <summary>Creates a new instance of the XMLIRW element.</summary>
        public XMLIRWText(object textValue) : this(null, textValue) {}
        
        /// <inheritdoc/>
        public override void Dispose() {
            textValue = Name = default;
            Parent = default;
            Type = default;
        }
        
        /// <inheritdoc/>
        public override string ToString()
            => ToString(CultureInfo.InvariantCulture);

        /// <inheritdoc cref="ToString()"/>
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

        /// <summary>Provide a conversion from type <see cref="XMLIRWText"/> to <see cref="string"/>.</summary>
        public static explicit operator string(XMLIRWText text) => Convert.ToString(text);
        /// <summary>Provide a conversion from type <see cref="XMLIRWText"/> to <see cref="char"/>[].</summary>
        public static explicit operator char[](XMLIRWText text) => Convert.ToString(text).ToCharArray();

        /// <summary>Provide a conversion from type <see cref="XMLIRWText"/> to <see cref="float"/>.</summary>
        public static explicit operator float(XMLIRWText text) => Convert.ToSingle(text);
        /// <summary>Provide a conversion from type <see cref="XMLIRWText"/> to <see cref="double"/>.</summary>
        public static explicit operator double(XMLIRWText text) => Convert.ToDouble(text);
        /// <summary>Provide a conversion from type <see cref="XMLIRWText"/> to <see cref="decimal"/>.</summary>
        public static explicit operator decimal(XMLIRWText text) => Convert.ToDecimal(text);

        /// <summary>Provide a conversion from type <see cref="XMLIRWText"/> to <see cref="sbyte"/>.</summary>
        public static explicit operator sbyte(XMLIRWText text) => Convert.ToSByte(text);
        /// <summary>Provide a conversion from type <see cref="XMLIRWText"/> to <see cref="short"/>.</summary>
        public static explicit operator short(XMLIRWText text) => Convert.ToInt16(text);
        /// <summary>Provide a conversion from type <see cref="XMLIRWText"/> to <see cref="int"/>.</summary>
        public static explicit operator int(XMLIRWText text) => Convert.ToInt32(text);
        /// <summary>Provide a conversion from type <see cref="XMLIRWText"/> to <see cref="long"/>.</summary>
        public static explicit operator long(XMLIRWText text) => Convert.ToInt64(text);

        /// <summary>Provide a conversion from type <see cref="XMLIRWText"/> to <see cref="byte"/>.</summary>
        public static explicit operator byte(XMLIRWText text) => Convert.ToByte(text);
        /// <summary>Provide a conversion from type <see cref="XMLIRWText"/> to <see cref="ushort"/>.</summary>
        public static explicit operator ushort(XMLIRWText text) => Convert.ToUInt16(text);
        /// <summary>Provide a conversion from type <see cref="XMLIRWText"/> to <see cref="uint"/>.</summary>
        public static explicit operator uint(XMLIRWText text) => Convert.ToUInt32(text);
        /// <summary>Provide a conversion from type <see cref="XMLIRWText"/> to <see cref="ulong"/>.</summary>
        public static explicit operator ulong(XMLIRWText text) => Convert.ToUInt64(text);

        /// <summary>Provide a conversion from type <see cref="XMLIRWText"/> to <see cref="DateTime"/>.</summary>
        public static explicit operator DateTime(XMLIRWText text) => Convert.ToDateTime(text);

        /// <summary>Provide a conversion from type <see cref="XMLIRWText"/> to <see cref="bool"/>.</summary>
        public static explicit operator bool(XMLIRWText text) => Convert.ToBoolean(text);

        /// <summary>Provide a conversion from type <see cref="string"/> to <see cref="XMLIRWText"/>.</summary>
        public static implicit operator XMLIRWText(string text) => new XMLIRWText(text);
        /// <summary>Provide a conversion from type <see cref="char"/>[] to <see cref="XMLIRWText"/>.</summary>
        public static implicit operator XMLIRWText(char[] text) => new XMLIRWText(new string(text));

        /// <summary>Provide a conversion from type <see cref="float"/> to <see cref="XMLIRWText"/>.</summary>
        public static implicit operator XMLIRWText(float text) => new XMLIRWText(text);
        /// <summary>Provide a conversion from type <see cref="double"/> to <see cref="XMLIRWText"/>.</summary>
        public static implicit operator XMLIRWText(double text) => new XMLIRWText(text);
        /// <summary>Provide a conversion from type <see cref="decimal"/> to <see cref="XMLIRWText"/>.</summary>
        public static implicit operator XMLIRWText(decimal text) => new XMLIRWText(text);

        /// <summary>Provide a conversion from type <see cref="sbyte"/> to <see cref="XMLIRWText"/>.</summary>
        public static implicit operator XMLIRWText(sbyte text) => new XMLIRWText(text);
        /// <summary>Provide a conversion from type <see cref="short"/> to <see cref="XMLIRWText"/>.</summary>
        public static implicit operator XMLIRWText(short text) => new XMLIRWText(text);
        /// <summary>Provide a conversion from type <see cref="int"/> to <see cref="XMLIRWText"/>.</summary>
        public static implicit operator XMLIRWText(int text) => new XMLIRWText(text);
        /// <summary>Provide a conversion from type <see cref="long"/> to <see cref="XMLIRWText"/>.</summary>
        public static implicit operator XMLIRWText(long text) => new XMLIRWText(text);

        /// <summary>Provide a conversion from type <see cref="byte"/> to <see cref="XMLIRWText"/>.</summary>
        public static implicit operator XMLIRWText(byte text) => new XMLIRWText(text);
        /// <summary>Provide a conversion from type <see cref="ushort"/> to <see cref="XMLIRWText"/>.</summary>
        public static implicit operator XMLIRWText(ushort text) => new XMLIRWText(text);
        /// <summary>Provide a conversion from type <see cref="uint"/> to <see cref="XMLIRWText"/>.</summary>
        public static implicit operator XMLIRWText(uint text) => new XMLIRWText(text);
        /// <summary>Provide a conversion from type <see cref="ulong"/> to <see cref="XMLIRWText"/>.</summary>
        public static implicit operator XMLIRWText(ulong text) => new XMLIRWText(text);

        /// <summary>Provide a conversion from type <see cref="DateTime"/> to <see cref="XMLIRWText"/>.</summary>
        public static implicit operator XMLIRWText(DateTime text) => new XMLIRWText(text);

        /// <summary>Provide a conversion from type <see cref="bool"/> to <see cref="XMLIRWText"/>.</summary>
        public static implicit operator XMLIRWText(bool text) => new XMLIRWText(text);
    }
}