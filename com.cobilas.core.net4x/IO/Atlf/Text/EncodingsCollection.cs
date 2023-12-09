using System;
using Cobilas.Collections;

namespace Cobilas.IO.Atlf.Text {
    public static class EncodingsCollection {
        private static readonly ATLFEncoding[] encodings;
        private static readonly ATLFDecoding[] decodings;

        static EncodingsCollection() {
            foreach(Type item in TypeUtilitarian.GetTypes()) {
                if (item.IsSubclassOf(typeof(ATLFEncoding)) && item != typeof(ATLFEncoding))
                    ArrayManipulation.Add((ATLFEncoding)item.Activator(), ref encodings);
                else if (item.IsSubclassOf(typeof(ATLFDecoding)) && item != typeof(ATLFDecoding))
                    ArrayManipulation.Add((ATLFDecoding)item.Activator(), ref decodings);
            }
        }

        public static bool ContainsEncoding(string version) {
            foreach (var item in encodings)
                if (item.Version == version)
                    return true;
            return false;
        }
        
        public static bool ContainsDecoding(string version) {
            foreach (var item in decodings)
                if (item.Version == version)
                    return true;
            return false;
        }

        public static string[] GetEncodingVersionList() {
            string[] stg = new string[ArrayManipulation.ArrayLength(encodings)];
            for (int I = 0; I < stg.Length; I++)
                stg[I] = encodings[I].Version;
            return stg;
        }
        
        public static string[] GetDecodingVersionList() {
            string[] stg = new string[ArrayManipulation.ArrayLength(decodings)];
            for (int I = 0; I < stg.Length; I++)
                stg[I] = decodings[I].Version;
            return stg;
        }

        public static ATLFEncoding GetEncoding(string version) {
            foreach (var item in encodings)
                if (item.Version == version)
                    return item;
            return null;
        }
        
        public static ATLFDecoding GetDecoding(string version) {
            foreach (var item in decodings)
                if (item.Version == version)
                    return item;
            return null;
        }
    }
}