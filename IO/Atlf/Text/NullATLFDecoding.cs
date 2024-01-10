using System;

namespace Cobilas.IO.Atlf.Text {
    public sealed class NullATLFDecoding : ATLFDecoding {
        public override string Version => string.Empty;

        public override ATLFNode[] Reader(params object[] args) => Array.Empty<ATLFNode>();
        public override ATLFNode[] Reader4Byte(params object[] args) => Array.Empty<ATLFNode>();
        protected override bool ValidCharacter(char c) => false;
    }
}