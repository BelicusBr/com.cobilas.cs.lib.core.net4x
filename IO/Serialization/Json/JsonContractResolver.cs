using System;
using Newtonsoft.Json;
using System.Reflection;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;

namespace Cobilas.IO.Serialization.Json;
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
public class JsonContractResolver : DefaultContractResolver {
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    
    /// <summary>
    /// Creates properties for the given <see cref="JsonContract"/>.
    /// </summary>
    /// <param name="type">The type to create properties for.</param>
    /// <param name="memberSerialization">The member serialization mode for the type.</param>
    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization) {
        List<JsonProperty> props = new();
        if (type.GetCustomAttribute<SerializableAttribute>() is null)
            return props;

        List<FieldInfo> fields = new();
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
