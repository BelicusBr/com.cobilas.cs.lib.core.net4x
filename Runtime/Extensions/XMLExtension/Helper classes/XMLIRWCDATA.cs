namespace System.Xml { 
    /// <summary>
    /// Represents an XML element of type CDATA.
    /// </summary>
    public class XMLIRWCDATA : XMLIRW, ITextValue {
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        private bool disposedValue;

        public XMLIRWText Text { get; set; }
        public override string Name { get; set; } = string.Empty;
        public override XMLIRW Parent { get; set; } = default;
        [Obsolete("Use the Text property.")]
        public XMLIRWValue Value { get; private set;}
        public override XmlNodeType Type { get; set; }

        [Obsolete("Use the XMLIRWCDATA(XMLIRW, object) constructor.")]
        public XMLIRWCDATA(XMLIRW parent, string name, XMLIRWValue value) {}
        [Obsolete("Use the XMLIRWCDATA(object) constructor.")]
        public XMLIRWCDATA(string name, XMLIRWValue value) {}
        [Obsolete("Use the XMLIRWCDATA(XMLIRW, object) constructor.")]
        public XMLIRWCDATA(XMLIRW parent, XMLIRWValue value) {}
        [Obsolete("Use the XMLIRWCDATA(object) constructor.")]
        public XMLIRWCDATA(XMLIRWValue value) {}
        [Obsolete("Use the XMLIRWCDATA(XMLIRW, object) constructor.")]
        public XMLIRWCDATA(XMLIRW parent, string name, object value) {}
        [Obsolete("Use the XMLIRWCDATA(object) constructor.")]
        public XMLIRWCDATA(string name, object value) {}

        public XMLIRWCDATA(XMLIRW parent, object value) : base(parent, "#cdata", XmlNodeType.CDATA)
        { Text = new XMLIRWText(value); }
        public XMLIRWCDATA(object value) : this((XMLIRW)null, value) {}

        ~XMLIRWCDATA()
            => Dispose(disposing: false);

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    Name = string.Empty;
                    Parent = default;
                    Type = default;
                    Text = default;
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
}