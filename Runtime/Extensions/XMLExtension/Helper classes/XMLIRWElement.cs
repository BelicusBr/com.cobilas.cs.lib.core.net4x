using System.Text;
using System.Collections;
using Cobilas.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Xml { 
    /// <summary>XML improved reader and writer element.</summary>
    public class XMLIRWElement : XMLIRW, ITextValue, IXMLIRWCollection {
        private bool disposedValue;
        private XMLIRW[] itens;
        
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
        public bool ValueIsEmpty => Text is null || Text.IsNull;
        /// <inheritdoc/>
        public override string Name { get; set; } = string.Empty;
        /// <inheritdoc/>
        public bool IsEmpty => ArrayManipulation.EmpytArray(itens);
        /// <inheritdoc/>
        public bool NoAttributes {
            get {
                if (!IsEmpty)
                    foreach (var item in itens)
                        if (item.Type == XmlNodeType.Attribute)
                            return false;
                return true;
            }
        }
        /// <inheritdoc/>
        public bool NoElements {
            get {
                if (!IsEmpty)
                    foreach (var item in itens)
                        if (item.Type != XmlNodeType.Attribute)
                            return false;
                return true;
            }
        }
        /// <inheritdoc/>
        public int AttributeCount {
            get {
                int res = 0;
                if (!NoAttributes)
                    foreach (var item in itens)
                        ++res;
                return res;
            }
        }
        /// <inheritdoc/>
        public IEnumerable<XMLIRW> Attributes {
            get {
                if (!IsEmpty)
                    foreach (var item in itens)
                        if (item.Type == XmlNodeType.Attribute)
                            yield return item;
            }
        }

        /// <inheritdoc cref="XMLIRW()"/>
        [Obsolete("Use the XMLIRWElement(XMLIRW, string, object, params XMLIRW[]) constructor.")]
        public XMLIRWElement(XMLIRW parent, string name, XMLIRWValue value, params XMLIRW[] itens)
            : base(parent, name, XmlNodeType.Element) {
            Name = name;
            this.itens = itens;
            this.Value = value;
            if (!ArrayManipulation.EmpytArray(itens))
                foreach (var item in itens)
                    item.Parent = this;
        }
        /// <inheritdoc cref="XMLIRW()"/>
        [Obsolete("Use the XMLIRWElement(XMLIRW, string, object) constructor.")]
        public XMLIRWElement(XMLIRW parent, string name, XMLIRWValue value) : this(parent, name, value, null) {}
        /// <inheritdoc cref="XMLIRW()"/>
        [Obsolete("Use the XMLIRWElement(string, object, params XMLIRW[]) constructor.")]
        public XMLIRWElement(string name, XMLIRWValue value, params XMLIRW[] itens) : this(null, name, value, itens) {}
        /// <inheritdoc cref="XMLIRW()"/>
        [Obsolete("Use the XMLIRWElement(string, object) constructor.")]
        public XMLIRWElement(string name, XMLIRWValue value) : this(null, name, value, null) {}

        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWElement(XMLIRW parent, string name, object value, params XMLIRW[] itens)
            : base(parent, name, XmlNodeType.Element) {
            Name = name;
            this.Text = new XMLIRWText(value);
            if (!ArrayManipulation.EmpytArray(itens))
                foreach (var item in itens)
                    Add(item);
        }
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWElement(XMLIRW parent, string name, object value) : this(parent, name, value, null) {}
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWElement(XMLIRW parent, string name, params XMLIRW[] itens) : this(parent, name, null, itens) {}

        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWElement(string name, object value, params XMLIRW[] itens) : this(null, name, value, itens) {}
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWElement(string name, object value) : this(null, name, value, null) {}
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWElement(string name, params XMLIRW[] itens) : this(null, name, null, itens) {}
        /// <inheritdoc cref="XMLIRW()"/>
        public XMLIRWElement(string name) : this(null, name, null) {}

        /// <summary>Called when the object is finished.</summary>
        ~XMLIRWElement() => Dispose(disposing: false);
        
        /// <inheritdoc/>
        public override void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        public bool Add(XMLIRW element) {
            if (element is null) return false;
            if (element is XMLIRWText text) {
                (Text = text).Parent = this;
                return true;
            }
            element.Parent = this;
            ArrayManipulation.Add(element, ref itens);
            return true;
        }

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        public IEnumerator<XMLIRW> GetEnumerator()
            => new ArrayToIEnumerator<XMLIRW>(itens);

        /// <inheritdoc/>
        public override string ToString() {
            StringBuilder builder = new StringBuilder();
            foreach (var item in this)
                builder.Append(ToString(item, 0UL));
            return builder.ToString();
        }

        /// <inheritdoc cref="Dispose()"/>
        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    Name = string.Empty;
                    Parent = default;
                    Type = default;
                    ArrayManipulation.ClearArraySafe(ref itens);
                }
                disposedValue = true;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => new ArrayToIEnumerator<XMLIRW>(itens);

        private static string ToString(XMLIRW element, ulong tab) {
            StringBuilder builder = new StringBuilder();

            if (element is XMLIRWElement ele) {
                builder.Append(GetTab(" ", tab))
                    .Append('>').AppendFormat("[{0}]", element.Name);
                if (!ele.NoAttributes) {
                    builder.Append("<attris: ");
                    foreach (XMLIRWAttribute item in ele.Attributes.Cast<XMLIRWAttribute>())
                        builder.AppendFormat("{0}[{1}]", item.Name, item.Text);
                    builder.Append(">");
                }
                if (!ele.ValueIsEmpty) {
                    builder.AppendLine(" {").AppendLine((string)ele.Text).Append(GetTab(" ", tab))
                        .AppendLine("}");
                } else builder.AppendLine(" {}");
                foreach (var item in ele)
                    builder.Append(ToString(item, tab + 1));
            } else if (element is XMLIRWComment cm) {
                builder.Append(GetTab(" ", tab))
                    .Append('>').AppendFormat("[{0}]", element.Name);
                string txt = (string)cm.Text;
                if (string.IsNullOrEmpty(txt)) builder.AppendLine(" {}");
                else {
                    builder.AppendLine(" {").AppendLine(txt).Append(GetTab(" ", tab))
                        .AppendLine("}");
                }
            } else if (element is XMLIRWCDATA cd) {
                builder.Append(GetTab(" ", tab))
                    .Append('>').AppendFormat("[{0}<{1}>]\r\n", element.Name, cd.Text);
            } else if (element is XMLIRWDocType doc) {
                builder.Append(GetTab(" ", tab))
                    .Append('>').AppendFormat("[{0}]<pubid[{1}] sysid[{2}] subset[{3}]>\r\n",
                     element.Name, doc.PudID, doc.SysID, doc.SubSet);
            } else if (element is XMLIRWProcessingInstruction pi) {
                builder.Append(GetTab(" ", tab))
                    .Append('>').AppendFormat("[{0}<", element.Name);
                if (pi.IsAttributeList) {
                    builder.Append("attris: ");
                    foreach (XMLIRWAttribute item in pi)
                        builder.AppendFormat("{0}[{1}]", item.Name, item.Text);
                    builder.Append(">]\r\n");
                } else builder.AppendFormat("{0}>]\r\n", pi.Text);
            } else if (element is XMLIRWDeclaration dec) {
                builder.Append(GetTab(" ", tab))
                    .Append('>').AppendFormat("[{0}<", element.Name);
                if (!string.IsNullOrEmpty(dec.Version))
                    builder.AppendFormat("version[{0}] ", dec.Version);
                if (!string.IsNullOrEmpty(dec.Encoding))
                    builder.AppendFormat("encoding[{0}] ", dec.Encoding);
                if (!string.IsNullOrEmpty(dec.Standalone))
                    builder.AppendFormat("standalone[{0}] ", dec.Standalone);
                builder.AppendLine(">]");
            }
            return builder.ToString();
        }

        private static string GetTab(string textTab, ulong count) {
            StringBuilder builder = new StringBuilder();
            for (ulong I = 0; I < count; I++)
                builder.Append(textTab);
            return builder.ToString();
        }
    }
}