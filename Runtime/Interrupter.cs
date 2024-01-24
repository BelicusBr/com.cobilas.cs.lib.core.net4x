using System;
using System.Text;
using Cobilas.Collections;

namespace Cobilas { 
    [Serializable]
#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
    public struct Interrupter : IDisposable {
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
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
        ///<summary>Esta propriedade permite a troca de unico interruptor para mult interruptores e vise versa.</summary>
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

#pragma warning disable CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente
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
#pragma warning restore CS1591 // O comentário XML ausente não foi encontrado para o tipo ou membro visível publicamente

        /// <summary>Only one switch specifying the index will be used, the others will remain at false value.</summary>
        /// <param name="Capacity">How many switches.</param>
        /// <param name="UseASwitch">Allows you to use one switch at a time.</param>
        public Interrupter(int Capacity, bool UseASwitch) {
            _switches = new bool[Capacity];
            currentIndex = -1;
            useASwitch = UseASwitch;
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
            ArrayManipulation.ClearArraySafe(ref _switches);
        }

        private readonly void WasDiscarded() {
            if (disposable) 
                throw new ObjectDisposedException("The object has already been discarded.");
        }
    }
}
