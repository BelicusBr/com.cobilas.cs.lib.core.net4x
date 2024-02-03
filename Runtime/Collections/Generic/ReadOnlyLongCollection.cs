using System;
using System.Collections;
using System.Collections.Generic;

namespace Cobilas.Collections.Generic {
    public class ReadOnlyLongCollection<T> : IReadOnlyLongList<T>
    {
        public T this[long index] => throw new NotImplementedException();

        public long Count => throw new NotImplementedException();

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}