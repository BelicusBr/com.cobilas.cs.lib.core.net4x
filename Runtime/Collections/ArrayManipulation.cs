using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Cobilas.Collections { 
    /// <summary>Array manipulation class.</summary>
    public static class ArrayManipulation {

        private readonly static Exception ArrayNullException = new ArgumentNullException("The array cannot be null.");
        // private readonly static Exception ArrayEmptyException = new ArgumentException("The array cannot be empty.");

        /// <summary>Insert a list of items at a given index into a target array.</summary>
        /// <param name="itens">The items that will be inserted into the list.</param>
        /// <param name="index">The index of the list where the items will be inserted.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static T[] Insert<T>(T[] itens, int index, T[] list) {
            if (itens is null) throw ArrayNullException;
            else if (itens.Length == 0)
                return list;
            list = list is null ? Array.Empty<T>() : list;

            T[] newList = new T[list.Length + itens.Length];
            Array.Copy(list, 0, newList, 0, index);
            Array.Copy(itens, 0, newList, index, itens.Length);
            Array.Copy(list, index, newList, itens.Length + index, list.Length - index);
            return newList;
        }

        /// <summary>Insert a list of items at a given index into a target array.</summary>
        /// <param name="item">The item that will be inserted into the list.</param>
        /// <param name="index">The index of the list where the items will be inserted.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static T[] Insert<T>(T item, int index, T[] list)
            => Insert<T>(new T[] { item }, index, list);

        /// <summary>Insert a list of items at a given index into a target array.</summary>
        /// <param name="itens">The items that will be inserted into the list.</param>
        /// <param name="index">The index of the list where the items will be inserted.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static T[] Insert<T>(IEnumerator<T> itens, int index, T[] list) {
            while (itens.MoveNext())
                list = Insert<T>(itens.Current, index, list);
            return list;
        }

        /// <summary>Insert a list of items at a given index into a target array.</summary>
        /// <param name="itens">The items that will be inserted into the list.</param>
        /// <param name="index">The index of the list where the items will be inserted.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Insert<T>(T[] itens, int index, ref T[] list)
            => list = Insert<T>(itens, index, list);

        /// <summary>Insert a list of items at a given index into a target array.</summary>
        /// <param name="item">The item that will be inserted into the list.</param>
        /// <param name="index">The index of the list where the items will be inserted.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Insert<T>(T item, int index, ref T[] list)
            => list = Insert<T>(item, index, list);

        /// <summary>Adds a list of items to the target list.
        /// <para>The method does not add items that already exist in the target list.</para>
        /// </summary>
        /// <param name="item">The item that will be inserted into the list.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static T[] AddNon_Existing<T>(T item, T[] list) {
            if (!Exists<T>(item, list))
                return Add(item, list);
            return list;
        }

        /// <summary>Adds a list of items to the target list.
        /// <para>The method does not add items that already exist in the target list.</para>
        /// </summary>
        /// <param name="item">The item that will be inserted into the list.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddNon_Existing<T>(T item, ref T[] list)
            => list = AddNon_Existing<T>(item, list);

        /// <summary>Adds a list of items to the target list.</summary>
        /// <param name="itens">The items that will be inserted into the list.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static T[] Add<T>(T[] itens, T[] list)
            => Insert<T>(itens, ArrayLength(list), list);

        /// <summary>Adds a list of items to the target list.</summary>
        /// <param name="itens">The items that will be inserted into the list.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static T[] Add<T>(IEnumerator<T> itens, T[] list)
            => Insert<T>(itens, ArrayLength(list), list);

        /// <summary>Adds a list of items to the target list.</summary>
        /// <param name="itens">The items that will be inserted into the list.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Add<T>(IEnumerator<T> itens, ref T[] list)
            => list = Add<T>(itens, list);

        /// <summary>Adds a list of items to the target list.</summary>
        /// <param name="item">The item that will be inserted into the list.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static T[] Add<T>(T item, T[] list)
            => Insert<T>(item, ArrayLength(list), list);

        /// <summary>Adds a list of items to the target list.</summary>
        /// <param name="itens">The items that will be inserted into the list.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Add<T>(T[] itens, ref T[] list)
            => Insert<T>(itens, ArrayLength(list), ref list);

        /// <summary>Adds a list of items to the target list.</summary>
        /// <param name="item">The item that will be inserted into the list.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Add<T>(T item, ref T[] list)
            => Insert<T>(item, ArrayLength(list), ref list);

        /// <summary>Remove items from the target list.</summary>
        /// <param name="index">The target index to remove from the target list.</param>
        /// <param name="length">The length and number of items to remove from the list from the index.</param>
        /// <param name="list">The list from which items will be removed.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static T[] Remove<T>(int index, int length, T[] list) {
            if (list is null) throw ArrayNullException;
            else if (list.Length == 0) return list;

            T[] newList = new T[list.Length - length];
            Array.Copy(list, 0, newList, 0, index);
            Array.Copy(list, index + length, newList, index, list.Length - (index + length));
            return newList;
        }

        /// <summary>Remove items from the target list.</summary>
        /// <param name="index">The target index to remove from the target list.</param>
        /// <param name="length">The length and number of items to remove from the list from the index.</param>
        /// <param name="list">The list from which items will be removed.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static void Remove<T>(int index, int length, ref T[] list)
            => list = Remove<T>(index, length, list);

        /// <summary>Remove items from the target list.</summary>
        /// <param name="index">The target index to remove from the target list.</param>
        /// <param name="list">The list from which items will be removed.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static T[] Remove<T>(int index, T[] list)
            => Remove<T>(index, 1, list);

        /// <summary>Remove items from the target list.</summary>
        /// <param name="index">The target index to remove from the target list.</param>
        /// <param name="list">The list from which items will be removed.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static void Remove<T>(int index, ref T[] list)
            => list = Remove<T>(index, list);

        /// <summary>Remove items from the target list.</summary>
        /// <param name="item">The target item to remove from the target list.</param>
        /// <param name="list">The list from which items will be removed.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static T[] Remove<T>(T item, T[] list)
            => Remove<T>(IndexOf(item, list), list);

        /// <summary>Remove items from the target list.</summary>
        /// <param name="item">The target item to remove from the target list.</param>
        /// <param name="list">The list from which items will be removed.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static void Remove<T>(T item, ref T[] list)
            => list = Remove<T>(item, list);

        /// <summary>Array cleaning.</summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static void ClearArray(Array array, int index, int length)
            => Array.Clear(array, index, length);

        /// <summary>Array cleaning.</summary>
        /// <exception cref="ArgumentNullException"/>
        public static void ClearArray(Array array)
            => ClearArray(array, 0, array.Length);

        /// <summary>Array cleaning.
        /// <para>In addition to clearing the array, it returns an empty array.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static void ClearArray<T>(int index, int length, ref T[] array) {
            Array.Clear(array, index, length);
            array = Array.Empty<T>();
        }

        /// <summary>Array cleaning.
        /// <para>In addition to clearing the array, it returns an empty array.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        public static void ClearArray<T>(ref T[] array)
            => ClearArray(0, array.Length, ref array);

        /// <summary>Array cleaning.
        /// <para>It will only perform cleaning if the array is not null.</para>
        /// </summary>
        /// <exception cref="IndexOutOfRangeException"/>
        public static void ClearArraySafe(Array array, int index, int length) {
            if (!EmpytArray(array))
                ClearArray(array, index, length);
        }

        /// <summary>Array cleaning.
        /// <para>It will only perform cleaning if the array is not null.</para>
        /// </summary>
        public static void ClearArraySafe(Array array) {
            if (!EmpytArray(array))
                ClearArray(array);
        }

        /// <summary>Array cleaning.
        /// <para>In addition to clearing the array, it returns an empty array.</para>
        /// <para>It will only perform cleaning if the array is not null.</para>
        /// </summary>
        /// <exception cref="IndexOutOfRangeException"/>
        public static void ClearArraySafe<T>(int index, int length, ref T[] array) {
            if (!EmpytArray(array))
                ClearArray(index, length, ref array);
        }
    
        /// <summary>Array cleaning.
        /// <para>In addition to clearing the array, it returns an empty array.</para>
        /// <para>It will only perform cleaning if the array is not null.</para>
        /// </summary>
        public static void ClearArraySafe<T>(ref T[] array) {
            if (!EmpytArray(array))
                ClearArray(ref array);
        }

        /// <summary>Separate a list into two using an index.</summary>
        /// <param name="list">The list that will be separated.</param>
        /// <param name="separationIndex">The index where the list will be separated.</param>
        /// <param name="part1"></param>
        /// <param name="part2"></param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static void SeparateList<T>(T[] list, int separationIndex, out T[] part1, out T[] part2) {
            if (list is null) throw ArrayNullException;
            else if (list.Length < 2) {
                part1 = list;
                part2 = Array.Empty<T>();
                return;
            }

            Array.Copy(list, 0, part1 = new T[separationIndex + 1], 0, separationIndex + 1);
            Array.Copy(list, separationIndex + 1, part2 = new T[list.Length - (separationIndex + 1)], 0, list.Length - (separationIndex + 1));
        }

        /// <summary>This function performs a cut in a list.</summary>
        /// <param name="index">The index where the clipping will begin.</param>
        /// <param name="length">The index where the clipping will begin.</param>
        /// <param name="list">The list that will be cut.</param>
        /// <returns>The function will return a list of items that were cut from the original list. The original list will not be modified.</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static T[] TakeStretch<T>(int index, int length, T[] list) {
            if (list is null) throw ArrayNullException;
            else if (list.Length == 0) throw new ArgumentException("The array cannot be empty.");

            T[] Res = new T[length];
            CopyTo(list, index, Res, 0, length);
            return Res;
        }

        /// <summary>
        /// Turn a list into a read-only list
        /// </summary>
        public static ReadOnlyCollection<T> ReadOnly<T>(T[] list)
            => Array.AsReadOnly<T>(list);

        /// <summary>
        /// Turn a list into a read-only list
        /// </summary>
        /// <returns>If the list is null, it will return an empty read-only list.</returns>
        public static ReadOnlyCollection<T> ReadOnlySafe<T>(T[] list)
            => list is null ? new ReadOnlyCollection<T>(Array.Empty<T>()) : ReadOnly<T>(list);

        /// <summary>
        /// Searches for the specified object and returns the index of its first occurrence in a one-dimensional array or a range of elements in the array.
        /// </summary>
        public static int IndexOf(object item, Array array, int index, int length)
            => Array.IndexOf(array, item, index, length);

        /// <summary>
        /// Searches for the specified object and returns the index of its first occurrence in a one-dimensional array or a range of elements in the array.
        /// </summary>
        public static int IndexOf(object item, Array array, int index)
            => Array.IndexOf(array, item, index);

        /// <summary>
        /// Searches for the specified object and returns the index of its first occurrence in a one-dimensional array or a range of elements in the array.
        /// </summary>
        public static int IndexOf(object item, Array array)
            => Array.IndexOf(array, item);

        /// <summary>
        /// Returns the index of the last occurrence of a value in a one-dimensional Array or part of the Array.
        /// </summary>
        public static int LastIndexOf(object item, Array array, int index, int length)
            => Array.LastIndexOf(array, item, index, length);

        /// <summary>
        /// Returns the index of the last occurrence of a value in a one-dimensional Array or part of the Array.
        /// </summary>
        public static int LastIndexOf(object item, Array array, int index)
            => Array.LastIndexOf(array, item, index);

        /// <summary>
        /// Returns the index of the last occurrence of a value in a one-dimensional Array or part of the Array.
        /// </summary>
        public static int LastIndexOf(object item, Array array)
            => Array.LastIndexOf(array, item);

        /// <summary>
        /// Searches for an element that matches the conditions defined by the specified predicate and returns
        /// the zero-based index of the first occurrence within the range of elements in the Array that starts
        /// at the specified index and contains the specified number of elements.
        /// </summary>
        public static int FindIndex<T>(T[] array, int index, int length, Predicate<T> match)
            => Array.FindIndex<T>(array, index, length, match);
    
        /// <summary>
        /// Searches for an element that matches the conditions defined by the specified predicate and returns
        /// the zero-based index of the first occurrence within the range of elements in the Array that starts
        /// at the specified index and contains the specified number of elements.
        /// </summary>
        public static int FindIndex<T>(T[] array, int index, Predicate<T> match)
            => Array.FindIndex<T>(array, index, match);

        /// <summary>
        /// Searches for an element that matches the conditions defined by the specified predicate and returns
        /// the zero-based index of the first occurrence within the range of elements in the Array that starts
        /// at the specified index and contains the specified number of elements.
        /// </summary>
        public static int FindIndex<T>(T[] array, Predicate<T> match)
            => Array.FindIndex<T>(array, match);

        /// <summary>
        /// Searches for an element that matches the conditions defined by a
        /// specified predicate and returns the zero-based index of the last
        /// occurrence in an Array or part of it.
        /// </summary>
        public static int FindLastIndex<T>(T[] array, int index, int length, Predicate<T> match)
            => Array.FindLastIndex<T>(array, index, length, match);
    
        /// <summary>
        /// Searches for an element that matches the conditions defined by a
        /// specified predicate and returns the zero-based index of the last
        /// occurrence in an Array or part of it.
        /// </summary>
        public static int FindLastIndex<T>(T[] array, int index, Predicate<T> match)
            => Array.FindLastIndex<T>(array, index, match);

        /// <summary>
        /// Searches for an element that matches the conditions defined by a
        /// specified predicate and returns the zero-based index of the last
        /// occurrence in an Array or part of it.
        /// </summary>
        public static int FindLastIndex<T>(T[] array, Predicate<T> match)
            => Array.FindLastIndex<T>(array, match);

        /// <summary>
        /// Searches for an element that matches the conditions defined by the 
        /// specified predicate and returns the first occurrence in the entire Array.
        /// </summary>
        public static T FindLast<T>(T[] array, Predicate<T> match)
            => Array.FindLast<T>(array, match);

        /// <summary>
        /// Retrieves all elements that match the conditions defined by the specified predicate.
        /// </summary>
        public static T[] FindAll<T>(T[] array, Predicate<T> match)
            => Array.FindAll<T>(array, match);

        /// <summary>
        /// Searches for an element that matches the conditions defined by the 
        /// specified predicate, and returns the first occurrence within the entire Array.
        /// </summary>
        public static T Find<T>(T[] array, Predicate<T> match)
            => Array.Find<T>(array, match);

        /// <summary>
        /// Determines whether the specified array contains elements that match the conditions defined by the specified predicate.
        /// </summary>
        [Obsolete("Use bool:Exists<T>(T[], Predicate<T>)")]
        public static bool Exists(object item, Array array) {
            for (int I = 0; I < ArrayLength(array); I++)
                if (array.GetValue(I) == item)
                    return true;
            return false;
        }

        /// <summary>
        /// Determines whether the specified array contains elements that match the conditions defined by the specified predicate.
        /// </summary>
        public static bool Exists<T>(T[] array, Predicate<T> match)
            => Array.Exists<T>(array, match);

        /// <summary>
        /// Determines whether the specified array contains elements that match the conditions defined by the specified predicate.
        /// </summary>
        public static bool Exists<T>(T item, T[] array)
            => Exists<T>(array, IT => EqualityComparer<T>.Default.Equals(IT, item));

        /// <summary>
        /// Copies all the elements of the current one-dimensional array to the specified one-dimensional array.
        /// </summary>
        public static void CopyTo(Array sourceArray, long sourceIndex, Array destinationArray, long destinationIndex, long length)
            => Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);

        /// <summary>
        /// Copies all the elements of the current one-dimensional array to the specified one-dimensional array.
        /// </summary>
        public static void CopyTo(Array sourceArray, int sourceIndex, Array destinationArray, int destinationIndex, int length)
            => CopyTo(sourceArray, (long)sourceIndex, destinationArray, (long)destinationIndex, (long)length);

        /// <summary>
        /// Copies all the elements of the current one-dimensional array to the specified one-dimensional array.
        /// </summary>
        public static void CopyTo(Array sourceArray, Array destinationArray, long length)
            => CopyTo(sourceArray, 0, destinationArray, 0, length);

        /// <summary>
        /// Copies all the elements of the current one-dimensional array to the specified one-dimensional array.
        /// </summary>
        public static void CopyTo(Array sourceArray, Array destinationArray, int length)
            => CopyTo(sourceArray, 0, destinationArray, 0, length);

        /// <summary>
        /// Copies all the elements of the current one-dimensional array to the specified one-dimensional array.
        /// </summary>
        public static void CopyTo(Array sourceArray, Array destinationArray)
            => CopyTo(sourceArray, 0, destinationArray, 0, sourceArray.Length);

        /// <summary>
        /// Reverses the order of the elements in a one-dimensional Array or in a portion of the Array.
        /// </summary>
        /// <param name="array">The one-dimensional Array to reverse.</param>
        /// <param name="index">The starting index of the section to reverse.</param>
        /// <param name="length">The number of elements in the section to reverse.</param>
        public static void Reverse(Array array, int index, int length)
            => Array.Reverse(array, index, length);

        /// <summary>
        /// Reverses the order of the elements in a one-dimensional Array or in a portion of the Array.
        /// </summary>
        /// <param name="array">The one-dimensional Array to reverse.</param>
        public static void Reverse(Array array)
            => Array.Reverse(array, 0, array.Length);

        /// <summary>
        /// Changes the number of elements of a one-dimensional array to the specified new size.
        /// </summary>
        public static void Resize<T>(ref T[] array, int newSize)
            => Array.Resize<T>(ref array, newSize);

        /// <summary>
        /// Determines whether the collection is empty or null.
        /// </summary>
        public static bool EmpytArray(ICollection array)
            => array is null || array.Count == 0;

        /// <summary>
        /// Determines the length of a collection.
        /// </summary>
        public static int ArrayLength(ICollection array)
            => array is null ? 0 : array.Count;

        /// <summary>
        /// Determines the length of an Array.
        /// </summary>
        public static long ArrayLongLength(Array array)
            => array is null ? 0L : array.LongLength;

        /// <summary>
        /// Determines whether the collection is read-only.
        /// </summary>
        public static bool IsReadOnlySafe(IList array)
            => !(array is null) && array.IsReadOnly;

        /// <summary>
        /// Determines whether the collection has a fixed size.
        /// </summary>
        public static bool IsFixedSizeSafe(IList array)
            => !(array is null) && array.IsFixedSize;

        /// <summary>
        /// Determines whether the collection is synchronized.
        /// </summary>
        public static bool IsSynchronizedSafe(ICollection collection)
            => !(collection is null) && collection.IsSynchronized;
    }
}
