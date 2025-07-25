# (2.3.0} (17/07/2025)
## Changed
The methods `Insert`, `Add`, `AddNon_Existing`, `Remove`, `ClearArray`, `LongClearArray`, `ClearArraySafe`, `LongClearArraySafe`, `FindAll` have parameters or returns with possibly null reference flag.

# [2.2.0] (25/02/2025)
## Added
The `Contains(this string, params string[])` and `ToGuid(this string)` extension methods have been added for the `string` class.

# [2.1.0] (18/11/2024)
## Added
### New Deserialization Methods
- Added `Json.Deserialize(string, Type?, JsonSerializerSettings?)` and `Json.Deserialize(string, Type?)` to provide more deserialization options regarding return type.
- **Note**: This also fixes the return type deficiency in non-generic deserialization methods.
## Changed
### Json Class Documentation
- Changes to the `Json` class documentation to describe in more detail what each method does.

# [2.0.1] (16/11/2024)
## Changed
### Discontinued support
The npm version of the package is no longer supported and is also no longer supported as a Unity3D package.
### New Target Frameworks
The NuGet version of the package now supports `.NET 6.0`, `.NET 8.0`, and `.NET Standard 2.1`.
### Nullable Types
The package has been updated to more robustly handle (nullable types).

## Fixed
### Object_CB_Extension.CompareTypeAndSubType Method
- **Problem**: When the IncludeInterface parameter was <kbd>true</kbd> and the method compared an interface and an object that inherited that interface, the result was always <kbd>false</kbd>.
- **Cause**: The issue was due to incorrect usage of the `Type.IsAssignableFrom(Type)` method, which was called from the object type instead of the interface type.
- **Fix**: Now `Type.IsAssignableFrom(Type)` method is called correctly from interface type.

## Deprecated
### ScratchObject class
The abstract class `ScratchObject` is marked as deprecated starting in `.NET 5` and will throw an error starting in `.NET 8`. \
For more information, visit https://aka.ms/binaryformatter. \
**Alternative**: Use the Json class as an alternative to `ScratchObject`.


# [1.6.1] (06/07/2024)
## Fixed
`IndexOutOfRangeException` issue in `Type_CB_Extension.GetAttribute<T>(this Type, bool)` method.

### Details
The `IndexOutOfRangeException` error consisted of the fact that the method did not correctly check the internal variable which is an array, when this array was empty it caused the `IndexOutOfRangeException`. \
The method that checks whether the first index of the array was null did not take into account whether the array was null and whether it was empty.
# [1.6.0] (09/05/2024)
## Add
New methods like `ArrayManipulation.ForSector<T>(Array, in Action<T, long>, in long)`, `ArrayManipulation.ForSector<T>(Array, in Action<T, long>)`, `ArrayManipulation.ForSector <T>(T[], in Action<T, long>, in long)`, `ArrayManipulation.ForSector<T>(T[], in Action<T, long>)`, `ArrayManipulation.ForSector<T> (IList, in Action<T, int>, in int)`, `ArrayManipulation.ForSector<T>(IList, in Action<T, int>)`, `ArrayManipulation.ForSector(IList, in Action<object, int> , in int)`, `ArrayManipulation.ForSector(IList, in Action<object, int>)`, `ArrayManipulation.ForSector<T>(IList<T>, in Action<T, int>, in int)` and ` ArrayManipulation.ForSector<T>(IList<T>, in Action<T, int>)` have been added.

# [1.5.0] (09/05/2024)
## Add
The methods `ArrayManipulation.ForSector(Array, in Action<object, long>, in long)` and `ArrayManipulation.ForSector(Array, in Action<object, long>)` have been added.

# [1.4.2] (14/02/2024)
## Fixed
Problem checking the `index` parameter in the `ArrayManipulation.FindIndex<T>(T[], long, long, Predicate<T>)` and `ArrayManipulation.FindLastIndex<T>(T[], long, long, Predicate methods <T>)`.
### Details
The problem was checking the `index` parameter, which was done to check whether it was greater than or equal to zero. \
If the array had size zero, the check returned a false positive, generating an `ArgumentOutOfRangeException`. \
The fix involved checking if the array is zero size before checking the `index` parameter.
### Affected versions
This problem was present since version `1.4.0`.
## Changed
Improvements to the `ArrayManipulation.SeparateList<T>(T[], long, out T[], out T[])` method.
### Details
The `ArrayManipulation.SeparateList<T>(T[], long, out T[], out T[])` method has received the following improvements:
- Checks to raise exceptions like `ArgumentNullException`, `RankException` and `ArgumentOutOfRangeException` if they occur.
- Returns an empty list in the `out` parameters if the input list is zero.

