using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Cobilas.Collections { 
    /// <summary>Array manipulation class.</summary>
    public static class ArrayManipulation {

        //private readonly static Exception ArrayNullException = new ArgumentNullException("The array cannot be null.");
        // private readonly static Exception ArrayEmptyException = new ArgumentException("The array cannot be empty.");

        /// <summary>Insert a list of items at a given index into a target array.</summary>
        /// <param name="itens">The items that will be inserted into the list.</param>
        /// <param name="index">The index of the list where the items will be inserted.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static T[]? Insert<T>(T[]? itens, long index, T[]? list) {
            ExceptionMessages.ThrowIfNull(itens, nameof(itens));
            if (itens.LongLength == 0)
                return list;
            list = list is null ? [] : list;

            T[] newList = new T[list.LongLength + itens.LongLength];
            Array.Copy(list, 0, newList, 0, index);
            Array.Copy(itens, 0, newList, index, itens.LongLength);
            Array.Copy(list, index, newList, itens.LongLength + index, list.LongLength - index);
            return newList;
        }

        /// <summary>Insert a list of items at a given index into a target array.</summary>
        /// <param name="item">The item that will be inserted into the list.</param>
        /// <param name="index">The index of the list where the items will be inserted.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static T[]? Insert<T>(T? item, long index, T[]? list) {
			ExceptionMessages.ThrowIfNull(item, nameof(item));
			return Insert<T>([item], index, list);
        }

        /// <summary>Insert a list of items at a given index into a target array.</summary>
        /// <param name="collection">The items that will be inserted into the list.</param>
        /// <param name="index">The index of the list where the items will be inserted.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <exception cref="ArgumentNullException"/>
        public static T[]? Insert<T>(IEnumerable<T>? collection, long index, T[]? list) {
			ExceptionMessages.ThrowIfNull(collection, nameof(collection));
			foreach (T item in collection)
                list = Insert<T>(item, index, list);
            return list;
        }

        /// <summary>Insert a list of items at a given index into a target array.</summary>
        /// <param name="collection">The items that will be inserted into the list.</param>
        /// <param name="index">The index of the list where the items will be inserted.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <exception cref="ArgumentNullException"/>
        public static void Insert<T>(IEnumerable<T>? collection, long index, ref T[]? list)
            => list = Insert<T>(collection, index, list);

        /// <summary>Insert a list of items at a given index into a target array.</summary>
        /// <param name="itens">The items that will be inserted into the list.</param>
        /// <param name="index">The index of the list where the items will be inserted.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Insert<T>(T[]? itens, long index, ref T[]? list)
            => list = Insert<T>(itens, index, list);

        /// <summary>Insert a list of items at a given index into a target array.</summary>
        /// <param name="item">The item that will be inserted into the list.</param>
        /// <param name="index">The index of the list where the items will be inserted.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Insert<T>(T? item, long index, ref T[]? list)
            => list = Insert<T>(item, index, list);

        /// <summary>Adds a list of items to the target list.
        /// <para>The method does not add items that already exist in the target list.</para>
        /// </summary>
        /// <param name="item">The item that will be inserted into the list.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <returns>This way, when adding the same object, the operation will not be performed 
        /// and the list will be returned without being modified.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T[]? AddNon_Existing<T>(T? item, T[]? list) {
            if (!Exists<T>(item, list))
                return Add(item, list);
            return list;
        }

        /// <summary>Adds a list of items to the target list.
        /// <para>The method does not add items that already exist in the target list.</para>
        /// </summary>
        /// <param name="item">The item that will be inserted into the list.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <returns>This way, when adding the same object, the operation will not be performed 
        /// and the list will be returned without being modified.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddNon_Existing<T>(T? item, ref T[]? list)
            => list = AddNon_Existing<T>(item, list);

        /// <summary>Adds a list of items to the target list.</summary>
        /// <param name="itens">The items that will be inserted into the list.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static T[]? Add<T>(T[]? itens, T[]? list)
            => Insert<T>(itens, ArrayLongLength(list), list);

        /// <summary>Adds a list of items to the target list.</summary>
        /// <param name="collection">The items that will be inserted into the list.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static T[]? Add<T>(IEnumerable<T>? collection, T[]? list)
            => Insert<T>(collection, ArrayLongLength(list), list);

        /// <summary>Adds a list of items to the target list.</summary>
        /// <param name="collection">The items that will be inserted into the list.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Add<T>(IEnumerable<T>? collection, ref T[]? list)
            => list = Insert<T>(collection, ArrayLongLength(list), list);

        /// <summary>Adds a list of items to the target list.</summary>
        /// <param name="item">The item that will be inserted into the list.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static T[]? Add<T>(T? item, T[]? list)
            => Insert<T>(item, ArrayLongLength(list), list);

        /// <summary>Adds a list of items to the target list.</summary>
        /// <param name="itens">The items that will be inserted into the list.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Add<T>(T[]? itens, ref T[]? list)
            => Insert<T>(itens, ArrayLongLength(list), ref list);

        /// <summary>Adds a list of items to the target list.</summary>
        /// <param name="item">The item that will be inserted into the list.</param>
        /// <param name="list">The list that will receive the items.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Add<T>(T? item, ref T[]? list)
            => Insert<T>(item, ArrayLongLength(list), ref list);

        /// <summary>Remove items from the target list.</summary>
        /// <param name="index">The target index to remove from the target list.</param>
        /// <param name="length">The length and number of items to remove from the list from the index.</param>
        /// <param name="list">The list from which items will be removed.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static T[] Remove<T>(long index, long length, T[]? list) {
			ExceptionMessages.ThrowIfNull(list, nameof(list));
			if (list.LongLength == 0) return list;

            T[] newList = new T[list.LongLength - length];
            Array.Copy(list, 0, newList, 0, index);
            Array.Copy(list, index + length, newList, index, list.LongLength - (index + length));
            return newList;
        }

        /// <summary>Remove items from the target list.</summary>
        /// <param name="index">The target index to remove from the target list.</param>
        /// <param name="length">The length and number of items to remove from the list from the index.</param>
        /// <param name="list">The list from which items will be removed.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static void Remove<T>(long index, long length, ref T[]? list)
            => list = Remove<T>(index, length, list);

        /// <summary>Remove items from the target list.</summary>
        /// <param name="index">The target index to remove from the target list.</param>
        /// <param name="list">The list from which items will be removed.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static T[] Remove<T>(long index, T[]? list)
            => Remove<T>(index, 1L, list);

        /// <summary>Remove items from the target list.</summary>
        /// <param name="index">The target index to remove from the target list.</param>
        /// <param name="list">The list from which items will be removed.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static void Remove<T>(long index, ref T[]? list)
            => list = Remove<T>(index, list);

        /// <summary>Remove items from the target list.</summary>
        /// <param name="item">The target item to remove from the target list.</param>
        /// <param name="list">The list from which items will be removed.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static T[] Remove<T>(T? item, T[]? list)
            => Remove<T>(LongIndexOf(item, list), list);

        /// <summary>Remove items from the target list.</summary>
        /// <param name="item">The target item to remove from the target list.</param>
        /// <param name="list">The list from which items will be removed.</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static void Remove<T>(T? item, ref T[]? list)
            => list = Remove<T>(item, list);

        /// <summary>Array cleaning.</summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="RankException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ClearArray(Array? array, long index, long length) {
            if (array is null)
                throw new ArgumentNullException(nameof(array));
            else if (array.Rank != 1)
                throw new RankException("The array cannot be multi-dimensional.");
            else if (index < 0 || index >= array.LongLength)
                throw new ArgumentOutOfRangeException(nameof(index));
            else if (length < 0 || length > array.LongLength || index + length > array.LongLength)
                throw new ArgumentOutOfRangeException(nameof(length));
            
            for (long I = index; I < length; I++)
                array.SetValue(default, I);
        }

        /// <summary>Array cleaning.</summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="RankException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void LongClearArray(Array? array)
            => ClearArray(array, 0L, ArrayLongLength(array));

        /// <summary>Array cleaning.</summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static void ClearArray(Array? array, int index, int length)
            => Array.Clear(array!, index, length);

        /// <summary>Array cleaning.</summary>
        /// <exception cref="ArgumentNullException"/>
        public static void ClearArray(Array? array)
            => ClearArray(array, 0, ArrayLength(array));

        /// <summary>Array cleaning.</summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="RankException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ClearArray<T>(long index, long length, ref T[]? array) {
            ClearArray(array, index, length);
            array = [];
        }

        /// <summary>Array cleaning.</summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="RankException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void LongClearArray<T>(ref T[]? array)
            => ClearArray(0, ArrayLongLength(array), ref array);

        /// <summary>Array cleaning.
        /// <para>In addition to clearing the array, it returns an empty array.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static void ClearArray<T>(int index, int length, ref T[]? array) {
            Array.Clear(array!, index, length);
            array = [];
        }

        /// <summary>Array cleaning.
        /// <para>In addition to clearing the array, it returns an empty array.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        public static void ClearArray<T>(ref T[]? array)
            => ClearArray(0, array is null ? 0 : array.Length, ref array);

        /// <summary>Array cleaning.
        /// <para>It will only perform cleaning if the array is not null.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="RankException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static void ClearArraySafe(Array? array, long index, long length) {
            if (!EmpytArray(array))
                ClearArray(array, index, length);
        }
        
        /// <summary>Array cleaning.
        /// <para>It will only perform cleaning if the array is not null.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="RankException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static void LongClearArraySafe(Array? array) {
            if (!EmpytArray(array))
                LongClearArray(array);
        }

        /// <summary>Array cleaning.
        /// <para>It will only perform cleaning if the array is not null.</para>
        /// </summary>
        /// <exception cref="IndexOutOfRangeException"/>
        public static void ClearArraySafe(Array? array, int index, int length) {
            if (!EmpytArray(array))
                ClearArray(array, index, length);
        }

        /// <summary>Array cleaning.
        /// <para>It will only perform cleaning if the array is not null.</para>
        /// </summary>
        public static void ClearArraySafe(Array? array) {
            if (!EmpytArray(array))
                ClearArray(array);
        }

        /// <summary>Array cleaning.
        /// <para>In addition to clearing the array, it returns an empty array.</para>
        /// <para>It will only perform cleaning if the array is not null.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="RankException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static void ClearArraySafe<T>(long index, long length, ref T[]? array) {
            if (!EmpytArray(array))
                ClearArray(index, length, ref array);
        }
        
        /// <summary>Array cleaning.
        /// <para>In addition to clearing the array, it returns an empty array.</para>
        /// <para>It will only perform cleaning if the array is not null.</para>
        /// </summary>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="RankException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static void LongClearArraySafe<T>(ref T[]? array) {
            if (!EmpytArray(array))
                LongClearArray(ref array);
        }

        /// <summary>Array cleaning.
        /// <para>In addition to clearing the array, it returns an empty array.</para>
        /// <para>It will only perform cleaning if the array is not null.</para>
        /// </summary>
        /// <exception cref="IndexOutOfRangeException"/>
        public static void ClearArraySafe<T>(int index, int length, ref T[]? array) {
            if (!EmpytArray(array))
                ClearArray(index, length, ref array);
        }
    
        /// <summary>Array cleaning.
        /// <para>In addition to clearing the array, it returns an empty array.</para>
        /// <para>It will only perform cleaning if the array is not null.</para>
        /// </summary>
        public static void ClearArraySafe<T>(ref T[]? array) {
            if (!EmpytArray(array))
                ClearArray(ref array);
        }

        /// <summary>Separate a list into two using an index.</summary>
        /// <param name="array">The list that will be separated.</param>
        /// <param name="separationIndex">The index where the list will be separated.</param>
        /// <param name="part1"></param>
        /// <param name="part2"></param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="RankException"/>
        /// <exception cref="ArgumentOutOfRangeException"/>
        public static void SeparateList<T>(T[]? array, long separationIndex, out T[] part1, out T[] part2) {
            long arrayLength = ArrayManipulation.ArrayLongLength(array);
            if (array is null)
                throw new ArgumentNullException(nameof(array));
            else if (array.Rank != 1)
                throw new RankException("The array cannot be multi-dimensional.");
            else if (separationIndex < 0 || !(separationIndex < arrayLength))
                throw new ArgumentOutOfRangeException(nameof(separationIndex));

            Array.Copy(array, 0, part1 = new T[separationIndex + 1], 0, separationIndex + 1);
            Array.Copy(array, separationIndex + 1, part2 = new T[array.LongLength - (separationIndex + 1)], 0, array.LongLength - (separationIndex + 1));
        }

        /// <summary>This function performs a cut in a list.</summary>
        /// <param name="index">The index where the clipping will begin.</param>
        /// <param name="length">The index where the clipping will begin.</param>
        /// <param name="list">The list that will be cut.</param>
        /// <returns>The function will return a list of items that were cut from the original list. The original list will not be modified.</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ArgumentException"/>
        /// <exception cref="IndexOutOfRangeException"/>
        public static T[] TakeStretch<T>(long index, long length, T[]? list) {
			ExceptionMessages.ThrowIfNull(list, nameof(list));
            if (list.LongLength == 0) throw new ArgumentException("The array cannot be empty.");

            T[] Res = new T[length];
            CopyTo(list, index, Res, 0, length);
            return Res;
        }

        /// <summary>
        /// Turn a list into a read-only list
        /// </summary>
        public static ReadOnlyCollection<T> ReadOnly<T>(T[]? list)
            => Array.AsReadOnly<T>(list ?? throw new ArgumentNullException(nameof(list)));

        /// <summary>
        /// Turn a list into a read-only list
        /// </summary>
        /// <returns>If the list is null, it will return an empty read-only list.</returns>
        public static ReadOnlyCollection<T> ReadOnlySafe<T>(T[] list)
            => list is null ? new ReadOnlyCollection<T>(Array.Empty<T>()) : ReadOnly<T>(list);

        /// <summary>
        /// Searches for the specified object and returns the index of its first occurrence in a one-dimensional array or a range of elements in the array.
        /// </summary>
        public static long IndexOf(object? item, Array? array, long index, long length) {
            if (array is null)
                throw new ArgumentNullException(nameof(array));
            else if (array.Rank != 1)
                throw new RankException("The array cannot be multi-dimensional.");
            else if (index < 0 || index >= array.LongLength)
                throw new ArgumentOutOfRangeException(nameof(index));
            else if (length < 0 || length > array.LongLength || index + length > array.LongLength)
                throw new ArgumentOutOfRangeException(nameof(length));
            else if (array.LongLength == 0) return -1;

            long endIndex = index + length;
            if (array is object[] obj_array) {
                for (long I = index; I < endIndex; I++)
                    if (item is null) {
                        if (obj_array[I] is null) return I;
                    } else {
                        object temp = obj_array[I];
                        if (temp != null && temp.Equals(item)) return I;
                    }
            } else {
                for (long I = index; I < endIndex; I++)
                    if (array.GetValue(I) is object obj) {
                        if (obj is null && item is null) return I;
                        else if (obj is not null && obj.Equals(item)) return I;
                    }
            }
            return -1L;
        }

        /// <summary>
        /// Searches for the specified object and returns the index of its first occurrence in a one-dimensional array or a range of elements in the array.
        /// </summary>
        public static long IndexOf(object? item, Array? array, long index) {
            ExceptionMessages.ThrowIfNull(array, nameof(array));
			ExceptionMessages.ThrowIfNull(item, nameof(item));
            if (array.Rank != 1)

                throw new RankException("The array cannot be multi-dimensional.");
            return IndexOf(item, array, index, array.LongLength - index);
        }

        /// <summary>
        /// Searches for the specified object and returns the index of its first occurrence in a one-dimensional array or a range of elements in the array.
        /// </summary>
        public static long LongIndexOf(object? item, Array? array)
            => IndexOf(item, array, 0L);

        /// <summary>
        /// Searches for the specified object and returns the index of its first occurrence in a one-dimensional array or a range of elements in the array.
        /// </summary>
        /// <exception cref="ArgumentNullException">array is null</exception>
        public static int IndexOf(object? item, Array? array, int index, int length)
            => Array.IndexOf(array ?? throw new ArgumentNullException(nameof(array)), item, index, length);

        /// <summary>
        /// Searches for the specified object and returns the index of its first occurrence in a one-dimensional array or a range of elements in the array.
        /// </summary>
        /// <exception cref="ArgumentNullException">array is null</exception>
        public static int IndexOf(object? item, Array? array, int index)
            => Array.IndexOf(array ?? throw new ArgumentNullException(nameof(array)), item, index);

        /// <summary>
        /// Searches for the specified object and returns the index of its first occurrence in a one-dimensional array or a range of elements in the array.
        /// </summary>
        /// <exception cref="ArgumentNullException">array is null</exception>
        public static int IndexOf(object? item, Array? array)
            => Array.IndexOf(array ?? throw new ArgumentNullException(nameof(array)), item);

        /// <summary>
        /// Returns the index of the last occurrence of a value in a one-dimensional Array or part of the Array.
        /// </summary>
        public static long LastIndexOf(object? item, Array? array, long index, long length) {
			ExceptionMessages.ThrowIfNull(array, nameof(array));
            if (array.Rank != 1)
                throw new RankException("The array cannot be multi-dimensional.");
            else if (index < 0 || index >= array.LongLength)
                throw new ArgumentOutOfRangeException(nameof(index));
            else if (length < 0 || length > array.LongLength || index + length > array.LongLength)
                throw new ArgumentOutOfRangeException(nameof(length));
            else if (array.LongLength == 0) return -1;

            long endIndex = length;
            if (array is object[] obj_array) {
                for (long I = index; I >= endIndex ; I--)
                    if (item is null) {
                        if (obj_array[I] is null) return I;
                    } else {
                        object temp = obj_array[I];
                        if (temp != null && temp.Equals(item)) return I;
                    }
            } else {
                for (long I = index; I >= endIndex ; I--)
                    if (array.GetValue(I) is object obj) {
                        if (obj is null && item is null) return I;
                        else if (obj is not null && obj.Equals(item)) return I;
                    }
            }
            return -1L;
        }

        /// <summary>
        /// Returns the index of the last occurrence of a value in a one-dimensional Array or part of the Array.
        /// </summary>
        public static long LastIndexOf(object item, Array array, long index) {
			ExceptionMessages.ThrowIfNull(array, nameof(array));
            if (array.Rank != 1)
                throw new RankException("The array cannot be multi-dimensional.");
            return LastIndexOf(item, array, array.LongLength - index, index);
        }

        /// <summary>
        /// Returns the index of the last occurrence of a value in a one-dimensional Array or part of the Array.
        /// </summary>
        public static long LongLastIndexOf(object item, Array array)
            => LastIndexOf(item, array, 0L);

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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="RankException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static long FindIndex<T>(T[] array, long index, long length, Predicate<T>? match) {
            long arrayLength = ArrayManipulation.ArrayLongLength(array);
            ExceptionMessages.ThrowIfNull(array, nameof(array));
			ExceptionMessages.ThrowIfNull(match, nameof(match));
            if (array.Rank != 1)
                throw new RankException("The array cannot be multi-dimensional.");
            else if (arrayLength == 0) return -1;
            else if (length < 0 || length > arrayLength)
                throw new ArgumentOutOfRangeException(nameof(length));
            else if (index < 0 || !(index < arrayLength))
                throw new ArgumentOutOfRangeException(nameof(index));

            for (long I = index; I < length; I++)
                if (match(array[I])) return I;

            return -1;
        }

        /// <summary>
        /// Searches for an element that matches the conditions defined by the specified predicate and returns
        /// the zero-based index of the first occurrence within the range of elements in the Array that starts
        /// at the specified index and contains the specified number of elements.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="RankException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static long FindIndex<T>(T[]? array, long index, Predicate<T>? match) {
			ExceptionMessages.ThrowIfNull(array, nameof(array));
            ExceptionMessages.ThrowIfNull(match, nameof(match));
            if (array.Rank != 1)
                throw new RankException("The array cannot be multi-dimensional.");
            return FindIndex<T>(array, index, array.LongLength, match);
        }

        /// <summary>
        /// Searches for an element that matches the conditions defined by the specified predicate and returns
        /// the zero-based index of the first occurrence within the range of elements in the Array that starts
        /// at the specified index and contains the specified number of elements.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="RankException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static long LongFindIndex<T>(T[]? array, Predicate<T>? match)
            => FindIndex<T>(array, 0L, match);

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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="RankException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static long FindLastIndex<T>(T[] array, long index, long length, Predicate<T> match) {
            long arrayLength = ArrayManipulation.ArrayLongLength(array);
            if (array is null)
                throw new ArgumentNullException(nameof(array));
            else if (match is null)
                throw new ArgumentNullException(nameof(match));
            else if (array.Rank != 1)
                throw new RankException("The array cannot be multi-dimensional.");
            else if (arrayLength == 0) return -1;
            else if (length < 0 || length > arrayLength)
                throw new ArgumentOutOfRangeException(nameof(length));
            else if (index < 0 || !(index < arrayLength))
                throw new ArgumentOutOfRangeException(nameof(index));

            long startIndex = length - index;
            for (long I = startIndex - 1; I >= index ; I--)
                if (match(array[I])) return I;

            return -1;
        }

        /// <summary>
        /// Searches for an element that matches the conditions defined by a
        /// specified predicate and returns the zero-based index of the last
        /// occurrence in an Array or part of it.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="RankException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static long FindLastIndex<T>(T[] array, long index, Predicate<T> match) {
            if (array is null)
                throw new ArgumentNullException(nameof(array));
            else if (array.Rank != 1)
                throw new RankException("The array cannot be multi-dimensional.");
            return FindLastIndex<T>(array, index, array.LongLength, match);
        }

        /// <summary>
        /// Searches for an element that matches the conditions defined by a
        /// specified predicate and returns the zero-based index of the last
        /// occurrence in an Array or part of it.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="RankException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static long LongFindLastIndex<T>(T[] array, Predicate<T> match)
            => FindLastIndex<T>(array, 0L, match);

        /// <summary>
        /// Searches for an element that matches the conditions defined by a
        /// specified predicate and returns the zero-based index of the last
        /// occurrence in an Array or part of it.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="RankException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static int FindLastIndex<T>(T[] array, int index, int length, Predicate<T> match)
            => Array.FindLastIndex<T>(array, index, length, match);
    
        /// <summary>
        /// Searches for an element that matches the conditions defined by a
        /// specified predicate and returns the zero-based index of the last
        /// occurrence in an Array or part of it.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="RankException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static int FindLastIndex<T>(T[] array, int index, Predicate<T> match)
            => Array.FindLastIndex<T>(array, index, match);

        /// <summary>
        /// Searches for an element that matches the conditions defined by a
        /// specified predicate and returns the zero-based index of the last
        /// occurrence in an Array or part of it.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="RankException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static int FindLastIndex<T>(T[] array, Predicate<T> match)
            => Array.FindLastIndex<T>(array, match);

        /// <summary>
        /// Searches for an element that matches the conditions defined by the 
        /// specified predicate and returns the first occurrence in the entire Array.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="RankException"></exception>
        public static T? FindLast<T>(T[]? array, Predicate<T>? match) {
            ExceptionMessages.ThrowIfNull(array, nameof(array));
			ExceptionMessages.ThrowIfNull(match, nameof(match));
            if (array.Rank != 1)
                throw new RankException("The array cannot be multi-dimensional.");
            
            for (long I = array.LongLength - 1; I >= 0 ; I--)
                if (match(array[I]))
                    return array[I];
            return default;
        }

        /// <summary>
        /// Retrieves all elements that match the conditions defined by the specified predicate.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="RankException"></exception>
        public static T[]? FindAll<T>(T[]? array, Predicate<T>? match) {
            ExceptionMessages.ThrowIfNull(array, nameof(array));
			ExceptionMessages.ThrowIfNull(match, nameof(match));
            if (array.Rank != 1)
                throw new RankException("The array cannot be multi-dimensional.");
            
            T[]? outArray = [];
            for (long I = 0; I < array.LongLength; I++)
                if (match(array[I]))
                    outArray = Add(array[I], outArray);
            return outArray;
        }

        /// <summary>
        /// Searches for an element that matches the conditions defined by the 
        /// specified predicate, and returns the first occurrence within the entire Array.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="RankException"></exception>
        public static T? Find<T>(T[]? array, Predicate<T>? match) {
			ExceptionMessages.ThrowIfNull(array, nameof(array));
            ExceptionMessages.ThrowIfNull(match, nameof(match));
            if (array.Rank != 1)
                throw new RankException("The array cannot be multi-dimensional.");

            for (long I = 0; I < array.LongLength; I++)
                if (match(array[I]))
                    return array[I];
            return default;
        }

        /// <summary>
        /// Determines whether the specified array contains elements that match the conditions defined by the specified predicate.
        /// </summary>
        public static bool Exists<T>([NotNullWhen(false)]T[]? array, Predicate<T>? match)
            => LongFindIndex<T>(array, match) != -1;

        /// <summary>
        /// Determines whether the specified array contains elements that match the conditions defined by the specified predicate.
        /// </summary>
        public static bool Exists<T>(T? item, [NotNullWhen(false)]T[]? array) {
			ExceptionMessages.ThrowIfNull(item, nameof(item));
            return Exists<T>(array, IT => EqualityComparer<T>.Default.Equals(IT, item));
        }

        /// <summary>
        /// Copies all the elements of the current one-dimensional array to the specified one-dimensional array.
        /// </summary>
        public static void CopyTo(Array? sourceArray, long sourceIndex, Array? destinationArray, long destinationIndex, long length) {
			ExceptionMessages.ThrowIfNull(sourceArray, nameof(sourceArray));
            ExceptionMessages.ThrowIfNull(destinationArray, nameof(destinationArray));

            Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);
        }

        /// <summary>
        /// Copies all the elements of the current one-dimensional array to the specified one-dimensional array.
        /// </summary>
        public static void CopyTo(Array? sourceArray, int sourceIndex, Array? destinationArray, int destinationIndex, int length)
            => CopyTo(sourceArray, (long)sourceIndex, destinationArray, (long)destinationIndex, (long)length);

        /// <summary>
        /// Copies all the elements of the current one-dimensional array to the specified one-dimensional array.
        /// </summary>
        public static void CopyTo(Array? sourceArray, Array? destinationArray, long length)
            => CopyTo(sourceArray, 0, destinationArray, 0, length);

        /// <summary>
        /// Copies all the elements of the current one-dimensional array to the specified one-dimensional array.
        /// </summary>
        public static void CopyTo(Array? sourceArray, Array? destinationArray, int length)
            => CopyTo(sourceArray, 0, destinationArray, 0, length);

        /// <summary>
        /// Copies all the elements of the current one-dimensional array to the specified one-dimensional array.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static void CopyTo(Array? sourceArray, Array? destinationArray) {
			ExceptionMessages.ThrowIfNull(sourceArray, nameof(sourceArray));

            CopyTo(sourceArray, 0, destinationArray, 0, sourceArray.Length);
        }

        /// <summary>Converts an array of one type to an array of another type.</summary>
        /// <typeparam name="TInput">The type of the elements of the source array.</typeparam>
        /// <typeparam name="TOutput">The type of the elements of the target array.</typeparam>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array"/> to convert to a target type.</param>
        /// <param name="converter">A <see cref="Converter{TInput, TOutput}"/> that converts each element from one type to another type.</param>
        /// <returns>An array of the target type containing the converted elements from the source array.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TOutput[] ConvertAll<TInput, TOutput>(TInput[]? array, Converter<TInput, TOutput>? converter) {
			ExceptionMessages.ThrowIfNull(array, nameof(array));
            ExceptionMessages.ThrowIfNull(converter, nameof(converter));

            return Array.ConvertAll<TInput, TOutput>(array, converter);
        }

        /// <summary>Converts an array of one type to an array of another type.</summary>
        /// <typeparam name="TInput">The type of the elements of the source array.</typeparam>
        /// <typeparam name="TOutput">The type of the elements of the target array.</typeparam>
        /// <param name="array">The one-dimensional, zero-based <see cref="Array"/> to convert to a target type.</param>
        /// <param name="converter">A <see cref="Converter{TInput, TOutput}"/> that converts each element from one type to another type.</param>
        /// <returns>An array of the target type containing the converted elements from the source array.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="RankException">The array cannot be multi-dimensional.</exception>
        public static TOutput[] LongConvertAll<TInput, TOutput>(TInput[]? array, Converter<TInput, TOutput>? converter) {
			ExceptionMessages.ThrowIfNull(array, nameof(array));
            ExceptionMessages.ThrowIfNull(converter, nameof(converter));
            if (array.Rank != 1)
                throw new RankException("The array cannot be multi-dimensional.");
            
            TOutput[] output = new TOutput[array.LongLength];
            for (long I = 0; I < array.LongLength; I++)
                output[I] = converter(array[I]);
            return output;
        }

        /// <summary>
        /// Reverses the order of the elements in a one-dimensional Array or in a portion of the Array.
        /// </summary>
        /// <param name="array">The one-dimensional Array to reverse.</param>
        /// <param name="index">The starting index of the section to reverse.</param>
        /// <param name="length">The number of elements in the section to reverse.</param>
        public static void Reverse(Array? array, int index, int length)
            => Array.Reverse(array ?? throw new ArgumentNullException(nameof(array)), index, length);

        /// <summary>
        /// Reverses the order of the elements in a one-dimensional Array or in a portion of the Array.
        /// </summary>
        /// <param name="array">The one-dimensional Array to reverse.</param>
        /// <param name="index">The starting index of the section to reverse.</param>
        /// <param name="length">The number of elements in the section to reverse.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="RankException">The array cannot be multi-dimensional.</exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void Reverse(Array? array, long index, long length) {
			ExceptionMessages.ThrowIfNull(array, nameof(array));
            if (array.Rank != 1)
                throw new RankException("The array cannot be multi-dimensional.");
            else if (index < 0 || index >= array.LongLength)
                throw new ArgumentOutOfRangeException(nameof(index));
            else if (length < 0 || length > array.LongLength || index + length - 1 > array.LongLength)
                throw new ArgumentOutOfRangeException(nameof(length));

            if (array is object[] obj_array) {
                for (long I = index, J = index + length - 1; I < J; I++, J--)
                    (obj_array[J], obj_array[I]) = (obj_array[I], obj_array[J]);
            } else {
                for (long I = index, J = index + length - 1; I < J; I++, J--) {
                    object temp = array.GetValue(I)!;
                    array.SetValue(array.GetValue(J), I);
                    array.SetValue(temp, J);
                }
            }
        }

        /// <summary>
        /// Reverses the order of the elements in a one-dimensional Array or in a portion of the Array.
        /// </summary>
        /// <param name="array">The one-dimensional Array to reverse.</param>
        /// <exception cref="ArgumentNullException">Occurs when <c>array</c> parameter is null.</exception>
        public static void Reverse(Array? array) {
			ExceptionMessages.ThrowIfNull(array, nameof(array));

            Array.Reverse(array, 0, array.Length);
        }

        /// <summary>
        /// Reverses the order of the elements in a one-dimensional Array or in a portion of the Array.
        /// </summary>
        /// <param name="array">The one-dimensional Array to reverse.</param>
        /// <exception cref="ArgumentNullException">Occurs when <c>array</c> parameter is null.</exception>
        public static void LongReverse(Array? array) {
			ExceptionMessages.ThrowIfNull(array, nameof(array));

            Reverse(array, 0L, array.LongLength);
        }

        /// <summary>
        /// Changes the number of elements of a one-dimensional array to the specified new size.
        /// </summary>
        public static void Resize<T>(ref T[]? array, int newSize)
            => Array.Resize<T>(ref array, newSize);
        /// <summary>
        /// Changes the number of elements of a one-dimensional array to the specified new size.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The <c>newSize</c> parameter cannot be less than zero.</exception>
        /// <exception cref="ArgumentNullException">Occurs when <c>array</c> parameter is null.</exception>
        public static void Resize<T>(ref T[]? array, long newSize) {
			ExceptionMessages.ThrowIfNull(array, nameof(array));
            ExceptionMessages.ThrowIfLessThan<long>(newSize, 0, nameof(newSize));

            T[] l_array = array;
            if (l_array is null) array = new T[newSize];
            else {
                T[] newArray = new T[newSize];
                CopyTo(l_array, 0L, newArray, 0L, l_array.LongLength > newSize ? newSize : l_array.LongLength);
                array = newArray;
            }
        }
        /// <summary>
        /// Determines whether the collection is empty or null.
        /// </summary>
        public static bool EmpytArray([NotNullWhen(false)]ICollection? array)
            => array is null || array.Count == 0;

        /// <summary>
        /// Determines whether the collection is empty or null.
        /// </summary>
        public static bool EmpytArray([NotNullWhen(false)]ILongCollection? array)
            => array is null || array.Count == 0;

        /// <summary>
        /// Determines the length of a collection.
        /// </summary>
        public static int ArrayLength([NotNullWhen(false)]ICollection? array)
            => array is null ? 0 : array.Count;

        /// <summary>
        /// Determines the length of an Array.
        /// </summary>
        public static long ArrayLongLength([NotNullWhen(false)]Array? array)
            => array is null ? 0L : array.LongLength;

        /// <summary>
        /// Determines the length of an Array.
        /// </summary>
        public static long ArrayLongLength([NotNullWhen(false)]ILongCollection? array)
            => array is null ? 0L : array.Count;

        /// <summary>
        /// Determines whether the collection is read-only.
        /// </summary>
        public static bool IsReadOnlySafe(IList? array)
            => array is not null && array.IsReadOnly;

        /// <summary>
        /// Determines whether the collection is read-only.
        /// </summary>
        public static bool IsReadOnlySafe(ILongList? array)
            => array is not null && array.IsReadOnly;

        /// <summary>
        /// Determines whether the collection has a fixed size.
        /// </summary>
        public static bool IsFixedSizeSafe(IList? array)
            => array is not null && array.IsFixedSize;

        /// <summary>
        /// Determines whether the collection has a fixed size.
        /// </summary>
        public static bool IsFixedSizeSafe(ILongList? array)
            => array is not null && array.IsFixedSize;

        /// <summary>
        /// Determines whether the collection is synchronized.
        /// </summary>
        public static bool IsSynchronizedSafe(ICollection? collection)
            => collection is not null && collection.IsSynchronized;

        /// <summary>
        /// Determines whether the collection is synchronized.
        /// </summary>
        public static bool IsSynchronizedSafe(ILongCollection? collection)
            => collection is not null && collection.IsSynchronized;

        /// <summary>The method traverses several parts of a list simultaneously.</summary>
        /// <param name="array">The array that will be read.</param>
        /// <param name="action">Action that receives the object and the index of the list.</param>
        /// <param name="sectorCount">The number of sectors to read.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ForSector<T>(Array? array, in Action<T, long>? action, in long sectorCount) {
			ExceptionMessages.ThrowIfNull(array, nameof(array));
			ExceptionMessages.ThrowIfNull(action, nameof(action));
            ExceptionMessages.ThrowIfLessThan<long>(sectorCount, 1, nameof(sectorCount));
            if (array.Rank != 1)
                throw new RankException("The array cannot be multi-dimensional.");

            SectorStatus[] sectors = new SectorStatus[sectorCount];

            long index = 0L;
            bool confirmations = true;
            bool preConfirmations = false;
            while(confirmations) {
                if (!sectors[index].Init) {
                    if (index == sectorCount - 1)
                        sectors[sectorCount - 1] = new SectorStatus((sectorCount - 1) * sectorCount, array.LongLength - 1);
                    else sectors[index] = new SectorStatus(index * sectorCount, (index + 1) * sectorCount);
                    sectors[index].Init = true;
                }
                if (!sectors[index].BrokenCount) {
                    preConfirmations = true;
                    action((T)array.GetValue(sectors[index].CurrentIndex)!, sectors[index].CurrentIndex);
                    sectors[index].Next();
                }
                index++;
                if (index >= sectorCount) {
                    index = 0L;
                    confirmations = preConfirmations;
                    preConfirmations = false;
                }
            }
        }

        /// <summary>The method traverses several parts of a list simultaneously.</summary>
        /// <param name="array">The array that will be read.</param>
        /// <param name="action">Action that receives the object and the index of the list.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ForSector<T>(Array? array, in Action<T, long>? action) {
			ExceptionMessages.ThrowIfNull(array, nameof(array));

            ForSector(array, in action, (long)Math.Sqrt(array.LongLength));
        }

        /// <summary>The method traverses several parts of a list simultaneously.</summary>
        /// <param name="array">The array that will be read.</param>
        /// <param name="action">Action that receives the object and the index of the list.</param>
        /// <param name="sectorCount">The number of sectors to read.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ForSector<T>(T[]? array, in Action<T, long>? action, in long sectorCount) {
			ExceptionMessages.ThrowIfNull(array, nameof(array));

            ForSector<T>((Array)array, action, sectorCount);
        }

        /// <summary>The method traverses several parts of a list simultaneously.</summary>
        /// <param name="array">The array that will be read.</param>
        /// <param name="action">Action that receives the object and the index of the list.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ForSector<T>(T[]? array, in Action<T, long>? action) {
			ExceptionMessages.ThrowIfNull(array, nameof(array));

            ForSector<T>((Array)array, action);
        }

        /// <summary>The method traverses several parts of a list simultaneously.</summary>
        /// <param name="array">The array that will be read.</param>
        /// <param name="action">Action that receives the object and the index of the list.</param>
        /// <param name="sectorCount">The number of sectors to read.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ForSector(Array? array, in Action<object, long>? action, in long sectorCount)
            => ForSector<object>(array, action, sectorCount);

        /// <summary>The method traverses several parts of a list simultaneously.</summary>
        /// <param name="array">The array that will be read.</param>
        /// <param name="action">Action that receives the object and the index of the list.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ForSector(Array? array, in Action<object, long>? action)
            => ForSector<object>(array, action);

        /// <summary>The method traverses several parts of a list simultaneously.</summary>
        /// <param name="list">The array that will be read.</param>
        /// <param name="action">Action that receives the object and the index of the list.</param>
        /// <param name="sectorCount">The number of sectors to read.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ForSector<T>(IList? list, in Action<T, int>? action, in int sectorCount) {
			ExceptionMessages.ThrowIfNull(list, nameof(list));
			ExceptionMessages.ThrowIfNull(action, nameof(action));
            ExceptionMessages.ThrowIfLessThan<long>(sectorCount, 1, nameof(sectorCount));

            SectorStatus[] sectors = new SectorStatus[sectorCount];

            long index = 0L;
            bool confirmations = true;
            bool preConfirmations = false;
            while(confirmations) {
                if (!sectors[index].Init) {
                    if (index == sectorCount - 1)
                        sectors[sectorCount - 1] = new SectorStatus((sectorCount - 1) * sectorCount, list.Count - 1);
                    else sectors[index] = new SectorStatus(index * sectorCount, (index + 1) * sectorCount);
                    sectors[index].Init = true;
                }
                if (!sectors[index].BrokenCount) {
                    preConfirmations = true;
                    action((T)list[(int)sectors[index].CurrentIndex]!, (int)sectors[index].CurrentIndex);
                    sectors[index].Next();
                }
                index++;
                if (index >= sectorCount) {
                    index = 0L;
                    confirmations = preConfirmations;
                    preConfirmations = false;
                }
            }
        }

        /// <summary>The method traverses several parts of a list simultaneously.</summary>
        /// <param name="list">The array that will be read.</param>
        /// <param name="action">Action that receives the object and the index of the list.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ForSector<T>(IList? list, in Action<T, int>? action) {
			ExceptionMessages.ThrowIfNull(list, nameof(list));

            ForSector(list, in action, (int)Math.Sqrt(list.Count));
        }

        /// <summary>The method traverses several parts of a list simultaneously.</summary>
        /// <param name="list">The array that will be read.</param>
        /// <param name="action">Action that receives the object and the index of the list.</param>
        /// <param name="sectorCount">The number of sectors to read.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ForSector<T>(IList<T>? list, in Action<T, int>? action, in int sectorCount) {
			ExceptionMessages.ThrowIfNull(list, nameof(list));

            ForSector<T>((IList)list, action, sectorCount);
        }

        /// <summary>The method traverses several parts of a list simultaneously.</summary>
        /// <param name="list">The array that will be read.</param>
        /// <param name="action">Action that receives the object and the index of the list.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ForSector<T>(IList<T>? list, in Action<T, int>? action) {
			ExceptionMessages.ThrowIfNull(list, nameof(list));

			ForSector<T>((IList)list, action);
        }

        /// <summary>The method traverses several parts of a list simultaneously.</summary>
        /// <param name="list">The array that will be read.</param>
        /// <param name="action">Action that receives the object and the index of the list.</param>
        /// <param name="sectorCount">The number of sectors to read.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ForSector(IList? list, in Action<object, int>? action, in int sectorCount)
            => ForSector<object>(list, action, sectorCount);

        /// <summary>The method traverses several parts of a list simultaneously.</summary>
        /// <param name="list">The array that will be read.</param>
        /// <param name="action">Action that receives the object and the index of the list.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ForSector(IList? list, in Action<object, int>? action)
            => ForSector<object>(list, action);

        private struct SectorStatus(long index, long count) {
            private bool init = false;
            private long index = index;
            private readonly long count = count;

            public readonly long CurrentIndex => index;
            public readonly bool BrokenCount => index >= count;
            public bool Init { readonly get => init; set => init = value; }

            public SectorStatus(long count) : this(0L, count) {}

            public void Next() {
                if (BrokenCount) return;
                index++;
            }
        }
    }
}
