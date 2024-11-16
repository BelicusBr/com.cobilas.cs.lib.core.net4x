using System.Linq;
using System.Text;
using Cobilas.Collections;

namespace System.Xml; 
/// <summary>
/// Extension that adds reading and writing functions for XML.
/// </summary>
public static class CB_XML_Extension {
    #region WriterXML
    /// <summary>
    /// Uses an <see cref="XMLIRWElement"/> to write to the xml document.
    /// </summary>
    public static void WriterXMLIRW(this XmlWriter writer, XMLIRWElement element) {
        writer.WriteStartDocument();
        if (writer.Settings.Indent)
            writer.WriteWhitespace(writer.Settings.NewLineChars);
        Xmlwriter(element, writer);
        writer.WriteEndDocument();
    }

    private static void Xmlwriter(XMLIRWElement element, XmlWriter writer, ulong level = 0UL) {
        if (element.IsEmpty) return;
        foreach (XMLIRW item in element) {
            if (item is null)
                throw new ArgumentNullException($"The element inside \"{element.Name}\" is null!");
            if (item is XMLIRWProcessingInstruction pi) {
                if (writer.Settings.Indent)
                    writer.WriteWhitespace(IndentLevel(level, writer.Settings.IndentChars));
                if (pi.IsAttributeList) {
                    StringBuilder builder = new();
                    foreach (XMLIRWAttribute attri in pi)
                        builder.AppendFormat("{0}=\"{1}\" ", attri.Name, (string)attri.Text);
                    writer.WriteProcessingInstruction(pi.Name, builder.ToString().TrimEnd());
                } else {
                    writer.WriteProcessingInstruction(pi.Name, (string)pi.Text);
                }
                if (writer.Settings.Indent)
                    writer.WriteWhitespace(writer.Settings.NewLineChars);
            } else if (item is XMLIRWComment cm) {
                if (writer.Settings.Indent)
                    writer.WriteWhitespace(IndentLevel(level, writer.Settings.IndentChars));
                writer.WriteComment((string)cm.Text);
                if (writer.Settings.Indent)
                    writer.WriteWhitespace(writer.Settings.NewLineChars);
            } else if (item is XMLIRWCDATA cd) {
                if (writer.Settings.Indent)
                    writer.WriteWhitespace(IndentLevel(level, writer.Settings.IndentChars));
                writer.WriteCData((string)cd.Text);
                if (writer.Settings.Indent)
                    writer.WriteWhitespace(writer.Settings.NewLineChars);
            } else if (item is XMLIRWDocType doc) {
                if (writer.Settings.Indent)
                    writer.WriteWhitespace(IndentLevel(level, writer.Settings.IndentChars));
                writer.WriteDocType(doc.Name, (string)doc.PudID, (string)doc.SysID, (string)doc.SubSet);
                if (writer.Settings.Indent)
                    writer.WriteWhitespace(writer.Settings.NewLineChars);
            }  else if (item is XMLIRWElement ele) {
                if (writer.Settings.Indent)
                    writer.WriteWhitespace(IndentLevel(level, writer.Settings.IndentChars));
                writer.WriteStartElement(ele.Name);
                if (!ele.NoAttributes)
                    foreach (XMLIRWAttribute attris in ele.Attributes.Cast<XMLIRWAttribute>()) 
                        writer.WriteAttributeString(attris.Name, (string)attris.Text);
                bool noText = false;
                if (!ele.ValueIsEmpty) {
                    noText = true;
                    writer.WriteString((string)ele.Text);
                    if (writer.Settings.Indent && !ele.NoElements)
                        writer.WriteWhitespace(writer.Settings.NewLineChars);
                }
                if (!ele.NoElements) {
                    if (writer.Settings.Indent && !noText)
                        writer.WriteWhitespace(writer.Settings.NewLineChars);
                    Xmlwriter(ele, writer, level + 1);
                }
                if (writer.Settings.Indent && !ele.NoElements)
                    writer.WriteWhitespace(IndentLevel(level, writer.Settings.IndentChars));
                writer.WriteEndElement();
                if (writer.Settings.Indent)
                    writer.WriteWhitespace(writer.Settings.NewLineChars);
            }
        }
    }

    private static string IndentLevel(ulong level, string text) {
        StringBuilder builder = new();
        for (ulong I = 0; I < level; I++)
            builder.Append(text);
        return builder.ToString();
    }
#endregion
#region ReadXML
    /// <summary>
    /// Reads an XML document and returns an <see cref="XMLIRWElement"/>.
    /// </summary>
    public static XMLIRWElement ReadXMLIRW(this XmlReader reader) {
        XMLIRWElement element = new("Root");
        XmlDocument document = new();
        document.Load(reader);
        SetXMLIRWElement(document, element);
        return element;
    }

    private static void SetXMLIRWElement(XmlNode document, XMLIRWElement root) {
        foreach (XmlNode node in document) {
            if (node is XmlDeclaration dec)
                root.Add(new XMLIRWDeclaration(dec.Version, dec.Encoding, dec.Standalone));
            else if (node is XmlDocumentType doc)
                root.Add(new XMLIRWDocType(node.LocalName, doc.PublicId, doc.SystemId, doc.InternalSubset));
            else if (node is XmlCDataSection cd)
                root.Add(new XMLIRWCDATA(cd.Value));
            else if (node is XmlProcessingInstruction pi) {
                if (!ArrayManipulation.EmpytArray(pi.Attributes)) {
                    XMLIRWAttribute[] attributes = new XMLIRWAttribute[pi.Attributes.Count];
                    for (int I = 0; I < pi.Attributes.Count; I++) {
                        XmlAttribute attribute = pi.Attributes[I];
                        attributes[I] = new XMLIRWAttribute(attribute.LocalName, attribute.InnerText);
                    }
                    root.Add(new XMLIRWProcessingInstruction(pi.Target, attributes));
                } else root.Add(new XMLIRWProcessingInstruction(pi.Target, pi.Data));
            } else if (node is XmlComment cm) 
                root.Add(new XMLIRWComment(cm.InnerText));
            else if (node is XmlText text) {
                root.Add(new XMLIRWText(text.Data));
            } else if (node is XmlElement ele) {
                XMLIRWElement element = new(ele.LocalName);
                if (ele.Attributes.Count != 0)
                    for (int I = 0; I < ele.Attributes.Count; I++) {
                        XmlAttribute attribute = ele.Attributes[I];
                        element.Add(new XMLIRWAttribute(attribute.LocalName, attribute.InnerText));
                    }
                if (ele.ChildNodes.Count != 0) {
                    SetXMLIRWElement(ele, element);
                }
                root.Add(element);
            }
        }
    }
#endregion
}