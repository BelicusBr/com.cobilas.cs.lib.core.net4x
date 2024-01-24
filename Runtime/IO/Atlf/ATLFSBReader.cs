using System.IO;

namespace Cobilas.IO.Atlf {
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    public abstract class ATLFSBReader : ATLFReader {
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        /// <summary>Represents the stream converted to <see cref="System.IO.Stream"/>.</summary>
        protected abstract Stream Stream { get; set; }
    }
}