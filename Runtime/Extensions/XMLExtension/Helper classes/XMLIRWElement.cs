using System.Text;
using System.Collections;
using Cobilas.Collections;
using System.Collections.Generic;

namespace System.Xml { 
    /// <summary>XML improved reader and writer element.</summary>
    public class XMLIRWElement : XMLIRW, IXMLIRWCollection {
        private bool disposedValue;
        private XMLIRW[] itens;

        /// <summary>Element name.</summary>
        public XMLIRWValue Value { get; set; }
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        public override string Name { get; set; } = string.Empty;
        public override XMLIRW Parent { get; set; } = default;
        public override XmlNodeType Type { get; set; }
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        /// <summary>Checks whether the element has a text value.</summary>
        public bool ValueIsEmpty => Value == XMLIRWValue.Empty;
        /// <summary>Checks whether the element has sub-elements or attributes.</summary>
        public bool IsEmpty => ArrayManipulation.EmpytArray(itens);
        /// <summary>Checks whether the element has attributes.</summary>
        public bool NoAttributes {
            get {
                if (!IsEmpty)
                    foreach (var item in itens)
                        if (item.Type == XmlNodeType.Attribute)
                            return false;
                return true;
            }
        }
        /// <summary>Checks whether the element has sub-elements.</summary>
        public bool NoElements {
            get {
                if (!IsEmpty)
                    foreach (var item in itens)
                        if (item.Type != XmlNodeType.Attribute)
                            return false;
                return true;
            }
        }
        /// <summary>Gets the count of attributes on the element.</summary>
        public int AttributeCount {
            get {
                int res = 0;
                if (!NoAttributes)
                    foreach (var item in itens)
                        ++res;
                return res;
            }
        }
        /// <summary>Gets the attributes on the element.</summary>
        public IEnumerable<XMLIRW> Attributes {
            get {
                if (!IsEmpty)
                    foreach (var item in itens)
                        if (item.Type == XmlNodeType.Attribute)
                            yield return item;
            }
        }

#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        public XMLIRWElement(XMLIRWElement parent, string name, XMLIRWValue value, XmlNodeType type, params XMLIRW[] itens)
            : base(parent, name, type) {
            Name = name;
            this.itens = itens;
            this.Value = value;
            if (!ArrayManipulation.EmpytArray(itens))
                foreach (var item in itens)
                    item.Parent = this;
        }
        public XMLIRWElement(XMLIRWElement parent, string name, object value, XmlNodeType type, params XMLIRW[] itens) : this(parent, name, new XMLIRWValue(value), type, itens) {}
        public XMLIRWElement(XMLIRWElement parent, string name, XMLIRWValue value, params XMLIRW[] itens) : this(parent, name, value, XmlNodeType.None, itens) {}
        public XMLIRWElement(XMLIRWElement parent, string name, object value, params XMLIRW[] itens) : this(parent, name, value, XmlNodeType.None, itens) {}
        public XMLIRWElement(XMLIRWElement parent, string name, params XMLIRW[] itens) : this(parent, name, XMLIRWValue.Empty, itens) {}
        public XMLIRWElement(XMLIRWElement parent, string name) : this(parent, name, Array.Empty<XMLIRW>()) {}

        public XMLIRWElement(XMLIRWElement parent, string name, XmlNodeType type, params XMLIRW[] itens) : this(parent, name, XMLIRWValue.Empty, type, itens) {}
        public XMLIRWElement(XMLIRWElement parent, string name, XmlNodeType type) : this(parent, name, XMLIRWValue.Empty, type, Array.Empty<XMLIRW>()) {}

        public XMLIRWElement(string name, XmlNodeType type, params XMLIRW[] itens) : this(default, name, XMLIRWValue.Empty, type, itens) {}
        public XMLIRWElement(string name, XmlNodeType type) : this(name, XMLIRWValue.Empty, type, Array.Empty<XMLIRW>()) {}

        public XMLIRWElement(string name, XMLIRWValue value, XmlNodeType type, params XMLIRW[] itens) : this(default, name, value, type, itens) {}
        public XMLIRWElement(string name, object value, XmlNodeType type, params XMLIRW[] itens) : this(name, new XMLIRWValue(value), type, itens) {}
        public XMLIRWElement(string name, XMLIRWValue value, params XMLIRW[] itens) : this(name, value, XmlNodeType.None, itens) {}
        public XMLIRWElement(string name, object value, params XMLIRW[] itens) : this(name, value, XmlNodeType.None, itens) {}
        public XMLIRWElement(string name, params XMLIRW[] itens) : this(name, XMLIRWValue.Empty, itens) {}
        public XMLIRWElement(string name) : this(name, Array.Empty<XMLIRW>()) {}

        ~XMLIRWElement() => Dispose(disposing: false);

        public override void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public bool Add(XMLIRW element) {
            if (element is null) return false;
            element.Parent = this;
            ArrayManipulation.Add(element, ref itens);
            return true;
        }

        public IEnumerator<XMLIRW> GetEnumerator()
            => new ArrayToIEnumerator<XMLIRW>(itens);

        public override string ToString() {
            StringBuilder builder = new StringBuilder();
            foreach (var item in this)
                builder.Append(ToString(item, 0UL));
            return builder.ToString();
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    Name = string.Empty;
                    Parent = default;
                    Type = default;
                    Value = default;
                    ArrayManipulation.ClearArraySafe(ref itens);
                }
                disposedValue = true;
            }
        }
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente

        IEnumerator IEnumerable.GetEnumerator()
            => new ArrayToIEnumerator<XMLIRW>(itens);

        private static string ToString(XMLIRW element, ulong tab) {
            StringBuilder builder = new StringBuilder();

            builder.Append("//").Append(GetTab("=", tab)).AppendLine($"===[{element.Name}]=>");

            if (element is XMLIRWElement ele) {
                builder.Append(GetTab(" ", tab)).AppendLine(ele.Value.ToString());
                foreach (var item in ele)
                    ToString(item, tab + 1);
            } else if (element is XMLIRWAttribute atri)
                builder.Append("//").Append(GetTab("=", tab)).AppendLine($"===[attr][{atri.Name}]>\"{atri.Value}\"");
            else if (element is XMLIRWComment com) 
                builder.Append("//").Append(GetTab("=", tab)).AppendLine($"===[{com.Name}]>\"{com.Value}\"");
            else if (element is XMLIRWCDATA cdata) 
                builder.Append("//").Append(GetTab("=", tab)).AppendLine($"===[{cdata.Name}]>\"{cdata.Value}\"");
            else if (element is XMLIRWDocType docType) {
                builder.Append("//").Append(GetTab("=", tab)).AppendLine($"===[{docType.Name}]>");

                builder.Append(GetTab(" ", tab + 2)).AppendLine($"[{docType.Name}][PudID]:{{{docType.PudID}}}");
                builder.Append(GetTab(" ", tab + 2)).AppendLine($"[{docType.Name}][SysID]:{{{docType.SysID}}}");
                builder.Append(GetTab(" ", tab + 2)).AppendLine($"[{docType.Name}][SubSet]:{{{docType.SubSet}}}");
            
                builder.Append("//").Append(GetTab("=", tab)).AppendLine($"===[{docType.Name}]<>");
            }

            builder.Append("//").Append(GetTab("=", tab)).AppendLine($"===[{element.Name}]<>");

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