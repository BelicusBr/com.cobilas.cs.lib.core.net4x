namespace System.Xml { 
    /// <summary>Base class for IRW class.</summary>
    public abstract class XMLIRW : IDisposable {
        /// <summary>Returns or sets the name of the XMLIRW object.</summary>
        public abstract string Name { get; set; }
        /// <summary>Returns or sets the name of the parent object of the XMLIRW element.</summary>
        public abstract XMLIRW Parent { get; set; }
        /// <summary>Returns or sets the type of the XMLIRW element.</summary>
        public abstract XmlNodeType Type { get; set; }

        /// <summary>Creates a new instance of the XMLIRW element.</summary>
        protected XMLIRW(XMLIRW parent, string name, XmlNodeType type) {
            Name = name;
            Type = type;
            Parent = parent;
        }
        /// <summary>Creates a new instance of the XMLIRW element.</summary>
        protected XMLIRW(XMLIRW parent, string name) : this(parent, name, XmlNodeType.None) {}
        /// <summary>Creates a new instance of the XMLIRW element.</summary>
        protected XMLIRW(string name, XmlNodeType type) : this(default, name, type) {}
        /// <summary>Creates a new instance of the XMLIRW element.</summary>
        protected XMLIRW(string name) : this(default, name, XmlNodeType.None) {}
        /// <summary>Creates a new instance of the XMLIRW element.</summary>
        protected XMLIRW() {}

        /// <summary>Discard the resources of the XMLIRW element.</summary>
        public abstract void Dispose();
        /// <summary>Creates a <see cref="string"/> representation of the <see cref="object"/>.</summary>
        public override string ToString() {
            return base.ToString();
        }
    }
}