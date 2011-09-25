using System;

namespace Ngol.Utilities.System.Extensions
{
    /// <summary>
    /// Useful extensions on <see cref="IDisposable" />.
    /// </summary>
    public static class IDisposableExtensions
    {
        /// <summary>
        /// <see cref="IDisposable.Dispose" /> of an <see cref="IDisposable" /> without throwing a <see cref="NullReferenceException" />
        /// if the object is <see langword="null" />.
        /// </summary>
        /// <param name="disposable">
        /// The <see cref="IDisposable" /> of which to <see cref="IDisposable.Dispose" />.
        /// </param>
        public static void SafeDispose(this IDisposable disposable)
        {
            if(disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}

