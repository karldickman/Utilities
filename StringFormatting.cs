using NUnit.Framework;
using System;

namespace TextFormat
{
    /// <summary>
    /// A function that aligns a string, and forces it to a specified width.
    /// </summary>
    public delegate string Alignment(object toAlign, int width);
    
    /// <summary>
    /// Some useful functions for formatting strings.
    /// </summary>
    public class StringFormatting
    {
        /// <summary>
        /// Make a blank string of a certain number of characters.
        /// </summary>
        /// <param name="width">
        /// A <see cref="System.Int32"/>.  The length of the blank string.
        /// </param>
        public static string Blank (int width)
        {
            string result = "";
            for (int i = 0; i < width; i++)
            {
                result += " ";
            }
            return result;
        }
        
        /// <summary>
        /// Centers a value in a string.
        /// </summary>
        /// <param name="toCenter">
        /// A <see cref="System.Object"/>.  The value to be centered.
        /// </param>
        /// <param name="width">
        /// A <see cref="System.Int32"/>.  The width of the required string.
        /// </param>
        /// <returns>
        /// A <see cref="System.String"/>.  If the value is longer than the
        /// string, this is equivalent ot toCenter.ToString().
        /// </returns>
        public static string Centered (object toCenter, int width)
        {
            if (toCenter == null) 
            {
                return Blank (width);
            }
            string result = toCenter.ToString ();
            int resultLength = result.Length;
            for (int i = 0; i < SpacesBefore (resultLength, width); i++)
            {
                result = " " + result + " ";
            }
            return result;
        }
        
        /// <summary>
        /// Left justify a value in a string.
        /// </summary>
        /// <param name="toJustify">
        /// A <see cref="System.Object"/>.  The value to justify.
        /// </param>
        /// <param name="width">
        /// A <see cref="System.Int32"/>.  The required width.
        /// </param>
        /// <returns>
        /// A <see cref="System.String"/>.  If the value is wider than the
        /// string, the value will be truncated.
        /// </returns>
        public static string LeftJustified (object toJustify, int width)
        {
            return Justified (toJustify, width, true);
        }
        
        /// <summary>
        /// Left pad a value in a string.
        /// </summary>
        /// <param name="toPad">
        /// A <see cref="System.Object"/>.  The value to pad.
        /// </param>
        /// <param name="width">
        /// A <see cref="System.Int32"/>.  The preferred width.
        /// </param>
        /// <returns>
        /// A <see cref="System.String"/>.  If the value is wider than the
        /// string, this is equivalent to value.ToString().
        /// </returns>
        public static string LeftPadded (object toPad, int width)
        {
            return Padded (toPad, width, true);
        }

        /// <summary>
        /// Right justify a value in a string.
        /// </summary>
        /// <param name="toJustify">
        /// A <see cref="System.Object"/>.  The value to justify.
        /// </param>
        /// <param name="width">
        /// A <see cref="System.Int32"/>.  The required width.
        /// </param>
        /// <returns>
        /// A <see cref="System.String"/>.  If the value is wider than the
        /// string, the value will be truncated.
        /// </returns>
        public static string RightJustified (object toJustify, int width)
        {
            return Justified (toJustify, width, false);
        }
         
        /// <summary>
        /// Right pad a value in a string.
        /// </summary>
        /// <param name="toPad">
        /// A <see cref="System.Object"/>.  The value to pad.
        /// </param>
        /// <param name="width">
        /// A <see cref="System.Int32"/>.  The preferred width.
        /// </param>
        /// <returns>
        /// A <see cref="System.String"/>.  If the value is wider than the
        /// string, this is equivalent to value.ToString().
        /// </returns>
        public static string RightPadded (object toPad, int width)
        {
            return Padded (toPad, width, false);
        }
        
