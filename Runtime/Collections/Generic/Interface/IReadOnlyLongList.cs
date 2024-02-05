namespace Cobilas.Collections.Generic {
    /// <summary>Represents a long read-only collection of elements that can be accessed by index.</summary>
    /// <typeparam name="T">The type of elements in the read-only list.
    /// <para>This type parameter is covariant. That is, you can use either the type you specified or any type that 
    /// is more derived. For more information about covariance and contravariance, see Covariance and Contravariance 
    /// in Generics.</para>
    /// </typeparam>
    public interface IReadOnlyLongList<out T> : IReadOnlyLongCollection<T>
    {
        /// <summary>Gets the element at the specified index in the read-only list.</summary>
        T this[long index] { get; }
    }
}