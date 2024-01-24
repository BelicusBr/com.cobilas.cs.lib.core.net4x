namespace System.Xml { 
    /// <summary>
    /// Represents an XML element of type Comment.
    /// </summary>
    public class XMLIRWComment : XMLIRW, IDisposable {
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
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
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    }
}