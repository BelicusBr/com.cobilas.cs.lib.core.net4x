using System.Collections.Generic;

namespace Cobilas.Collections.Generic {
    public interface ILongCollection<T> : IEnumerable<T> {
        long Count { get; }
        bool IsReadOnly { get; }

        void Add(T item);
        void Clear();
        bool Contains(T item);
        void CopyTo(T[] array, long arrayIndex);
        bool Remove(T item);
    }
}