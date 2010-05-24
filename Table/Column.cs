using NUnit.Framework;
using System.Collections.Generic;

namespace Formatting.Table
{
	
	/// <summary>
	/// An enumeration for the alignments in a column.
	/// </summary>
	public enum alignment {L, R};
	
	/// <summary>
	/// A column in a table.
	/// </summary>
	public class Column
	{
		private List<ICell> cells;
		private int maxWidth;
		
		/// <summary>
		/// The values for each cell of this column
		/// </summary>
		public List<ICell> Cells
		{
			get { return cells; }
		}
		
		/// <summary>
		/// The largest size the column is allowed to have.
		/// </summary>
		public int MaxWidth
		{
			get { return maxWidth; }
		}
        
		public Column (List<ICell> cells) : this(cells, int.MaxValue) {}
		
		public Column (List<ICell> cells, int maxWidth)
		{
			this.cells = cells;
			this.maxWidth = maxWidth;
		}
		
		/// <summary>
		/// Pad all cells to the width of the column.
		/// </summary>
		public void Pad ()
		{
			Pad(Width());
		}
		
		/// <summary>
		/// Pad all cells to the given width.
		/// </summary>
        public void Pad (int width)
        {
            foreach (ICell cell in cells)
            {
                cell.Pad (width);
            }     
		}
		
		/// <summary>
		/// Returns the width of the wideste cell.
		/// </summary>
		public int WidestCell ()
		{
			int width = 0;
			foreach (ICell cell in cells)
			{
				if (cell.Width() > width)
				{
					width = cell.Width();
				}
			}
			return width;
		}
		
		/// <summary>
		/// Returns the width of the column when it will be padded.
		/// </summary>
		public int Width ()
		{
			int widestCell = WidestCell ();
			if (widestCell < MaxWidth)
			{
				return widestCell;
			}
			return MaxWidth;
		}
	}
	
	[TestFixture]
	public class TestColumn
	{
		private List<ICell> cells;
		private Column column;
		private int maxWidth;
		
		[SetUp]
		public void Setup ()
		{
			maxWidth = 8;
			string[] cellValues = new string[] { "12345", "123", "12345678", "1234567", "1234", "1", "", "123456" };
			cells = new List<ICell> ();
			foreach (string cellValue in cellValues)
			{
				cells.Add (new Cell(cellValue));
			}
			column = new Column (cells);
		}
		
		[Test]
		public void TestPad ()
		{
			column.Pad ();
			foreach (Cell cell in column.Cells)
			{
				Assert.AreEqual (maxWidth, cell.Text.Length);
			}
			maxWidth = 3;
			column = new Column (cells, maxWidth);
			column.Pad ();
			foreach (Cell cell in column.Cells)
			{
				Assert.AreEqual (maxWidth, cell.Text.Length);
			}
		}
		
		[Test]
		public void TestWidestCell ()
		{
			Assert.AreEqual (maxWidth, column.WidestCell ());
		}
		
		[Test]
		public void TestWidth ()
		{
			Assert.AreEqual (maxWidth, column.Width ());
			maxWidth = 3;
			column = new Column (cells, maxWidth);
			Assert.AreEqual (maxWidth, column.Width ());			
		}
	}
}