namespace System.Xml { 
    /// <summary>
    /// Represents an XML element of type Attribute.
    /// </summary>
    public class XMLIRWAttribute : XMLIRW, ITextValue {
        private bool disposedValue;

        /// <inheritdoc cref="Text"/>
        [Obsolete("Use the Text property.")]
        public XMLIRWValue Value {get; set;}
        /// <inheritdoc/>
        public XMLIRWText Text { get; set; }
        /// <inheritdoc/>
        public override XmlNodeType Type { get; set; }
        /// <inheritdoc/>
        public override XMLIRW Parent { get; set; } = default;
        /// <inheritdoc/>
        public override string Name { get; set; } = string.Empty;

        /// <inheritdoc cref="XMLIRW()"/>
        [Obsolete("Use the XMLIRWAttribute(XMLIRWElement, string, object) constructor.")]
        public XMLIRWAttribute(XMLIRWElement parent, string name, XMLIRWValue value) {}
        /// <inheritdoc cref="XMLIRW()"/>
        [Obsolete("Use the XMLIRWAttribute(string, object) constructor.")]
        public XMLIRWAttribute(string name, XMLIRWValue value) {}

        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWAttribute(XMLIRWElement parent, string name, object value) : 
            base(parent, name, XmlNodeType.Attribute) { Text = new XMLIRWText(value); }
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWAttribute(string name, object value) : this(default, name, value) {}

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
                    Parent = default;
                    Type = default;
                    Text = default;
                }
                disposedValue = true;
            }
        }
    }
}