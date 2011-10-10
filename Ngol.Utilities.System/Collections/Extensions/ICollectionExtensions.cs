using System;
using System.Collections.Generic;
using System.Linq;

namespace Ngol.Utilities.Collections.Extensions
{
    /// <summary>
    /// Useful extension methods for ICollection&lt;T&gt;.
    /// </summary>
    public static class ICollectionExtensions
    {
        /// <summary>
        /// Add a series of values to this collection.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the values contained in the collection.
        /// </typeparam>
        /// <param name="collection">
        /// The collection to which the values should be added.
        /// </param>
        /// <param name="values">
        /// The values to add to the collection.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="collection"/> or <paramref name="values"/>
        /// is <see langword="null" />.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// Thrown if this collection is read-only.
        /// </exception>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> values)
        {
            if(collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            if(values == null)
            {
                throw new ArgumentNullException("values");
            }
            foreach(T value in values)
            {
                collection.Add(value);
            }
        }

        /// <summary>
        /// Remove all properties from a <paramref name="collection"/> for which the specified
        /// <paramref name="predicate"/> is true.
        /// </summary>
        /// <param name="collection">
        /// The collection from which to remove items.
        /// </param>
        /// <param name="predicate">
        /// The predicate to use to test whether items should be removed.
        /// </param>
        /// <typeparam name="T">
        /// The type of the items in the <paramref name="collection"/>.
        /// </typeparam>
        public static bool Remove<T>(this ICollection<T> collection, Func<T, bool> predicate)
        {
            bool found = false;
            for(T match = collection.SingleOrDefault(predicate);
                match != null && !match.Equals(default(T));
                match = collection.SingleOrDefault(predicate))
            {
                collection.Remove(match);
                found = true;
            }
            return found;
        }
    }
}

