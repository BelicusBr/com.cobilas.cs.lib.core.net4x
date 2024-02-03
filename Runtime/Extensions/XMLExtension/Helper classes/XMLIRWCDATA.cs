namespace System.Xml { 
    /// <summary>Represents an XML element of type CDATA.</summary>
    public class XMLIRWCDATA : XMLIRW, ITextValue {
        private bool disposedValue;
        
        /// <inheritdoc/>
        public XMLIRWText Text { get; set; }
        /// <inheritdoc cref="Text"/>
        [Obsolete("Use the Text property.")]
        public XMLIRWValue Value { get; private set;}
        /// <inheritdoc/>
        public override XmlNodeType Type { get; set; }
        /// <inheritdoc/>
        public override XMLIRW Parent { get; set; } = default;
        /// <inheritdoc/>
        public override string Name { get; set; } = string.Empty;

        /// <inheritdoc cref="XMLIRW()"/>
        [Obsolete("Use the XMLIRWCDATA(XMLIRW, object) constructor.")]
        public XMLIRWCDATA(XMLIRW parent, string name, XMLIRWValue value) {}
        /// <inheritdoc cref="XMLIRW()"/>
        [Obsolete("Use the XMLIRWCDATA(object) constructor.")]
        public XMLIRWCDATA(string name, XMLIRWValue value) {}
        /// <inheritdoc cref="XMLIRW()"/>
        [Obsolete("Use the XMLIRWCDATA(XMLIRW, object) constructor.")]
        public XMLIRWCDATA(XMLIRW parent, XMLIRWValue value) {}
        /// <inheritdoc cref="XMLIRW()"/>
        [Obsolete("Use the XMLIRWCDATA(object) constructor.")]
        public XMLIRWCDATA(XMLIRWValue value) {}
        /// <inheritdoc cref="XMLIRW()"/>
        [Obsolete("Use the XMLIRWCDATA(XMLIRW, object) constructor.")]
        public XMLIRWCDATA(XMLIRW parent, string name, object value) {}
        /// <inheritdoc cref="XMLIRW()"/>
        [Obsolete("Use the XMLIRWCDATA(object) constructor.")]
        public XMLIRWCDATA(string name, object value) {}

        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWCDATA(XMLIRW parent, object value) : base(parent, "#cdata", XmlNodeType.CDATA)
        { Text = new XMLIRWText(value); }
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWCDATA(object value) : this((XMLIRW)null, value) {}

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
                    Parent = default;
                    Type = default;
                    Text = default;
                }
                disposedValue = true;
            }
        }
    }
}