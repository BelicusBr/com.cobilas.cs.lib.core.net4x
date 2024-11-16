using System;
using System.Collections;

namespace Cobilas.Collections {
    /// <summary>Defines size, enumerators, and synchronization methods for all long non-generic collections.</summary>
    public interface ILongCollection : IEnumerable {
        /// <summary>Gets the number of elements contained in the <seealso cref="ILongCollection"/>.</summary>
        long Count { get; }
        /// <summary>Gets a value indicating whether access to the <seealso cref="ILongCollection"/> is synchronized (thread safe).</summary>
        bool IsSynchronized { get; }
        /// <summary>Gets an object that can be used to synchronize access to the <seealso cref="ILongCollection"/>.</summary>
        object SyncRoot { get; }

        /// <summary>Copies the elements of the <seealso cref="ILongCollection"/> to an Array, starting at a particular Array index.</summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements copied from ICollection. The Array must have zero-based indexing.</param>
        /// <param name="index">The zero-based <c>index</c> in array at which copying begins.</param>
        void CopyTo(Array array, long index);
    }
}