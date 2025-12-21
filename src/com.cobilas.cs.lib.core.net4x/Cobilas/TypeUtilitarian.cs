using System;
using System.Reflection;
using Cobilas.Exceptions;
using Cobilas.Collections;

namespace Cobilas;
/// <summary>Utility static class to obtain type or assembly.</summary>
public static class TypeUtilitarian {
	/// <summary>Get all types of assembly.</summary>
	/// <param name="assemblyName">The full name of the assembly to get types from. If <see langword="null"/>, types from all assemblies are returned.</param>
	/// <returns>Returns all types of objects from the specified assembly or from all assemblies.</returns>
	/// <exception cref="AssemblyNotFoundException">Thrown when the specified assembly name is not found.</exception>
	/// <remarks>
	/// When <paramref name="assemblyName"/> is <see langword="null"/>, this method returns types from all loaded assemblies.
	/// Otherwise, it returns types only from the specified assembly.
	/// </remarks>
	public static Type[] GetTypes(string? assemblyName = null) {
		Type[] types = [];
		if (assemblyName is null) {
			Assembly[] assemblies = GetAssemblies();
			foreach (var item in assemblies)
				ArrayManipulation.Add(item.GetTypes(), ref types!);
		} else {
			Assembly assembly = GetAssembly(assemblyName) ?? throw new AssemblyNotFoundException(assemblyName);
			types = assembly.GetTypes();
		}
		return types;
	}
	/// <summary>Checks if the type exists.</summary>
	/// <param name="fullName">The full name of the type (e.g., <c>System.String</c>).</param>
	/// <param name="assemblyName">The full name of the assembly to search in. If <see langword="null"/>, all assemblies are searched.</param>
	/// <returns>Returns <see langword="true"/> when the type exists; otherwise, <see langword="false"/>.</returns>
	/// <remarks>
	/// This method compares the type's full name (including namespace) with the provided <paramref name="fullName"/>.
	/// </remarks>
	public static bool TypeExist(string fullName, string? assemblyName = null) {
		foreach (var item in GetTypes(assemblyName))
			if (item.Name == fullName)
				return true;
		return false;
	}
	/// <summary>Get a specific type.</summary>
	/// <param name="fullName">The full name of the type (e.g., <c>System.String</c>).</param>
	/// <param name="assemblyName">The full name of the assembly to search in. If <see langword="null"/>, all assemblies are searched.</param>
	/// <returns>
	/// Returns the type that was specified in the parameter.
	/// <para>If the specified type is not found, returns the <see cref="NullObject.Null"/> type.</para>
	/// </returns>
	/// <remarks>
	/// This method searches for a type by its full name (including namespace). If not found, it returns the type of <see cref="NullObject.Null"/>.
	/// </remarks>
	public static Type GetType(string fullName, string? assemblyName = null) {
		foreach (Type item in GetTypes(assemblyName))
			if (item.FullName == fullName)
				return item;
		return NullObject.Null.GetType();
	}
	/// <summary>Get all assemblies for the current domain.</summary>
	/// <returns>Returns a list of all assemblies in the current application domain.</returns>
	public static Assembly[] GetAssemblies()
		=> AppDomain.CurrentDomain.GetAssemblies();
	/// <summary>Get a specific assembly by its full name.</summary>
	/// <param name="assemblyName">The full name of the assembly to retrieve.</param>
	/// <returns>
	/// The requested <see cref="Assembly"/> if found; otherwise, <see langword="null"/>.
	/// </returns>
	/// <exception cref="ArgumentException">Thrown when <paramref name="assemblyName"/> is null or empty.</exception>
	/// <remarks>
	/// This method searches the current application domain for an assembly with the exact full name.
	/// </remarks>
	public static Assembly? GetAssembly(string? assemblyName) {
		ExceptionMessages.ThrowIfNullOrEmpty(assemblyName);
		foreach (Assembly item in GetAssemblies())
			if (item.FullName == assemblyName)
				return item;
		return null;
	}
}