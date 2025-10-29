using System;
using System.Text;
using Cobilas.Collections;

namespace Cobilas;  
/// <summary>Represents a list of switches.</summary>
[Serializable]
public struct Interrupter : IDisposable {
    private int currentIndex;
    private bool useASwitch;
    private bool disposable;
    private bool[] _switches;

    /// <summary>Returns the current switch index.</summary>
    public readonly int CurrentIndex {
        get {
            WasDiscarded();
            return currentIndex;
        }
    }
    ///<summary>This property allows the exchange of a single switch for multiple switches and vice versa.</summary>
    public bool UseASwitch {
		readonly get {
            WasDiscarded();
            return useASwitch;
        }
        set {
            WasDiscarded();
            useASwitch = value;
        }
    }

    /// <summary>Gets or sets a switch when specifying an index.</summary>
    public bool this[int Index] {
		readonly get {
            WasDiscarded();
            return _switches[Index];
        }
        set {
            WasDiscarded();
            if (currentIndex != Index && useASwitch) {
                for (int I = 0; I < _switches.Length; I++)
                    if (I != Index) _switches[I] = false;
                currentIndex = Index;
            }
            _switches[Index] = value;
        }
    }

    /// <summary>Only one switch specifying the index will be used, the others will remain at false value.</summary>
    /// <param name="Capacity">How many switches.</param>
    /// <param name="UseASwitch">Allows you to use one switch at a time.</param>
    public Interrupter(int Capacity, bool UseASwitch) {
        _switches = new bool[Capacity];
        currentIndex = -1;
        useASwitch = UseASwitch;
        disposable = false;
    }

    /// <summary>Only one switch specifying the index will be used, the others will remain at false value.</summary>
    /// <param name="Capacity">How many switches.</param>
    public Interrupter(int Capacity) : this(Capacity, true) { }

    /// <summary>Returns a text representation of the object.</summary>
    public override readonly string ToString() {
        WasDiscarded();
        StringBuilder builder = new();
        builder.AppendLine("Switches {");
        for (int I = 0; I < _switches.Length; I++)
            builder.AppendLine($"\tswitch({I})[status:{_switches[I]}]");
        builder.AppendLine("}");
        return builder.ToString();
    }

    /// <summary>Resource disposal.</summary>
    public void Dispose() {
        WasDiscarded();
        disposable = true;
        useASwitch = default;
        currentIndex = default;
        ArrayManipulation.ClearArraySafe(ref _switches!);
    }

    private readonly void WasDiscarded() {
        if (disposable) 
            throw new ObjectDisposedException("The object has already been discarded.");
    }
}
