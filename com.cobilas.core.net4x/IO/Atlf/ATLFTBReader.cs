using System.IO;

namespace Cobilas.IO.Atlf {
    public abstract class ATLFTBReader : ATLFReader {
        protected abstract TextReader Stream { get; set; }
    }
}