# [04/02/2024] #1.4.1
- ## Fixed
- - The metadata files required to work in Unity3D have been added to the package.
# [04/02/2024] #1.4.0-rc3
## ArrayManipulation
- ### Changed
- - The functions `Remove<T>`, `Insert<T>`, `SeparateList<T>` and `TakeStretch<T>` had their parameters
of type `int` changed to type `long`.
- ### Add
- - Added `long` type versions to methods that were `int` type versions.
- ### Obsolete
- - The functions `T[] Insert<T>(IEnumerator<T>, long, T[])`, `T[] Add<T>(IEnumerator<T>, T[])` and `Add<T>( IEnumerator<T>, ref T[])` have been deprecated.
- ## Add
- - The generic collection classes `LongList<T>`, ReadOnlyLongCollection<T> and generic and non-generic interfaces such as `ILongList`, `ILongList<T>`, `ILongCollection`, `ILongCollection<T>`, `IReadOnlyLongCollection<T> ` and `IReadOnlyLongList<T>` have been added.
# [03/02/2024] #1.4.0-rc1
- ## Removed
The `IReadOnlyArray` and `IReadOnlyArray<T>` interfaces have been removed using the `IReadOnlyLongList` or `IReadOnlyList` interface.
# [01/02/2024] #1.3.1-rc2
- ## Add
The `XMLIRWDeclaration` class that represents an xml declaration has been added.<br>
The `XMLIRWText` class that represents xml text has been added.<br>
The `ITextValue` interface has been added.<br>
- ## Obsolete
The `XMLIRWValue` structure has been deprecated and replaced by the `XMLIRWText` class. In addition to the `XMLIRWValue` structure, functions, properties and constructors that use the `XMLIRWValue` structure have become obsolete.
# [01/02/2024] #1.3.1-rc1
- ## Fixed
The XML extension has problems reading the xml file.<br>
The constructor of the `ICollectionToIEnumerator` class had the `ArgumentException` problem due to the fact that the protected field `list` is not instantiated.
# [26/01/2024] #1.3.0
- ## Add
The `TypeUtilitarian` class received a new function called `Type TypeUtilitarian.GetType(string fullName)`.
# [25/01/2024]
## [nuget#1.2.0-rc6]
The NuGet package is using C# version 7.3
## [npm#1.2.2]
The NPM package is using version 7.3 of C# in addition to having fixed the problems inherent in the migration to Unity2019.

# [18/01/2024]#1.2.0-rc5
- ## Changed
Package has regressed to `netstandard2.0`.
- ## Removed
The `ReadOnlyIterrupter` class and `ALF` classes have been removed.

# [17/01/2024]#1.2.0-rc3
- ## Add
TaskPool class objects have been added:
```c#
public static int Count { get; }
public static int VacantTaskCount { get; }
public static int NonVacantTaskCount { get; }
public static void InitTask(Action action, out Task task);
public static void InitTask(Action action);
public static void InitTask<TRes>(Func<TRes> func, out Task<TRes> res);
public static void InitTask<TRes>(Func<TRes> func);
```
- ## Removed
Objects such as `TaskPoolItem`, `TaskPoolItem`, `TaskPoolItem`, `IAsyncTaskWait`, `IAsyncTask`, `AsyncTaskWait` and `AsyncTask` have been removed.<br>
TaskPool class objects have been removed:
```c#
public static int PoolCount { get; }
public static int CountTaskCompleted { get; }
public static void AddTask(Action<InternalWait> action);
public static void AddTask(AsyncTaskWait task);
public static void AddTask(AsyncTask task);
public static void AddTask(Action action);
public static void AddTask(Action<InternalWait> action, CancellationToken token);
```

# [16/01/2024]#12.0-rc2
- ## Obsolete
The method `ArrayManipulation.Exists(object?, Array)` has become obsolete the alternative method is `ArrayManipulation.Exists<T>(T item, T[] array)`.
- ## Remove
The static class `PrintOut` has been removed.
- ## Add
```c#
//String_CB_Extension extension methods have been added.
public static sbyte ToSByte(this string S, NumberStyles style, IFormatProvider formatProvider);
public static sbyte ToSByte(this string S, IFormatProvider formatProvider);
public static short ToShort(this string S, NumberStyles style, IFormatProvider formatProvider);
public static short ToShort(this string S, IFormatProvider formatProvider);
public static int ToInt(this string S, NumberStyles style, IFormatProvider formatProvider);
public static int ToInt(this string S, IFormatProvider formatProvider);
public static long ToLong(this string S, NumberStyles style, IFormatProvider formatProvider);
public static long ToLong(this string S, IFormatProvider formatProvider);
public static byte ToByte(this string S, NumberStyles style, IFormatProvider formatProvider);
public static byte ToByte(this string S, IFormatProvider formatProvider);
public static ushort ToUShort(this string S, NumberStyles style, IFormatProvider formatProvider);
public static ushort ToUShort(this string S, IFormatProvider formatProvider);
public static uint ToUInt(this string S, NumberStyles style, IFormatProvider formatProvider);
public static uint ToUInt(this string S, IFormatProvider formatProvider);
public static ulong ToULong(this string S, NumberStyles style, IFormatProvider formatProvider);
public static ulong ToULong(this string S, IFormatProvider formatProvider);
public static float ToFloat(this string S, NumberStyles style, IFormatProvider formatProvider);
public static float ToFloat(this string S, IFormatProvider formatProvider);
public static double ToDouble(this string S, NumberStyles style, IFormatProvider formatProvider);
public static double ToDouble(this string S, IFormatProvider formatProvider);
public static decimal ToDecimal(this string S, NumberStyles style, IFormatProvider formatProvider);
public static decimal ToDecimal(this string S, IFormatProvider formatProvider);
```
```c#
//ArrayManipulation class methods have been added.
public static void ClearArray(Array array, int index, int length);
public static void ClearArray<T>(int index, int length, ref T[] array);
public static void ClearArray<T>(ref T[] array);
public static void ClearArraySafe(Array array, int index, int length);
public static void ClearArraySafe<T>(int index, int length, ref T[] array);
public static int LastIndexOf(object? item, Array array, int index, int length);
public static int LastIndexOf(object? item, Array array, int index);
public static int LastIndexOf(object? item, Array array);
public static int FindIndex<T>(T[] array, int index, int length, Predicate<T> match);
public static int FindIndex<T>(T[] array, int index, Predicate<T> match);
public static int FindIndex<T>(T[] array, Predicate<T> match);
public static int FindLastIndex<T>(T[] array, int index, int length, Predicate<T> match);
public static int FindLastIndex<T>(T[] array, int index, Predicate<T> match);
public static int FindLastIndex<T>(T[] array, Predicate<T> match);
public static T FindLast<T>(T[] array, Predicate<T> match);
public static T[] FindAll<T>(T[] array, Predicate<T> match);
public static T Find<T>(T[] array, Predicate<T> match);
public static bool Exists<T>(T[] array, Predicate<T> match);
public static bool Exists<T>(T item, T[] array);
public static void Reverse(Array array, int index, int length);
```

