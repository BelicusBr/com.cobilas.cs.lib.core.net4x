namespace System.Text {
    public static class StringBuilder_CB_Extension {
#if !NET5_0_OR_GREATER
        public static StringBuilder Clear(this StringBuilder S)
            => S.Remove(0, S.Length);
#endif
    }
}
