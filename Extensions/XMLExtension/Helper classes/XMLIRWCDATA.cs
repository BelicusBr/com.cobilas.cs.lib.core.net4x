namespace System.Xml; 
public class XMLIRWCDATA : XMLIRW {
    private bool disposedValue;

    public override string Name { get; set; } = string.Empty;
    public override XMLIRW Parent { get; set; } = default!;
    public XMLIRWValue Value { get; private set;}
    public override XmlNodeType Type { get; set; }

    public XMLIRWCDATA(XMLIRW parent, XMLIRWValue value) : base(parent, "CData", XmlNodeType.CDATA) {
        Value = value;
    }

    ~XMLIRWCDATA()
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