# [16/01/2024]#1.2.0-rc1
- ## Removed
```c#
//The Enum_CB_Extension extension methods have been removed.
public static bool CompareFlag(this Enum E, Enum enum);
public static bool CompareFlag(this Enum E, Enum[] enum);
public static bool ContainsFlag(this Enum E, Enum enum);
public static bool ContainsFlag(this Enum E, Enum[] enum);
```
- ## Add
The `HashString` structure has been added.
```c#
//New extension methods for `Stream` have been added.
public static void Write(this Stream F, string text);
public static void Write(this Stream F, char[] chars);
public static char[] GetChars(this Stream F);
public static string GetString(this Stream F);
```
```c#
//Enum_CB_Extension extension methods have been added.
public static bool HasFlag(this Enum E, params Enum[] enum);
public static string Format(this Enum E, object value, string format);
```

# [15/01/2024]#1.2.0
- ## Removed
The `T[] ArrayManipulation.Empty<T>();` method has been removed.
```c#
//The `HashAlgorithm_CB_Extension` extension methods have been removed.
public static bool CompareComprestFileHash(this HashAlgorithm H, string FileName1, string FileName2);
public static bool CompareComprestFileHash(this HashAlgorithm H, FileStream file1, FileStream file2);
public static bool CompareFileHash(this HashAlgorithm H, string FileName1, string FileName2);
public static bool CompareFileHash(this HashAlgorithm H, FileStream file1, FileStream file2);
public static byte[] ComputeHash(this HashAlgorithm H, string FileName);
public static string ComputeHashToString(this HashAlgorithm H, FileStream File);
public static string ComputeHashToString(this HashAlgorithm H, string FileName);
public static string ComputeHashToString(this HashAlgorithm H, byte[] bytes);
public static string ComprestComputehashFileNameToString(this HashAlgorithm H, string FileName);
public static string ComputehashFileNameToString(this HashAlgorithm H, string FileName);
public static byte[] ComputehashFileName(this HashAlgorithm H, string FileName);
public static string ComprestComputehashToString(this HashAlgorithm H, string FileName);
public static string ComprestComputehashToString(this HashAlgorithm H, FileStream File);
public static string ComprestComputehashToString(this HashAlgorithm H, byte[] bytes);
public static string ComputehashDirectoryToString(this HashAlgorithm H, string DirectoryPath);
public static string ComprestComputehashDirectoryToString(this HashAlgorithm H, string DirectoryPath);
public static string ComputehashDirectoryNameToString(this HashAlgorithm H, string DirectoryPath);
public static string ComprestComputehashDirectoryNameToString(this HashAlgorithm H, string DirectoryPath);
public static byte[] ComputehashDirectoryName(this HashAlgorithm H, string DirectoryPath);
public static byte[] ComputehashDirectory(this HashAlgorithm H, string DirectoryPath);
```
- ## Add
```c#
//`HashAlgorithm_CB_Extension` extension methods have been added.
public static byte[] ComputeHash(this HashAlgorithm H, string FilePath);
public static string ComputeHashToString(this HashAlgorithm H, string FilePath);
public static string ComputeHashToString(this HashAlgorithm H, byte[] buffer);        
public static string ComputeHashToString(this HashAlgorithm H, byte[] buffer, int offset, int count);        
public static string ComputeHashToString(this HashAlgorithm H, Stream inputStream);
```
# [31/12/2023]#1.1.0
- ## Changed
The package now has the `net472` and `netstandard2.1` target frameworks.