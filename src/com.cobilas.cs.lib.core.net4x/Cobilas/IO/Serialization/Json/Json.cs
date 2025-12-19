using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Cobilas.IO.Serialization.Json;

/// <summary>This class of serialization and deserving C# for json support.</summary>
public static class Json {
    /// <summary>Serializes the specified object to a JSON string using <see cref="JsonSerializerSettings"/>.</summary>
    /// <param name="value">The object to serialize.</param>
    /// <param name="settings">The Newtonsoft.Json.JsonSerializerSettings used to serialize the object. 
    /// If this is null, default serialization settings will be used.</param>
    /// <returns>Returns the string conversion of the object that was passed.</returns>
    public static string Serialize(object? value, JsonSerializerSettings? settings)
        => JsonConvert.SerializeObject(value, settings);
    /// <inheritdoc cref="Serialize(object, JsonSerializerSettings)"/>
    /// <param name="Indented">This parameter allows you to format the json file.</param>
    /// <param name="value">The object to serialize.</param>
    public static string Serialize(object? value, bool Indented) {
        JsonSerializerSettings resolver = new() {
            ContractResolver = new JsonContractResolver(),
            Formatting = Indented ? Formatting.Indented : Formatting.None
        };
        return Serialize(value, resolver);
    }
    /// <inheritdoc cref="Serialize(object, JsonSerializerSettings)"/>
    public static string Serialize(object? value)
        => Serialize(value, true);
    /// <inheritdoc cref="Deserialize(string, Type?, JsonSerializerSettings?)"/>
    public static object Deserialize(string? value, JsonSerializerSettings? settings)
        => Deserialize(value, (Type?)null, settings);
    /// <inheritdoc cref="Deserialize(string, Type?, JsonSerializerSettings?)"/>
    public static object Deserialize(string? value)
        => Deserialize(value, (Type?)null);
    /// <summary>Deserializes the JSON to a .NET object using <see cref="JsonSerializerSettings"/>.</summary>
    /// <param name="value">The JSON to deserialize.</param>
    /// <param name="typeObject">The type of json value that will be deserialized.</param>
    /// <param name="settings">The <seealso cref="JsonSerializerSettings"/> used to deserialize the object. 
    /// If this is null, default serialization settings will be used.</param>
    /// <returns>By default, the return value is <seealso cref="NullObject"/> when the return value is null.
    /// When the <c>typeObject</c> parameter is null, the method returns <seealso cref="JObject"/>.</returns>
    public static object Deserialize(string? value, Type? typeObject, JsonSerializerSettings? settings)
        => JsonConvert.DeserializeObject(value ?? string.Empty, typeObject, settings) ?? NullObject.Null;
    /// <inheritdoc cref="Deserialize(string, Type?, JsonSerializerSettings?)"/>
    public static object Deserialize(string? value, Type? typeObject)
        => Deserialize(value, typeObject, new JsonSerializerSettings() { ContractResolver = new JsonContractResolver() });
    /// <inheritdoc cref="Deserialize(string, Type?, JsonSerializerSettings?)"/>
    /// <typeparam name="T">The type of json value that will be deserialized.</typeparam>
    public static T? Deserialize<T>(string? value, JsonSerializerSettings settings)
        => JsonConvert.DeserializeObject<T>(value ?? string.Empty, settings);
    /// <inheritdoc cref="Deserialize{T}(string, JsonSerializerSettings)"/>
    public static T? Deserialize<T>(string? value) {
        JsonSerializerSettings resolver = new() {
            ContractResolver = new JsonContractResolver()
        };
        return Deserialize<T>(value, resolver);
    }
}