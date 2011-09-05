using System;

namespace Ngol.Utilities.Collections
{
    /// <summary>
    /// Helper class for generating instances of NumberedEntry&lt;T&gt;.
    /// </summary>
    public static class NumberedEntry
    {
        /// <summary>
        /// Construct a new NumberedEntry&lt;T&gt;.
        /// </summary>
        /// <param name="value">
        /// The value of the entry.
        /// </param>
        /// <param name="index">
        /// The index of the entry.
        /// </param>
        /// <typeparam name="T">
        /// The type of the <paramref name="value"/> argument.
        /// </typeparam>
        public static NumberedEntry<T> Create<T>(T value, int index)
        {
            return new NumberedEntry<T>(value, index);
        }
    }

    /// <summary>
    /// Represents an entry in a sequence with an index notation.
    /// </summary>
    /// <typeparam name="T">
    /// The type of values in the sequence.
    /// </typeparam>
    public class NumberedEntry<T>
    {
        #region Properties

        /// <summary>
        /// The location of the entry in the sequence.
        /// </summary>
        public readonly int Index;

        /// <summary>
        /// The value of the entry.
        /// </summary>
        public readonly T Value;

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a new numbered entry.
        /// </summary>
        /// <param name="value">
        /// The value of the entry.
        /// </param>
        /// <param name="index">
        /// The index of the entry in its parent sequence.
        /// </param>
        public NumberedEntry(T value, int index)
        {
            Value = value;
            Index = index;
        }

        #endregion
    }
}

