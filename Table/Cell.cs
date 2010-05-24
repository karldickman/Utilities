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
		
		protected internal char Seperator
		{
			get { return seperator; }
		}
        
        /// <summary>
        /// The text in the cell.
        /// </summary>
        public string Text
        {
            get { return text; }
            protected internal set { text = value; }
        }
		
		public HorizontalCellSeperator (char seperator)
		{
			this.seperator = seperator;
		}
		
		public void Pad (int width)
		{
			Text = "";
			for (int i = 0; i < width; i++)
			{
				Text += Seperator;
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
		/// The alignment of the text in the cell.
		/// </summary>
		public alignment Alignment
		{
			get { return alignment; }
		}
        
        /// <summary>
        /// The text in the cell.
        /// </summary>
        public string Text
        {
            get { return text; }
            protected internal set { text = value; }
        }
		
		/// <summary>
		/// The value in the cell.
		/// </summary>
		public object Value
		{
			get { return value_; }
		}
		
		public Cell (object value_) : this(value_, DEFAULT_ALIGNMENT) {}
		
		public Cell (object value_, alignment alignment)
		{
			this.value_ = value_;
			this.alignment = alignment;
		}
        
		public void Pad (int width)
        {
            Text = Value.ToString ();
            if (Text.Length > width)
			{
		        Text = Text.Substring (0, width);
		    }
		    switch (alignment)
			{
		    case alignment.L:
		        Text = Text.PadRight (width);
                break;
		    case alignment.R:
		        Text = Text.PadLeft (width);
                break;
			}
		}
		
		public int Width ()
		{
			return Value.ToString ().Length;
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
