using System;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;

namespace Ngol.Utilities.Collections
{
    /// <summary>
    /// Useful extensions on IEnumerable and IEnumerable&lt;T&gt;.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Copies the elements of the iterable to an <see cref="Array" />, starting
        /// at the specified array index.
        /// </summary>
        /// <param name="iterable">
        /// The iterable to copy.
        /// </param>
        /// <param name="array">
        /// The one-dimensional <see cref="Array" /> that is the destination of the elements copied from the sequence.
        /// </param>
        /// <param name="arrayIndex">
        /// A <see cref="System.Int32"/>
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if
        /// <list type="bullet">
        /// <item>
        /// The number of elements in the source collection is greater than the available space from <paramref name="arrayIndex"/>
        /// to the end of the destination <paramref name="array"/>.
        /// </item>
        /// <item>
        /// The array is not zero-index.
        /// </item>
        /// </list>
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="iterable"/> or <paramref name="array"/> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="arrayIndex" /> is less than zero.
        /// </exception>
        public static void CopyTo<T>(this IEnumerable<T> iterable, T[] array, int arrayIndex)
        {
            if(iterable == null)
                throw new ArgumentNullException("iterable");
            if(array == null)
                throw new ArgumentNullException("array");
            if(array.GetLowerBound(0) != 0)
                throw new ArgumentException("Array must be zero-indexed.");
            if(arrayIndex <= 0)
                throw new ArgumentException("Array index must be at least zero.");
            if(iterable.Count() + arrayIndex >= array.Length)
                throw new ArgumentException("There is not enough space in the array to perform the copy operation.");
            ForEach(iterable, arrayIndex, (value, index) =>
                {
                    array[index] = value;
                });
        }

        /// <summary>
        /// Generate a numbered sequence from this iterable.
        /// </summary>
        /// <param name="iterable">
        /// The iterable over which to enumerate.
        /// </param>
        public static IEnumerable<NumberedEntry<T>> Enumerate<T>(this IEnumerable<T> iterable)
        {
            return Enumerate(iterable, 0);
        }

        /// <summary>
        /// Generate a numbered sequence from this iterable.
        /// </summary>
        /// <param name="iterable">
        /// The iterable over which to enumerate.
        /// </param>
        /// <param name="startIndex">
        /// The value to use for the index of the first item in the sequence.
        /// </param>
        public static IEnumerable<NumberedEntry<T>> Enumerate<T>(this IEnumerable<T> iterable, int startIndex)
        {
            if(iterable == null)
                throw new ArgumentNullException("iterable");
            int index = startIndex;
            foreach(T value in iterable)
            {
                yield return NumberedEntry.Create(value, index);
                index++;
            }
        }

        /// <summary>
        /// Apply an action to each entry in this iterable.
        /// </summary>
        /// <param name="iterable">
        /// The sequence to which to apply the actions.
        /// </param>
        /// <param name="action">
        /// The action to apply.  It should take an argument of type <typeparamref name="T" />
        /// corresponding to the current value of the sequence and an <see cref="int" />
        /// corresponding to the index of the value in the sequence.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="iterable"/> or <paramref name="action"/>
        /// are <see langword="null" />.
        /// </exception>
        public static void ForEach<T>(this IEnumerable<T> iterable, Action<T, int> action)
        {
            ForEach(iterable, 0, action);
        }

        /// <summary>
        /// Apply an action to each entry in this iterable.
        /// </summary>
        /// <param name="iterable">
        /// The sequence to which to apply the actions.
        /// </param>
        /// <param name="action">
        /// The action to apply.  It should take an argument of type <typeparamref name="T" />
        /// corresponding to the current value of the sequence and an <see cref="int" />
        /// corresponding to the index of the value in the sequence.
        /// </param>
        /// <param name="startIndex">
        /// The value to use for the index of the first item in the sequence.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="iterable"/> or <paramref name="action"/>
        /// are <see langword="null" />.
        /// </exception>
        public static void ForEach<T>(this IEnumerable<T> iterable, int startIndex, Action<T, int> action)
        {
            if(action == null)
                throw new ArgumentNullException("action");
            Enumerate(iterable, startIndex).ForEach(entry => action(entry.Value, entry.Index));
        }
    }
}

