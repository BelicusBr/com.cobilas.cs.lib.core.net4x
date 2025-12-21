namespace System.Xml;  
/// <summary>
/// Represents an XML element of type Attribute.
/// </summary>
public class XMLIRWAttribute : XMLIRW, ITextValue {
    private bool disposedValue;
    /// <inheritdoc/>
    public XMLIRWText Text { get; set; }
    /// <inheritdoc/>
    public override XmlNodeType Type { get; set; }
    /// <inheritdoc/>
    public override XMLIRW Parent { get; set; } = XMLIRWNull.Null;
    /// <inheritdoc/>
    public override string Name { get; set; } = string.Empty;

    private XMLIRWAttribute(XMLIRW parent, string name, object value) :
        base(parent, name, XmlNodeType.Attribute) { Text = new XMLIRWText(value); }
    /// <inheritdoc cref="XMLIRW()"/>
    public XMLIRWAttribute(XMLIRWElement parent, string name, object value) : this((XMLIRW)parent, name, value) {}
    /// <inheritdoc cref="XMLIRW()"/>
    public XMLIRWAttribute(string name, object value) : this(XMLIRWNull.Null, name, value) {}
    /// <summary>Called when the object is finished.</summary>
    ~XMLIRWAttribute() => Dispose(disposing: false);
    /// <inheritdoc/>
    public override void Dispose() {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    /// <inheritdoc cref="Dispose()"/>
    protected virtual void Dispose(bool disposing) {
        if (!disposedValue) {
            if (disposing) {
                Name = string.Empty;
                Parent = XMLIRWNull.Null;
                Type = default;
                Text = XMLIRWTextNull.Null;
            }
            disposedValue = true;
        }
    }
}