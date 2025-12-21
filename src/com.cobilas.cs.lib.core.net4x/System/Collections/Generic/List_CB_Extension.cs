namespace System.Collections.Generic;
/// <summary>An extension class for the generic List class.</summary>
public static class List_CB_Extension {
    /// <inheritdoc cref="Stack{T}.Pop"/>
    public static T Pop<T>(this List<T> list) {
        T item = list[list.Count - 1];
        list.RemoveAt(list.Count - 1);
        return item;
    }
    /// <inheritdoc cref="Queue{T}.Dequeue"/>
    public static T Dequeue<T>(this List<T> list) {
        T item = list[0];
        list.RemoveAt(0);
        return item;
    }
    /// <inheritdoc cref="Queue{T}.Peek"/>
    public static T QueuePeek<T>(this List<T> list) => list[0];
    /// <inheritdoc cref="Stack{T}.Peek"/>
    public static T StackPeek<T>(this List<T> list) => list[list.Count - 1];
}
