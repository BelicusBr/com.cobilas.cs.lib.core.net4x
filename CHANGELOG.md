# [15/01/2024]#1.2.0
- ## Changed
Now the package supports `netstandard2.0`.
- ## Removed
```c#
//Enum_CB_Extension
public static bool CompareFlag(this Enum, Enum);
public static bool CompareFlag(this Enum, Enum[]);
public static bool ContainsFlag(this Enum, Enum);
public static bool ContainsFlag(this Enum, Enum[]);
```
```c#
//HashAlgorithm_CB_Extension
public static bool CompareComprestFileHash(this HashAlgorithm, string, string);
public static bool CompareComprestFileHash(this HashAlgorithm, FileStream, FileStream);
public static bool CompareFileHash(this HashAlgorithm, string, string);
public static bool CompareFileHash(this HashAlgorithm, FileStream, FileStream);
public static byte[] ComputeHash(this HashAlgorithm, string);
public static string ComputeHashToString(this HashAlgorithm, FileStream);
public static string ComputeHashToString(this HashAlgorithm, string);
public static string ComputeHashToString(this HashAlgorithm, byte[]);
public static string ComprestComputehashFileNameToString(this HashAlgorithm, string);
public static string ComputehashFileNameToString(this HashAlgorithm, string);
public static byte[] ComputehashFileName(this HashAlgorithm, string);
public static string ComprestComputehashToString(this HashAlgorithm, string);
public static string ComprestComputehashToString(this HashAlgorithm, FileStream);
public static string ComprestComputehashToString(this HashAlgorithm, byte[]);
public static string ComputehashDirectoryToString(this HashAlgorithm, string);
public static string ComprestComputehashDirectoryToString(this HashAlgorithm, string);
public static string ComputehashDirectoryNameToString(this HashAlgorithm, string);
public static string ComprestComputehashDirectoryNameToString(this HashAlgorithm, string);
public static byte[] ComputehashDirectoryName(this HashAlgorithm, string);
public static byte[] ComputehashDirectory(this HashAlgorithm, string);
```
- ## Add
The `HashString` structure has been added.
```c#
//Enum_CB_Extension
public static bool HasFlag(this Enum, params Enum[])
```
```c#
//HashAlgorithm_CB_Extension
public static byte[] ComputeHash(this HashAlgorithm, string);
public static string ComputeHashToString(this HashAlgorithm, string);
public static string ComputeHashToString(this HashAlgorithm, byte[]);
public static string ComputeHashToString(this HashAlgorithm, byte[], int, int);
public static string ComputeHashToString(this HashAlgorithm, Stream);
```
# [31/12/2023]#1.1.0
- ## Changed
The package now has the `net472` and `netstandard2.1` target frameworks.