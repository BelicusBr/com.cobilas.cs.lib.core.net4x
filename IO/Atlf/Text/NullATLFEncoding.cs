using System;

namespace Cobilas.IO.Atlf.Text {
    public sealed class NullATLFEncoding : ATLFEncoding {
        public override string Version => string.Empty;

        public override string Writer(params object[] args) => string.Empty;
        public override byte[] Writer4Byte(params object[] args) => Array.Empty<byte>();
    }
}