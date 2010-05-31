using NUnit.Framework;
using System;

namespace TextFormat
{
    public delegate string Alignment(object toAlign, int width);
    
    public class StringFormatting
    {
        public static string Blank (int width)
        {
            string result = "";
            for (int i = 0; i < width; i++)
            {
                result += " ";
            }
            return result;
        }
        
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
        
        public static string LeftJustified (object toJustify, int width)
        {
            return Justified (toJustify, width, true);
        }
        
        public static string LeftPadded (object toPad, int width)
        {
            return Padded (toPad, width, true);
        }
        
        public static string RightJustified (object toJustify, int width)
        {
            return Justified (toJustify, width, false);
        }
        
        public static string RightPadded (object toPad, int width)
        {
            return Padded (toPad, width, false);
        }
        
        protected internal static string Justified(object toJustify, int width, bool left)
        {
            string result = Padded(toJustify, width, left);
            if (result.Length > width)
            {
                return result.Substring (0, width);
            }
            return result;
        }
        
        protected internal static string Padded(object toPad, int width, bool left)
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