        /// <summary>
        /// Justify a value to a given width.
        /// </summary>
        /// <param name="toJustify">
        /// A <see cref="System.Object"/>.  The value to justify.
        /// </param>
        /// <param name="width">
        /// A <see cref="System.Int32"/>.  The required width.
        /// </param>
        /// <param name="left">
        /// A <see cref="System.Boolean"/>.  If true, pad left; if false, pad
        /// right.
        /// </param>
        /// <returns>
        /// A <see cref="System.String"/>.  If the value is wider than width,
        /// the value is truncated.
        /// </returns>
        protected internal static string Justified(object toJustify, int width,
            bool left)
        {
            string result = Padded(toJustify, width, left);
            if (result.Length > width)
            {
                return result.Substring (0, width);
            }
            return result;
        }
        
        /// <summary>
        /// Pad a value to a given width.
        /// </summary>
        /// <param name="toPad">
        /// A <see cref="System.Object"/>.  The value to pad.
        /// </param>
        /// <param name="width">
        /// A <see cref="System.Int32"/>.  The preferred width.
        /// </param>
        /// <param name="left">
        /// A <see cref="System.Boolean"/>.  If true, pad left; if false, pad
        /// right.
        /// </param>
        /// <returns>
        /// A <see cref="System.String"/>.  If the value is wider than width,
        /// this is the same as toPad.ToString().
        /// </returns>
        protected internal static string Padded(object toPad, int width,
            bool left)
        {
            if(toPad == null)
            {
                 return Blank(width);
            }
            if(left)
            {
                return toPad.ToString().PadRight(width);
            }
            return toPad.ToString().PadLeft(width);
        }
        
        /// <summary>
        /// Calculate the number of spaces needed to center a word.
        /// </summary>
        /// <param name="wordLength">
        /// A <see cref="System.Int32"/>.  The length of the word.
        /// </param>
        /// <param name="width">
        /// A <see cref="System.Int32"/>.  The width of the string in which the
        /// word will be centered.
        /// </param>
        /// <returns>
        /// A <see cref="System.Int32"/>.
        /// </returns>
        protected internal static int SpacesBefore(int wordLength, int width)
        {
            return (width - wordLength) / 2;
        }
    }
    
    [TestFixture]
    public class TestStringFormatting
    {
        [Test]
        public void TestCentered ()
        {
            string actual, text = "1234", expected;
            expected = "1234";
            actual = StringFormatting.Centered (text, expected.Length);
            Assert.AreEqual (expected, actual);
            expected = " 1234 ";
            actual = StringFormatting.Centered (text, expected.Length);
            Assert.AreEqual (expected, actual);
            expected = "  1234  ";
            actual = StringFormatting.Centered (text, expected.Length);
            Assert.AreEqual (expected, actual);
            expected = "    1234    ";
            actual = StringFormatting.Centered (text, expected.Length);
            Assert.AreEqual (expected, actual);
            expected = "      1234      ";
            actual = StringFormatting.Centered (text, expected.Length);
            Assert.AreEqual (expected, actual);
        }
        
        [Test]
        public void TestLeftJustified ()
        {
            string text = "12345";
            string expected1 = "12345   ", expected2 = "123";
            text = StringFormatting.LeftJustified (text, 8);
            Assert.AreEqual (expected1, text);
            text = StringFormatting.LeftJustified (text, 3);
            Assert.AreEqual (expected2, text);
        }
        
        [Test]
        public void TestRightJustified ()
        {
            string text = "1234567890";
            string expected = "     1234567890";
            text = StringFormatting.RightJustified (text, 15);
            Assert.AreEqual (expected, text);
        }
        
        [Test]
        public void TestSpacesBefore ()
        {
            Assert.AreEqual (0, StringFormatting.SpacesBefore (4, 4));
            Assert.AreEqual (1, StringFormatting.SpacesBefore (4, 6));
            Assert.AreEqual (2, StringFormatting.SpacesBefore (4, 8));
            Assert.AreEqual (3, StringFormatting.SpacesBefore (4, 10));
            Assert.AreEqual (4, StringFormatting.SpacesBefore (4, 12));
        }
    }
}
