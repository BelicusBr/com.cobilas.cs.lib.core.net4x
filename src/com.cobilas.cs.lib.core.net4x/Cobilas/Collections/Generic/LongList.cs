using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace Cobilas.Collections.Generic {
    /// <summary>Represents a long, strongly typed list of objects that can be accessed by index. 
    /// Provides methods for searching, sorting, and manipulating lists.</summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    [Serializable]
    public class LongList<T> : ILongList<T>, IReadOnlyLongList<T>, ILongList, ICloneable {
        private long _size;
        private T[] longArray;
        
        /// <inheritdoc/>
        public long Count => _size;
        /// <summary>Gets or sets the total number of elements the internal data structure can hold without resizing.</summary>
        public long Capacity { 
            get => ArrayManipulation.ArrayLongLength(longArray);
            set {
				ExceptionMessages.ThrowIfLessThan(value, _size, nameof(Capacity));
                if (value != ArrayManipulation.ArrayLongLength(longArray)) {
                    if (value > 0) {
                        T[] newArray = new T[value];
                        if (_size > 0)
                            ArrayManipulation.CopyTo(longArray, 0L, newArray, 0L, _size);
                        longArray = newArray;
                    } else longArray = [];
                }
            }
        }

        /// <inheritdoc/>
        public bool IsReadOnly => false;
        bool ILongList.IsFixedSize => false;
        bool ILongCollection.IsSynchronized => false;
        object ILongCollection.SyncRoot => longArray is ICollection col ? col.SyncRoot : this;

        /// <inheritdoc/>
        public T this[long index] { get => longArray[index]; set => longArray[index] = value; }
        object ILongList.this[long index] { get => longArray[index]!; set => longArray[index] = (T)value; }

        /// <summary>Creates a new instance of the object.</summary>
        public LongList() => longArray = [];
        /// <summary>Creates a new instance of the object.</summary>
        public LongList(long capacity) => longArray = new T[capacity];
        /// <summary>Creates a new instance of the object.</summary>
        public LongList(IEnumerable<T> collection) {
            longArray = [];
            foreach (T item in collection) Add(item);
        }
        /// <summary>Creates a new instance of the object.</summary>
        public LongList(params T[] collection) : this((IEnumerable<T>)collection) {}

        /// <summary>Returns a read-only ReadOnlyLongCollection&lt; T &gt; wrapper for the current collection.</summary>
        public ReadOnlyLongCollection<T> AsReadOnly() {
            T[] newArray = new T[_size];
            CopyTo(newArray, 0L);
            return new ReadOnlyLongCollection<T>(newArray);
        }

        /// <summary>Creates a shallow copy of a range of elements in the source <seealso cref="LongList{T}"/>.</summary>
        /// <param name="index">The zero-based <seealso cref="LongList{T}"/> index at which the range starts.</param>
        /// <param name="count">The number of elements in the range.</param>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <returns>A shallow copy of a range of elements in the source <seealso cref="LongList{T}"/>.</returns>
        public LongList<T> GetRange(long index, long count) {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException(nameof(index));
            else if (count < 0 || count > Count)
                throw new ArgumentOutOfRangeException(nameof(count));
            LongList<T> clone = new(count);
            ArrayManipulation.CopyTo(longArray, index, clone.longArray, 0L, count);
            clone._size = count;
            return clone;
        }

        /// <inheritdoc/>
        public void Add(T item) {
            if (_size == Capacity) Capacity = _size + 1;
            longArray[_size++] = item;
        }

        /// <summary>Adds the elements of the specified collection to the end of the <seealso cref="LongList{T}"/>.</summary>
        /// <param name="collection">The collection whose elements should be added to the end of the <seealso cref="LongList{T}"/>. 
        /// The collection itself cannot be <c>null</c>, but it can contain elements that are <c>null</c>, if type <c>T</c> is a 
        /// reference type.</param>
        public void AddRange(IEnumerable<T> collection) {
            foreach (T item in collection)
                Add(item);
        }
        
        /// <inheritdoc/>
        public void Clear() {
            ArrayManipulation.LongClearArraySafe(longArray);
            _size = 0;
        }

        /// <inheritdoc/>
        public bool Contains(T item) => ArrayManipulation.Exists(item, longArray);

        /// <inheritdoc cref="Contains(T)"/>
        /// <param name="match">The match parameter allows you to create custom comparison logic.</param>
        public bool Contains(Predicate<T> match) => ArrayManipulation.Exists(longArray, match);
        
        /// <inheritdoc/>
        public void CopyTo(T[] array, long arrayIndex) 
            => ArrayManipulation.CopyTo(longArray, arrayIndex, array, arrayIndex, Count);
        
        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator() {
            T[] newArray = new T[_size];
            CopyTo(newArray, 0L);
            return new ArrayToIEnumerator<T>(newArray);
        }

        /// <inheritdoc/>
        public long IndexOf(T item) => ArrayManipulation.IndexOf(item, longArray);

        /// <inheritdoc/>
        public void Insert(long index, T item) {
            ArrayManipulation.Insert<T>(item, index, ref longArray!);
            _size++;
        }

        /// <summary>Inserts the elements of a collection into the <seealso cref="LongList{T}"/> at the specified index.</summary>
        /// <param name="index">The zero-based index at which the new elements should be inserted.</param>
        /// <param name="collection">The collection whose elements should be inserted into the <seealso cref="LongList{T}"/>. 
        /// The collection itself cannot be <c>null</c>, but it can contain elements that are <c>null</c>, 
        /// if type <c>T</c> is a reference type.</param>
        public void InsertRange(long index, IEnumerable<T> collection) {
            foreach (T item in collection)
                Insert(index, item);
        }

        /// <inheritdoc/>
        public bool Remove(T item) {
            long index = IndexOf(item);
            if (index != -1) RemoveAt(index);
            return false;
        }

        /// <inheritdoc/>
        public void RemoveAt(long index) {
            long old_capacity = Capacity;
            ArrayManipulation.Remove<T>(index, ref longArray!);
            _size--;
            Capacity = old_capacity;
        }

        /// <summary>Removes all the elements that match the conditions defined by the specified predicate.</summary>
        /// <param name="match">The <seealso cref="Predicate{T}"/> delegate that defines the conditions of the elements to remove.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>The number of elements removed from the <seealso cref="LongList{T}"/>.</returns>
        public long RemoveAll(Predicate<T> match) {
			ExceptionMessages.ThrowIfNull(match, nameof(match));
            long freeIndex = 0;

            while( freeIndex < _size && !match(longArray[freeIndex])) freeIndex++;            
            if( freeIndex >= _size) return 0;

            long current = freeIndex + 1;
            while( current < _size) {
                while( current < _size && match(longArray[current])) current++;
                if ( current < _size)
                    longArray[freeIndex++] = longArray[current++];
            }
            ArrayManipulation.ClearArray(longArray, freeIndex, _size - freeIndex);
            long result = _size - freeIndex;
            _size = freeIndex;
            return result;
        }

        /// <summary>Removes a range of elements from the <seealso cref="LongList{T}"/></summary>
        /// <param name="index">The zero-based starting index of the range of elements to remove.</param>
        /// <param name="count">The number of elements to remove.</param>
        public void RemoveRamge(long index, long count) {
            long old_capacity = Capacity;
            ArrayManipulation.Remove<T>(index, count, ref longArray!);
            _size -= count;
            Capacity = old_capacity;
        }

        /// <inheritdoc/>
        public object Clone() => new LongList<T>(longArray);

        /// <summary>Copies the elements of the <seealso cref="LongList{T}"/> to a new array.</summary>
        /// <returns></returns>
        public T[] ToArray() {
            T[] newArray = new T[_size];
            CopyTo(newArray, 0L);
            return newArray;
        }

        /// <inheritdoc cref="ArrayManipulation.Reverse(Array, long, long)"/>
        public void Reverse(long index, long length)
            => ArrayManipulation.Reverse(longArray, index, length);

        /// <inheritdoc cref="ArrayManipulation.Reverse(Array)"/>
        public void Reverse() => Reverse(0L, Count);

        /// <inheritdoc cref="ArrayManipulation.Find{T}(T[], Predicate{T})"/>
        public T Find(Predicate<T> match)
            => ArrayManipulation.Find<T>(longArray, match)!;

        /// <inheritdoc cref="ArrayManipulation.FindLast{T}(T[], Predicate{T})"/>
        public T FindLast(Predicate<T> match)
            => ArrayManipulation.FindLast<T>(longArray, match)!;

        /// <inheritdoc cref="ArrayManipulation.FindAll{T}(T[], Predicate{T})"/>
        public T[] FindAll(Predicate<T> match)
            => ArrayManipulation.FindAll<T>(longArray, match)!;

        /// <inheritdoc cref="ArrayManipulation.FindIndex{T}(T[], Predicate{T})"/>
        public long FindIndex(Predicate<T> match)
            => ArrayManipulation.LongFindIndex<T>(longArray, match);

        /// <inheritdoc cref="ArrayManipulation.FindIndex{T}(T[], long, Predicate{T})"/>
        public long FindIndex(long startIndex, Predicate<T> match)
            => ArrayManipulation.FindIndex<T>(longArray, startIndex, match);

        /// <inheritdoc cref="ArrayManipulation.FindIndex{T}(T[], long, long, Predicate{T})"/>
        public long FindIndex(long startIndex, long count, Predicate<T> match)
            => ArrayManipulation.FindIndex<T>(longArray, startIndex, count, match);

        /// <inheritdoc cref="ArrayManipulation.FindLastIndex{T}(T[], Predicate{T})"/>
        public long FindLastIndex(Predicate<T> match)
            => ArrayManipulation.LongFindLastIndex<T>(longArray, match);

        /// <inheritdoc cref="ArrayManipulation.FindLastIndex{T}(T[], long, Predicate{T})"/>
        public long FindLastIndex(long startIndex, Predicate<T> match)
            => ArrayManipulation.FindLastIndex<T>(longArray, startIndex, match);

        /// <inheritdoc cref="ArrayManipulation.FindLastIndex{T}(T[], long, long, Predicate{T})"/>
        public long FindLastIndex(long startIndex, long count, Predicate<T> match)
            => ArrayManipulation.FindLastIndex<T>(longArray, startIndex, count, match);

        /// <inheritdoc cref="ArrayManipulation.LongConvertAll{TInput, TOutput}(TInput[], Converter{TInput, TOutput})"/>
        public TOutput[] ConvertAll<TOutput>(Converter<T, TOutput> converter)
            => ArrayManipulation.LongConvertAll(longArray, converter);

        /// <summary>Performs the specified action on each element of the <seealso cref="LongList{T}"/>.</summary>
        /// <param name="action">The <seealso cref="Action{T}"/> delegate to perform on each element of the <seealso cref="LongList{T}"/>.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public void ForEach(Action<T> action) {
			ExceptionMessages.ThrowIfNull(action, nameof(action));

            long _version = longArray.GetHashCode();
            foreach (T item in longArray) {
                if (_version != longArray.GetHashCode())
                    throw new InvalidOperationException("The list cannot be changed while executing the ForEach function.");
                action(item);
            }
        }

        long ILongList.Add(object value) {
            Add((T)value);
            return _size - 1;
        }

        bool ILongList.Contains(object value) => ArrayManipulation.Exists<T>((T)value, longArray);
        void ILongCollection.CopyTo(Array array, long index) 
            => ArrayManipulation.CopyTo(longArray, index, array, index, Count);
        IEnumerator IEnumerable.GetEnumerator() {
            T[] newArray = new T[_size];
            CopyTo(newArray, 0L);
            return new ArrayToIEnumerator<T>(newArray);
        }
        long ILongList.IndexOf(object value) => IndexOf((T)value);
        void ILongList.Insert(long index, object value) => Insert(index, (T)value);
        void ILongList.Remove(object value) => Remove((T)value);
    }
}