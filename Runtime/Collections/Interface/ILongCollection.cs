using System;
using System.Collections;

namespace Cobilas.Collections {
    public interface ILongCollection : IEnumerable
    {
        long Count { get; }
        bool IsSynchronized { get; }
        object SyncRoot { get; }

        void CopyTo(Array array, long index);
    }
}