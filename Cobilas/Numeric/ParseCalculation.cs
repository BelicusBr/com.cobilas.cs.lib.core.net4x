using System;
using System.Text;
using System.Diagnostics;
using Cobilas.Collections;
using System.Globalization;
using Cobilas.IO.Atlf.Components;

namespace Cobilas.Numeric; 
/// <summary>Performs all parse operations.</summary>
public static class ParseCalculation {

    /// <summary>All collections of mathematical operations created.</summary>
    public static CalculationsCollection[] Collections { get; private set; } = [];

    static ParseCalculation() {
        Collections = new CalculationsCollection[1];
        Collections[0] = new BasicCalculation();
        Collections[0].Initialization();

        foreach (var item in TypeUtilitarian.GetTypes()) {
            if (item.IsSubclassOf(typeof(CalculationsCollection)) &&
                !item.CompareType<BasicCalculation>() &&
                !item.CompareType<CalculationsCollection>()) {
                CalculationsCollection calc = item.Activator<CalculationsCollection>();
                calc.Initialization();
                Collections = ArrayManipulation.Add(calc, Collections)!;
            }
        }
    }

    /// <summary>Take a text containing a mathematical formula and perform the calculation.</summary>
    public static double Parse(string text)
        => GetMathBlock(new CharacterCursor(text.Replace(" ", string.Empty)), GetSignals(), false);

    /// <summary>Take a text containing a mathematical formula and perform the calculation.
    /// <para>Prints the mathematical formula and its result.</para>
    /// </summary>
    public static double PrintConsoleCalc(string text) {
        double res = Parse(text);
        Console.Write("{0}={1}\r\n", text, res);
        return res;
    }

    /// <summary>Take a text containing a mathematical formula and perform the calculation.
    /// <para>Prints the mathematical formula and its result.</para>
    /// </summary>
    public static double DebugLogCalc(string text) {
        double res = Parse(text);
        Debug.Print("{0}={1}", text, res);
        return res;
    }

    private static MathOperator[] GetSignals() {
        MathOperator[] s = [];
        foreach (var item in Collections)
            ArrayManipulation.Add(item.Calculations, ref s!);
        return s;
    }

    private static double GetMathBlock(CharacterCursor txt, MathOperator[] signals, bool isMathBlock) {
        StringBuilder builder = new();
        while (txt.MoveToCharacter()) {
            if (txt.CharIsEqualToIndex('('))
                builder.Append(GetMathBlock(txt, signals, true));
            else if (txt.CharIsEqualToIndex(')'))
                return CalcMathBlock(new CharacterCursor(builder), signals);
            else if (char.IsWhiteSpace(txt.CurrentCharacter))
                throw new FormatException("The expression cannot contain blanks!");
            else builder.Append(txt.CurrentCharacter);
        }
        if (isMathBlock)
            throw new FormatException("The parenthesis was not closed!");
        return CalcMathBlock(new CharacterCursor(builder), signals);
    }

