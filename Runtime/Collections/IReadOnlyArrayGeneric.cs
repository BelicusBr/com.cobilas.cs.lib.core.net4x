using System.Collections.Generic;

namespace Cobilas.Collections { 
#pragma warning disable CS0108 // O membro oculta o membro herdado; nova palavra-chave ausente
#pragma warning disable CS1591
    public interface IReadOnlyArray<T> : IReadOnlyArray, IEnumerable<T> {
        T this[int index] { get; }
    }

#pragma warning restore // O membro oculta o membro herdado; nova palavra-chave ausente
}