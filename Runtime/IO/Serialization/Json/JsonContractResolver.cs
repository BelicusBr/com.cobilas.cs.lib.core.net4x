#if !UNITY_5_3_OR_NEWER
using System;
using Newtonsoft.Json;
using System.Reflection;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;

namespace Cobilas.IO.Serialization.Json {
    /// <inheritdoc/>
    public class JsonContractResolver : DefaultContractResolver {
    
        /// <inheritdoc/>
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization) {
            List<JsonProperty> props = new List<JsonProperty>();
            if (type.GetCustomAttribute<SerializableAttribute>() is null)
                return props;

            List<FieldInfo> fields = new List<FieldInfo>();
            Type temp = type;
            while (temp != null) {

                FieldInfo[] infos = temp.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (infos != null)
                    foreach (FieldInfo item in infos)
                        if (item.DeclaringType == temp && !item.IsBackingField())
                            fields.Add(item);

                temp = temp.BaseType;
            }

            foreach (FieldInfo item in fields) {
                if (item.GetCustomAttribute<NonSerializedAttribute>() == null)
                    props.Add(base.CreateProperty(item, memberSerialization));
            }

            props.ForEach(p => { p.Writable = true; p.Readable = true; });
            return props;
        }
    }
}
#endif