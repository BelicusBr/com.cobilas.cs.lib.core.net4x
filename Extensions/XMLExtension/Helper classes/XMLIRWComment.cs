namespace System.Xml; 
public class XMLIRWComment : XMLIRW, IDisposable {
    private bool disposedValue;

    public override string Name { get; set; } = string.Empty;
    public override XMLIRW Parent { get; set; } = default!;
    public XMLIRWValue Value { get; private set; }
    public override XmlNodeType Type { get; set; }

    public XMLIRWComment(XMLIRW parent, XMLIRWValue value) : base(parent, "Comment", XmlNodeType.Comment) {
        this.Value = value;
    }

    public XMLIRWComment(XMLIRWValue value) : this(default!, value) {}

    ~XMLIRWComment()
        => Dispose(disposing: false);

    protected virtual void Dispose(bool disposing) {
        if (!disposedValue) {
            if (disposing) {
                Value = XMLIRWValue.Empty;
                Name = string.Empty;
                Parent = default!;
                Type = XmlNodeType.None;
            }
            disposedValue = true;
        }
    }

    public override void Dispose() {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}