using System.IO;

namespace Cobilas.IO.Atlf; 
/// <summary>Base class for string readers for ATLF.</summary>
public abstract class ATLFTBReader : ATLFReader {
    /// <summary>Represents the stream converted to <see cref="TextWriter"/>.</summary>
    protected abstract TextReader Stream { get; set; }
}