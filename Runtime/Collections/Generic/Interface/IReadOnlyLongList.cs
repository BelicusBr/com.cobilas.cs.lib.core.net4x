namespace Cobilas.Collections.Generic {
    public interface IReadOnlyLongList<out T> : IReadOnlyLongCollection<T>
    {
        T this[long index] { get; }
    }
}