using System;

namespace Cobilas.IO.Atlf.Text {
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    public sealed class NullATLFDecoding : ATLFDecoding {
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        /// <inheritdoc/>
        public override string Version => string.Empty;

        /// <inheritdoc/>
        public override ATLFNode[] Reader(params object[] args) => Array.Empty<ATLFNode>();
        /// <inheritdoc/>
        public override ATLFNode[] Reader4Byte(params object[] args) => Array.Empty<ATLFNode>();
        /// <inheritdoc/>
        protected override bool ValidCharacter(char c) => false;
    }
}