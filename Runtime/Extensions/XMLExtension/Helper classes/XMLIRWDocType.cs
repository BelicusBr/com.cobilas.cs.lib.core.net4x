namespace System.Xml { 
    /// <summary>
    /// Represents an XML element of type DocType.
    /// </summary>
    public class XMLIRWDocType : XMLIRW {
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        private bool disposedValue;

        public XMLIRWText PudID { get; private set; }
        public XMLIRWText SysID { get; private set; }
        public XMLIRWText SubSet { get; private set; }
        public override XmlNodeType Type { get; set; }
        public override XMLIRW Parent { get; set; } = default;
        public override string Name { get; set; } = string.Empty;

        [Obsolete("Use the XMLIRWDocType(XMLIRW, string, object, object, object) constructor.")]
        public XMLIRWDocType(XMLIRW parent, string name, XMLIRWValue pudid, XMLIRWValue sysid, XMLIRWValue subset) {}
        [Obsolete("Use the XMLIRWDocType(string, object, object, object) constructor.")]
        public XMLIRWDocType(string name, XMLIRWValue pudid, XMLIRWValue sysid, XMLIRWValue subset) {}
        
        public XMLIRWDocType(XMLIRW parent, string name, object pudid, object sysid, object subset) : base(parent, name, XmlNodeType.DocumentType) {
            PudID = new XMLIRWText(pudid);
            SysID = new XMLIRWText(sysid);
            SubSet = new XMLIRWText(subset);
        }
        public XMLIRWDocType(string name, object pudid, object sysid, object subset) :
            this((XMLIRW)null, name, pudid, sysid, subset) {}

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