    private static double CalcMathBlock(CharacterCursor txt, MathOperator[] signals) {
        string[] calc = [];
        StringBuilder builder = new();
        while (txt.MoveToCharacter()) {
            if (IsSignal(txt, signals, out string sg)) {
                txt.MoveToCharacter(sg.Length - 1);
                if (builder.Length != 0) {
                    ArrayManipulation.Add(builder.ToString(), ref calc!);
                    builder.Clear();
                }
                ArrayManipulation.Add(sg, ref calc!);
            } else builder.Append(txt.CurrentCharacter);
        }
        if (builder.Length != 0) {
            ArrayManipulation.Add(builder.ToString(), ref calc!);
            builder.Clear();
        }
        byte executionLevel = 0;
        foreach (var item in signals)
            if (item.ExecutionLevel > executionLevel)
                executionLevel = item.ExecutionLevel;
        for (int I = 0; I <= executionLevel && ArrayManipulation.ArrayLength(calc) > 1; I++) {
            for (int J = 0; J < ArrayManipulation.ArrayLength(calc); J++) {
                if (IsSignal(calc![J], signals, out MathOperator math))
                    if (math.ExecutionLevel == I)
                        switch (math.Orientation) {
                            case SignalOrientation.both:
                                if (J == 0)
                                    throw new FormatException($"The signal({calc[J]}) must have a value on the left.");
                                else if (J + 1 >= ArrayManipulation.ArrayLength(calc))
                                    throw new FormatException($"The signal({calc[J]}) must have a value to the right.");

                                if (!IsDigit(calc[J - 1]))
                                    throw new FormatException($"The value({calc[J - 1]}{calc[J]}) on the left is not a digit.");
                                else if (!IsDigit(calc[J + 1]))
                                    throw new FormatException($"The value({calc[J]}{calc[J + 1]}) on the right is not a digit.");

                                calc[J - 1] = (string)GetMathFuncResult(math, double.Parse(calc[J - 1]), double.Parse(calc[J + 1]));
                                ArrayManipulation.Remove(J, 2, ref calc!);
                                J = -1;
                                break;
                            case SignalOrientation.left:
                                if (J == 0)
                                    throw new FormatException($"The signal({calc[J]}) must have a value on the left.");

                                if (!IsDigit(calc[J - 1]))
                                    throw new FormatException($"The value({calc[J - 1]}{calc[J]}) on the left is not a digit.");
                                else if (J + 1 < ArrayManipulation.ArrayLength(calc)) {
                                    if (!IsSignal(calc[J + 1], signals, out _))
                                        throw new FormatException($"The value({calc[J]}{calc[J + 1]}) on the right is not a signal.");
                                }

                                calc[J - 1] = (string)GetMathFuncResult(math, double.Parse(calc[J - 1]), 0);
                                ArrayManipulation.Remove(J, 1, ref calc!);
                                J = -1;
                                break;
                            case SignalOrientation.right:
                                if (J + 1 >= ArrayManipulation.ArrayLength(calc))
                                    throw new FormatException($"The signal({calc[J]}) must have a value to the right.");
                                if (J != 0) {
                                    if (!IsSignal(calc[J - 1], signals, out _))
                                        throw new FormatException($"The value({calc[J - 1]}{calc[J]}) on the left is not a signal.");
                                } else if (!IsDigit(calc[J + 1]))
                                    throw new FormatException($"The value({calc[J]}{calc[J + 1]}) on the right is not a digit.");
                                calc[J] = (string)GetMathFuncResult(math, 0, double.Parse(calc[J + 1]));
                                ArrayManipulation.Remove(J + 1, 1, ref calc!);
                                J = -1;
                                break;
                        }
            }
        }
        return double.Parse(calc[0]);
    }

    private static bool IsDigit(string txt)
        => char.IsDigit(txt.ToString(CultureInfo.InvariantCulture), 0);

    private static bool IsSignal(string signal, MathOperator[] signals, out MathOperator math) {
        foreach (var item in signals)
            if (item.Signal == signal) {
                math = item;
                return true;
            }
        math = default;
        return false;
    }

    private static bool IsSignal(CharacterCursor txt, MathOperator[] signals, out string sg) {
        foreach (var item in signals)
            if (txt.CharIsEqualToIndex(item.Signal)) {
                sg = item.Signal;
                return true;
            }
        sg = string.Empty;
        return false;
    }

    internal static object GetMathFuncResult(MathOperator math, params object[] args) {
        if (math.Function is null) throw new ArgumentNullException($"Property {nameof(MathOperator)}.Function was defined as null.", new NullReferenceException());
        Delegate func = math.Function;
        object obj = func.DynamicInvoke(args) ?? throw new ArgumentNullException($"Return of property {nameof(MathOperator)}.Function cannot be null.", new NullReferenceException());
        string stg = obj.ToString() ?? throw new ArgumentNullException($"Return of property {nameof(MathOperator)}.Function cannot be null.", new NullReferenceException());
        return stg;
    }
}