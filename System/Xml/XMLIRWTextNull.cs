using Cobilas;

namespace System.Xml;
/// <summary>This class is a representation of a null XMLIRW text.</summary>
public class XMLIRWTextNull : XMLIRWText, INullObject {
    private static readonly XMLIRWTextNull @null = new(XMLIRWNull.Null, string.Empty, string.Empty, XmlNodeType.None);
    /// <inheritdoc/>
    public override bool IsNull => true;
    /// <inheritdoc/>
    public override string Name { get => "#txtnull"; set => _ = value; }
    /// <inheritdoc/>
    public override XMLIRW Parent { get => XMLIRWNull.Null; set => _ = value; }
    /// <inheritdoc/>
    public override XmlNodeType Type { get => XmlNodeType.None; set => _ = value; }
    /// <summary>Returns a representation of a null XMLIRW text.</summary>
    public static XMLIRWTextNull Null => @null;
    /// <inheritdoc/>
    public XMLIRWTextNull(object textValue) : base(textValue) {}
    /// <inheritdoc/>
    public XMLIRWTextNull(XMLIRW parent, object textValue) : base(parent, textValue) {}
    /// <inheritdoc/>
    protected XMLIRWTextNull(XMLIRW parent, object textValue, string name, XmlNodeType type) : base(parent, textValue, name, type) {}
}
