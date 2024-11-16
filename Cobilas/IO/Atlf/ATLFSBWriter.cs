using System.IO;

namespace Cobilas.IO.Atlf;
/// <summary>Base class for stream writers for ATLF.</summary>
public abstract class ATLFSBWriter : ATLFWriter {
    /// <summary>Represents the stream converted to <see cref="System.IO.Stream"/>.</summary>
    protected abstract Stream Stream { get; set; }
}