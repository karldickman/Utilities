using NUnit.Framework;
using System;

namespace Formatting.Table
{
    
    /// <summary>
    /// The interface that all cells of the table must implement.
    /// </summary>
    public interface ICell
    {
        /// <summary>
        /// Returns the cell padded to the given width.
        /// </summary>
        void Pad(int width);
        
        /// <summary>
        /// Returns the width of the cell.
        /// </summary>
        int Width();
    }
    
    /// <summary>
    /// The horizontal seperator between two cells.
    /// </summary>
    public class HorizontalCellSeperator : ICell
    {
        private char seperator;
        private string text;
        
        /// <summary>
        /// The text in the cell.
        /// </summary>
        protected internal string Text
        {
            get { return text; }
        }
        
        public HorizontalCellSeperator (char seperator)
        {
            this.seperator = seperator;
        }
        
        public void Pad (int width)
        {
            text = "";
            for (int i = 0; i < width; i++)
            {
                text += seperator;
            }
        }
        
        public override string ToString ()
        {
            return text;
        }
        
        public int Width ()
        {
            return 0;
        }
    }
    
    /// <summary>
    /// A cell containing a string.
    /// </summary>
    public class Cell : ICell
    {        
        private static readonly Alignment DEFAULT_ALIGNMENT = StringFormatting.LeftJustified;
        
        private Alignment Align;
        private string text;
        private object value_;
        
        public Cell (object value_) : this(value_, DEFAULT_ALIGNMENT) {}
        
        public Cell (object value_, Alignment Align)
        {
            if (Align == null)
            {
                Align = DEFAULT_ALIGNMENT;
            }
            this.value_ = value_;
            this.Align = Align;
        }
        
        public void Pad (int width)
        {
            text = Align (value_, width);
        }
        
        public override string ToString ()
        {
            return text;
        }
        
        /// <summary>
        /// Get the default width of the cell.
        /// </summary>
        public int Width ()
        {
            if (value_ == null)
            {
                return 0;
            }     
            return value_.ToString ().Length;
        }
    }
    
    [TestFixture]
    public class TestHorizontalCellSeperator
    {
        [Test]
        public void TestPad ()
        {
            HorizontalCellSeperator seperator = new HorizontalCellSeperator ('-');
            string expected = "--------";
            seperator.Pad (8);
            Assert.AreEqual (expected, seperator.Text);
        }
    }
}
