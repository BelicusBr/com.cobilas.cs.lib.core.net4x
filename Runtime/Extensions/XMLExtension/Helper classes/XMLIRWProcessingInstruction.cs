using System.Collections;
using Cobilas.Collections;
using System.Collections.Generic;

namespace System.Xml {
    /// <summary>
    /// Represents an XML element of type ProcessingInstruction.
    /// </summary>
    public class XMLIRWProcessingInstruction : XMLIRW, ITextValue, IEnumerable<XMLIRWAttribute>, IDisposable {
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        private bool disposedValue;

        public XMLIRWText Text { get; set; }
        [Obsolete("Use the Text property.")]
        public XMLIRWValue Value { get; set; }
        public override XmlNodeType Type { get; set; }
        public override XMLIRW Parent { get; set; } = default;
        public override string Name { get; set; } = string.Empty;
        public bool IsAttributeList => Text.Value is XMLIRWAttribute[];
        public int AttributeCount => ArrayManipulation.ArrayLength(IsAttributeList ? Text.Value as XMLIRWAttribute[] : new XMLIRWAttribute[0]);

        [Obsolete("Use the XMLIRWProcessingInstruction(XMLIRW, string, object) constructor.")]
        public XMLIRWProcessingInstruction(XMLIRW parent, string name, XMLIRWValue value) {}
        [Obsolete("Use the XMLIRWProcessingInstruction(string, object) constructor.")]
        public XMLIRWProcessingInstruction(string name, XMLIRWValue value) {}

        public XMLIRWProcessingInstruction(XMLIRW parent, string name, object value) : base(parent, name, XmlNodeType.ProcessingInstruction)
        { Text = new XMLIRWText(value); }
        public XMLIRWProcessingInstruction(string name, object value) : this((XMLIRW)null, name, value) {}
        public XMLIRWProcessingInstruction(XMLIRW parent, string name, XMLIRWAttribute[] attributes) : this(parent, name, (object)attributes) {}
        public XMLIRWProcessingInstruction(string name, XMLIRWAttribute[] attributes) : this(default, name, attributes) {}

        ~XMLIRWProcessingInstruction()
            => Dispose(disposing: false);

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

        public override void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public IEnumerator<XMLIRWAttribute> GetEnumerator()
            => new ArrayToIEnumerator<XMLIRWAttribute>(IsAttributeList ? (XMLIRWAttribute[])Text.Value : new XMLIRWAttribute[0]);

        IEnumerator IEnumerable.GetEnumerator()
            => new ArrayToIEnumerator<XMLIRWAttribute>(IsAttributeList ? (XMLIRWAttribute[])Text.Value : new XMLIRWAttribute[0]);
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    }
}