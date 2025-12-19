namespace System.Xml { 
    /// <summary>Represents an XML element of type DocType.</summary>
    public class XMLIRWDocType : XMLIRW {
        private bool disposedValue;

        /// <summary>Gets the value of the public identifier on the DOCTYPE declaration.</summary>
        public XMLIRWText PudID { get; private set; }
        /// <summary>Gets the value of the system identifier on the DOCTYPE declaration.</summary>
        public XMLIRWText SysID { get; private set; }
        /// <summary>Gets the value of the document type definition (DTD) internal subset on the DOCTYPE declaration.</summary>
        public XMLIRWText SubSet { get; private set; }
        /// <inheritdoc/>
        public override XmlNodeType Type { get; set; }
        /// <inheritdoc/>
        public override XMLIRW Parent { get; set; } = XMLIRWNull.Null;
        /// <inheritdoc/>
        public override string Name { get; set; } = string.Empty;
        
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWDocType(XMLIRW parent, string name, object? pudid, object? sysid, object? subset) : base(parent, name, XmlNodeType.DocumentType) {
            PudID = pudid is null ? XMLIRWTextNull.Null : new XMLIRWText(pudid);
            SysID = sysid is null ? XMLIRWTextNull.Null : new XMLIRWText(sysid);
            SubSet = subset is null ? XMLIRWTextNull.Null : new XMLIRWText(subset);
        }
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWDocType(string name, object? pudid, object? sysid, object? subset) :
            this(XMLIRWNull.Null, name, pudid, sysid, subset) {}

        /// <summary>Called when the object is finished.</summary>
        ~XMLIRWDocType() => Dispose(disposing: false);
        
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
                    PudID = XMLIRWTextNull.Null;
                    SysID = XMLIRWTextNull.Null;
                    SubSet = XMLIRWTextNull.Null;
                }
                disposedValue = true;
            }
        }
    }
}