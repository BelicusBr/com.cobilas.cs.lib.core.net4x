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

        /// <summary>Adds a new XMLIRW element.</summary>
        /// <returns>Returns <c>true</c> when the element is added XMLIRW.</returns>
        bool Add(XMLIRW element);
    }
}