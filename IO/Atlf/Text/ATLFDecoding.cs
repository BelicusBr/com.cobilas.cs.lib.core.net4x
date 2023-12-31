using System.Text;

namespace Cobilas.IO.Atlf.Text {
    public abstract class ATLFDecoding {
        public abstract string Version { get; }

        public abstract ATLFNode[] Reader(params object[] args);
        public abstract ATLFNode[] Reader4Byte(params object[] args);
        protected abstract bool ValidCharacter(char c);
    }
}