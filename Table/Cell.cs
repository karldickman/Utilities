using NUnit.Framework;
using System;

namespace TextFormat.Table
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
        /// Get the width required for the data in the cell.
        /// </summary>
        int Width();
    }
    
    /// <summary>
    /// The horizontal separator between two cells.
    /// </summary>
    public class HorizontalCellSeparator : ICell
    {
        /// <summary>
        /// The character used to seperate rows.
        /// </summary>
        protected internal char Separator { get; set; }

        /// <summary>
        /// The text of the cell.
        /// </summary>
        protected internal string Text { get; set; }

        public HorizontalCellSeparator (char separator)
        {
            Separator = separator;
        }
        
        public void Pad (int width)
        {
            Text = "";
            for (int i = 0; i < width; i++)
            {
                Text += Separator;
            }
        }
        
        /// <summary>
        /// Render the cell.
        /// </summary>
        override public string ToString ()
        {
            return Text;
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
        /// <summary>
        /// The default alignment for text in the cell.
        /// </summary>
        protected internal static readonly Alignment DEFAULT_ALIGNMENT = 
            StringFormatting.LeftJustified;

        /// <summary>
        /// The <see cref="Alignment"/> of the cell.
        /// </summary>
        protected internal Alignment Align { get; set; }

        /// <summary>
        /// The text in the rendered cell.
        /// </summary>
        protected internal string Text { get; set; }

        /// <summary>
        /// The value stored in the cell.
        /// </summary>
        protected internal object Value { get; set; }
        
        public Cell (object value_) : this(value_, DEFAULT_ALIGNMENT) {}
        
        public Cell (object value_, Alignment Align)
        {
            if (Align == null)
            {
                Align = DEFAULT_ALIGNMENT;
            }
            Value = value_;
            this.Align = Align;
        }
        
        public void Pad (int width)
        {
            Text = Align (Value, width);
        }
        
        /// <summary>
        /// Render the cell.
        /// </summary>
        override public string ToString ()
        {
            return Text;
        }
        
        /// <summary>
        /// Get the default width of the cell.
        /// </summary>
        public int Width ()
        {
            if (Value == null)
            {
                return 0;
            }     
            return Value.ToString ().Length;
        }
    }
    
    [TestFixture]
    public class TestHorizontalCellSeparator
    {
        [Test]
        public void TestPad ()
        {
            HorizontalCellSeparator separator = new HorizontalCellSeparator ('-');
            string expected = "--------";
            separator.Pad (8);
            Assert.AreEqual (expected, separator.Text);
        }
    }
    
    [TestFixture]
    public class TestCell
    {
        [Test]
        public void TestPad ()
        {
            string text = "xkcd";
            ICell cell = new Cell (text);
            cell.Pad (8);
            Assert.AreEqual ("xkcd    ", cell.ToString ());
            cell = new Cell (text, StringFormatting.RightJustified);
            cell.Pad (8);
            Assert.AreEqual ("    xkcd", cell.ToString ());
            cell = new Cell (text, StringFormatting.Centered);
            cell.Pad (8);
            Assert.AreEqual ("  xkcd  ", cell.ToString ());
        }
    }
}
