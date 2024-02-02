namespace System.Xml {
    /// <summary>Represents an xml declaration.</summary>
    public class XMLIRWDeclaration : XMLIRW {
        /// <inheritdoc/>
        public override string Name { get; set; }
        /// <inheritdoc/>
        public override XMLIRW Parent { get; set; }
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
            this(parent, version, encoding, default) {}
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWDeclaration(XMLIRW parent, string version) :
            this(parent, version, default) {}
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWDeclaration(XMLIRW parent) : this(parent, "1.0") {}
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWDeclaration(string version, string encoding, string standalone) :
            this(default, version, encoding, standalone) {}
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWDeclaration(string version, string encoding) :
            this(default, version, encoding, default) {}
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWDeclaration(string version) : this(default(XMLIRW), version, default) {}
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWDeclaration() : this(default(XMLIRW), "1.0") {}
        /// <inheritdoc/>
        public override void Dispose() {
            Name =
            Version =
            Encoding =
            Standalone = default;
            Type = default;
            Parent = default;
        }
    }
}