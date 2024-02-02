namespace System.Xml { 
    /// <summary>
    /// Represents an XML element of type Comment.
    /// </summary>
    public class XMLIRWComment : XMLIRW, ITextValue, IDisposable {
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        private bool disposedValue;

        public XMLIRWText Text { get; set; }
        [Obsolete("Use the Text property.")]
        public XMLIRWValue Value { get; private set; }
        public override XmlNodeType Type { get; set; }
        public override XMLIRW Parent { get; set; } = default;
        public override string Name { get; set; } = string.Empty;

        [Obsolete("Use the XMLIRWComment(XMLIRW, object) constructor.")]
        public XMLIRWComment(XMLIRW parent, XMLIRWValue value) {}
        [Obsolete("Use the XMLIRWComment(object) constructor.")]
        public XMLIRWComment(XMLIRWValue value) {}

        public XMLIRWComment(XMLIRW parent, object value) : base(parent, "#comment", XmlNodeType.Comment) 
        { Text = new XMLIRWText(value); }
        public XMLIRWComment(object value) : this((XMLIRW)null, value) {}

        ~XMLIRWComment()
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