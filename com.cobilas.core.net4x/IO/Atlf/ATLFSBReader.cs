using System.IO;

namespace Cobilas.IO.Atlf {
    public abstract class ATLFSBReader : ATLFReader {
        protected abstract Stream Stream { get; set; }
    }
}