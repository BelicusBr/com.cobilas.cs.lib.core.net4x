using System.Text;
using System.Collections;
using Cobilas.Collections;
using System.Collections.Generic;

namespace System.Xml { 
    /// <summary>XML improved reader and writer element.</summary>
    public class XMLIRWElement : XMLIRW, ITextValue, IXMLIRWCollection {
        private bool disposedValue;
        private XMLIRW[] itens;

        public XMLIRWText Text { get; set; }
        /// <summary>Element name.</summary>
        [Obsolete("Use the Text property.")]
        public XMLIRWValue Value { get; set; }
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        public override string Name { get; set; } = string.Empty;
        public override XMLIRW Parent { get; set; } = default;
        public override XmlNodeType Type { get; set; }
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        /// <summary>Checks whether the element has a text value.</summary>
        public bool ValueIsEmpty => Text is null || Text.IsNull;
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
        [Obsolete("Use the XMLIRWElement(XMLIRW, string, object) constructor.")]
        public XMLIRWElement(XMLIRW parent, string name, XMLIRWValue value) : this(parent, name, value, null) {}
        [Obsolete("Use the XMLIRWElement(string, object, params XMLIRW[]) constructor.")]
        public XMLIRWElement(string name, XMLIRWValue value, params XMLIRW[] itens) : this(null, name, value, itens) {}
        [Obsolete("Use the XMLIRWElement(string, object) constructor.")]
        public XMLIRWElement(string name, XMLIRWValue value) : this(null, name, value, null) {}

        public XMLIRWElement(XMLIRW parent, string name, object value, params XMLIRW[] itens)
            : base(parent, name, XmlNodeType.Element) {
            Name = name;
            this.itens = itens;
            this.Text = new XMLIRWText(value);
            if (!ArrayManipulation.EmpytArray(itens))
                foreach (var item in itens)
                    item.Parent = this;
        }
        public XMLIRWElement(XMLIRW parent, string name, object value) : this(parent, name, value, null) {}
        public XMLIRWElement(XMLIRW parent, string name, params XMLIRW[] itens) : this(parent, name, null, itens) {}

        public XMLIRWElement(string name, object value, params XMLIRW[] itens) : this(null, name, value, itens) {}
        public XMLIRWElement(string name, object value) : this(null, name, value, null) {}
        public XMLIRWElement(string name, params XMLIRW[] itens) : this(null, name, null, itens) {}
        public XMLIRWElement(string name) : this(null, name, null) {}

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

            if (element is XMLIRWElement ele) {
                builder.Append(GetTab(" ", tab))
                    .Append('>').AppendFormat("[{0}]", element.Name);
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
                    builder.AppendLine(" {").AppendLine((string)cm.Text).Append(GetTab(" ", tab))
                        .AppendLine("}");
                }
            } else if (element is XMLIRWCDATA cd) {
                builder.Append(GetTab(" ", tab))
                    .Append('>').AppendFormat("[{0}]", element.Name);
                string txt = (string)cd.Text;
                if (string.IsNullOrEmpty(txt)) builder.AppendLine(" {}");
                else {
                    builder.AppendLine(" {").AppendLine((string)cd.Text).Append(GetTab(" ", tab))
                        .AppendLine("}");
                }
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