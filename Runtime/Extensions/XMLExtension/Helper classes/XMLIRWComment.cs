namespace System.Xml { 
    /// <summary>Represents an XML element of type Comment.</summary>
    public class XMLIRWComment : XMLIRW, ITextValue, IDisposable {
        private bool disposedValue;

        /// <inheritdoc/>
        public XMLIRWText Text { get; set; }
        /// <inheritdoc cref="Text"/>
        [Obsolete("Use the Text property.")]
        public XMLIRWValue Value { get; private set; }
        /// <inheritdoc/>
        public override XmlNodeType Type { get; set; }
        /// <inheritdoc/>
        public override XMLIRW Parent { get; set; } = default;
        /// <inheritdoc/>
        public override string Name { get; set; } = string.Empty;

        /// <inheritdoc cref="XMLIRW()"/>
        [Obsolete("Use the XMLIRWComment(XMLIRW, object) constructor.")]
        public XMLIRWComment(XMLIRW parent, XMLIRWValue value) {}
        /// <inheritdoc cref="XMLIRW()"/>
        [Obsolete("Use the XMLIRWComment(object) constructor.")]
        public XMLIRWComment(XMLIRWValue value) {}

        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWComment(XMLIRW parent, object value) : base(parent, "#comment", XmlNodeType.Comment) 
        { Text = new XMLIRWText(value); }
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWComment(object value) : this((XMLIRW)null, value) {}

        /// <summary>Called when the object is finished.</summary>
        ~XMLIRWComment()
            => Dispose(disposing: false);
        
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