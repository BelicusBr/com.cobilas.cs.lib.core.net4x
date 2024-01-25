namespace System.Xml { 
    /// <summary>
    /// Represents an XML element of type DocType.
    /// </summary>
    public class XMLIRWDocType : XMLIRW {
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        private bool disposedValue;

        public override string Name { get; set; } = string.Empty;
        public override XMLIRW Parent { get; set; } = default;
        public XMLIRWValue PudID { get; private set;}
        public XMLIRWValue SysID { get; private set;}
        public XMLIRWValue SubSet { get; private set;}
        public override XmlNodeType Type { get; set; }

        public XMLIRWDocType(XMLIRWElement parent, string name, XMLIRWValue pudid, XMLIRWValue sysid, XMLIRWValue subset) : base(parent, name, XmlNodeType.DocumentType) {
            this.PudID = pudid;
            this.SysID = sysid;
            this.SubSet = subset;
        }
        public XMLIRWDocType(string name, XMLIRWValue pudid, XMLIRWValue sysid, XMLIRWValue subset) : this(default, name, pudid, sysid, subset) {}
        public XMLIRWDocType(XMLIRWElement parent, string name, object pudid, object sysid, object subset) :
            this(parent, name, new XMLIRWValue(pudid), new XMLIRWValue(sysid), new XMLIRWValue(subset)) {}
        public XMLIRWDocType(string name, object pudid, object sysid, object subset) :
            this(name, new XMLIRWValue(pudid), new XMLIRWValue(sysid), new XMLIRWValue(subset)) {}

        ~XMLIRWDocType() => Dispose(disposing: false);

        public override void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    Name = string.Empty;
                    Parent = default;
                    Type = default;
                    PudID = default;
                    SysID = default;
                    SubSet = default;
                }
                disposedValue = true;
            }
        }
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    }
}