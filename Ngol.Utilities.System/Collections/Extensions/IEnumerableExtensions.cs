using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MoreLinq;
using Ngol.Utilities.System;

namespace Ngol.Utilities.Collections.Extensions
{
    /// <summary>
    /// Useful extensions on IEnumerable and IEnumerable&lt;T&gt;.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Determines whether all values in a sequence of booleans are true.
        /// </summary>
        /// <param name="booleans">
        /// A sequence of <see cref="bool" />s.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if every single one of the <paramref name="booleans"/> is <see langword="false" />;
        /// <see langword="false" /> if any of the <paramref name="booleans"/> is <see langword="false" />.
        /// </returns>
        public static bool All(this IEnumerable<bool> booleans)
        {
            return Enumerable.All(booleans, boolean => boolean);
        }

        /// <summary>
        /// Determines whether any values in a sequence of booleans are true.
        /// </summary>
        /// <param name="booleans">
        /// A sequence of <see cref="bool" />s.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if any values were <see langword="true" />;
        /// <see langword="false" /> if every single value was false.
        /// </returns>
        public static bool Any(this IEnumerable<bool> booleans)
        {
            return Enumerable.Any(booleans, boolean => boolean);
        }

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
            {
                throw new ArgumentNullException("iterable");
            }
            if(array == null)
            {
                throw new ArgumentNullException("array");
            }
            if(array.GetLowerBound(0) != 0)
            {
                throw new ArgumentException("Array must be zero-indexed.");
            }
            if(arrayIndex < 0)
            {
                throw new ArgumentException("Array index must be at least zero.");
            }
            if(iterable.Count() + arrayIndex > array.Length)
            {
                throw new ArgumentException("There is not enough space in the array to perform the copy operation.");
            }
            ForEachIndexed(iterable, arrayIndex, (value, index) =>
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
            {
                throw new ArgumentNullException("iterable");
            }
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
        public static void ForEachIndexed<T>(this IEnumerable<T> iterable, Action<T, int> action)
        {
            ForEachIndexed(iterable, 0, action);
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
        public static void ForEachIndexed<T>(this IEnumerable<T> iterable, int startIndex, Action<T, int> action)
        {
            if(action == null)
            {
                throw new ArgumentNullException("action");
            }
            Enumerate(iterable, startIndex).ForEach(entry => action(entry.Value, entry.Index));
        }

        /// <summary>
        /// Apply an action to each element in an iterable.
        /// </summary>
        /// <param name="iterable">
        /// The iterable to which to apply the actions.
        /// </param>
        /// <param name="action">
        /// The action to apply.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="iterable"/> or <paramref name="action"/> is <see langword="null" />.
        /// </exception>
        public static void ForEach<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> iterable, Action<TKey, TValue> action)
        {
            if(action == null)
            {
                throw new ArgumentNullException("action");
            }
            Action<KeyValuePair<TKey, TValue >> actualAction = keyValuePair => action(keyValuePair.Key, keyValuePair.Value);
            MoreEnumerable.ForEach(iterable, actualAction);
        }

        /// <summary>
        /// Apply an action to each element in an iterable, keeping track of the index within the sequence.
        /// </summary>
        /// <param name="iterable">
        /// The iterable to which to apply the actions.
        /// </param>
        /// <param name="action">
        /// The action to apply.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="iterable"/> or <paramref name="action"/> is <see langword="null" />.
        /// </exception>
        public static void ForEachIndexed<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> iterable, Action<TKey, TValue, int> action)
        {
            ForEachIndexed(iterable, action);
        }

