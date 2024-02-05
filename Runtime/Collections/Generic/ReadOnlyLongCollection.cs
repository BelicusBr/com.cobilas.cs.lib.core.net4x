using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace Cobilas.Collections.Generic {
    /// <summary>Provides the base class for a read-only generic Long collection.</summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    [Serializable]
    public class ReadOnlyLongCollection<T> : ILongList, ILongList<T>, IReadOnlyLongList<T>, ICloneable {
        private T[] readOnlyList;
        [NonSerialized]
        private object _syncRoot;
        
        /// <inheritdoc/>
        public long Count => ArrayManipulation.ArrayLongLength(readOnlyList);
        bool ILongList.IsReadOnly => true;
        bool ILongList.IsFixedSize => true;
        bool ILongCollection<T>.IsReadOnly => true;
        bool ILongCollection.IsSynchronized => false;
        object ILongCollection.SyncRoot {
            get {
                if (_syncRoot is null) {
                    if (readOnlyList is ICollection collection)
                        _syncRoot = collection.SyncRoot;
                    else Interlocked.CompareExchange<object>(ref _syncRoot, new object(), null);
                }
                return _syncRoot;
            }
        }

        /// <inheritdoc/>
        public T this[long index] => readOnlyList[index];

        object ILongList.this[long index] { get => readOnlyList[index]; set => throw new NotImplementedException(); }
        T ILongList<T>.this[long index] { get => readOnlyList[index]; set => throw new NotImplementedException(); }

        /// <summary>Creates a new instance of the object.</summary>
        public ReadOnlyLongCollection(IEnumerable<T> enumerable) {
            foreach (T item in enumerable)
                ArrayManipulation.Add<T>(item, ref readOnlyList);
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
            => new ArrayToIEnumerator<T>(readOnlyList);

        /// <inheritdoc/>
        public object Clone() => new ReadOnlyLongCollection<T>(readOnlyList);

        IEnumerator IEnumerable.GetEnumerator()
            => new ArrayToIEnumerator<T>(readOnlyList);

        void ILongList<T>.Insert(long index, T item) => throw new NotImplementedException();
        void ILongList<T>.RemoveAt(long index) => throw new NotImplementedException();
        void ILongCollection<T>.Add(T item) => throw new NotImplementedException();
        long ILongList.Add(object value) => throw new NotImplementedException();
        bool ILongCollection<T>.Remove(T item) => throw new NotImplementedException();
        void ILongList.Insert(long index, object value) => throw new NotImplementedException();
        void ILongList.Remove(object value) => throw new NotImplementedException();
        void ILongList.RemoveAt(long index) => throw new NotImplementedException();

        void ILongCollection<T>.CopyTo(T[] array, long arrayIndex)
            => ArrayManipulation.CopyTo(readOnlyList, arrayIndex, array, arrayIndex, Count);
        void ILongCollection.CopyTo(Array array, long index)
            => ArrayManipulation.CopyTo(readOnlyList, index, array, index, Count);

        void ILongCollection<T>.Clear() => ArrayManipulation.ClearArraySafe(ref readOnlyList);
        void ILongList.Clear() => ArrayManipulation.ClearArraySafe(ref readOnlyList);

        bool ILongCollection<T>.Contains(T item)
            => ArrayManipulation.Exists(readOnlyList, (E) => EqualityComparer<T>.Default.Equals(E, item));
        bool ILongList.Contains(object value)
            => ArrayManipulation.Exists(readOnlyList, (E) => (object)E == value);

        long ILongList<T>.IndexOf(T item)
            => ArrayManipulation.LongIndexOf(item, readOnlyList);
        long ILongList.IndexOf(object value)
            => ArrayManipulation.LongIndexOf(value, readOnlyList);
    }
}