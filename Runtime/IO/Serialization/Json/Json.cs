#if !UNITY_5_3_OR_NEWER
using Newtonsoft.Json;

namespace Cobilas.IO.Serialization.Json {
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    public static class Json {
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente

        /// <summary>
        /// Serializes the specified object to a JSON string using <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <param name="settings">The Newtonsoft.Json.JsonSerializerSettings used to serialize the object. If this
        /// is null, default serialization settings will be used.</param>
        public static string Serialize(object value, JsonSerializerSettings settings)
            => JsonConvert.SerializeObject(value, settings);

        /// <summary>
        /// Serializes the specified object to a JSON string using <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <param name="Indented">This parameter allows you to format the json file.</param>
        public static string Serialize(object value, bool Indented) {
            JsonSerializerSettings resolver = new JsonSerializerSettings() {
                ContractResolver = new JsonContractResolver(),
                Formatting = Indented ? Formatting.Indented : Formatting.None
            };
            return Serialize(value, resolver);
        }

        /// <summary>
        /// Serializes the specified object to a JSON string using <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        public static string Serialize(object value)
            => Serialize(value, true);

        /// <summary>
        /// Deserializes the JSON to a .NET object using <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <param name="value">The JSON to deserialize.</param>
        /// <param name="settings">The Newtonsoft.Json.JsonSerializerSettings used to deserialize the object. If
        /// this is null, default serialization settings will be used.</param>
        public static object Deserialize(string value, JsonSerializerSettings settings)
            => JsonConvert.DeserializeObject(value, settings);

        /// <summary>
        /// Deserializes the JSON to a .NET object using <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <param name="value">The JSON to deserialize.</param>
        public static object Deserialize(string value) {
            JsonSerializerSettings resolver = new JsonSerializerSettings() {
                ContractResolver = new JsonContractResolver()
            };
            return Deserialize(value, resolver);
        }

        /// <summary>
        /// Deserializes the JSON to a .NET object using <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <param name="value">The JSON to deserialize.</param>
        /// <param name="settings">The Newtonsoft.Json.JsonSerializerSettings used to deserialize the object. If
        /// this is null, default serialization settings will be used.</param>
        public static T Deserialize<T>(string value, JsonSerializerSettings settings)
            => JsonConvert.DeserializeObject<T>(value, settings);

        /// <summary>
        /// Deserializes the JSON to a .NET object using <see cref="JsonSerializerSettings"/>.
        /// </summary>
        /// <param name="value">The JSON to deserialize.</param>
        public static T Deserialize<T>(string value) {
            JsonSerializerSettings resolver = new JsonSerializerSettings() {
                ContractResolver = new JsonContractResolver()
            };
            return Deserialize<T>(value, resolver);
        }
    }
}
#endif