namespace System.Xml; 
public class XMLIRWProcessingInstruction : XMLIRW, IDisposable {
    private bool disposedValue;

    public override string Name { get; set; } = string.Empty;
    public override XMLIRW Parent { get; set; } = default!;
    public XMLIRWValue Value { get; private set; }
    public override XmlNodeType Type { get; set; }

    public XMLIRWProcessingInstruction(XMLIRW parent, string name, XMLIRWValue value) : base(parent, name, XmlNodeType.ProcessingInstruction) {
        this.Value = value;
    }

    public XMLIRWProcessingInstruction(string name, XMLIRWValue value) : this(default!, name, value) {}

    ~XMLIRWProcessingInstruction()
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