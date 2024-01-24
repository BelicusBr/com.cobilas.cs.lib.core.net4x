using System;

namespace Cobilas.IO.Atlf.Text {
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    public sealed class NullATLFEncoding : ATLFEncoding {
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        /// <inheritdoc/>
        public override string Version => string.Empty;

        /// <inheritdoc/>
        public override string Writer(params object[] args) => string.Empty;
        /// <inheritdoc/>
        public override byte[] Writer4Byte(params object[] args) => Array.Empty<byte>();
    }
}