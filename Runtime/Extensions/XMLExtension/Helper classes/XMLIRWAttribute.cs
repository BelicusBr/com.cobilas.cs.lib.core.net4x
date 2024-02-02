namespace System.Xml { 
    /// <summary>
    /// Represents an XML element of type Attribute.
    /// </summary>
    public class XMLIRWAttribute : XMLIRW, ITextValue {
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        private bool disposedValue;

        [Obsolete("Use the Text property.")]
        public XMLIRWValue Value {get; set;}
        public XMLIRWText Text { get; set; }
        public override XmlNodeType Type { get; set; }
        public override XMLIRW Parent { get; set; } = default;
        public override string Name { get; set; } = string.Empty;

        [Obsolete("Use the XMLIRWAttribute(XMLIRWElement, string, object) constructor.")]
        public XMLIRWAttribute(XMLIRWElement parent, string name, XMLIRWValue value) {}
        [Obsolete("Use the XMLIRWAttribute(string, object) constructor.")]
        public XMLIRWAttribute(string name, XMLIRWValue value) {}

        public XMLIRWAttribute(XMLIRWElement parent, string name, object value) : 
            base(parent, name, XmlNodeType.Attribute) { Text = new XMLIRWText(value); }
        public XMLIRWAttribute(string name, object value) : this(default, name, value) {}

        ~XMLIRWAttribute() => Dispose(disposing: false);

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