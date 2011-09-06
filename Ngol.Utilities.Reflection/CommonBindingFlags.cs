using System;
using System.Reflection;

namespace Ngol.Utilities.Reflection
{
    /// <summary>
    /// Useful common combinations of <see cref="BindingFlags" />.
    /// </summary>
    internal static class CommonBindingFlags
    {
        /// <summary>
        /// The <see cref="BindingFlags" /> used to find an instance method.
        /// </summary>
        internal static readonly BindingFlags InstanceFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        /// <summary>
        /// The <see cref="BindingFlags" /> used to find a static method.
        /// </summary>
        internal static readonly BindingFlags StaticFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
    }
}

