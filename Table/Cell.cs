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
		
        /*
		protected internal char Seperator
		{
			get { return seperator; }
		}*/
        
        /// <summary>
        /// The text in the cell.
        /// </summary>
        protected internal string Text
        {
            get { return text; }
            set { text = value; }
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
        public const alignment DEFAULT_ALIGNMENT = alignment.L;
        
		private alignment alignment;
        private string text;
		private object value_;
        
        /// <summary>
        /// The text in the cell.
        /// </summary>
        protected internal string Text
        {
            get { return text; }
            set { text = value; }
        }
		
		public Cell (object value_) : this(value_, DEFAULT_ALIGNMENT) {}
		
		public Cell (object value_, alignment alignment)
		{
			this.value_ = value_;
			this.alignment = alignment;
		}
        
		public void Pad (int width)
        {
            text = value_.ToString ();
            if (text.Length > width)
			{
		        text = text.Substring (0, width);
		    }
		    switch (alignment)
			{
		    case alignment.L:
		        text = text.PadRight (width);
                break;
		    case alignment.R:
		        text = text.PadLeft (width);
                break;
			}
		}
		
        /// <summary>
        /// Get the default width of the cell.
        /// </summary>
		public int Width ()
		{
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
	
	[TestFixture]
	public class TestStringCell
	{					
		[Test]
		public void TestPad ()
		{
		    Cell cell = new Cell ("12345");
		    string expected1 = "12345   ", expected2 = "123";
            cell.Pad (8);
            Assert.AreEqual (expected1, cell.Text);
            cell.Pad (3);
			Assert.AreEqual (expected2, cell.Text);
		}
	}
}
