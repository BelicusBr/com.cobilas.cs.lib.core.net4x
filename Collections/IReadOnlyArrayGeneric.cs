using System.Collections.Generic;

namespace Cobilas.Collections; 
public interface IReadOnlyArray<T> : IReadOnlyArray, IEnumerable<T> {
#pragma warning disable CS0108 // O membro oculta o membro herdado; nova palavra-chave ausente
    T this[int index] { get; }
#pragma warning restore CS0108 // O membro oculta o membro herdado; nova palavra-chave ausente
}