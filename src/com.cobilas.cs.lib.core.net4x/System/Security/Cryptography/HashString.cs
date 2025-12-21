namespace System.Security.Cryptography;
/// <summary>
/// The HashString structure can be used to transform a list of bytes with a minimum length of 16 into a 
/// <see cref="string"/> or <see cref="Guid"/>.
/// </summary>
public readonly struct HashString : IEquatable<HashString>, IComparable<HashString>, IEquatable<string>, IFormattable
{
	private readonly byte[] hash;

	/// <summary>
	/// A read-only instance of type <see cref="HashString"/> whose value is all zero.
	/// </summary>
	public readonly static HashString Empty = new(new byte[16]);

	/// <summary>
	/// Initializes a <see cref="HashString"/> object using a list of bytes with a minimum length of 16.
	/// </summary>
	/// <param name="hash">The byte array to convert to a hash string.</param>
	/// <exception cref="ArgumentNullException">The hash parameter cannot be null.</exception>
	/// <exception cref="ArgumentException">The hash parameter cannot have a length shorter than 16.</exception>
	/// <remarks>
	/// The input byte array is processed using an XOR operation to create a 16-byte hash value.
	/// If the input array is longer than 16 bytes, the bytes are cyclically XORed into the result.
	/// </remarks>
	public HashString(byte[] hash)
	{
		if (hash is null) throw new ArgumentNullException(nameof(hash), "The hash parameter cannot be null.");
		else if (hash.Length < 16) throw new ArgumentException("The hash parameter cannot have a length shorter than 16.");
		byte[] h16 = new byte[16];
		for (long I = 0, J = 0; I < hash.LongLength; I++)
		{
			h16[J++] ^= hash[I];
			J = J >= 16 ? 0 : J;
		}
		this.hash = h16;
	}

	/// <inheritdoc/>
	public override bool Equals(object? obj)
		=> (obj is HashString hs && Equals(hs)) ||
		(obj is string stg && Equals(stg));

	/// <inheritdoc/>
	public override int GetHashCode()
	{
		int res = 0;
		foreach (byte item in hash)
			res ^= item;
		return res;
	}

	/// <inheritdoc/>
	public bool Equals(HashString other)
		=> ((Guid)this) == ((Guid)other);

	/// <inheritdoc/>
	public bool Equals(string? other) => ((Guid)this).ToString() == other;

	/// <inheritdoc/>
	public override string ToString()
		=> ((Guid)this).ToString();

	/// <inheritdoc/>
	public string ToString(string format)
		=> ((Guid)this).ToString(format);

	/// <inheritdoc/>
	public string ToString(string? format, IFormatProvider? formatProvider)
		=> ((Guid)this).ToString(format, formatProvider);

	/// <inheritdoc/>
	public int CompareTo(HashString other)
		=> ((Guid)this).CompareTo((Guid)other);

	/// <summary>
	/// Implicit conversion from <see cref="HashString"/> to <see cref="string"/> type.
	/// </summary>
	/// <param name="hash">The HashString to convert.</param>
	/// <returns>A string representation of the HashString in GUID format.</returns>
	public static implicit operator string(HashString hash) => hash.ToString();

	/// <summary>
	/// Explicit conversion from <see cref="HashString"/> object to <see cref="Guid"/>.
	/// </summary>
	/// <param name="hash">The HashString to convert.</param>
	/// <returns>A Guid representation of the HashString.</returns>
	public static explicit operator Guid(HashString hash) => new(hash.hash);

	/// <summary>
	/// Determines whether two specified instances of <see cref="HashString"/> are equal.
	/// </summary>
	/// <param name="left">The first object to compare.</param>
	/// <param name="right">The second object to compare.</param>
	/// <returns>true if left and right are equal; otherwise, false.</returns>
	public static bool operator ==(HashString left, HashString right) => left.Equals(right);

	/// <summary>
	/// Determines whether two specified instances of <see cref="HashString"/> are not equal.
	/// </summary>
	/// <param name="left">The first object to compare.</param>
	/// <param name="right">The second object to compare.</param>
	/// <returns>true if left and right are not equal; otherwise, false.</returns>
	public static bool operator !=(HashString left, HashString right) => !(left == right);

	/// <summary>
	/// Determines whether one specified <see cref="HashString"/> is less than another specified <see cref="HashString"/>.
	/// </summary>
	/// <param name="left">The first object to compare.</param>
	/// <param name="right">The second object to compare.</param>
	/// <returns>true if left is less than right; otherwise, false.</returns>
	public static bool operator <(HashString left, HashString right) => left.CompareTo(right) < 0;

	/// <summary>
	/// Determines whether one specified <see cref="HashString"/> is less than or equal to another specified <see cref="HashString"/>.
	/// </summary>
	/// <param name="left">The first object to compare.</param>
	/// <param name="right">The second object to compare.</param>
	/// <returns>true if left is less than or equal to right; otherwise, false.</returns>
	public static bool operator <=(HashString left, HashString right) => left.CompareTo(right) <= 0;

	/// <summary>
	/// Determines whether one specified <see cref="HashString"/> is greater than another specified <see cref="HashString"/>.
	/// </summary>
	/// <param name="left">The first object to compare.</param>
	/// <param name="right">The second object to compare.</param>
	/// <returns>true if left is greater than right; otherwise, false.</returns>
	public static bool operator >(HashString left, HashString right) => left.CompareTo(right) > 0;

	/// <summary>
	/// Determines whether one specified <see cref="HashString"/> is greater than or equal to another specified <see cref="HashString"/>.
	/// </summary>
	/// <param name="left">The first object to compare.</param>
	/// <param name="right">The second object to compare.</param>
	/// <returns>true if left is greater than or equal to right; otherwise, false.</returns>
	public static bool operator >=(HashString left, HashString right) => left.CompareTo(right) >= 0;
}