using System.Collections;
using Cobilas.Collections;
using System.Collections.Generic;

namespace System.Xml {
    /// <summary>
    /// Represents an XML element of type ProcessingInstruction.
    /// </summary>
    public class XMLIRWProcessingInstruction : XMLIRW, ITextValue, IEnumerable<XMLIRWAttribute>, IDisposable {
        private bool disposedValue;
        
        /// <inheritdoc/>
        public XMLIRWText Text { get; set; }
        /// <inheritdoc cref="Text"/>
        [Obsolete("Use the Text property.")]
        public XMLIRWValue Value { get; set; }
        /// <inheritdoc/>
        public override XmlNodeType Type { get; set; }
        /// <inheritdoc/>
        public override XMLIRW Parent { get; set; } = default;
        /// <inheritdoc/>
        public override string Name { get; set; } = string.Empty;
        /// <summary>Checks whether the value is a list of attributes.</summary>
        public bool IsAttributeList => Text.Value is XMLIRWAttribute[];
        /// <summary>Returns the number of attributes in the list.</summary>
        public int AttributeCount => ArrayManipulation.ArrayLength(IsAttributeList ? Text.Value as XMLIRWAttribute[] : new XMLIRWAttribute[0]);

        /// <inheritdoc cref="XMLIRW()"/>
        [Obsolete("Use the XMLIRWProcessingInstruction(XMLIRW, string, object) constructor.")]
        public XMLIRWProcessingInstruction(XMLIRW parent, string name, XMLIRWValue value) {}
        /// <inheritdoc cref="XMLIRW()"/>
        [Obsolete("Use the XMLIRWProcessingInstruction(string, object) constructor.")]
        public XMLIRWProcessingInstruction(string name, XMLIRWValue value) {}

        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWProcessingInstruction(XMLIRW parent, string name, object value) : base(parent, name, XmlNodeType.ProcessingInstruction)
        { Text = new XMLIRWText(value); }
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWProcessingInstruction(string name, object value) : this((XMLIRW)null, name, value) {}
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWProcessingInstruction(XMLIRW parent, string name, XMLIRWAttribute[] attributes) : this(parent, name, (object)attributes) {}
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWProcessingInstruction(string name, XMLIRWAttribute[] attributes) : this(default, name, attributes) {}

        /// <summary>Called when the object is finished.</summary>
        ~XMLIRWProcessingInstruction()
            => Dispose(disposing: false);
        
        /// <inheritdoc/>
        public override void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc cref="XMLIRWElement.GetEnumerator()"/>
        public IEnumerator<XMLIRWAttribute> GetEnumerator()
            => new ArrayToIEnumerator<XMLIRWAttribute>(IsAttributeList ? (XMLIRWAttribute[])Text.Value : new XMLIRWAttribute[0]);

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

        IEnumerator IEnumerable.GetEnumerator()
            => new ArrayToIEnumerator<XMLIRWAttribute>(IsAttributeList ? (XMLIRWAttribute[])Text.Value : new XMLIRWAttribute[0]);
    }
}