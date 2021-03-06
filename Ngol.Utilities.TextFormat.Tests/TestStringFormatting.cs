using System;
using Ngol.Utilities.Reflection.Extensions;
using Ngol.Utilities.TextFormat;
using NUnit.Framework;

namespace Ngol.Utilities.TextFormat.Tests
{
    [TestFixture]
    public class TestStringFormatting
    {
        [Test]
        public void TestCentered()
        {
            string actual, text = "1234", expected;
            expected = "1234";
            actual = StringFormatting.Centered(text, expected.Length);
            Assert.AreEqual(expected, actual);
            expected = " 1234 ";
            actual = StringFormatting.Centered(text, expected.Length);
            Assert.AreEqual(expected, actual);
            expected = "  1234  ";
            actual = StringFormatting.Centered(text, expected.Length);
            Assert.AreEqual(expected, actual);
            expected = "    1234    ";
            actual = StringFormatting.Centered(text, expected.Length);
            Assert.AreEqual(expected, actual);
            expected = "      1234      ";
            actual = StringFormatting.Centered(text, expected.Length);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestLeftJustified()
        {
            string text = "12345";
            string expected1 = "12345   ", expected2 = "123";
            text = StringFormatting.LeftJustified(text, 8);
            Assert.AreEqual(expected1, text);
            text = StringFormatting.LeftJustified(text, 3);
            Assert.AreEqual(expected2, text);
        }

        [Test]
        public void TestRightJustified()
        {
            string text = "1234567890";
            string expected = "     1234567890";
            text = StringFormatting.RightJustified(text, 15);
            Assert.AreEqual(expected, text);
        }

        [Test]
        public void TestSpacesBefore()
        {
            Assert.AreEqual(0, typeof(StringFormatting).InvokeMethod("SpacesBefore", 4, 4));
            Assert.AreEqual(1, typeof(StringFormatting).InvokeMethod("SpacesBefore", 4, 6));
            Assert.AreEqual(2, typeof(StringFormatting).InvokeMethod("SpacesBefore", 4, 8));
            Assert.AreEqual(3, typeof(StringFormatting).InvokeMethod("SpacesBefore", 4, 10));
            Assert.AreEqual(4, typeof(StringFormatting).InvokeMethod("SpacesBefore", 4, 12));
        }
    }
}
