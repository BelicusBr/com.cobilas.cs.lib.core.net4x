using System;
using System.Reflection;
using Cobilas.Collections;

namespace Cobilas {
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    public static class TypeUtilitarian {
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente

        /// <summary>Get all types of assembly.</summary>
        public static Type[] GetTypes() {
            Type[] types = Array.Empty<Type>();
            Assembly[] assemblies = GetAssemblies();
            foreach (var item in assemblies)
                ArrayManipulation.Add(item.GetTypes(), ref types);
            return types;
        }

        /// <summary>Checks if the type exists.</summary>
        /// <param name="fullName">The full name of the type. (example:<c>System.String</c>)</param>
        public static bool TypeExist(string fullName) {
            foreach (var item in GetTypes())
                if (item.Name == fullName)
                    return true;
            return false;
        }

        /// <summary>Get all assemblies for the current domain.</summary>
        public static Assembly[] GetAssemblies()
            => AppDomain.CurrentDomain.GetAssemblies();
    }
}
