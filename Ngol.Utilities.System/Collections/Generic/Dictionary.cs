using System;
using System.Collections;
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
    public class Dictionary<TKey1, TKey2, TValue> : IDictionary<TKey1, TKey2, TValue>
    {
        /// <summary>
        /// The basic dictionary to which all calls are delegated.
        /// </summary>
        protected readonly IDictionary<Tuple<TKey1, TKey2>, TValue> InnerDictionary;

        /// <summary>
        /// Initializes a new dictionary that is empty, has the default
        /// initial capacity, and uses the default equality comparer
        /// for the key.
        /// </summary>
        public Dictionary()
        {
            InnerDictionary = new Dictionary<Tuple<TKey1, TKey2>, TValue>();
        }

        /// <summary>
        /// Initializes a new dictionary that is empty, has the specified initial capacity,
        /// and uses the default equality comparer
        /// for the key type.
        /// </summary>
        /// <param name="size">
        /// The initial number of elements the dictionary can contain.
        /// </param>
        public Dictionary(int size)
        {
            InnerDictionary = new Dictionary<Tuple<TKey1, TKey2>, TValue>(size);
        }

        #region IDictionary[TKey1,TKey2,TValue] implementation

        /// <inheritdoc />
        public void Add(TKey1 key1, TKey2 key2, TValue value)
        {
            InnerDictionary.Add(Tuple.Create(key1, key2), value);
        }

        /// <inheritdoc />
        public bool ContainsKey(TKey1 key1, TKey2 key2)
        {
            return InnerDictionary.ContainsKey(Tuple.Create(key1, key2));
        }

        /// <inheritdoc />
        public bool Remove(TKey1 key1, TKey2 key2)
        {
            return InnerDictionary.Remove(Tuple.Create(key1, key2));
        }

        /// <inheritdoc />
        public bool TryGetValue(TKey1 key1, TKey2 key2, out TValue value)
        {
            return InnerDictionary.TryGetValue(Tuple.Create(key1, key2), out value);
        }

        /// <inheritdoc />
        public TValue this[TKey1 key1, TKey2 key2]
        {
            get { return InnerDictionary[Tuple.Create(key1, key2)]; }

            set { InnerDictionary[Tuple.Create(key1, key2)] = value; }
        }

        #region IDictionary[Tuple[TKey1,TKey2],TValue] implementation

        /// <inheritdoc />
        public void Add(Tuple<TKey1, TKey2> key, TValue value)
        {
            InnerDictionary.Add(key, value);
        }

        /// <inheritdoc />
        public bool ContainsKey(Tuple<TKey1, TKey2> key)
        {
            return InnerDictionary.ContainsKey(key);
        }

        /// <inheritdoc />
        public bool Remove(Tuple<TKey1, TKey2> key)
        {
            return InnerDictionary.Remove(key);
        }

        /// <inheritdoc />
        public bool TryGetValue(Tuple<TKey1, TKey2> key, out TValue value)
        {
            return InnerDictionary.TryGetValue(key, out value);
        }

        /// <inheritdoc />
        public TValue this[Tuple<TKey1, TKey2> key]
        {
            get { return InnerDictionary[key]; }

            set { InnerDictionary[key] = value; }
        }

        /// <inheritdoc />
        public ICollection<Tuple<TKey1, TKey2>> Keys
        {
            get { return InnerDictionary.Keys; }
        }

        /// <inheritdoc />
        public ICollection<TValue> Values
        {
            get { return InnerDictionary.Values; }
        }

        #region ICollection[KeyValuePair[Tuple[TKey1,TKey2],TValue]] implementation

        /// <inheritdoc />
        public int Count
        {
            get { return InnerDictionary.Count; }
        }

        /// <inheritdoc />
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <inheritdoc />
        public void Add(KeyValuePair<Tuple<TKey1, TKey2>, TValue> entry)
        {
            InnerDictionary.Add(entry);
        }

        /// <inheritdoc />
        public void Clear()
        {
            InnerDictionary.Clear();
        }

        /// <inheritdoc />
        public bool Contains(KeyValuePair<Tuple<TKey1, TKey2>, TValue> entry)
        {
            return InnerDictionary.Contains(entry);
        }

        /// <inheritdoc />
        public void CopyTo(KeyValuePair<Tuple<TKey1, TKey2>, TValue>[] array, int arrayIndex)
        {
            InnerDictionary.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc />
        public bool Remove(KeyValuePair<Tuple<TKey1, TKey2>, TValue> entry)
        {
            return InnerDictionary.Remove(entry);
        }

        #region IEnumerable[KeyValuePair[Tuple[TKey1,TKey2],TValue]] implementation

        /// <inheritdoc />
        public IEnumerator<KeyValuePair<Tuple<TKey1, TKey2>, TValue>> GetEnumerator()
        {
            return InnerDictionary.GetEnumerator();
        }

        #region IEnumerable implmentation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        #endregion
        
        #endregion
        
        #endregion
        
        #endregion
        
        #endregion
    }
}
