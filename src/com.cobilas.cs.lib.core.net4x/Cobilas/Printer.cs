using System;
using System.Collections.Generic;

namespace Cobilas;
/// <summary>
/// Provides a flexible, event-driven console printing API that supports
/// plain output, formatted strings, and inline color tagging via <see cref="ColorPrinting(string?)"/>.
/// All output is routed through the <see cref="PrintFunction"/> delegate,
/// allowing the default behavior to be replaced or extended at runtime.
/// </summary>
public static class Printer {
	/// <summary>
	/// Command key that routes output through <see cref="Console.Write(object)"/>.
	/// Passed as the first argument to <see cref="PrintFunction"/>.
	/// </summary>
	public const string PrintLine = "ptl";
	/// <summary>
	/// Command key that routes output through <see cref="Console.WriteLine()"/>.
	/// Passed as the first argument to <see cref="PrintFunction"/>.
	/// </summary>
	public const string PrintNewLine = "ptnl";
	/// <summary>
	/// Shared argument array reused across all <c>Print</c>/<c>PrintInLine</c> calls
	/// to avoid per-call allocations. Index 0 holds a dispatch code; subsequent
	/// indices hold the value(s) to print.
	/// </summary>
	private static readonly object[] fixedArgument = new object[5];
	/// <summary>
	/// Cache of pre-parsed <see cref="ColorfulString"/> sequences keyed by the
	/// hash of the original tagged string. Avoids re-parsing the same color markup
	/// on repeated calls to <see cref="ColorPrinting(string?)"/>.
	/// </summary>
	private static readonly Dictionary<int, List<ColorfulString>?> buffer = [];
	/// <summary>
	/// Raised inside <see cref="PrintFunction"/> when dispatch code <c>3</c> is used,
	/// allowing the caller to apply a console color identified by a string ID
	/// before the text is written. Subscribers should set <see cref="Console.ForegroundColor"/>
	/// based on the received color ID, then <see cref="Console.ResetColor"/> is called afterward.
	/// </summary>
	public static event Action<string>? ColoringPrint = null;
	/// <summary>
	/// Core output delegate. Receives a command key (<see cref="PrintLine"/> or
	/// <see cref="PrintNewLine"/>) and an argument array and performs the actual write.
	/// <para>Dispatch codes stored in <c>args[0]</c>:</para>
	/// <list type="bullet">
	///   <item><c>0</c> — write <c>args[1]</c> directly.</item>
	///   <item><c>1</c> — write a char buffer slice: <c>args[1]</c> (char[]), <c>args[2]</c> (index), <c>args[3]</c> (count).</item>
	///   <item><c>2</c> — write a format string: <c>args[1]</c> (format), <c>args[2]</c> (params object[]).</item>
	///   <item><c>3</c> — fire <see cref="ColoringPrint"/> with <c>args[1]</c> (color ID), then write <c>args[2]</c> (text).</item>
	/// </list>
	/// Replace this delegate to redirect or suppress all console output.
	/// </summary>
	public static event Action<string, object[]>? PrintFunction = (stg, a) => {
		switch (stg) {
			case PrintLine:
				switch (a[0]) {
					case 0:
						Console.Write(a[1]);
						break;
					case 1:
						Console.Write((char[])a[1], (int)a[2], (int)a[3]);
						break;
					case 2:
						Console.Write((string)a[1], (object[])a[2]);
						break;
					case 3:
						ColoringPrint?.Invoke((string)a[1]);
						Console.Write((string)a[2]);
						Console.ResetColor();
						break;
				}
				break;
			case PrintNewLine:
				switch (a[0]) {
					case 0:
						Console.WriteLine(a[1]);
						break;
					case 1:
						Console.WriteLine((char[])a[1], (int)a[2], (int)a[3]);
						break;
					case 2:
						Console.WriteLine((string)a[1], (object[])a[2]);
						break;
					case 3:
						ColoringPrint?.Invoke((string)a[1]);
						Console.WriteLine((string)a[2]);
						Console.ResetColor();
						break;
				}
				break;
		}
	};
	/// <summary>Writes a <see cref="bool"/> value without a trailing newline.</summary>
	/// <param name="value">The value to write.</param>
	public static void Print(bool value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintLine, fixedArgument);
	}
	/// <summary>Writes a <see cref="char"/> value without a trailing newline.</summary>
	/// <param name="value">The value to write.</param>
	public static void Print(char value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintLine, fixedArgument);
	}
	/// <summary>Writes a <see cref="char"/> array without a trailing newline.</summary>
	/// <param name="buffer">The character array to write.</param>
	public static void Print(char[] buffer) {
		fixedArgument[0] = 0;
		fixedArgument[1] = buffer;
		PrintFunction?.Invoke(PrintLine, fixedArgument);
	}
	/// <summary>Writes a slice of a <see cref="char"/> array without a trailing newline.</summary>
	/// <param name="buffer">The source character array.</param>
	/// <param name="index">The zero-based starting index within <paramref name="buffer"/>.</param>
	/// <param name="count">The number of characters to write.</param>
	public static void Print(char[] buffer, int index, int count) {
		fixedArgument[0] = 1;
		fixedArgument[1] = buffer;
		fixedArgument[2] = index;
		fixedArgument[3] = count;
		PrintFunction?.Invoke(PrintLine, fixedArgument);
	}
	/// <summary>Writes a <see cref="string"/> value without a trailing newline.</summary>
	/// <param name="value">The string to write.</param>
	public static void Print(string value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintLine, fixedArgument);
	}
	/// <summary>Writes an <see cref="object"/> value without a trailing newline.</summary>
	/// <param name="value">The object to write.</param>
	public static void Print(object value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintLine, fixedArgument);
	}
	/// <summary>Writes an <see cref="sbyte"/> value without a trailing newline.</summary>
	/// <param name="value">The value to write.</param>
	public static void Print(sbyte value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintLine, fixedArgument);
	}
	/// <summary>Writes a <see cref="byte"/> value without a trailing newline.</summary>
	/// <param name="value">The value to write.</param>
	public static void Print(byte value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintLine, fixedArgument);
	}
	/// <summary>Writes a <see cref="ushort"/> value without a trailing newline.</summary>
	/// <param name="value">The value to write.</param>
	public static void Print(ushort value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintLine, fixedArgument);
	}
	/// <summary>Writes a <see cref="short"/> value without a trailing newline.</summary>
	/// <param name="value">The value to write.</param>
	public static void Print(short value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintLine, fixedArgument);
	}
	/// <summary>Writes a <see cref="uint"/> value without a trailing newline.</summary>
	/// <param name="value">The value to write.</param>
	public static void Print(uint value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintLine, fixedArgument);
	}
	/// <summary>Writes an <see cref="int"/> value without a trailing newline.</summary>
	/// <param name="value">The value to write.</param>
	public static void Print(int value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintLine, fixedArgument);
	}
	/// <summary>Writes a <see cref="ulong"/> value without a trailing newline.</summary>
	/// <param name="value">The value to write.</param>
	public static void Print(ulong value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintLine, fixedArgument);
	}
	/// <summary>Writes a <see cref="long"/> value without a trailing newline.</summary>
	/// <param name="value">The value to write.</param>
	public static void Print(long value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintLine, fixedArgument);
	}
	/// <summary>Writes a <see cref="float"/> value without a trailing newline.</summary>
	/// <param name="value">The value to write.</param>
	public static void Print(float value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintLine, fixedArgument);
	}
	/// <summary>Writes a <see cref="double"/> value without a trailing newline.</summary>
	/// <param name="value">The value to write.</param>
	public static void Print(double value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintLine, fixedArgument);
	}
	/// <summary>Writes a <see cref="decimal"/> value without a trailing newline.</summary>
	/// <param name="value">The value to write.</param>
	public static void Print(decimal value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintLine, fixedArgument);
	}
	/// <summary>
	/// Writes a formatted string without a trailing newline,
	/// substituting <paramref name="args"/> into <paramref name="format"/>.
	/// </summary>
	/// <param name="format">A composite format string.</param>
	/// <param name="args">Objects to format into the string.</param>
	public static void Print(string format, params object[] args) {
		fixedArgument[0] = 2;
		fixedArgument[1] = format;
		fixedArgument[2] = args;
		PrintFunction?.Invoke(PrintLine, fixedArgument);
	}
	/// <summary>Writes a <see cref="bool"/> value followed by a newline.</summary>
	/// <param name="value">The value to write.</param>
	public static void PrintInLine(bool value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintNewLine, fixedArgument);
	}
	/// <summary>Writes a <see cref="char"/> value followed by a newline.</summary>
	/// <param name="value">The value to write.</param>
	public static void PrintInLine(char value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintNewLine, fixedArgument);
	}
	/// <summary>Writes a <see cref="char"/> array followed by a newline.</summary>
	/// <param name="buffer">The character array to write.</param>
	public static void PrintInLine(char[] buffer) {
		fixedArgument[0] = 0;
		fixedArgument[1] = buffer;
		PrintFunction?.Invoke(PrintNewLine, fixedArgument);
	}
	/// <summary>Writes a slice of a <see cref="char"/> array followed by a newline.</summary>
	/// <param name="buffer">The source character array.</param>
	/// <param name="index">The zero-based starting index within <paramref name="buffer"/>.</param>
	/// <param name="count">The number of characters to write.</param>
	public static void PrintInLine(char[] buffer, int index, int count) {
		fixedArgument[0] = 1;
		fixedArgument[1] = buffer;
		fixedArgument[2] = index;
		fixedArgument[3] = count;
		PrintFunction?.Invoke(PrintNewLine, fixedArgument);
	}
	/// <summary>Writes a <see cref="string"/> value followed by a newline.</summary>
	/// <param name="value">The string to write.</param>
	public static void PrintInLine(string value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintNewLine, fixedArgument);
	}
	/// <summary>Writes an <see cref="object"/> value followed by a newline.</summary>
	/// <param name="value">The object to write.</param>
	public static void PrintInLine(object value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintNewLine, fixedArgument);
	}
	/// <summary>Writes an <see cref="sbyte"/> value followed by a newline.</summary>
	/// <param name="value">The value to write.</param>
	public static void PrintInLine(sbyte value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintNewLine, fixedArgument);
	}
	/// <summary>Writes a <see cref="byte"/> value followed by a newline.</summary>
	/// <param name="value">The value to write.</param>
	public static void PrintInLine(byte value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintNewLine, fixedArgument);
	}
	/// <summary>Writes a <see cref="ushort"/> value followed by a newline.</summary>
	/// <param name="value">The value to write.</param>
	public static void PrintInLine(ushort value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintNewLine, fixedArgument);
	}
	/// <summary>Writes a <see cref="short"/> value followed by a newline.</summary>
	/// <param name="value">The value to write.</param>
	public static void PrintInLine(short value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintNewLine, fixedArgument);
	}
	/// <summary>Writes a <see cref="uint"/> value followed by a newline.</summary>
	/// <param name="value">The value to write.</param>
	public static void PrintInLine(uint value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintNewLine, fixedArgument);
	}
	/// <summary>Writes an <see cref="int"/> value followed by a newline.</summary>
	/// <param name="value">The value to write.</param>
	public static void PrintInLine(int value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintNewLine, fixedArgument);
	}
	/// <summary>Writes a <see cref="ulong"/> value followed by a newline.</summary>
	/// <param name="value">The value to write.</param>
	public static void PrintInLine(ulong value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintNewLine, fixedArgument);
	}
	/// <summary>Writes a <see cref="long"/> value followed by a newline.</summary>
	/// <param name="value">The value to write.</param>
	public static void PrintInLine(long value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintNewLine, fixedArgument);
	}
	/// <summary>Writes a <see cref="float"/> value followed by a newline.</summary>
	/// <param name="value">The value to write.</param>
	public static void PrintInLine(float value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintNewLine, fixedArgument);
	}
	/// <summary>Writes a <see cref="double"/> value followed by a newline.</summary>
	/// <param name="value">The value to write.</param>
	public static void PrintInLine(double value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintNewLine, fixedArgument);
	}
	/// <summary>Writes a <see cref="decimal"/> value followed by a newline.</summary>
	/// <param name="value">The value to write.</param>
	public static void PrintInLine(decimal value) {
		fixedArgument[0] = 0;
		fixedArgument[1] = value;
		PrintFunction?.Invoke(PrintNewLine, fixedArgument);
	}
	/// <summary>
	/// Writes a formatted string followed by a newline,
	/// substituting <paramref name="args"/> into <paramref name="format"/>.
	/// </summary>
	/// <param name="format">A composite format string.</param>
	/// <param name="args">Objects to format into the string.</param>
	public static void PrintInLine(string format, params object[] args) {
		fixedArgument[0] = 2;
		fixedArgument[1] = format;
		fixedArgument[2] = args;
		PrintFunction?.Invoke(PrintNewLine, fixedArgument);
	}
	/// <inheritdoc cref="ColorPrinting(bool, string?)"/>
	public static void ColorPrinting(string? print)
		=> ColorPrinting(false, print);
	/// <summary>
	/// Parses and prints a color-tagged string followed by a newline.
	/// Equivalent to calling <see cref="ColorPrinting(string?)"/> with a trailing line break.
	/// See <see cref="ColorPrinting(string?)"/> for full markup syntax and caching behavior.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Special escape sequences inside tag text:
	/// <list type="bullet">
	///   <item><c>$lft</c> → <c>&gt;</c></item>
	///   <item><c>$rgt</c> → <c>&lt;</c></item>
	///   <item><c>$\lft</c> → literal <c>$lft</c></item>
	///   <item><c>$\rgt</c> → literal <c>$rgt</c></item>
	/// </list>
	/// </para>
	/// </remarks>
	/// <param name="print">The color-tagged string to parse and print.</param>
	/// <exception cref="ArgumentNullException">Thrown when <paramref name="print"/> is <c>null</c>.</exception>
	public static void ColorPrintingInLine(string? print)
		=> ColorPrinting(true, print);
	/// <summary>
	/// Parses and prints a color-tagged string of the form
	/// <c>&lt;colorId:text&gt;</c> interspersed with plain text segments.
	/// <para>
	/// On the first call for a given string, the markup is parsed into a list of
	/// <see cref="ColorfulString"/> records and cached by hash. Subsequent calls
	/// reuse the cached result without re-parsing.
	/// </para>
	/// <para>
	/// Special escape sequences inside tag text:
	/// <list type="bullet">
	///   <item><c>$lft</c> → <c>&gt;</c></item>
	///   <item><c>$rgt</c> → <c>&lt;</c></item>
	///   <item><c>$\lft</c> → literal <c>$lft</c></item>
	///   <item><c>$\rgt</c> → literal <c>$rgt</c></item>
	/// </list>
	/// </para>
	/// </summary>
	/// <param name="print">The color-tagged string to parse and print.</param>
	/// <param name="printInLine">Determines whether the method prints the message inline.</param>
	/// <exception cref="ArgumentNullException">Thrown when <paramref name="print"/> is <c>null</c>.</exception>
	private static void ColorPrinting(bool printInLine, string? print) {
		ExceptionMessages.ThrowIfNull(print, nameof(print));

		int textHash = print.GetHashCode();
		if (buffer.TryGetValue(textHash, out List<ColorfulString>? value)) {
			ExceptionMessages.ThrowIfNull(value, nameof(value));
			foreach (ColorfulString item in value) {
				fixedArgument[0] = 3;
				fixedArgument[1] = item.colorId;
				fixedArgument[2] = item.text;
				PrintFunction?.Invoke(PrintLine, fixedArgument);
			}
			if (printInLine)
				Print("\r\n");
			return;
		}

		int index = 0;
		List<string> raws = [];
		List<string> textList;
		List<ColorfulString> colors = [];

		while (true) {
			int start = print.IndexOf('<', index);
			if (start < 0) break;

			int end = print.IndexOf('>', start);
			if (end < 0) break;

			string raw = print.Substring(start, end + 1 - start);

			if (!raws.Contains(raw))
				raws.Add(raw);

			index = end + 1;
		}

		for (int I = 0; I < raws.Count; I++)
			print = print.Replace(raws[I], $";{{{I}}};");

		textList = [.. print.Split([';'], StringSplitOptions.RemoveEmptyEntries)];

		for (int A = 0; A < textList.Count; A++)
			for (int B = 0; B < raws.Count; B++)
				if (textList[A] == $"{{{B}}}")
					textList[A] = raws[B];

		foreach (string stg in textList) {
			if (stg.Contains('<') && stg.Contains('>')) {
				string[] txt = stg.Split([':'], StringSplitOptions.RemoveEmptyEntries);
				fixedArgument[0] = 3;
				fixedArgument[1] = txt[0].TrimStart('<');
				fixedArgument[2] = txt[1].TrimEnd('>').Replace("$lft", ">").Replace("$rgt", "<")
					.Replace("$\\lft", "$lft").Replace("$\\rgt", "$rgt");
				PrintFunction?.Invoke(PrintLine, fixedArgument);
			} else {
				fixedArgument[0] = 3;
				fixedArgument[1] = "none";
				fixedArgument[2] = stg;
				PrintFunction?.Invoke(PrintLine, fixedArgument);
			}
			colors.Add(new((string)fixedArgument[1], (string)fixedArgument[2]));
		}
		if (printInLine)
			Print("\r\n");
		buffer.Add(textHash, colors);
	}

	/// <summary>
	/// Stores a parsed segment from a color-tagged string,
	/// pairing a color identifier with the text to display.
	/// Plain text segments use <c>"none"</c> as the color ID.
	/// </summary>
	private record struct ColorfulString(string colorId, string text);
}