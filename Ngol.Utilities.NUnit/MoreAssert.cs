using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Ngol.Utilities.NUnit
{
    /// <summary>
    /// Additional assertions for NUnit.
    /// </summary>
    public static class MoreAssert
    {
        /// <summary>
        /// Assert that a collection is empty.
        /// </summary>
        /// <param name="collection">
        /// The collection to test.
        /// </param>
        public static void IsEmpty(IEnumerable collection)
        {
            Assert.AreEqual(0, collection.Cast<object>().Count());
        }

        /// <summary>
        /// Assert that a collection is empty.
        /// </summary>
        /// <param name="collection">
        /// The collection to test.
        /// </param>
        public static void IsEmpty<T>(IEnumerable<T> collection)
        {
            Assert.AreEqual(0, collection.Count());
        }
    }
}

