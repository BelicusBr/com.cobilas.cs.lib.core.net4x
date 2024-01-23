using System.Text;

namespace System.Xml;
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
public static class CB_XML_Extension {
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    #region WriterXML
    /// <summary>
    /// Uses an <see cref="XMLIRWElement"/> to write to the xml document.
    /// </summary>
    public static void WriterXMLIRW(this XmlWriter writer, XMLIRWElement element)
        => xmlwriter(element, writer, 0UL);
    
    private static void xmlwriter(XMLIRWElement element, XmlWriter writer, ulong layer) {
        XmlWriterSettings settings = writer.Settings;
        if (element.IsEmpty) return;
        foreach (var item in element) {
            if (item is null)
                throw new ArgumentNullException($"The element inside \"{element.Name}\" is null!");
            switch (item.Type) {
                case XmlNodeType.ProcessingInstruction:
                    if (settings.Indent)
                        writer.WriteWhitespace(GetWhitespace(settings.IndentChars, layer));
                    writer.WriteProcessingInstruction(item.Name, (string)(item as XMLIRWProcessingInstruction)!.Value);
                    if (settings.Indent)
                        writer.WriteWhitespace(settings.NewLineChars);
                    break;
                case XmlNodeType.CDATA:
                        if (settings.Indent)
                            writer.WriteWhitespace(GetWhitespace(settings.IndentChars, layer));
                        writer.WriteCData((string)(item as XMLIRWCDATA)!.Value);
                        if (settings.Indent)
                            writer.WriteWhitespace(settings.NewLineChars);
                    break;
                case XmlNodeType.DocumentType:
                        if (settings.Indent)
                            writer.WriteWhitespace(GetWhitespace(settings.IndentChars, layer));
                        XMLIRWDocType docType = (item as XMLIRWDocType)!;
                        writer.WriteDocType(docType!.Name, (string)docType.PudID, (string)docType.SysID, (string)docType.SubSet);
                        if (settings.Indent)
                            writer.WriteWhitespace(settings.NewLineChars);
                    break;
                case XmlNodeType.Comment:
                        if (settings.Indent)
                            writer.WriteWhitespace(GetWhitespace(settings.IndentChars, layer));
                        writer.WriteComment((string)(item as XMLIRWComment)!.Value);
                        if (settings.Indent)
                            writer.WriteWhitespace(settings.NewLineChars);
                    break;
                case XmlNodeType.Element:
                    if (settings.Indent)
                        writer.WriteWhitespace(GetWhitespace(settings.IndentChars, layer));
                    XMLIRWElement elem = (item as XMLIRWElement)!;
                    writer.WriteStartElement(elem!.Name);
                    foreach (XMLIRWAttribute attri in elem.Attributes)
                        writer.WriteAttributeString(attri.Name, (string)attri.Value);

                    if (!elem.ValueIsEmpty) {
                        writer.WriteString((string)elem.Value);
                        if (settings.Indent)
                            writer.WriteWhitespace(settings.NewLineChars);
                    }
                    xmlwriter(elem, writer, layer + 1);
                    if (settings.Indent && !elem.NoElements)
                        writer.WriteWhitespace(GetWhitespace(settings.IndentChars, layer));
                    writer.WriteEndElement();
                    if (settings.Indent)
                        writer.WriteWhitespace(settings.NewLineChars);
                    break;
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
    public static XMLIRWElement? ReadXMLIRW(this XmlReader reader) {
        XMLIRWElement element = new("Root");
        ulong cdata = 0;
        XMLIRWElement[] attributes;
        while (reader.Read()) {
            switch (reader.NodeType) {
                case XmlNodeType.ProcessingInstruction:
                    attributes = new XMLIRWElement[reader.AttributeCount];
                    if(reader.AttributeCount != 0) {
                        for (int I = 0; I < reader.AttributeCount; I++) {
                            reader.MoveToAttribute(I);
                            attributes[I] = new XMLIRWElement(reader.Name, reader.Value, XmlNodeType.Attribute, default!);
                        }
                        reader.MoveToElement();
                    }
                    element!.Add(new XMLIRWElement(reader.Name, XmlNodeType.ProcessingInstruction, attributes));
                    break;
                case XmlNodeType.XmlDeclaration:
                    attributes = new XMLIRWElement[reader.AttributeCount];
                    if(reader.AttributeCount != 0) {
                        for (int I = 0; I < reader.AttributeCount; I++) {
                            reader.MoveToAttribute(I);
                            attributes[I] = new XMLIRWElement(reader.Name, reader.Value, XmlNodeType.Attribute, default!);
                        }
                        reader.MoveToElement();
                    }
                    element!.Add(new XMLIRWElement(reader.Name, XmlNodeType.XmlDeclaration, attributes));
                    break;
                case XmlNodeType.CDATA:
                    element!.Add(new XMLIRWElement($"CDATA:{cdata}", reader.Value, XmlNodeType.CDATA, default!));
                    ++cdata;
                    break;
                case XmlNodeType.DocumentType:
                    element!.Add(new XMLIRWElement(reader.Name, reader.Value, XmlNodeType.DocumentType, default!));
                    break;
                case XmlNodeType.Element:
                    attributes = new XMLIRWElement[reader.AttributeCount];
                    if(reader.AttributeCount != 0) {
                        for (int I = 0; I < reader.AttributeCount; I++) {
                            reader.MoveToAttribute(I);
                            attributes[I] = new XMLIRWElement(reader.Name, reader.Value, XmlNodeType.Attribute, default!);
                        }
                        reader.MoveToElement();
                    }
                    if (reader.IsEmptyElement)
                        element!.Add(new XMLIRWElement(reader.Name, XmlNodeType.Element, attributes));
                    else element!.Add(element = new XMLIRWElement(reader.Name, XmlNodeType.Element, attributes));
                    break;
                case XmlNodeType.Text:
                    element!.Value = new XMLIRWValue(reader.Value);
                    break;
                case XmlNodeType.EndElement:
                        element = (element.Parent as XMLIRWElement)!;
                    break;
            }
        }
        return element;
    }
#endregion
}