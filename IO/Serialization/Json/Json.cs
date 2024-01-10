using Newtonsoft.Json;

namespace Cobilas.IO.Serialization.Json {
    public static class Json {

        public static string Serialize(object value, JsonSerializerSettings settings)
            => JsonConvert.SerializeObject(value, settings);

        public static string Serialize(object value, bool Indented) {
            JsonSerializerSettings resolver = new() {
                ContractResolver = new JsonContractResolver(),
                Formatting = Indented ? Formatting.Indented : Formatting.None
            };
            return Serialize(value, resolver);
        }

        public static string Serialize(object value)
            => Serialize(value, true);

        public static object Deserialize(string value, JsonSerializerSettings settings)
            => JsonConvert.DeserializeObject(value, settings)!;

        public static object Deserialize(string value) {
            JsonSerializerSettings resolver = new() {
                ContractResolver = new JsonContractResolver()
            };
            return Deserialize(value, resolver);
        }

        public static T Deserialize<T>(string value, JsonSerializerSettings settings)
            => JsonConvert.DeserializeObject<T>(value, settings)!;

        public static T Deserialize<T>(string value) {
            JsonSerializerSettings resolver = new() {
                ContractResolver = new JsonContractResolver()
            };
            return Deserialize<T>(value, resolver);
        }
    }
}
