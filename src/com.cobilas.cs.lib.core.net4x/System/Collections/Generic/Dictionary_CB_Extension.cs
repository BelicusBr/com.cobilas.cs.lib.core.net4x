namespace System.Collections.Generic;
/// <summary>Provides extension methods for <see cref="Dictionary{TKey, TValue}"/>.</summary>
public static class Dictionary_CB_Extension {
	/// <summary>Searches for a key-value pair in the dictionary using a predicate for the key.</summary>
	/// <typeparam name="Tkey">The type of keys in the dictionary.</typeparam>
	/// <typeparam name="Tvalue">The type of values in the dictionary.</typeparam>
	/// <param name="d">The dictionary to search in.</param>
	/// <param name="predicate">A function to test each key for a condition.</param>
	/// <returns>The first key-value pair whose key matches the predicate, or a <c><see langword="default"/>(<see cref="KeyValuePair{TKey, TValue}"/>)</c> if no match is found.</returns>
	/// <exception cref="ArgumentNullException">
	/// Thrown when <paramref name="d"/> or <paramref name="predicate"/> is <see langword="null"/>.
	/// </exception>
	/// <remarks>
	/// This method iterates through the dictionary and returns the first key-value pair where the key satisfies the predicate.
	/// If no match is found, returns <c><see langword="default"/>(<see cref="KeyValuePair{TKey, TValue}"/>)</c>, which typically has a default key and value.
	/// </remarks>
	public static KeyValuePair<Tkey, Tvalue> Find<Tkey, Tvalue>(this Dictionary<Tkey, Tvalue>? d, Predicate<Tkey>? predicate) where Tkey : notnull {
		ExceptionMessages.ThrowIfNull(d);
		ExceptionMessages.ThrowIfNull(predicate);
		foreach (KeyValuePair<Tkey, Tvalue> item in d)
			if (predicate(item.Key))
				return item;
		return default;
	}
}