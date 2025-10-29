namespace System.Xml {
    /// <summary>Represents an xml declaration.</summary>
    public class XMLIRWDeclaration : XMLIRW {
		private bool disposedValue;

		/// <inheritdoc/>
		public override string Name { get; set; } = string.Empty;
        /// <inheritdoc/>
        public override XMLIRW Parent { get; set; } = XMLIRWNull.Null;
        /// <summary>Gets the XML version of the document.</summary>
        public string Version { get; protected set; }
        /// <summary>Gets or sets the encoding level of the XML document.</summary>
        public string Encoding { get; protected set; }
        /// <inheritdoc/>
        public override XmlNodeType Type { get; set; }
        /// <summary>Gets or sets the value of the standalone attribute.</summary>
        public string Standalone { get; protected set; }

        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWDeclaration(XMLIRW parent, string version, string encoding, string standalone) : base(parent, "xml", XmlNodeType.XmlDeclaration) {
            Version = version;
            Encoding = encoding;
            Standalone = standalone;
        }

        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWDeclaration(XMLIRW parent, string version, string encoding) :
            this(parent, version, encoding, string.Empty) {}
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWDeclaration(XMLIRW parent, string version) :
            this(parent, version, string.Empty) {}
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWDeclaration(XMLIRW parent) : this(parent, "1.0") {}
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWDeclaration(string version, string encoding, string standalone) :
            this(XMLIRWNull.Null, version, encoding, standalone) {}
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWDeclaration(string version, string encoding) :
            this(XMLIRWNull.Null, version, encoding, string.Empty) {}
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWDeclaration(string version) : this(XMLIRWNull.Null, version, string.Empty) {}
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWDeclaration() : this(XMLIRWNull.Null, "1.0") {}
		/// <summary>Destructor</summary>
		~XMLIRWDeclaration() => Dispose(disposing: false);
		/// <inheritdoc/>
		public override void Dispose() {
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
        /// <inheritdoc cref="Dispose()"/>
		protected virtual void Dispose(bool disposing) {
			if (!disposedValue) {
				if (disposing) {
					Name =
					Version =
					Encoding =
					Standalone = string.Empty;
					Type = default;
					Parent = XMLIRWNull.Null;
				}
				disposedValue = true;
			}
		}
	}
}