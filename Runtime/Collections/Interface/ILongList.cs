namespace Cobilas.Collections {
    public interface ILongList : ILongCollection {
        object this[long index] { get; set; }

        bool IsFixedSize { get; }
        bool IsReadOnly { get; }

        long Add(object value);
        void Clear();
        bool Contains(object value);
        long IndexOf(object value);
        void Insert(long index, object value);
        void Remove(object value);
        void RemoveAt(long index);
    }
}