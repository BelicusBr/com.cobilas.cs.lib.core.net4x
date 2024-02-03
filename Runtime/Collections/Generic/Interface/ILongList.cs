namespace Cobilas.Collections.Generic {
    public interface ILongList<T> : ILongCollection<T> {
        T this[long index] { get; set; }

        long IndexOf(T item);
        void Insert(long index, T item);
        void RemoveAt(long index);   
    }
}