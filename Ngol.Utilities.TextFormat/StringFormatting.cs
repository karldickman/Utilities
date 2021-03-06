using System;

namespace Ngol.Utilities.TextFormat
{    
    /// <summary>
    /// Some useful functions for formatting strings.
    /// </summary>
    public static class StringFormatting
    {
        /// <summary>
        /// Make a blank string of a certain number of characters.
        /// </summary>
        /// <param name="width">
        /// A <see cref="System.Int32"/>.  The length of the blank string.
        /// </param>
        public static string Blank(int width)
        {
            return new string(' ', width);
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
        public static string Centered(object toCenter, int width)
        {
            if(toCenter == null)
            {
                return Blank(width);
            }
            string result = toCenter.ToString();
            if(result.Length > width)
            {
                return result;
            }
            int spaces = (width - result.Length) / 2;
            return new string(' ', spaces) + result + new string(' ', spaces);
        }
        
        /// <summary>
        /// Left justify a value in a string.  If the <paramref name="toJustify"/>
        /// has more characters than <paramref name="width"/>, the resulting
        /// string will be truncated.
        /// </summary>
        /// <param name="toJustify">
        /// The value to justify.
        /// </param>
        /// <param name="width">
        /// The required width.
        /// </param>
        public static string LeftJustified(object toJustify, int width)
        {
            return Justified(toJustify, width, true);
        }
        
        /// <summary>
        /// Left pad a value in a string.  If the number of characters
        /// in <paramref name="toPad"/> is greater than <paramref name="width"/>,
        /// this is equivalent to <see cref="object.ToString" />.
        /// </summary>
        /// <param name="toPad">
        /// The value to pad.
        /// </param>
        /// <param name="width">
        /// The preferred width.
        /// </param>
        public static string LeftPadded(object toPad, int width)
        {
            return Padded(toPad, width, true);
        }

        /// <summary>
        /// Right justify a value in a string.  If the <paramref name="toJustify"/>
        /// has more characters than <paramref name="width"/>, the resulting
        /// string will be truncated.
        /// </summary>
        /// <param name="toJustify">
        /// The value to justify.
        /// </param>
        /// <param name="width">
        /// The required width.
        /// </param>
        public static string RightJustified(object toJustify, int width)
        {
            return Justified(toJustify, width, false);
        }
         
        /// <summary>
        /// Right pad a value in a string.  If the number of characters
        /// in <paramref name="toPad"/> is greater than <paramref name="width"/>,
        /// this is equivalent to <see cref="object.ToString" />.
        /// </summary>
        /// <param name="toPad">
        /// The value to pad.
        /// </param>
        /// <param name="width">
        /// The preferred width.
        /// </param>
        public static string RightPadded(object toPad, int width)
        {
            return Padded(toPad, width, false);
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
        private static string Justified(object toJustify, int width,
            bool left)
        {
            string result = Padded(toJustify, width, left);
            if(result.Length > width)
            {
                return result.Substring(0, width);
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
        private static string Padded(object toPad, int width,
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
    }
}
