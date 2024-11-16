using Cobilas;

namespace System.Xml;
/// <summary>This class represents a null XMLIRW object.</summary>
public sealed class XMLIRWNull : XMLIRW, INullObject {
    private static readonly XMLIRWNull @null = new();
    /// <inheritdoc/>
    public override string Name { get => "_null"; set => _ = value; }
    /// <inheritdoc/>
    public override XMLIRW Parent { get => @null; set => _ = value; }
    /// <inheritdoc/>
    public override XmlNodeType Type { get => XmlNodeType.None; set => _ = value; }
    /// <summary>Returns a representation of a null XMLIRW object.</summary>
    public static XMLIRWNull Null => @null;
    /// <inheritdoc/>
    public override void Dispose() {}
}
