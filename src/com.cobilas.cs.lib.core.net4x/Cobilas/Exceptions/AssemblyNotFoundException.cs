using System;
using System.Runtime.Serialization;

namespace Cobilas.Exceptions;
/// <summary>The exception that is thrown when an assembly cannot be found.</summary>
/// <remarks>
/// This exception is thrown when attempting to load or reference an assembly
/// that does not exist or cannot be located in the application domain.
/// </remarks>
[Serializable]
public class AssemblyNotFoundException : Exception {
	/// <summary>Initializes a new instance of the <see cref="AssemblyNotFoundException"/> class with the specified assembly name.</summary>
	/// <param name="assemblyName">The name of the assembly that could not be found.</param>
	public AssemblyNotFoundException(string assemblyName) : base($"The assembly '{assemblyName}' could not be found!") { }
	/// <summary>Initializes a new instance of the <see cref="AssemblyNotFoundException"/> class with the specified assembly name and a reference to the inner exception that is the cause of this exception.</summary>
	/// <param name="assemblyName">The name of the assembly that could not be found.</param>
	/// <param name="inner">The exception that is the cause of the current exception.</param>
	public AssemblyNotFoundException(string assemblyName, Exception inner) : base($"The assembly '{assemblyName}' could not be found!", inner) { }
	/// <inheritdoc/>
#if NET8_0_OR_GREATER
	[Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId = "SYSLIB0051")]
#endif
	protected AssemblyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	/// <inheritdoc/>
#if NET8_0_OR_GREATER
	[Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId = "SYSLIB0051")]
#endif
	public override void GetObjectData(SerializationInfo info, StreamingContext context) => base.GetObjectData(info, context);
}