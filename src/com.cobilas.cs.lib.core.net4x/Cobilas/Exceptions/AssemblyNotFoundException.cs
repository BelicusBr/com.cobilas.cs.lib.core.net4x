using System;
using System.Runtime.Serialization;

namespace Cobilas.Exceptions;

[Serializable]
public class AssemblyNotFoundException : Exception {
	public AssemblyNotFoundException(string assemblyName) : base($"The assembly '{assemblyName}' could not be found!") { }
	public AssemblyNotFoundException(string assemblyName, Exception inner) : base($"The assembly '{assemblyName}' could not be found!", inner) { }
#if NET8_0_OR_GREATER
    [Obsolete(DiagnosticId = "SYSLIB0051")]
#endif
	protected AssemblyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
#if NET8_0_OR_GREATER
    [Obsolete(DiagnosticId = "SYSLIB0051")]
#endif
	public override void GetObjectData(SerializationInfo info, StreamingContext context) => base.GetObjectData(info, context);
}