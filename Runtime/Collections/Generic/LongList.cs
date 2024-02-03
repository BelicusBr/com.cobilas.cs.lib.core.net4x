using System;
using System.Collections;
using System.Collections.Generic;

namespace Cobilas.Collections.Generic {
    public class LongList<T> : ILongList<T>, IReadOnlyLongList<T>, ILongList {
        private T[] longArray;
        
        public long Count => throw new NotImplementedException();
        public long Capacity { get; set; }
        public bool IsReadOnly => throw new NotImplementedException();
        bool ILongList.IsFixedSize => throw new NotImplementedException();
        bool ILongCollection.IsSynchronized => throw new NotImplementedException();
        object ILongCollection.SyncRoot => throw new NotImplementedException();

        public T this[long index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        object ILongList.this[long index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public LongList() {}
        public LongList(long capacity) {}
        public LongList(IEnumerable<T> collection) {}

        public ReadOnlyLongCollection<T> AsReadOnly() {
            return null;
        }

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

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