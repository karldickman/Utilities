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
        /// Verify that a value appears in a collection.
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
        /// Verify that a value does not appear in a collection.
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
        /// Verify that a collection has the specified number of values.
        /// </summary>
        /// <param name="expected">
        /// The expected length of the collection.
        /// </param>
        /// <param name="collection">
        /// The collection to test.
        /// </param>
        /// <exception cref="AssertionException">
        /// Thrown if the assertion failed.
        /// </exception>
        public static void HasCount(int expected, IEnumerable collection)
        {
            Assert.AreEqual(expected, collection.Cast<object>().Count());
        }

        /// <summary>
        /// Verify that a collection has the specified number of values.
        /// </summary>
        /// <param name="expected">
        /// The expected length of the collection.
        /// </param>
        /// <param name="collection">
        /// The collection to test.
        /// </param>
        /// <exception cref="AssertionException">
        /// Thrown if the assertion failed.
        /// </exception>
        public static void HasCount<T>(int expected, IEnumerable<T> collection)
        {
            Assert.AreEqual(expected, collection.Count());
        }

        /// <summary>
        /// Verify that a collection is empty.
        /// </summary>
        /// <param name="collection">
        /// The collection to test.
        /// </param>
        /// <exception cref="AssertionException">
        /// Thrown if the assertion failed.
        /// </exception>
        public static void IsEmpty(IEnumerable collection)
        {
            HasCount(0, collection);
        }

        /// <summary>
        /// Verify that a collection is empty.
        /// </summary>
        /// <param name="collection">
        /// The collection to test.
        /// </param>
        /// <exception cref="AssertionException">
        /// Thrown if the assertion failed.
        /// </exception>
        public static void IsEmpty<T>(IEnumerable<T> collection)
        {
            HasCount(0, collection);
        }

        /// <summary>
        /// Verify that a collection is not empty.
        /// </summary>
        /// <param name="collection">
        /// The collection to test.
        /// </param>
        /// <exception cref="AssertionException">
        /// Thrown if the assertion failed.
        /// </exception>
        public static void IsNotEmpty(IEnumerable collection)
        {
            Assert.AreNotEqual(0, collection.Cast<object>().Count());
        }

        /// <summary>
        /// Verify that a collection is not empty.
        /// </summary>
        /// <param name="collection">
        /// The collection to test.
        /// </param>
        /// <exception cref="AssertionException">
        /// Thrown if the assertion failed.
        /// </exception>
        public static void IsNotEmpty<T>(IEnumerable<T> collection)
        {
            Assert.AreNotEqual(0, collection.Count());
        }
    }
}

