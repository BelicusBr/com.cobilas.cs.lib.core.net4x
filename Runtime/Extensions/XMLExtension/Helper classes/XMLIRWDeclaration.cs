namespace System.Xml {
    public class XMLIRWDeclaration : XMLIRW {
        public override string Name { get; set; }
        public override XMLIRW Parent { get; set; }
        public string Version { get; protected set; }
        public string Encoding { get; protected set; }
        public override XmlNodeType Type { get; set; }
        public string Standalone { get; protected set; }

        public XMLIRWDeclaration(XMLIRW parent, string version, string encoding, string standalone) : base(parent, "xml", XmlNodeType.XmlDeclaration) {
            Version = version;
            Encoding = encoding;
            Standalone = standalone;
        }

        public XMLIRWDeclaration(XMLIRW parent, string version, string encoding) :
            this(parent, version, encoding, default) {}

        public XMLIRWDeclaration(XMLIRW parent, string version) :
            this(parent, version, default) {}

        public XMLIRWDeclaration(XMLIRW parent) : this(parent, "1.0") {}

        public XMLIRWDeclaration(string version, string encoding, string standalone) :
            this(default, version, encoding, standalone) {}

        public XMLIRWDeclaration(string version, string encoding) :
            this(default, version, encoding, default) {}

        public XMLIRWDeclaration(string version) : this(default(XMLIRW), version, default) {}

        public XMLIRWDeclaration() : this(default(XMLIRW), "1.0") {}

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