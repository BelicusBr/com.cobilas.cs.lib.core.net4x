#if !NET8_0_OR_GREATER
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Cobilas.IO.Serialization.Binary; 
/// <summary>Base sketch object.</summary>
[Serializable]
public abstract class ScratchObject {
    /// <summary>Draft name.</summary>
    public abstract string Name { get; }

    /// <summary>Downloads draft object to a file.</summary>
    /// <param name="scratch">Scratch object.</param>
    /// <param name="filePath">Path of the file where the object will be downloaded.</param>
    public static void UnloadScratchObject(ScratchObject scratch, string filePath) {
        BinaryFormatter formatter = new();
        using FileStream stream = File.Create(filePath);
        formatter.Serialize(stream, scratch);
    }

    /// <summary>Downloads draft object to a file.</summary>
    /// <param name="scratch">Scratch object.</param>
    /// <param name="folderPath">Path of the directory where the file will be created.</param>
    /// <param name="name">File name.</param>
    /// <param name="extension">File extension.</param>
    public static void UnloadScratchObject(ScratchObject scratch, string folderPath, string name, string extension = "sobj")
        => UnloadScratchObject(scratch, Path.ChangeExtension(Path.Combine(folderPath, name), extension));

    /// <summary>Downloads draft object to a file.</summary>
    /// <param name="scratch">Scratch object.</param>
    /// <param name="folderPath">Path of the directory where the file will be created.</param>
    /// <param name="extension">File extension.</param>
    public static void UnloadScratchObject(ScratchObject scratch, string folderPath, string extension = "sobj")
        => UnloadScratchObject(scratch, folderPath, scratch.Name, extension);

    /// <summary>Loads an object <see cref="ScratchObject"/> from a file.</summary>
    /// <param name="filePath">File path.</param>
    public static ScratchObject LoadScratchObject(string filePath) {
        BinaryFormatter formatter = new();
        using FileStream stream = File.OpenRead(filePath);
        return (ScratchObject)formatter.Deserialize(stream);
    }
}
#endif