using System.Collections.Generic;

namespace System.Xml { 
    /// <summary>
    /// Represents a collection of XMLIRW.
    /// </summary>
    public interface IXMLIRWCollection : IEnumerable<XMLIRW>, IDisposable {
        /// <summary>Checks whether the element has sub-elements or attributes.</summary>
        bool IsEmpty {get;}
        /// <summary>Checks whether the element has sub-elements.</summary>
        bool NoElements {get;}
        /// <summary>Checks whether the element has a text value.</summary>
        bool ValueIsEmpty {get;}
        /// <summary>Checks whether the element has attributes.</summary>
        bool NoAttributes {get;}
        /// <summary>Gets the count of attributes on the element.</summary>
        int AttributeCount {get;}
        /// <summary>Gets the attributes on the element.</summary>
        IEnumerable<XMLIRW> Attributes {get;}

#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        bool Add(XMLIRWElement element);
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    }
}