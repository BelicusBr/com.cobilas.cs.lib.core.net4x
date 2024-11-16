using System;
using Cobilas.Collections;

namespace Cobilas.IO.Atlf.Text;  
/// <summary>Represents a collection of ATLF encoding and decoding.</summary>
public static class EncodingsCollection {
    private static readonly ATLFEncoding[] encodings = [];
    private static readonly ATLFDecoding[] decodings = [];

    static EncodingsCollection() {
        foreach(Type item in TypeUtilitarian.GetTypes()) {
            if (item == typeof(NullATLFDecoding) || item == typeof(NullATLFEncoding)) continue;
            if (item.IsSubclassOf(typeof(ATLFEncoding)) && item != typeof(ATLFEncoding))
                ArrayManipulation.Add((ATLFEncoding)item.Activator(), ref encodings);
            else if (item.IsSubclassOf(typeof(ATLFDecoding)) && item != typeof(ATLFDecoding))
                ArrayManipulation.Add((ATLFDecoding)item.Activator(), ref decodings);
        }
    }

    /// <summary>Checks whether a given version of the ATLF encoder exists.</summary>
    public static bool ContainsEncoding(string version) {
        foreach (var item in encodings)
            if (item.Version == version)
                return true;
        return false;
    }

    /// <summary>Checks whether a certain version of the ATLF decoder exists.</summary>
    public static bool ContainsDecoding(string version) {
        foreach (var item in decodings)
            if (item.Version == version)
                return true;
        return false;
    }

    /// <summary>Gets a list of ATLF encoder versions.</summary>
    public static string[] GetEncodingVersionList() {
        string[] stg = new string[ArrayManipulation.ArrayLength(encodings)];
        for (int I = 0; I < stg.Length; I++)
            stg[I] = encodings[I].Version;
        return stg;
    }

    /// <summary>Gets a list of ATLF decoder versions.</summary>
    public static string[] GetDecodingVersionList() {
        string[] stg = new string[ArrayManipulation.ArrayLength(decodings)];
        for (int I = 0; I < stg.Length; I++)
            stg[I] = decodings[I].Version;
        return stg;
    }

    /// <summary>Gets a specific version of the ATLF encoder.</summary>
    public static ATLFEncoding GetEncoding(string version) {
        foreach (var item in encodings)
            if (item.Version == version)
                return item;
        return ATLFEncoding.Null;
    }

    /// <summary>Gets a specific version of the ATLF decoder.</summary>
    public static ATLFDecoding GetDecoding(string version) {
        foreach (var item in decodings)
            if (item.Version == version)
                return item;
        return ATLFDecoding.Null;
    }
}