using System;
using System.Collections.Generic;
using System.Linq;

namespace Ngol.Utilities.System.Extensions
{
    /// <summary>
    /// Useful extension methods on <see cref="string" />.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Similar to string.Join() but allows any kind
        /// of enumerable.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any argument is <see langword="null" />.
        /// </exception>
        public static string Join<T>(this string joinWith, IEnumerable<T> sequence)
        {
            if(joinWith == null)
                throw new ArgumentNullException("joinWith");
            if(sequence == null)
                throw new ArgumentNullException("sequence");
            return string.Join(joinWith, sequence.Select(item => item.ToString()).ToArray());
        }
    }
}

