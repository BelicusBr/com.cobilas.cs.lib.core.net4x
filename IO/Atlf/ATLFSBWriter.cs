using System.IO;

namespace Cobilas.IO.Atlf {
    public abstract class ATLFSBWriter : ATLFWriter {
        protected abstract Stream Stream { get; set; }
    }
}