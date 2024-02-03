using System.Collections;

namespace Cobilas.Collections { 
#pragma warning disable CS1591
    public interface IReadOnlyArray : IEnumerable {
        int Count { get; }
        object this[int index] { get; }
    }
#pragma warning restore
}
