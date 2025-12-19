using System.Diagnostics.CodeAnalysis;
#if !NET8_0_OR_GREATER
using System.Collections.Generic;
#endif

namespace System;

public static class ExceptionMessages {
	#region ArgumentException
	/// <summary>Throws an <see cref="ArgumentNullException"/> if <paramref name="argument"/> is null.</summary>
	/// <param name="argument">The reference type argument to validate as non-null.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
	public static void ThrowIfNull([NotNull]object? argument, string? paramName = null) {
		//ArgumentOutOfRangeException.ThrowIfZero
#if NET7_0_OR_GREATER
		ArgumentNullException.ThrowIfNull(argument, paramName);
#else
		if (argument is null)
			throw new ArgumentNullException(paramName);
#endif
	}
	/// <summary>Throws an <see cref="ArgumentNullException"/> if <paramref name="argument"/> is null.</summary>
	/// <param name="argument">The pointer argument to validate as non-null.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
	public static void ThrowIfNull(IntPtr argument, string? paramName = null)
	{
#if NET7_0_OR_GREATER
		ArgumentNullException.ThrowIfNull(argument, paramName);
#else
		if (argument == IntPtr.Zero)
			throw new ArgumentNullException(paramName);
#endif
	}
	/// <summary>Throws an exception if <paramref name="argument"/> is null or empty.</summary>
	/// <param name="argument">The string argument to validate as non-null and non-empty.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
	/// <exception cref="ArgumentNullException"><paramref name="argument"/> is null.</exception>
	/// <exception cref="ArgumentException"><paramref name="argument"/> is empty.</exception>
	public static void ThrowIfNullOrEmpty([NotNull]string? argument, string? paramName = null) {
#if NET7_0_OR_GREATER
		ArgumentException.ThrowIfNullOrEmpty(argument, paramName);
#else
		if (argument is null)
			throw new ArgumentNullException(paramName);
		else if (string.IsNullOrEmpty(argument))
			throw new ArgumentException($"The parameter '{paramName}' is empty.", paramName);
#endif
	}
	/// <summary>Throws an exception if <paramref name="argument"/> is null, empty, or consists only of white-space characters.</summary>
	/// <param name="argument">The string argument to validate.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
	/// <exception cref="ArgumentNullException"><paramref name="argument"/> is null.</exception>
	/// <exception cref="ArgumentException"><paramref name="argument"/> is empty or consists only of white-space characters.</exception>
	public static void ThrowIfNullOrWhiteSpace([NotNull] string? argument, string? paramName = null) {
#if NET7_0_OR_GREATER
		ArgumentException.ThrowIfNullOrWhiteSpace(argument, paramName);
#else
		ThrowIfNull(argument, paramName);
		if (string.IsNullOrWhiteSpace(argument))
			throw new ArgumentException($"The parameter {paramName} is empty or consists only of whitespace characters.", paramName);
#endif
	}
	#endregion
	#region ArgumentOutOfRangeException.ThrowIfZero
	/// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is zero.</summary>
	/// <param name="value">The argument to validate as non-zero.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	public static void ThrowIfZero(int value, string? paramName = null)
		=> ThrowIfZero<int>(value, paramName);
	/// <inheritdoc cref="ThrowIfZero(int, string?)"/>
	public static void ThrowIfZero(uint value, string? paramName = null)
		=> ThrowIfZero<uint>(value, paramName);
	/// <inheritdoc cref="ThrowIfZero(int, string?)"/>
	public static void ThrowIfZero(byte value, string? paramName = null)
		=> ThrowIfZero<byte>(value, paramName);
	/// <inheritdoc cref="ThrowIfZero(int, string?)"/>
	public static void ThrowIfZero(sbyte value, string? paramName = null)
		=> ThrowIfZero<sbyte>(value, paramName);
	/// <inheritdoc cref="ThrowIfZero(int, string?)"/>
	public static void ThrowIfZero(short value, string? paramName = null)
		=> ThrowIfZero<short>(value, paramName);
	/// <inheritdoc cref="ThrowIfZero(int, string?)"/>
	public static void ThrowIfZero(ushort value, string? paramName = null)
		=> ThrowIfZero<ushort>(value, paramName);
	/// <inheritdoc cref="ThrowIfZero(int, string?)"/>
	public static void ThrowIfZero(long value, string? paramName = null)
		=> ThrowIfZero<long>(value, paramName);
	/// <inheritdoc cref="ThrowIfZero(int, string?)"/>
	public static void ThrowIfZero(ulong value, string? paramName = null)
		=> ThrowIfZero<ulong>(value, paramName);
	/// <inheritdoc cref="ThrowIfZero(int, string?)"/>
	public static void ThrowIfZero(float value, string? paramName = null)
		=> ThrowIfZero<float>(value, paramName);
	/// <inheritdoc cref="ThrowIfZero(int, string?)"/>
	public static void ThrowIfZero(double value, string? paramName = null)
		=> ThrowIfZero<double>(value, paramName);
	/// <inheritdoc cref="ThrowIfZero(int, string?)"/>
	public static void ThrowIfZero(decimal value, string? paramName = null)
		=> ThrowIfZero<decimal>(value, paramName);
	private static void ThrowIfZero<T>(T value, string? paramName = null) 
#if NET8_0_OR_GREATER
		where T : Numerics.INumberBase<T> {
		ArgumentOutOfRangeException.ThrowIfZero<T>(value, paramName);
#else
		where T : struct {
		if (value.Equals(Convert.ChangeType(byte.MinValue, Convert.GetTypeCode(value))))
			throw new ArgumentOutOfRangeException(paramName, value, "('0') must be a non-zero value.");
#endif
	}
	#endregion
	#region ArgumentOutOfRangeException.ThrowIfNegative
	/// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is negative.</summary>
	/// <param name="value">The argument to validate as non-negative.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	public static void ThrowIfNegative(sbyte value, string? paramName = null)
		=> ThrowIfNegative<sbyte>(value, paramName);
	/// <inheritdoc cref="ThrowIfNegative(sbyte, string?)"/>
	public static void ThrowIfNegative(short value, string? paramName = null)
		=> ThrowIfNegative<short>(value, paramName);
	/// <inheritdoc cref="ThrowIfNegative(sbyte, string?)"/>
	public static void ThrowIfNegative(int value, string? paramName = null)
		=> ThrowIfNegative<int>(value, paramName);
	/// <inheritdoc cref="ThrowIfNegative(sbyte, string?)"/>
	public static void ThrowIfNegative(long value, string? paramName = null)
		=> ThrowIfNegative<long>(value, paramName);
	/// <inheritdoc cref="ThrowIfNegative(sbyte, string?)"/>
	public static void ThrowIfNegative(float value, string? paramName = null)
		=> ThrowIfNegative<float>(value, paramName);
	/// <inheritdoc cref="ThrowIfNegative(sbyte, string?)"/>
	public static void ThrowIfNegative(double value, string? paramName = null)
		=> ThrowIfNegative<double>(value, paramName);
	/// <inheritdoc cref="ThrowIfNegative(sbyte, string?)"/>
	public static void ThrowIfNegative(decimal value, string? paramName = null)
		=> ThrowIfNegative<decimal>(value, paramName);
	private static void ThrowIfNegative<T>(T value, string? paramName = null) 
#if NET8_0_OR_GREATER
		where T : Numerics.INumberBase<T> {
		ArgumentOutOfRangeException.ThrowIfNegative<T>(value, paramName);
#else
		where T : struct {
		if (Convert.ToDouble(value) < 0)
			throw new ArgumentOutOfRangeException(paramName, value, $"('{value}') must be a non-negative value.");
#endif
	}
	#endregion
	#region ArgumentOutOfRangeException.ThrowIfNegativeOrZero
	/// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is negative or zero.</summary>
	/// <param name="value">The argument to validate as non-zero or non-negative.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	public static void ThrowIfNegativeOrZero(int value, string? paramName = null)
		=> ThrowIfNegativeOrZero<int>(value, paramName);
	/// <inheritdoc cref="ThrowIfNegativeOrZero(int, string?)"/>
	public static void ThrowIfNegativeOrZero(uint value, string? paramName = null)
		=> ThrowIfNegativeOrZero<uint>(value, paramName);
	/// <inheritdoc cref="ThrowIfNegativeOrZero(int, string?)"/>
	public static void ThrowIfNegativeOrZero(byte value, string? paramName = null)
		=> ThrowIfNegativeOrZero<byte>(value, paramName);
	/// <inheritdoc cref="ThrowIfNegativeOrZero(int, string?)"/>
	public static void ThrowIfNegativeOrZero(sbyte value, string? paramName = null)
		=> ThrowIfNegativeOrZero<sbyte>(value, paramName);
	/// <inheritdoc cref="ThrowIfNegativeOrZero(int, string?)"/>
	public static void ThrowIfNegativeOrZero(short value, string? paramName = null)
		=> ThrowIfNegativeOrZero<short>(value, paramName);
	/// <inheritdoc cref="ThrowIfNegativeOrZero(int, string?)"/>
	public static void ThrowIfNegativeOrZero(ushort value, string? paramName = null)
		=> ThrowIfNegativeOrZero<ushort>(value, paramName);
	/// <inheritdoc cref="ThrowIfNegativeOrZero(int, string?)"/>
	public static void ThrowIfNegativeOrZero(long value, string? paramName = null)
		=> ThrowIfNegativeOrZero<long>(value, paramName);
	/// <inheritdoc cref="ThrowIfNegativeOrZero(int, string?)"/>
	public static void ThrowIfNegativeOrZero(ulong value, string? paramName = null)
		=> ThrowIfNegativeOrZero<ulong>(value, paramName);
	/// <inheritdoc cref="ThrowIfNegativeOrZero(int, string?)"/>
	public static void ThrowIfNegativeOrZero(float value, string? paramName = null)
		=> ThrowIfNegativeOrZero<float>(value, paramName);
	/// <inheritdoc cref="ThrowIfNegativeOrZero(int, string?)"/>
	public static void ThrowIfNegativeOrZero(double value, string? paramName = null)
		=> ThrowIfNegativeOrZero<double>(value, paramName);
	/// <inheritdoc cref="ThrowIfNegativeOrZero(int, string?)"/>
	public static void ThrowIfNegativeOrZero(decimal value, string? paramName = null)
		=> ThrowIfNegativeOrZero<decimal>(value, paramName);
	private static void ThrowIfNegativeOrZero<T>(T value, string? paramName = null)
#if NET8_0_OR_GREATER
		where T : Numerics.INumberBase<T> {
		ArgumentOutOfRangeException.ThrowIfNegativeOrZero<T>(value, paramName);
#else
		where T : struct
	{
		ThrowIfNegative<T>(value, paramName);
		ThrowIfZero<T>(value, paramName);
#endif
	}
	#endregion
	#region ArgumentOutOfRangeException.ThrowIfEqual
	/// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is equal to <paramref name="other"/>.</summary>
	/// <param name="value">The argument to validate as not equal to <paramref name="other"/>.</param>
	/// <param name="other">The value to compare with <paramref name="value"/>.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	public static void ThrowIfEqual<T>(T value, T other, string? paramName = null) where T : IEquatable<T> {
#if NET8_0_OR_GREATER
		ArgumentOutOfRangeException.ThrowIfEqual<T>(value, other, paramName);
#else
		if (EqualityComparer<T>.Default.Equals(value, other))
			throw new ArgumentOutOfRangeException(paramName, value, $"('{value}') must not be equal to '{other}'.");
#endif
	}
	#endregion
	#region ArgumentOutOfRangeException.ThrowIfNotEqual
	/// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is not equal to <paramref name="other"/>.</summary>
	/// <param name="value">The argument to validate as equal to <paramref name="other"/>.</param>
	/// <param name="other">The value to compare with <paramref name="value"/>.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	public static void ThrowIfNotEqual<T>(T value, T other, string? paramName = null) where T : IEquatable<T> {
#if NET8_0_OR_GREATER
		ArgumentOutOfRangeException.ThrowIfNotEqual<T>(value, other, paramName);
#else
		if (!EqualityComparer<T>.Default.Equals(value, other))
			throw new ArgumentOutOfRangeException(paramName, value, $"('{value}') must be equal to '{other}'.");
#endif
	}
	#endregion
	#region ArgumentOutOfRangeException.ThrowIfGreaterThan
	/// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is greater than <paramref name="other"/>.</summary>
	/// <param name="value">The argument to validate as less or equal than <paramref name="other"/>.</param>
	/// <param name="other">The value to compare with <paramref name="value"/>.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	public static void ThrowIfGreaterThan<T>(T value, T other, string? paramName = null) where T : IComparable<T>
	{
#if NET8_0_OR_GREATER
		ArgumentOutOfRangeException.ThrowIfGreaterThan<T>(value, other, paramName);
#else
		if (value.CompareTo(other) > 0)
			throw new ArgumentOutOfRangeException(paramName, value, $"('{value}') must be less than or equal to '{other}'.");
#endif
	}
	#endregion
	#region ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual
	/// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is greater than or equal <paramref name="other"/>.</summary>
	/// <param name="value">The argument to validate as less than <paramref name="other"/>.</param>
	/// <param name="other">The value to compare with <paramref name="value"/>.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	public static void ThrowIfGreaterThanOrEqual<T>(T value, T other, string? paramName = null) where T : IComparable<T>, IEquatable<T> {
#if NET8_0_OR_GREATER
		ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual<T>(value, other, paramName);
#else
		ThrowIfGreaterThan<T>(value, other, paramName);
		ThrowIfEqual<T>(value, other, paramName);
#endif
	}
	#endregion
	#region ArgumentOutOfRangeException.ThrowIfLessThan
	/// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is less than <paramref name="other"/>.</summary>
	/// <param name="value">The argument to validate as greater than or equal than <paramref name="other"/>.</param>
	/// <param name="other">The value to compare with <paramref name="value"/>.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	public static void ThrowIfLessThan<T>(T value, T other, string? paramName = null) where T : IComparable<T> {
#if NET8_0_OR_GREATER
		ArgumentOutOfRangeException.ThrowIfLessThan<T>(value, other, paramName);
#else
		if (value.CompareTo(other) < 0)
			throw new ArgumentOutOfRangeException(paramName, value, $"('{value}') must be greater than or equal to '{other}'.");
#endif
	}
	#endregion
	#region ArgumentOutOfRangeException.ThrowIfLessThanOrEqual
	/// <summary>Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is greater than or equal <paramref name="other"/>.</summary>
	/// <param name="value">The argument to validate as less than <paramref name="other"/>.</param>
	/// <param name="other">The value to compare with <paramref name="value"/>.</param>
	/// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
	public static void ThrowIfLessThanOrEqual<T>(T value, T other, string? paramName = null) where T : IComparable<T>, IEquatable<T> {
#if NET8_0_OR_GREATER
		ArgumentOutOfRangeException.ThrowIfLessThanOrEqual<T>(value, other, paramName);
#else
		ThrowIfLessThan<T>(value, other, paramName);
		ThrowIfEqual<T>(value, other, paramName);
#endif
	}
	#endregion
}
