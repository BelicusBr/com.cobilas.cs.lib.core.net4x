using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cobilas.IO.Atlf {
    public abstract class ATLFReader : ATLFBase {
        public abstract ATLFNode[] GetHeader();
        public abstract string GetTag(string name);
        public abstract ATLFNode[] GetTagGroup(string path);

        public static T Create<T>(Stream stream) where T : ATLFReader => Create<T>((MarshalByRefObject)stream);
        public static T Create<T>(TextWriter text) where T : ATLFReader => Create<T>((MarshalByRefObject)text);

        private static T Create<T>(MarshalByRefObject refObject) where T : ATLFReader{
            T writer = Activator.CreateInstance<T>();
            writer.RefObject = refObject;
            return writer;
        }
    }
}