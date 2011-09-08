using System;
using System.Collections.Generic;

namespace Ngol.Utilities.System.Collections.Generic
{
    /// <summary>
    /// Represents a generic collection of key/value pairs, where each key consists of two heterogeneous
    /// components.
    /// </summary>
    /// <typeparam name="TKey1">
    /// The type of the first component of the key.
    /// </typeparam>
    /// <typeparam name="TKey2">
    /// The type of the second component of the key.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// The type of the values.
    /// </typeparam>
    public interface IDictionary<TKey1, TKey2, TValue> : IDictionary<Tuple<TKey1, TKey2>, TValue>
    {
        /// <summary>
        /// Gets or sets the element with the specified key.
        /// </summary>
        /// <param name="key1">
        /// The first component of the key.
        /// </param>
        /// <param name="key2">
        /// The second component of the key.
        /// </param>
        /// <exception cref="KeyNotFoundException">
        /// Thrown if the property is retrieved and the key was not found.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// Thrown if the property is set and the dictionary is read-only.
        /// </exception>
        TValue this[TKey1 key1, TKey2 key2]
        {
            get;
            set;
        }

        /// <summary>
        /// Adds an alement with the specified key and value to the dictionary.
        /// </summary>
        /// <param name="key1">
        /// The first component of the key.
        /// </param>
        /// <param name="key2">
        /// The second component of the key.
        /// </param>
        /// <param name="value">
        /// The value to add.
        /// </param>
        /// <exception cref="ArgumentException">
        /// A value with the same key already exists in the dictionary.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// The dictionary is read-only.
        /// </exception>
        void Add(TKey1 key1, TKey2 key2, TValue value);

        /// <summary>
        /// Determines whether the dictionary contains a value with the specified key.
        /// </summary>
        /// <param name="key1">
        /// The first component of the key to locate.
        /// </param>
        /// <param name="key2">
        /// The second component of the key to locate.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if the dictionary contains a value with the key; otherwise <see langword="false" />
        /// </returns>
        bool ContainsKey(TKey1 key1, TKey2 key2);

        /// <summary>
        /// Removes the element with the specified key from the dictionary.
        /// </summary>
        /// <param name="key1">
        /// The first component of the key to remove.
        /// </param>
        /// <param name="key2">
        /// The second component of the key to remove.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if the key was successfully remove.
        /// <see langword="false" /> if an error occurred while removing the key,
        /// or if the key was not present.
        /// </returns>
        /// <exception cref="NotSupportedException">
        /// The dictionary is read-only.
        /// </exception>
        bool Remove(TKey1 key1, TKey2 key2);

        /// <summary>
        /// Get the value associated with the specified key.
        /// </summary>
        /// <param name="key1">
        /// The first component of the key whose value to get.
        /// </param>
        /// <param name="key2">
        /// The second component of the key whose value to get.
        /// </param>
        /// <param name="value">
        /// When the method returns, the variable passed to this parameter will have
        /// the value for the specified key, if the key is found.  Otherwise, the default value for
        /// <typeparamref name="TValue" /> will be provided.  This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        /// <see langword="true" /> if the dictionary contains an element
        /// with the specified key; otherwise <see langword="false" />.
        /// </returns>
        bool TryGetValue(TKey1 key1, TKey2 key2, out TValue value);
    }
}

