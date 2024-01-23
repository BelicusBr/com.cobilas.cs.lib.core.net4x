namespace System.Xml; 
/// <summary>
/// Represents an XML element of type Attribute.
/// </summary>
public class XMLIRWAttribute : XMLIRW {
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    private bool disposedValue;

    public XMLIRWValue Value {get; set;}
    public override string Name { get; set; } = string.Empty;
    public override XMLIRW Parent { get; set; } = default!;
    public override XmlNodeType Type { get; set; }

    public XMLIRWAttribute(XMLIRWElement parent, string name, XMLIRWValue value) : base(parent, name, XmlNodeType.Attribute) {
        Value = value;
    }
    public XMLIRWAttribute(string name, XMLIRWValue value) : this(default!, name, value) {}
    public XMLIRWAttribute(XMLIRWElement parent, string name, object value) : this(parent, name, new XMLIRWValue(value)) {}
    public XMLIRWAttribute(string name, object value) : this(default!, name, value) {}

    ~XMLIRWAttribute() => Dispose(disposing: false);

    protected virtual void Dispose(bool disposing) {
        if (!disposedValue) {
            if (disposing) {
                Name = string.Empty;
                Parent = default!;
                Type = default;
                Value = default;
            }
            disposedValue = true;
        }
    }

    public override void Dispose() {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
}