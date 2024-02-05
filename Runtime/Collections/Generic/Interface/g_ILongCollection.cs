using System.Collections.Generic;

namespace Cobilas.Collections.Generic {
    /// <summary>Defines methods for manipulating Long generic collections.</summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    public interface ILongCollection<T> : IEnumerable<T> {
        /// <summary>Gets the number of elements contained in the <seealso cref="ILongCollection{T}"/>.</summary>
        long Count { get; }
        /// <summary>Gets a value indicating whether the <seealso cref="ILongCollection{T}"/> is read-only.</summary>
        bool IsReadOnly { get; }

        /// <summary>Adds an item to the <seealso cref="ILongCollection{T}"/>.</summary>
        /// <param name="item">The object to add to the <seealso cref="ILongCollection{T}"/>.</param>
        void Add(T item);
        /// <summary>Removes all items from the <seealso cref="ILongCollection{T}"/>.</summary>
        void Clear();
        /// <summary>Determines whether the <seealso cref="ILongCollection{T}"/> contains a specific value.</summary>
        /// <param name="item">The object to locate in the <seealso cref="ILongCollection{T}"/>.</param>
        /// <returns><c>true</c> if <c>item</c> is found in the <seealso cref="ILongCollection{T}"/>; otherwise, <c>false</c>.</returns>
        bool Contains(T item);
        /// <summary>Copies the elements of the <seealso cref="ILongCollection{T}"/> to an Array, starting at a particular Array index.</summary>
        /// <param name="array">The one-dimensional Array that is the destination of the elements copied from <seealso cref="ILongCollection{T}"/>. The Array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <c>array</c> at which copying begins.</param>
        void CopyTo(T[] array, long arrayIndex);
        /// <summary>Removes the first occurrence of a specific object from the <seealso cref="ILongCollection{T}"/>.</summary>
        /// <param name="item">The object to remove from the <seealso cref="ILongCollection{T}"/>.</param>
        bool Remove(T item);
    }
}