namespace Cobilas.IO.Atlf.Text;  
/// <summary>Encoding base class.</summary>
public abstract class ATLFEncoding {
    /// <summary>Represents a null value of type <see cref="NullATLFEncoding"/>.</summary>
    public readonly static ATLFEncoding Null = new NullATLFEncoding();

    /// <summary>Represents the encoder version.</summary>
    public abstract string Version { get; }

    /// <summary>Writes a list of objects.</summary>
    public abstract string Writer(params object[] args);
    /// <summary>Writes a list of objects.</summary>
    public abstract byte[] Writer4Byte(params object[] args);
}