using System;
using System.Reflection;
using Cobilas.Collections;

namespace Cobilas {
    /// <summary>Utility static class to obtain type or assembly.</summary>
    public static class TypeUtilitarian {

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

        /// <summary>Get a specific type.</summary>
        /// <param name="fullName">The full name of the type. (example:<c>System.String</c>)</param>
        public static Type GetType(string fullName) {
            foreach (Type item in GetTypes())
                if (item.FullName == fullName)
                    return item;
            return null;
        }

        /// <summary>Get all assemblies for the current domain.</summary>
        public static Assembly[] GetAssemblies()
            => AppDomain.CurrentDomain.GetAssemblies();
    }
}
