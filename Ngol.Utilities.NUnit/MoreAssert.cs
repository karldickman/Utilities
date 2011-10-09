using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ngol.Utilities.Collections.Extensions;
using NUnit.Framework;

namespace Ngol.Utilities.NUnit
{
    /// <summary>
    /// Additional assertions for NUnit.
    /// </summary>
    public static class MoreAssert
    {
        /// <summary>
        /// Verify that two iterables contain the same sequence of values.
        /// </summary>
        /// <param name="expected">
        /// The expected iterable.
        /// </param>
        /// <param name="actual">
        /// The actual iterable.
        /// </param>
        /// <typeparam name='T'>
        /// The type of values in the collections.
        /// </typeparam>
        /// <exception cref="AssertionException">
        /// Thrown if the assertion failed.
        /// </exception>
        public static void CollectionsEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            Assert.IsNotNull(actual);
            Assert.IsNotNull(expected);
            MoreAssert.HaveSameCount(expected, actual);
            expected.ForEach(actual, Assert.AreEqual);
        }

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
            if(collection == null)
            {
                Assert.Fail("  Expected: Count = {0}\n  But was: a null collection", expected);
            }
            int actual = collection.Cast<object>().Count();
            if(expected != actual)
            {
                Assert.Fail("  Expected: Count = {0}\n  But was: Count = {1}", expected, actual);
            }
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
            if(collection == null)
            {
                Assert.Fail("  Expected: Count = {0}\n  But was: a null collection", expected);
            }
            int actual = collection.Count();
            if(expected != actual)
            {
                Assert.Fail("  Expected: Count = {0}\n  But was: Count = {1}", expected, actual);
            }
        }

        /// <summary>
        /// Verify that a string has the specified number of characters.
        /// </summary>
        /// <param name="expected">
        /// The expected length of the string.
        /// </param>
        /// <param name="actual">
        /// The string to test.
        /// </param>
        /// <exception cref="AssertionException">
        /// Thrown if the assertion failed.
        /// </exception>
        public static void HasLength(int expected, string actual)
        {
            if(actual == null)
            {
                Assert.Fail("  Expected: string with length {0}\n  But was: a null string", expected);
            }
            if(expected != actual.Length)
            {
                Assert.Fail("  Expected: string with length {0}\n  But was: string with length {1}", expected, actual.Length);
            }
        }

        /// <summary>
        /// Check that two sequences are the same length.
        /// </summary>
        /// <param name="expected">
        /// The sequence whose length is expected to be the same
        /// as that of the <paramref name="actual" /> sequence.
        /// </param>
        /// <param name="actual">
        /// The sequence whose length to check.
        /// </param>
        /// <exception cref="AssertionException">
        /// Thrown if the assertion failed.
        /// </exception>
        public static void HaveSameCount<T1, T2>(IEnumerable<T1> expected, IEnumerable<T2> actual)
        {
            Assert.IsNotNull(expected);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Count(), actual.Count());
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
            if(collection == null)
            {
                Assert.Fail("  Expected: empty collection\n  But was: null collection");
            }
            if(0 != collection.Cast<object>().Count())
            {
                Assert.Fail("  Expected: empty collection\n  But was: non-empty collection");
            }
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
            if(collection == null)
            {
                Assert.Fail("  Expected: empty collection\n  But was: null collection");
            }
            if(0 != collection.Count())
            {
                Assert.Fail("  Expected: empty collection\n  But was: non-empty collection");
            }
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

