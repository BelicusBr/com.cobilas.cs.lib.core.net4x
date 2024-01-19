namespace System.Xml; 
/// <summary>
/// Base class for IRW class.
/// </summary>
public abstract class XMLIRW : IDisposable {
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    public abstract string Name { get; set; }
    public abstract XMLIRW Parent { get; set; }
    public abstract XmlNodeType Type { get; set; }

    protected XMLIRW(XMLIRW parent, string name, XmlNodeType type) {
        Name = name;
        Type = type;
        Parent = parent;
    }
    protected XMLIRW(XMLIRW parent, string name) : this(parent, name, XmlNodeType.None) {}
    protected XMLIRW(string name, XmlNodeType type) : this(default!, name, type) {}
    protected XMLIRW(string name) : this(default!, name, XmlNodeType.None) {}

    public abstract void Dispose();
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
}