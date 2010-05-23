using System.Collections.Generic;

namespace Formatting
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
		public const alignment DEFAULT_ALIGNMENT = alignment.L;
		private alignment alignment;
		private List<string> items;
		
		/// <summary>
		/// The alignment of the column.
		/// </summary>
		public alignment Alignment
		{
			get { return alignment; }
		}
		
		/// <summary>
		/// The values for each cell of this column
		/// </summary>
		public List<string> Items
		{
			get { return items; }
		}
		
		public Column(List<string>items) : this(items, alignment.L) {}
		
		public Column (List<string> items, alignment alignment)
		{
			this.items = items;
			this.alignment = alignment;
		}
		
		/// <summary>
		/// Return all the items padded to the width of the widest item.
		/// </summary>
		public List<string> PaddedItems ()
		{
			return PaddedItems (Width ());
		}
		
		/// <summary>
		/// Return all the items padded to the given width.
		/// </summary>
		public List<string> PaddedItems (int width)
		{
			List<string> paddedItems = new List<string> (items);
			foreach (string item in paddedItems)
			{
				switch (Alignment)
				{
				case alignment.L:
					item.PadLeft (width);
					break;
				case alignment.R:
					item.PadRight (width);
					break;
				}
			}
			return paddedItems;
		}
		
		public int Width ()
		{
			int width = 0;
			foreach (string item in Items) 
			{
				if (item.Length > width)
				{
					width = item.Length;
				}
			}
			return width;
		}
	}
}