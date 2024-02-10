using System.Collections.Generic;

namespace Cobilas.Collections.Generic {
    /// <summary>Represents a long, read-only, strongly typed collection of elements.</summary>
    /// <typeparam name="T">The type of the elements.
    /// <para>
    /// This type parameter is covariant. That is, you can use either the type you specified or any type that 
    /// is more derived. For more information about covariance and contravariance, see Covariance and Contravariance 
    /// in Generics. 
    /// </para>
    /// </typeparam>
    public interface IReadOnlyLongCollection<out T> : IEnumerable<T>
    {
        /// <summary>Gets the number of elements in the collection.</summary>
        long Count { get; }
    }
}