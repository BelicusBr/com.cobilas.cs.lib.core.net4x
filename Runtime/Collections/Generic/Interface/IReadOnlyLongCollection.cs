using System.Collections.Generic;

namespace Cobilas.Collections.Generic {
    public interface IReadOnlyLongCollection<out T> : IEnumerable<T>
    {
        long Count { get; }
    }
}