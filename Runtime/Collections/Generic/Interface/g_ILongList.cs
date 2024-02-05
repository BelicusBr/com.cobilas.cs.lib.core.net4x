namespace Cobilas.Collections.Generic {
    /// <summary>Represents a long collection of objects that can be accessed individually by index.</summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public interface ILongList<T> : ILongCollection<T> {
        /// <summary>Gets or sets the element at the specified index.</summary>
        T this[long index] { get; set; }

        /// <summary>Determines the index of a specific item in the <seealso cref="ILongList{T}"/>.</summary>
        /// <param name="item">The object to locate in the <seealso cref="ILongList{T}"/>.</param>
        /// <returns>The index of <c>item</c> if found in the list; otherwise, -1.</returns>
        long IndexOf(T item);
        /// <summary>Inserts an item to the <seealso cref="ILongList{T}"/> at the specified index.</summary>
        /// <param name="index">The zero-based index at which <c>item</c> should be inserted.</param>
        /// <param name="item">The object to insert into the <seealso cref="ILongList{T}"/>.</param>
        void Insert(long index, T item);
        /// <summary>Removes the <seealso cref="ILongList{T}"/> item at the specified index.</summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        void RemoveAt(long index);   
    }
}