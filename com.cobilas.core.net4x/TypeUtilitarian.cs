using System;
using System.Reflection;
using Cobilas.Collections;

namespace Cobilas {
    public static class TypeUtilitarian {
        public static Type[] GetTypes() {
            Type[] types = null;
            Assembly[] assemblies = GetAssemblies();
            foreach (var item in assemblies)
                ArrayManipulation.Add(item.GetTypes(), ref types);
            return types;
        }

        public static bool TypeExist(string fullName) {
            foreach (var item in GetTypes())
                if (item.Name == fullName)
                    return true;
            return false;
        }

        public static Assembly[] GetAssemblies()
            => AppDomain.CurrentDomain.GetAssemblies();
    }
}
