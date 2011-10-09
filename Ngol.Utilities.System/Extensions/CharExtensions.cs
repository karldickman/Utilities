using System;
using System.Collections.Generic;
using System.Linq;

namespace Ngol.Utilities.System.Extensions
{
    /// <summary>
    /// Useful extension methods on <see cref="char" />.
    /// </summary>
    public static class CharExtensions
    {
        /// <summary>
        /// Similar to string.Join() but allows any kind
        /// of enumerable.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="sequence"/> is <see langword="null" />.
        /// </exception>
        public static string Join<T>(this char joinWith, IEnumerable<T> sequence)
        {
            if(sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }
            return string.Join(joinWith.ToString(), sequence.Select(item => item.ToString()).ToArray());
        }
    }
}

