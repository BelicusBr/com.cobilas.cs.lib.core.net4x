using System.Text;
using System.Collections;
using Cobilas.Collections;
using System.Collections.Generic;

namespace System.Xml {
    /// <summary>
    /// Represents an XML element of type ProcessingInstruction.
    /// </summary>
    public class XMLIRWProcessingInstruction : XMLIRW, IEnumerable<XMLIRWAttribute>, IDisposable {
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        private object attriList;
        private bool disposedValue;

        public override string Name { get; set; } = string.Empty;
        public override XMLIRW Parent { get; set; } = default;
        public bool IsAttributeList => attriList is XMLIRWAttribute[];
        public XMLIRWValue Value { get => GetAttriList(); private set => attriList = value; }
        public override XmlNodeType Type { get; set; }

        public XMLIRWProcessingInstruction(XMLIRW parent, string name, XMLIRWValue value) : base(parent, name, XmlNodeType.ProcessingInstruction) {
            this.Value = value;
        }

        public XMLIRWProcessingInstruction(XMLIRW parent, string name, XMLIRWAttribute[] attributes) : base(parent, name, XmlNodeType.ProcessingInstruction) {
            this.attriList = attributes;
        }

        public XMLIRWProcessingInstruction(string name, XMLIRWValue value) : this(default, name, value) {}

        public XMLIRWProcessingInstruction(string name, XMLIRWAttribute[] attributes) : this(default, name, attributes) {}

        ~XMLIRWProcessingInstruction()
            => Dispose(disposing: false);

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    Value = XMLIRWValue.Empty;
                    Name = string.Empty;
                    Parent = default;
                    attriList = default;
                    Type = XmlNodeType.None;
                }
                disposedValue = true;
            }
        }

        public override void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private XMLIRWValue GetAttriList() {
            if (attriList is XMLIRWValue) return (XMLIRWValue)attriList;
            if (attriList != null) {
                StringBuilder builder = new StringBuilder();
                foreach (var item in (XMLIRWAttribute[])attriList)
                    builder.AppendFormat("{0}=\"{1}\" ", item.Name, (string)item.Value);
                return new XMLIRWValue(builder.ToString().TrimEnd());
            }
            return new XMLIRWValue(string.Empty);
        }

        public IEnumerator<XMLIRWAttribute> GetEnumerator()
            => new ArrayToIEnumerator<XMLIRWAttribute>(attriList is XMLIRWAttribute[] ? (XMLIRWAttribute[])attriList : new XMLIRWAttribute[0]);

        IEnumerator IEnumerable.GetEnumerator()
            => new ArrayToIEnumerator<XMLIRWAttribute>(attriList is XMLIRWAttribute[] ? (XMLIRWAttribute[])attriList : new XMLIRWAttribute[0]);
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    }
}