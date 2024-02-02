using System.Linq;
using System.Text;

namespace System.Xml {
    /// <summary>
    /// Extension that adds reading and writing functions for XML.
    /// </summary>
    public static class CB_XML_Extension {
        #region WriterXML
        /// <summary>
        /// Uses an <see cref="XMLIRWElement"/> to write to the xml document.
        /// </summary>
        public static void WriterXMLIRW(this XmlWriter writer, XMLIRWElement element) {
            XmlDocument xmlDocument = new XmlDocument();
            Xmlwriter(element, xmlDocument, xmlDocument);
            xmlDocument.Save(writer);
        }
    
        private static void Xmlwriter(XMLIRWElement element, XmlNode node, XmlDocument writer) {
            if (element.IsEmpty) return;

            foreach (XMLIRW item in element) {
                if (item is null)
                    throw new ArgumentNullException($"The element inside \"{element.Name}\" is null!");
                if (item is XMLIRWProcessingInstruction pi) {
                    XmlProcessingInstruction xmlpi;
                    if (pi.IsAttributeList) {
                        StringBuilder builder = new StringBuilder();
                        foreach (XMLIRWAttribute attri in pi)
                            builder.AppendFormat("{0}=\"{1}\" ", attri.Name, (string)attri.Text);
                        xmlpi = writer.CreateProcessingInstruction(pi.Name, builder.ToString().TrimEnd());
                    } else {
                        xmlpi = writer.CreateProcessingInstruction(pi.Name, (string)pi.Text);
                    }
                    node.AppendChild(xmlpi);
                } else if (item is XMLIRWComment cm)
                    node.AppendChild(writer.CreateComment((string)cm.Text));
                else if (item is XMLIRWCDATA cd)
                    node.AppendChild(writer.CreateCDataSection((string)cd.Text));
                else if (item is XMLIRWDocType doc)
                    node.AppendChild(writer.CreateDocumentType(doc.Name, (string)doc.PudID, (string)doc.SysID, (string)doc.SubSet));
                else if (item is XMLIRWDeclaration dec)
                    node.AppendChild(writer.CreateXmlDeclaration(dec.Version, dec.Encoding, dec.Standalone));
                else if (item is XMLIRWElement ele) {
                    XmlElement xmlele = writer.CreateElement(ele.Name);
                    xmlele.InnerText = (string)ele.Text;
                    foreach (XMLIRWAttribute attri in ele.Attributes.Cast<XMLIRWAttribute>())
                        xmlele.SetAttribute(attri.Name, (string)attri.Text);
                    
                    if (!ele.NoElements)
                        Xmlwriter(ele, xmlele, writer);
                    node.AppendChild(xmlele);
                }
            }
        }
    #endregion
    #region ReadXML
        /// <summary>
        /// Reads an XML document and returns an <see cref="XMLIRWElement"/>.
        /// </summary>
        public static XMLIRWElement ReadXMLIRW(this XmlReader reader) {
            XMLIRWElement element = new XMLIRWElement("Root");
            XmlDocument document = new XmlDocument();
            document.Load(reader);
            SetXMLIRWElement(document, element);
            return element;
        }

        private static void SetXMLIRWElement(XmlNode document, XMLIRWElement root) {
            XMLIRWElement forText = default;
            foreach (XmlNode node in document) {
                if (node is XmlDeclaration dec)
                    root.Add(new XMLIRWDeclaration(dec.Version, dec.Encoding, dec.Standalone));
                else if (node is XmlDocumentType doc)
                    root.Add(new XMLIRWDocType(node.LocalName, doc.PublicId, doc.SystemId, doc.InternalSubset));
                else if (node is XmlCDataSection cd)
                    root.Add(new XMLIRWCDATA(cd.Value));
                else if (node is XmlProcessingInstruction pi) {
                    if (pi.Attributes.Count != 0) {
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
                    if (forText != default && forText.Name == text.LocalName)
                        forText.Add(new XMLIRWText(text.Data));
                } else if (node is XmlElement ele) {
                    XMLIRWElement element = forText = new XMLIRWElement(ele.LocalName);
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
}