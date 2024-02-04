using System;
using System.Collections;
using System.Collections.Generic;

namespace Cobilas.Collections.Generic {
    [Serializable]
    public class LongList<T> : ILongList<T>, IReadOnlyLongList<T>, ILongList, ICloneable {
        private long _size;
        private T[] longArray;
        
        public long Count => _size;
        public long Capacity { 
            get => ArrayManipulation.ArrayLongLength(longArray);
            set {
                if (value < _size)
                    throw new ArgumentOutOfRangeException("The capacity cannot be smaller than the list size.");
                if (value != ArrayManipulation.ArrayLongLength(longArray)) {
                    if (value > 0) {
                        T[] newArray = new T[value];
                        if (_size > 0)
                            ArrayManipulation.CopyTo(longArray, 0L, newArray, 0L, _size);
                        longArray = newArray;
                    } else longArray = Array.Empty<T>();
                }
            }
        }
        public bool IsReadOnly => throw new NotImplementedException();
        bool ILongList.IsFixedSize => throw new NotImplementedException();
        bool ILongCollection.IsSynchronized => throw new NotImplementedException();
        object ILongCollection.SyncRoot => throw new NotImplementedException();

        public T this[long index] { get => longArray[index]; set => longArray[index] = value; }
        object ILongList.this[long index] { get => longArray[index]; set => longArray[index] = (T)value; }

        public LongList() => longArray = Array.Empty<T>();
        public LongList(long capacity) => longArray = new T[capacity];
        public LongList(IEnumerable<T> collection) {

        }

        public ReadOnlyLongCollection<T> AsReadOnly() => new ReadOnlyLongCollection<T>(longArray);

        public void Add(T item) {
            if (_size == Capacity) Capacity = _size + 1;
            longArray[_size++] = item;
        }

        public void Clear() {
            ArrayManipulation.LongClearArraySafe(longArray);
            _size = 0;
        }

        public bool Contains(T item)
            => ArrayManipulation.Exists(item, longArray);

        public void CopyTo(T[] array, long arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public long IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        public void Insert(long index, T item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(long index)
        {
            throw new NotImplementedException();
        }
        public object Clone()
        {
            throw new NotImplementedException();
        }

        long ILongList.Add(object value)
        {
            throw new NotImplementedException();
        }

        bool ILongList.Contains(object value)
        {
            throw new NotImplementedException();
        }

        void ILongCollection.CopyTo(Array array, long index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        long ILongList.IndexOf(object value)
        {
            throw new NotImplementedException();
        }

        void ILongList.Insert(long index, object value)
        {
            throw new NotImplementedException();
        }

        void ILongList.Remove(object value)
        {
            throw new NotImplementedException();
        }
    }
}