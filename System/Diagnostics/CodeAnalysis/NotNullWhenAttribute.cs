#if NET472 || NETSTANDARD2_0
namespace System.Diagnostics.CodeAnalysis;
/// <summary>Indicates that the parameter will not be null if the method returns the specified value.</summary>
/// <param name="returnValue">The condition of the return value. If the method returns this value, the associated parameter will not be <c>null</c>.</param>
[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
internal sealed class NotNullWhenAttribute(bool returnValue) : Attribute {
    /// <summary>Gets the condition of the returned value.</summary>
    /// <returns>The condition of the return value. If the method returns this value, the associated parameter will not be <c>null</c>.</returns>
    public bool ReturnValue { get; } = returnValue;
}
#endif