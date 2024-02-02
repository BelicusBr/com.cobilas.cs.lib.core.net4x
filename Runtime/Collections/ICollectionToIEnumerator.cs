using System.Collections.Generic;

namespace Cobilas.Collections { 
    /// <summary>Transforms a collection into an enumerator.</summary>
    public class ICollectionToIEnumerator<T> : ArrayToIEnumerator<T> {
        /// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
        public override T Current => base.Current;

#pragma warning disable CS1591
        public ICollectionToIEnumerator(ICollection<T> collection) : base() {
            if (collection is null || collection.Count == 0) return;
            collection.CopyTo(list = new T[collection.Count], 0);
        }
#pragma warning restore CS1591

        /// <summary>Advances the enumerator to the next element of the collection.</summary>
        public override bool MoveNext() => base.MoveNext();

        /// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
        public override void Reset() => base.Reset();

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        protected override void Dispose(bool disposing) => base.Dispose(disposing);
    }
}