        /// <summary>
        /// Apply an action to each element in an iterable, keeping track of the index within the sequence.
        /// </summary>
        /// <param name="iterable">
        /// The iterable to which to apply the actions.
        /// </param>
        /// <param name="startIndex">
        /// The value at which to start the indexed.
        /// </param>
        /// <param name="action">
        /// The action to apply.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="iterable"/> or <paramref name="action"/> is <see langword="null" />.
        /// </exception>
        public static void ForEach<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> iterable, int startIndex, Action<TKey, TValue, int> action)
        {
            if(action == null)
            {
                throw new ArgumentNullException("action");
            }
            Action<KeyValuePair<TKey, TValue>, int > actualAction = (keyValuePair, index) => action(keyValuePair.Key, keyValuePair.Value, index);
            ForEachIndexed(iterable, startIndex, actualAction);
        }

        /// <summary>
        /// Apply an action to every sequential pair of this iterable.
        /// </summary>
        /// <param name="iterable">
        /// The iterable on which to apply the action.
        /// </param>
        /// <param name="action">
        /// The action to apply.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="iterable"/> or <paramref name="action"/>
        /// is <see langword="null" />.
        /// </exception>
        public static void ForEachPair<T>(this IEnumerable<T> iterable, Action<T, T> action)
        {
            ForEachIndexedPair(iterable, (a, b, i) => action(a, b));
        }

        /// <summary>
        /// Apply an action to every sequential pair of this iterable, with an index to keep
        /// track of where the pair is in the sequence.
        /// </summary>
        /// <param name="iterable">
        /// The iterable on which to apply the action.
        /// </param>
        /// <param name="action">
        /// The action to apply.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="iterable"/> or <paramref name="action"/>
        /// is <see langword="null" />.
        /// </exception>
        public static void ForEachIndexedPair<T>(this IEnumerable<T> iterable, Action<T, T, int> action)
        {
            ForEachIndexedPair(iterable, 0, action);
        }

        /// <summary>
        /// Apply an action to every sequential pair of this iterable, with an index to keep
        /// track of where the pair is in the sequence.
        /// </summary>
        /// <param name="iterable">
        /// The iterable on which to apply the action.
        /// </param>
        /// <param name="startIndex">
        /// The value to which to initialize the index.
        /// </param>
        /// <param name="action">
        /// The action to apply.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="iterable"/> or <paramref name="action"/>
        /// is <see langword="null" />.
        /// </exception>
        public static void ForEachIndexedPair<T>(this IEnumerable<T> iterable, int startIndex, Action<T, T, int> action)
        {
            Func<T, T, Tuple<T, T >> selector = (a, b) => Tuple.Create(a, b);
            IEnumerable<Tuple<T, T >> pairs = Pairs(iterable, selector);
            Action<Tuple<T, T>, int > actualAction = (tuple, index) => action(tuple.Item1, tuple.Item2, index);
            ForEachIndexed(pairs, startIndex, actualAction);
        }

        /// <summary>
        /// Loop over two sequences at once and apply an action to each corresponding entry in the sequence.
        /// </summary>
        /// <remarks>
        /// If the input sequences are of different lengths, the result sequence is terminated
        /// as soon as the shortest input sequence is exhausted. This operator uses deferred
        /// execution and streams its results.
        /// </remarks>
        /// <param name="first">
        /// The first sequence.
        /// </param>
        /// <param name="second">
        /// The second sequence.
        /// </param>
        /// <param name="action">
        /// The action to apply to each element in the sequence.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any argument is <see langword="null" />.
        /// </exception>
        public static void ForEach<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second, Action<T1, T2> action)
        {
            if(first == null)
            {
                throw new ArgumentNullException("first");
            }
            if(second == null)
            {
                throw new ArgumentNullException("second");
            }
            if(action == null)
            {
                throw new ArgumentNullException("action");
            }
            first.Zip(second, Tuple.Create<T1, T2>)
                 .ForEach(pair => action(pair.Item1, pair.Item2));
        }

        /// <summary>
        /// Loop over two sequences at once and apply an action to each corresponding entry in the sequence.
        /// </summary>
        /// <remarks>
        /// If the input sequences are of different lengths, then <see cref="InvalidOperationException" />
        /// is thrown if they do not terminate at the same time.  This operator uses deferred execution
        /// and streams its results.
        /// </remarks>
        /// <param name="first">
        /// The first sequence.
        /// </param>
        /// <param name="second">
        /// The second sequence.
        /// </param>
        /// <param name="action">
        /// The action to apply to each element in the sequence.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any argument is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the end of one sequence is encountered before the end of the others.
        /// </exception>
        public static void ForEachEqual<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second, Action<T1, T2> action)
        {
            MoreEnumerable.ForEach(MoreEnumerable.EquiZip<T1, T2, Tuple<T1, T2>>(first, second, Tuple.Create<T1, T2>), pair => action(pair.Item1, pair.Item2));
        }

        /// <summary>
        /// Loop over two sequences at once and apply an action to each corresponding entry in the sequence.
        /// </summary>
        /// <remarks>
        /// If the input sequences are of different lengths, then <see cref="InvalidOperationException" />
        /// is thrown if they do not terminate at the same time.  This operator uses deferred execution
        /// and streams its results.
        /// </remarks>
        /// <param name="first">
        /// The first sequence.
        /// </param>
        /// <param name="second">
        /// The second sequence.
        /// </param>
        /// <param name="action">
        /// The action to apply to each element in the sequence.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any argument is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the end of one sequence is encountered before the end of the others.
        /// </exception>
        public static void ForEachEqualIndexed<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second, Action<T1, T2, int> action)
        {
            if(first == null)
            {
                throw new ArgumentNullException("first");
            }
            if(second == null)
            {
                throw new ArgumentNullException("second");
            }
            if(action == null)
            {
                throw new ArgumentNullException("action");
            }
            first.ForEachEqualIndexed(second, 0, action);
        }

        /// <summary>
        /// Loop over two sequences at once and apply an action to each corresponding entry in the sequence.
        /// </summary>
        /// <remarks>
        /// If the input sequences are of different lengths, then <see cref="InvalidOperationException" />
        /// is thrown if they do not terminate at the same time.  This operator uses deferred execution
        /// and streams its results.
        /// </remarks>
        /// <param name="first">
        /// The first sequence.
        /// </param>
        /// <param name="second">
        /// The second sequence.
        /// </param>
        /// <param name="startIndex">
        /// The value at which to start the indexes provided to the action.
        /// </param>
        /// <param name="action">
        /// The action to apply to each element in the sequence.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any argument is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the end of one sequence is encountered before the end of the others.
        /// </exception>
        public static void ForEachEqualIndexed<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second, int startIndex, Action<T1, T2, int> action)
        {
            if(first == null)
            {
                throw new ArgumentNullException("first");
            }
            if(second == null)
            {
                throw new ArgumentNullException("second");
            }
            if(action == null)
            {
                throw new ArgumentNullException("action");
            }
            first.EquiZip<T1, T2, Tuple<T1, T2>>(second, Tuple.Create<T1, T2>)
                 .ForEachIndexed(startIndex, (entry, index) => action(entry.Item1, entry.Item2, index));
        }

        /// <summary>
        /// Loop over two sequences at once and apply an action to each corresponding entry in the sequence.
        /// </summary>
        /// <remarks>
        /// If the input sequences are of different lengths, the result sequence will always be as long as the longest
        /// input sequence.  The default value of the shorter sequence value type is used to
        /// pad the other sequences.  This operator uses deferred execution and streams its results.
        /// </remarks>
        /// <param name="first">
        /// The first sequence.
        /// </param>
        /// <param name="second">
        /// The second sequence.
        /// </param>
        /// <param name="action">
        /// The action to apply to each element in the sequence.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any argument is <see langword="null" />.
        /// </exception>
        public static void ForEachLongest<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second, Action<T1, T2> action)
        {
            MoreEnumerable.ForEach(MoreEnumerable.ZipLongest<T1, T2, Tuple<T1, T2>>(first, second, Tuple.Create<T1, T2>), pair => action(pair.Item1, pair.Item2));
        }

        /// <summary>
        /// Loop over three sequences at once and apply an action to each corresponding entry in the sequence.
        /// </summary>
        /// <remarks>
        /// If the input sequences are of different lengths, the result sequence is terminated
        /// as soon as the shortest input sequence is exhausted. This operator uses deferred
        /// execution and streams its results.
        /// </remarks>
        /// <param name="first">
        /// The first sequence.
        /// </param>
        /// <param name="second">
        /// The second sequence.
        /// </param>
        /// <param name="third">
        /// The third sequence.
        /// </param>
        /// <param name="action">
        /// The action to apply to each element in the sequence.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any argument is <see langword="null" />.
        /// </exception>
        public static void ForEach<T1, T2, T3>(this IEnumerable<T1> first, IEnumerable<T2> second, IEnumerable<T3> third, Action<T1, T2, T3> action)
        {
            MoreEnumerable.ForEach(Zip<T1, T2, T3, Tuple<T1, T2, T3>>(first, second, third, Tuple.Create<T1, T2, T3>), triple => action(triple.Item1, triple.Item2, triple.Item3));
        }

        /// <summary>
        /// Loop over three sequences at once and apply an action to each corresponding entry in the sequence.
        /// </summary>
        /// <remarks>
        /// If the input sequences are of different lengths, then <see cref="InvalidOperationException" />
        /// is thrown if they do not terminate at the same time.  This operator uses deferred execution
        /// and streams its results.
        /// </remarks>
        /// <param name="first">
        /// The first sequence.
        /// </param>
        /// <param name="second">
        /// The second sequence.
        /// </param>
        /// <param name="third">
        /// The third sequence.
        /// </param>
        /// <param name="action">
        /// The action to apply to each element in the sequence.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any argument is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the end of one sequence is encountered before the end of the others.
        /// </exception>
        public static void ForEachEqual<T1, T2, T3>(this IEnumerable<T1> first, IEnumerable<T2> second, IEnumerable<T3> third, Action<T1, T2, T3> action)
        {
            MoreEnumerable.ForEach(EquiZip<T1, T2, T3, Tuple<T1, T2, T3>>(first, second, third, Tuple.Create<T1, T2, T3>), triple => action(triple.Item1, triple.Item2, triple.Item3));
        }

        /// <summary>
        /// Loop over three sequences at once and apply an action to each corresponding entry in the sequence.
        /// </summary>
        /// <remarks>
        /// If the input sequences are of different lengths, the result sequence will always be as long as the longest
        /// input sequence.  The default value of the shorter sequence value type is used to
        /// pad the other sequences.  This operator uses deferred execution and streams its results.
        /// </remarks>
        /// <param name="first">
        /// The first sequence.
        /// </param>
        /// <param name="second">
        /// The second sequence.
        /// </param>
        /// <param name="third">
        /// The third sequence.
        /// </param>
        /// <param name="action">
        /// The action to apply to each element in the sequence.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any argument is <see langword="null" />.
        /// </exception>
        public static void ForEachLongest<T1, T2, T3>(this IEnumerable<T1> first, IEnumerable<T2> second, IEnumerable<T3> third, Action<T1, T2, T3> action)
        {
            MoreEnumerable.ForEach(ZipLongest<T1, T2, T3, Tuple<T1, T2, T3>>(first, second, third, Tuple.Create<T1, T2, T3>), triple => action(triple.Item1, triple.Item2, triple.Item3));
        }

        /// <summary>
        /// Check whether the sequence is empty.
        /// </summary>
        /// <param name="iterable">
        /// The sequence to check.
        /// </param>
        /// <returns>
        /// A <see cref="System.Boolean"/>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="iterable"/> is <see langword="null" />.
        /// </exception>
        public static bool IsEmpty<T>(this IEnumerable<T> iterable)
        {
            if(iterable == null)
            {
                throw new ArgumentNullException("iterable");
            }
#pragma warning disable 0219
            foreach(T value in iterable)
            {
                return false;
            }
#pragma warning restore 0219
            return true;
        }

        /// <summary>
        /// Invoke a transformation function on each element of a <paramref name="sequence"/>
        /// and return the maximum <see cref="uint" /> from that transformation.
        /// </summary>
        /// <param name="sequence">
        /// A sequence of values whose maximum transformation is to be
        /// determined.
        /// </param>
        /// <param name="selector">
        /// A transformation function to apply to each element of the <paramref name="sequence"/>.
        /// </param>
        /// <returns>
        /// The maximum value in the transformed sequence, or if the original <paramref name="sequence"/>
        /// was empty, 0.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="sequence"/> or <paramref name="selector"/> is <see langword="null" /> .
        /// </exception>
        public static uint MaxOrIdentity<T>(this IEnumerable<T> sequence, Func<T, uint> selector)
        {
            if(sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }
            if(selector == null)
            {
                throw new ArgumentNullException("selector");
            }
            if(sequence.Count() == 0)
            {
                return 0;
            }
            return Enumerable.Max(sequence, selector);
        }

        /// <summary>
        /// Determine whether all of the values of the sequence are false.
        /// </summary>
        /// <param name="booleans">
        /// The booleans to inspect.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if every single one of the <paramref name="booleans"/> is <see langword="false" />.
        /// <see langword="false" /> if any of the the <paramref name="booleans"/> is <see langword="true" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="booleans"/> is <see langword="null" />.
        /// </exception>
        public static bool None(this IEnumerable<bool> booleans)
        {
            return Enumerable.All(booleans, boolean => !boolean);
        }

        /// <summary>
        /// Determines whether none of the values in the sequence satisfy the predicate.
        /// </summary>
        /// <param name="iterable">
        /// The sequence whose values to test.
        /// </param>
        /// <param name="predicate">
        /// The predicate against which to test the values in the sequence.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if the predicate was <see langword="false" /> for every value in the <paramref name="iterable" />.
        /// <see langword="false" /> if the predicate was <see langword="true" /> true for any value in the <paramref name="iterable"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="iterable"/> or <paramref name="predicate"/> is <see langword="null" />.
        /// </exception>
        public static bool None<TSource>(this IEnumerable<TSource> iterable, Predicate<TSource> predicate)
        {
            return !Enumerable.Any(iterable, new Func<TSource, bool>(predicate));
        }

        /// <summary>
        /// Projects each element of a seqquence into a new form by incorporating the element's index
        /// within the sequence.
        /// </summary>
        /// <param name="source">
        /// A sequence of values on which to invoke a transform function.
        /// </param>
        /// <param name="selector">
        /// A transform function to apply to each source element; the second parameter of the function represents the index
        /// of the source element.
        /// </param>
        /// <param name="startIndex">
        /// The value at which to initialize the indexes that are passed to the <paramref name="selector"/>.
        /// </param>
        /// <returns>
        /// An IEnumerable&lt;<typeparamref name="TResult" /> whose elements are the result of invoking the transform function
        /// on each element of <paramref name="source"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> or <paramref name="selector"/> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, int startIndex, Func<TSource, int, TResult> selector)
        {
            if(source == null)
            {
                throw new ArgumentNullException("iterable");
            }
            if(selector == null)
            {
                throw new ArgumentNullException("selector");
            }
            return source.Select((entry, index) => selector(entry, index + startIndex));
        }

        /// <summary>
        /// Projects each elment of a sequence to an IEnumerable&gt;T&lt;, and
        /// flattens the resulting sequences into one sequence.
        /// The index of each source element is used in the projected form of that element.
        /// </summary>
        /// <param name="source">
        /// A sequence of values to project.
        /// </param>
        /// <param name="selector">
        /// A transform function to apply to each source elment; the second parameter of the function represents
        /// the index of the source element.
        /// </param>
        /// <returns>
        /// An IEnumerable&gt;T&lt; whose elements are the result of invoking the one-to-many transform function on each element of an input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> or <paramref name="selector"/> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TResult>> selector)
        {
            if(source == null)
            {
                throw new ArgumentNullException("iterable");
            }
            if(selector == null)
            {
                throw new ArgumentNullException("selector");
            }
            return source.SelectMany(0, selector);
        }

        /// <summary>
        /// Projects each elment of a sequence to an IEnumerable&gt;T&lt;, and
        /// flattens the resulting sequences into one sequence.
        /// The index of each source element is used in the projected form of that element.
        /// </summary>
        /// <param name="source">
        /// A sequence of values to project.
        /// </param>
        /// <param name="startIndex">
        /// The value at which to initialize the indexes of the source elements.
        /// </param>
        /// <param name="selector">
        /// A transform function to apply to each source elment; the second parameter of the function represents
        /// the index of the source element.
        /// </param>
        /// <returns>
        /// An IEnumerable&gt;T&lt; whose elements are the result of invoking the one-to-many transform function on each element of an input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="source"/> or <paramref name="selector"/> is <see langword="null" />.
        /// </exception>
        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, int startIndex, Func<TSource, int, IEnumerable<TResult>> selector)
        {
            if(source == null)
            {
                throw new ArgumentNullException("iterable");
            }
            if(selector == null)
            {
                throw new ArgumentNullException("selector");
            }
            return source.Enumerate(startIndex).SelectMany(entry => selector(entry.Value, entry.Index));
        }

        /// <summary>
        /// Returns the elements of the <paramref name="iterable"/> sorted using the default comparer.
        /// </summary>
        /// <param name="iterable">
        /// The iterable to sort.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="iterable"/> is <see langword="null" />.
        /// </exception>
        public static IOrderedEnumerable<T> Sorted<T>(this IEnumerable<T> iterable) where T : IComparable<T>
        {
            if(iterable == null)
            {
                throw new ArgumentNullException("iterable");
            }
            return iterable.OrderBy<T, T>(item => item);
        }

        /// <summary>
        /// Returns the elements of the <paramref name="iterable"/> sorted using the default comparer.
        /// </summary>
        /// <param name="iterable">
        /// The iterable to sort.
        /// </param>
        /// <param name="comparer">
        /// The IComparer&gt;T&lt; to use.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if
        /// <list type="bullet">
        /// <item><paramref name="iterable"/> is <see langword="null" />.</item>
        /// <item><paramref name="comparer"/> is <see langword="null" />
        /// and the default comparer cannot find an implementation of the IComparable&gt;T&lt;
        /// generic interface or the <see cref="IComparable" /> interface for type
        /// <typeparamref name="T" />.</item>
        /// </list>
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the <paramref name="comparer"/> caused an error during the sort.  For
        /// example, <paramref name="comparer"/> might not return 0 when comparing an item
        /// with itself.
        /// </exception>
        public static IOrderedEnumerable<T> Sorted<T>(this IEnumerable<T> iterable, IComparer<T> comparer)
        {
            if(iterable == null)
            {
                throw new ArgumentNullException("iterable");
            }
            return iterable.OrderBy<T, T>(item => item, comparer);
        }

        /// <summary>
        /// Take every consecutive pair of values in a sequence
        /// and apply a selector function to them, generating a new sequence.
        /// </summary>
        /// <example>
        /// <code>
        /// IEnumerable&gt;int&lt; numbers = new List&gt;int&lt; { -1, 0, 1, 1, 2, 3, 5, 8, 13, };
        /// Console.WriteLine(string.Join(", ", numbers.PairWise((a, b) =&lt; a + b)));
        /// </code>
        /// This will result in the following output: 0, 1, 1, 2, 3, 5, 8, 13, 21, 34.
        /// </example>
        /// <param name="iterable">
        /// The iterable to which to apply pairwise operations.
        /// </param>
        /// <param name="selector">
        /// The selector function used to convert the pairs into output values.
        /// </param>
        /// <returns>
        /// An empty iterable if there are less than two items in the <paramref name="iterable"/>.
        /// </returns>
        public static IEnumerable<TOut> Pairs<TIn, TOut>(this IEnumerable<TIn> iterable, Func<TIn, TIn, TOut> selector)
        {
            if(iterable == null)
            {
                throw new ArgumentNullException("iterable");
            }
            if(selector == null)
            {
                throw new ArgumentNullException("selector");
            }
            return iterable.Zip(iterable.Skip(1), selector);
        }

        /// <summary>
        /// Returns a projection of 3-tuples, where each tuple contains the nth element from each of the arguemnt sequences.
        /// </summary>
        /// <remarks>
        /// If the input sequences are of different lengths, the result sequence is terminated
        /// as soon as the shortest input sequence is exhausted. This operator uses deferred
        /// execution and streams its results.
        /// </remarks>
        /// <typeparam name="TIn1">
        /// The type of values of the first sequence.
        /// </typeparam>
        /// <typeparam name="TIn2">
        /// The type of values of the first sequence.
        /// </typeparam>
        /// <typeparam name="TIn3">
        /// The type of values of the first sequence.
        /// </typeparam>
        /// <typeparam name="TOut">
        /// The type of values in the result sequence.
        /// </typeparam>
        /// <param name="first">
        /// First sequence.
        /// </param>
        /// <param name="second">
        /// Second sequence.
        /// </param>
        /// <param name="third">
        /// Third sequence.
        /// </param>
        /// <param name="selector">
        /// Function to select each tuple of elements.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any argument is <see langword="null" />.
        /// </exception>
        public static IEnumerable<TOut> Zip<TIn1, TIn2, TIn3, TOut>(this IEnumerable<TIn1> first, IEnumerable<TIn2> second, IEnumerable<TIn3> third, Func<TIn1, TIn2, TIn3, TOut> selector)
        {
            if(first == null)
            {
                throw new ArgumentNullException("first");
            }
            if(second == null)
            {
                throw new ArgumentNullException("second");
            }
            if(third == null)
            {
                throw new ArgumentNullException("third");
            }
            if(selector == null)
            {
                throw new ArgumentNullException("selector");
            }
            return first.Zip(second, Tuple.Create<TIn1, TIn2>)
                        .Zip(third, (tuple, value3) => selector(tuple.Item1, tuple.Item2, value3));
        }


        /// <summary>
        /// Returns a projection of 3-tuples, where each tuple contains the nth element from each of the arguemnt sequences.
        /// </summary>
        /// <remarks>
        /// If the input sequences are of different lengths, then <see cref="InvalidOperationException" />
        /// is thrown if they do not terminate at the same time.  This operator uses deferred execution
        /// and streams its results.
        /// </remarks>
        /// <typeparam name="TIn1">
        /// The type of values of the first sequence.
        /// </typeparam>
        /// <typeparam name="TIn2">
        /// The type of values of the first sequence.
        /// </typeparam>
        /// <typeparam name="TIn3">
        /// The type of values of the first sequence.
        /// </typeparam>
        /// <typeparam name="TOut">
        /// The type of values in the result sequence.
        /// </typeparam>
        /// <param name="first">
        /// First sequence.
        /// </param>
        /// <param name="second">
        /// Second sequence.
        /// </param>
        /// <param name="third">
        /// Third sequence.
        /// </param>
        /// <param name="selector">
        /// Function to select each tuple of elements.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any argument is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the end of one sequence is reached before the end of the others.
        /// </exception>
        public static IEnumerable<TOut> EquiZip<TIn1, TIn2, TIn3, TOut>(this IEnumerable<TIn1> first, IEnumerable<TIn2> second, IEnumerable<TIn3> third, Func<TIn1, TIn2, TIn3, TOut> selector)
        {
            if(first == null)
            {
                throw new ArgumentNullException("first");
            }
            if(second == null)
            {
                throw new ArgumentNullException("second");
            }
            if(third == null)
            {
                throw new ArgumentNullException("third");
            }
            if(selector == null)
            {
                throw new ArgumentNullException("selector");
            }
            return first.EquiZip(second, Tuple.Create<TIn1, TIn2>)
                        .EquiZip(third, (tuple, value3) => selector(tuple.Item1, tuple.Item2, value3));
        }

        /// <summary>
        /// Returns a projection of 3-tuples, where each tuple contains the nth element from each of the arguemnt sequences.
        /// </summary>
        /// <remarks>
        /// If the input sequences are of different lengths, the result sequence will always be as long as the longest
        /// input sequence.  The default value of the shorter sequence value type is used to
        /// pad the other sequences.  This operator uses deferred execution and streams its results.
        /// </remarks>
        /// <typeparam name="TIn1">
        /// The type of values of the first sequence.
        /// </typeparam>
        /// <typeparam name="TIn2">
        /// The type of values of the first sequence.
        /// </typeparam>
        /// <typeparam name="TIn3">
        /// The type of values of the first sequence.
        /// </typeparam>
        /// <typeparam name="TOut">
        /// The type of values in the result sequence.
        /// </typeparam>
        /// <param name="first">
        /// First sequence.
        /// </param>
        /// <param name="second">
        /// Second sequence.
        /// </param>
        /// <param name="third">
        /// Third sequence.
        /// </param>
        /// <param name="selector">
        /// Function to select each tuple of elements.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any argument is <see langword="null" />.
        /// </exception>
        public static IEnumerable<TOut> ZipLongest<TIn1, TIn2, TIn3, TOut>(this IEnumerable<TIn1> first, IEnumerable<TIn2> second, IEnumerable<TIn3> third, Func<TIn1, TIn2, TIn3, TOut> selector)
        {
            if(first == null)
            {
                throw new ArgumentNullException("first");
            }
            if(second == null)
            {
                throw new ArgumentNullException("second");
            }
            if(third == null)
            {
                throw new ArgumentNullException("third");
            }
            if(selector == null)
            {
                throw new ArgumentNullException("selector");
            }
            return first.ZipLongest(second, Tuple.Create<TIn1, TIn2>)
                        .ZipLongest(third, (tuple, value3) => selector(tuple.Item1, tuple.Item2, value3));
        }
    }
}
