using System;

namespace Ngol.Utilities.System.Extensions
{
    /// <summary>
    /// Useful extension methods on <see cref="ICloneable" />.
    /// </summary>
    public static class ICloneableExtensions
    {
        /// <summary>
        /// Creates a new instance that is a copy of the current instance.
        /// </summary>
        /// <param name="instance">
        /// The instance to copy.
        /// </param>
        /// <typeparam name="T">
        /// The type of the instance to copy.
        /// </typeparam>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="instance"/> is <see langword="null" />.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// Thrown if <paramref name="instance"/> could not be cast to <typeparamref name="T" />.
        /// </exception>
        public static T Clone<T>(this ICloneable instance)
        {
            return (T)instance.Clone();
        }
    }
}

