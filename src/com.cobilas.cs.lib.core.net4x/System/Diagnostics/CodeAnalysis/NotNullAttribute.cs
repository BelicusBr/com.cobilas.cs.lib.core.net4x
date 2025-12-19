#if NET45_OR_GREATER || (NETSTANDARD1_0_OR_GREATER && !NETSTANDARD2_1_OR_GREATER)
#pragma warning disable IDE0130 // O namespace não corresponde à estrutura da pasta
namespace System.Diagnostics.CodeAnalysis;
#pragma warning restore IDE0130 // O namespace não corresponde à estrutura da pasta
/// <summary>Specifies that an output will not be null even if the corresponding type allows it. Specifies that an input argument was not null when the call returns.</summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, Inherited = false)]
public sealed class NotNullAttribute : Attribute { }
#endif