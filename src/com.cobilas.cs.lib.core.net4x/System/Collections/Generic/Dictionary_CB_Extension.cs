namespace System.Collections.Generic;

public static class Dictionary_CB_Extension {

	public static KeyValuePair<key, value> Find<key, value>(this Dictionary<key, value>? d, Predicate<key>? predicate) {
		ExceptionMessages.ThrowIfNull(d);
		ExceptionMessages.ThrowIfNull(predicate);
		foreach (KeyValuePair<key, value> item in d)
			if (predicate(item.Key))
				return item;
		return default;
	}
}
