# [Cobilas Core](https://belicusbr.github.io/com.cobilas.docs/mds/core-net4x-getting-started.html)
### Descripition
Cobilas Core Net4x is a utility library for CSharp.

## Json
(namespace:Cobilas.IO.Serialization.Json)<br>
Only present in the NuGet version.<br>
The static class `Json` grants static read and write functions.

### JsonContractResolver
Used by `JsonSerializer` to resolve a `JsonContract` for a given `Type`. 
Furthermore, `JsonContractResolver` determines how the fields of an `Object` will be serialized.

## ATLF(Arquivo de tradução de leitura facil)
ATLF (Easy to Read Translation File) can be used to create and load translations for apps.
```
#>Header
The use of the header is not mandatory.<#
#! version:/*std:1.0*/
#! encoding:/*utf-8*/

#> Comment <#
#> ATLF format(1.0) <#

#> Uni-line marking <#
#! Tag1:/*value1*/

#> Multi-line marking <#
#! Tag2:/*
value1
value2
value3
value4
*/
```
### How to read ATLF
```csharp
static void Main(string[] args) {
    using ATLFReader reader = ATLFReader.Create(@"C:\folder1\file.txt");
    reader.Reader();
    Console.WriteLine($"tag.value.1:{reader.GetTag("tag.value.1")}");
    Console.WriteLine($"tag.value.2:{reader.GetTag("tag.value.2")}");
    Console.WriteLine($"tag.value.3:{reader.GetTag("tag.value.3")}");
}
```
The other reading functions.
- The `ATLFNode[]:ATLFReader.GetHeader()` function allows you to get the header tags.
- The `ATLFNode[]:ATLFReader.GetAllComments()` function allows you to get all comments.
The `ATLFNode[]:ATLFReader.GetTagGroup(string path)` function allows you to obtain tags that belong to the same path.
```csharp
/*C:\folder1\file.txt
* #! version:/*std:1.0* /
* #! encoding:/*utf-8* /
* 
* #! tag.value.cop1:/*value1* /
* #! tag.value.map.cop1:/*value1* /
* #! tag.value.map.cop2:/*value1* /
* #! tag.value.cop2:/*value1* /
* #! tag.value.cop3:/*value1* /
*/
static void Main(string[] args) {
    using ATLFReader reader = ATLFReader.Create(@"C:\folder1\file.txt");
    reader.Reader();
    foreach(var item in reader.GetTagGroup("tag.value.map"))
        Console.WriteLine(item);
}
```
### How to write ATLF
```csharp
static void Main(string[] args) {
    using ATLFWriter writer = ATLFWriter.Create(File.OpenWrite(@"C:\folder1\file.txt"));
    writer.WriteHeader();//The header is not mandatory but if you add a header, call this function first.
    writer.WriteComment("my tag1");
    writer.WriteNode("tag1", "value1");
    writer.WriteWhitespace("\r\n");//This function is called automatically when the `Indent` property is `true`. By default the `Indent` property is `true`.
    writer.WriteComment("my tag2");
    writer.WriteNode("tag2", "value2");
    writer.WriteWhitespace(2, "\r\n");//This function is called automatically when the `Indent` property is `true`. By default the `Indent` property is `true`.
    writer.WriteComment("my tag3");
    writer.WriteNode("tag3", "value3");
}
```
### Encoders and decoders
Regarding encoders and decoders, ATLF allows the creation of customized encoders and decoders.<br>
To use a custom encoder or decoder, assign a version to your custom encoder or decoder using the `Version` property and then assign the version of the custom encoder or decoder in the `TargetVersion` property of the `ATLFWriter` and `ATLFReader` classes.<br>
#### Creating a custom encoding class
To create a custom encoding class, the class must inherit the `ATLFEncoding` class.
#### Creating a custom decoding class
To create a custom decoding class, the class must inherit the `ATLFDecoding` class.

# [Cobilas.Core.Net4x](https://www.nuget.org/packages/Cobilas.Core.Net4x) is on nuget.org
To include the package, open the `.csproj` file and add it.
```xml
<ItemGroup>
  <PackageReference Include="Cobilas.Core.Net4x" Version="2.10.0" />
</ItemGroup>
```
Or use command line.
```ps1
dotnet add package Cobilas.Core.Net4x --version 2.10.0
```
