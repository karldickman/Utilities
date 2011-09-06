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
        /// Assert that two integers are equal.
        /// </summary>
        /// <param name="expected">
        /// One integer.
        /// </param>
        /// <param name="actual">
        /// The other integer.
        /// </param>
        /// <exception cref="AssertionException">
        /// Thrown if the assertion failed.
        /// </exception>
        public static void AreEqual(int expected, int actual)
        {
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Assert that two strings are equal.
        /// </summary>
        /// <param name="expected">
        /// One string.
        /// </param>
        /// <param name="actual">
        /// The other string.
        /// </param>
        /// <exception cref="AssertionException">
        /// Thrown if the assertion failed.
        /// </exception>
        public static void AreEqual(string expected, string actual)
        {
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Assert that a value appears in a collection.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the value and the collection.
        /// </typeparam>
        /// <param name="expected">
        /// The value expected to be in the collection.
        /// </param>
        /// <param name="actual">
        /// The collection in which to check for the value.
        /// </param>
        /// <exception cref="AssertionException">
        /// Thrown if the assertion failed.
        /// </exception>
        public static void Contains<T>(T expected, IEnumerable<T> actual)
        {
            Assert.IsNotNull(actual);
            Assert.That(actual.Contains(expected));
        }

        /// <summary>
        /// Assert that a value does not appear in a collection.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the value and the collection.
        /// </typeparam>
        /// <param name="expected">
        /// The value expected not to be in the collection.
        /// </param>
        /// <param name="actual">
        /// The collection in which to check for the value.
        /// </param>
        /// <exception cref="AssertionException">
        /// Thrown if the assertion failed.
        /// </exception>
        public static void DoesNotContain<T>(T expected, IEnumerable<T> actual)
        {
            Assert.IsNotNull(actual);
            Assert.IsFalse(actual.Contains(expected));
        }

        /// <summary>
        /// Throw an <see cref="AssertionException" />.
        /// </summary>
        /// <exception cref="AssertionException">
        /// Thrown if the assertion failed.
        /// </exception>
        public static void Fail()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Throw an <see cref="AssertionException" />.
        /// </summary>
        /// <param name="message">
        /// The message to provide in the exception.
        /// </param>
        /// <exception cref="AssertionException">
        /// Thrown if the assertion failed.
        /// </exception>
        public static void Fail(string message)
        {
            Assert.Fail(message);
        }

        /// <summary>
        /// Assert that a collection is empty.
        /// </summary>
        /// <param name="collection">
        /// The collection to test.
        /// </param>
        /// <exception cref="AssertionException">
        /// Thrown if the assertion failed.
        /// </exception>
        public static void IsEmpty(IEnumerable collection)
        {
            AreEqual(0, collection.Cast<object>().Count());
        }

        /// <summary>
        /// Assert that a collection is empty.
        /// </summary>
        /// <param name="collection">
        /// The collection to test.
        /// </param>
        /// <exception cref="AssertionException">
        /// Thrown if the assertion failed.
        /// </exception>
        public static void IsEmpty<T>(IEnumerable<T> collection)
        {
            AreEqual(0, collection.Count());
        }
    }
}

