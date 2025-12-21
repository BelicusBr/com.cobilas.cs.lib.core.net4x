using System.IO;

namespace Cobilas.IO.Atlf;
/// <summary>Base class for string writers for ATLF.</summary>
public abstract class ATLFTBWriter : ATLFWriter {
    /// <summary>Represents the stream converted to <see cref="TextWriter"/>.</summary>
    protected abstract TextWriter Stream { get; set; }
}
