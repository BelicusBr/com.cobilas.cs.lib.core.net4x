using System;
using System.Collections;
using System.Collections.Generic;

namespace Cobilas.Collections {
    /// <summary>Transforms an array into an enumerator.</summary>
    public class ArrayToIEnumerator<T> : IEnumerator<T> {
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        protected T[]? list;
        protected long index;
        protected T current = default!;
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
        private bool disposedValue;
        /// <summary>Gets the element in the collection at the current position of the enumerator.</summary>
        public virtual T Current => current;
        object? IEnumerator.Current => current;
        /// <summary>Internal constructor.</summary>
        protected ArrayToIEnumerator() {
            list = [];
            index = -1;
        }
        /// <summary>Creates a new instance of this object.</summary>
        public ArrayToIEnumerator(T[]? list) : this()
            => this.list = list;

        /// <summary>Advances the enumerator to the next element of the collection.</summary>
        public virtual bool MoveNext() {
            if (++index >= ArrayManipulation.ArrayLongLength(list)) return false;
            else if (ArrayManipulation.EmpytArray(list)) return false;
            else current = list[index];
            return true;
        }

        /// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
        public virtual void Reset()
            => index = -1;

        /// <summary>Internal disposal of the object.</summary>
        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    list = [];
                    current = default!;
                    index = default;
                }
                disposedValue = true;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
