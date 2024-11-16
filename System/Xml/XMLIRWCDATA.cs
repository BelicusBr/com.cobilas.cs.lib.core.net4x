namespace System.Xml { 
    /// <summary>Represents an XML element of type CDATA.</summary>
    public class XMLIRWCDATA : XMLIRW, ITextValue {
        private bool disposedValue;
        /// <inheritdoc/>
        public XMLIRWText Text { get; set; }
        /// <inheritdoc/>
        public override XmlNodeType Type { get; set; }
        /// <inheritdoc/>
        public override XMLIRW Parent { get; set; } = XMLIRWNull.Null;
        /// <inheritdoc/>
        public override string Name { get; set; } = string.Empty;
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWCDATA(XMLIRW parent, object? value) : base(parent, "#cdata", XmlNodeType.CDATA)
        { Text = value is null ? XMLIRWTextNull.Null : new XMLIRWText(value); }
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWCDATA(object? value) : this(XMLIRWNull.Null, value) {}
        /// <summary>Called when the object is finished.</summary>
        ~XMLIRWCDATA()
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
                    Parent = XMLIRWNull.Null;
                    Type = default;
                    Text = XMLIRWTextNull.Null;
                }
                disposedValue = true;
            }
        }
    }
}