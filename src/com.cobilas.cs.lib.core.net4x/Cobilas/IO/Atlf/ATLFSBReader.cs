using System.IO;

namespace Cobilas.IO.Atlf;
/// <summary>Base class for stream readers for ATLF.</summary>
public abstract class ATLFSBReader : ATLFReader {
    /// <summary>Represents the stream converted to <see cref="System.IO.Stream"/>.</summary>
    protected abstract Stream Stream { get; set; }
}