using System.Linq;
using System.Text;

namespace System.Xml {
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    public static class CB_XML_Extension {
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        #region WriterXML
        /// <summary>
        /// Uses an <see cref="XMLIRWElement"/> to write to the xml document.
        /// </summary>
        public static void WriterXMLIRW(this XmlWriter writer, XMLIRWElement element) {
            XmlDocument xmlDocument = new XmlDocument();
            xmlwriter(element, xmlDocument);
            xmlDocument.Save(writer);
        }
    
        private static void xmlwriter(XMLIRWElement element, XmlDocument writer) {
            if (element.IsEmpty) return;

            foreach (XMLIRW item in element) {
                if (item is null)
                    throw new ArgumentNullException($"The element inside \"{element.Name}\" is null!");
                if (item is XMLIRWProcessingInstruction pi)
                    writer.AppendChild(writer.CreateProcessingInstruction(pi.Name, (string)pi.Value));
                else if (item is XMLIRWComment cm)
                    writer.AppendChild(writer.CreateComment((string)cm.Value));
                else if (item is XMLIRWCDATA cd)
                    writer.AppendChild(writer.CreateCDataSection((string)cd.Value));
                else if (item is XMLIRWDocType doc)
                    writer.AppendChild(writer.CreateDocumentType(doc.Name, (string)doc.PudID, (string)doc.SysID, (string)doc.SubSet));
                else if (item is XMLIRWDeclaration dec)
                    writer.AppendChild(writer.CreateXmlDeclaration(dec.Version, dec.Encoding, dec.Standalone));
                else if (item is XMLIRWElement ele) {
                    XmlElement xmlele = writer.CreateElement(ele.Name);
                    xmlele.Value = (string)ele.Value;
                    foreach (XMLIRWAttribute attri in ele.Attributes.Cast<XMLIRWAttribute>())
                        xmlele.SetAttribute(attri.Name, (string)attri.Value);
                    
                    if (!ele.NoElements) {
                        XmlDocument document = new XmlDocument();
                        xmlwriter(ele, document);
                        foreach (XmlNode item2 in document)
                            xmlele.AppendChild(item2);
                    }
                    writer.AppendChild(xmlele);
                }
            }
            
        }

        private static string GetWhitespace(string IndentChars, ulong layer) {
            StringBuilder builder = new StringBuilder();
            for (ulong I = 0; I < layer; I++)
                builder.Append(IndentChars);
            return builder.ToString();
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
            foreach (XmlNode item in document)
                SetXMLIRWElement(item, element);
            return element;
        }

        private static void SetXMLIRWElement(XmlNode node, XMLIRWElement root) {
            if (node is XmlDeclaration dec)
                root.Add(new XMLIRWDeclaration(dec.Version, dec.Encoding, dec.Standalone));
            else if (node is XmlDocumentType doc)
                root.Add(new XMLIRWDocType(node.LocalName, doc.PublicId, doc.SystemId, doc.InternalSubset));
            else if (node is XmlCDataSection cd)
                root.Add(new XMLIRWCDATA(cd.LocalName, new XMLIRWValue(cd.Value)));
            else if (node is XmlProcessingInstruction pi) {
                if (pi.Attributes.Count != 0) {
                    XMLIRWAttribute[] attributes = new XMLIRWAttribute[pi.Attributes.Count];
                    for (int I = 0; I < pi.Attributes.Count; I++) {
                        XmlAttribute attribute = pi.Attributes[I];
                        attributes[I] = new XMLIRWAttribute(attribute.LocalName, attribute.Value);
                    }
                    root.Add(new XMLIRWProcessingInstruction(pi.Target, attributes));
                } else root.Add(new XMLIRWProcessingInstruction(pi.Target, new XMLIRWValue(pi.Data)));
            } else if (node is XmlComment cm) 
                root.Add(new XMLIRWComment(new XMLIRWValue(cm.Value)));
            else if (node is XmlElement ele) {
                XMLIRWElement element = new XMLIRWElement(ele.LocalName);
                if (ele.Attributes.Count != 0)
                    for (int I = 0; I < ele.Attributes.Count; I++) {
                        XmlAttribute attribute = ele.Attributes[I];
                        element.Add(new XMLIRWAttribute(attribute.LocalName, attribute.Value));
                    }
                if (ele.ChildNodes.Count != 0)
                    SetXMLIRWElement(ele, element);
                root.Add(element);
            }
        }
    #endregion
    }
}