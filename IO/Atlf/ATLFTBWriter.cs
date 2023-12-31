using System.IO;

namespace Cobilas.IO.Atlf {
    public abstract class ATLFTBWriter : ATLFWriter {
        protected abstract TextWriter Stream { get; set; }
    }
}