namespace Cobilas.Collections {
    /// <summary>Represents a long, non-generic collection of objects that can be accessed individually by index.</summary>
    public interface ILongList : ILongCollection {
        /// <summary>Gets or sets the element at the specified index.</summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        object this[long index] { get; set; }

        /// <summary>Gets a value indicating whether the <seealso cref="ILongList"/> has a fixed size.</summary>
        bool IsFixedSize { get; }
        /// <summary>Gets a value indicating whether the <seealso cref="ILongList"/> is read-only.</summary>
        bool IsReadOnly { get; }

        /// <summary>Adds an item to the <seealso cref="ILongList"/>.</summary>
        /// <param name="value">The object to add to the <seealso cref="ILongList"/>.</param>
        /// <returns>The position into which the new element was inserted, or -1 to indicate that the item was not inserted into the collection.</returns>
        long Add(object value);
        /// <summary>Removes all items from the <seealso cref="ILongList"/>.</summary>
        void Clear();
        /// <summary>Determines whether the <seealso cref="ILongList"/> contains a specific value.</summary>
        /// <param name="value">The object to locate in the <seealso cref="ILongList"/>.</param>
        /// <returns><c>true</c> if the Object is found in the <seealso cref="ILongList"/>; otherwise, <c>false</c>.</returns>
        bool Contains(object value);
        /// <summary>Determines the index of a specific item in the <seealso cref="ILongList"/>.</summary>
        /// <param name="value">The object to locate in the <seealso cref="ILongList"/>.</param>
        /// <returns>The index of <c>value</c> if found in the list; otherwise, -1.</returns>
        long IndexOf(object value);
        /// <summary>Inserts an item to the <seealso cref="ILongList"/> at the specified index.</summary>
        /// <param name="index">The zero-based index at which <c>value</c> should be inserted.</param>
        /// <param name="value">The object to insert into the <seealso cref="ILongList"/>.</param>
        void Insert(long index, object value);
        /// <summary>Removes the first occurrence of a specific object from the <seealso cref="ILongList"/>.</summary>
        /// <param name="value">The object to remove from the <seealso cref="ILongList"/>.</param>
        void Remove(object value);
        /// <summary>Removes the <seealso cref="ILongList"/> item at the specified index.</summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        void RemoveAt(long index);
    }
}