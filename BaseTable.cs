using System.Collections.Generic;

namespace Formatting
{	
	
	/// <summary>
	/// A class to format a list of data into columns.
	/// </summary>
	public class BaseTable
	{		
		private string[,] borders;
		private List<Column> columns;
		private string label;
		private List<List<string>> rows;

		protected string[,] Borders
		{
			get { return borders; }
		}
		
		protected List<Column> Columns
		{
			get { return columns; }
		}
		
		public string Label
		{
			get { return label; }
		}
		
		protected List<List<string>> Rows
		{
			get { return rows; }
		}
		
		protected internal BaseTable (List<List<string>> rows, List<Column> columns, string label, string[,] borders)
		{
			this.rows = rows;
			this.columns = columns;
			this.label = label;
			this.borders = borders;
		}
		
		public static string Border (string pattern, int size)
		{
			string border = "";
			for(int i = 0; i <= size / pattern.Length; i++)
			{
				border += pattern;
			}
			return border.Substring(0, size);
		}
		
		protected internal string BorderByIndex(int index)
		{
			string border = Borders[index, 0];
			index *= 2;
			border += Border (Borders[index, 1], Columns[0].Width ());
			for (int i = 1; i < Columns.Count; i++)
			{
				border += Borders[index, 2];
				border += Border (Borders[index, 1], Columns[i].Width ());
			}
			border += Borders[index, 4];
			return border;
		}
		
		public string BottomBorder ()
		{
			return BorderByIndex (2);
		}
		
		protected internal bool HasBottomBorder()
		{
			return HasSeperatorByIndex(2);
		}
		
		protected internal bool HasSeperatorByIndex(int index)
		{
			int[] indices = { 0, 1, 2, 4 };
			foreach (int i in indices) 
			{
				if (Borders[index, i] != null) 
				{
					return true;
				}
			}
			return false;
		}
		
		protected bool HasRowSeperator ()
		{
			return HasSeperatorByIndex (1);
		}
		
		protected bool HasTopBorder ()
		{
			return HasSeperatorByIndex (0);
		}
		
		/// <summary>
		/// Format the row for presentation in the table.
		/// </summary>
		public string Row (int index)
		{
			string row = Borders[1, 0] + Rows[index][0];
			for (int i = 1; i < Rows[index].Count; i++)
			{
				row += Borders[1, 2] + Rows[index][i];
			}
			return row + Borders[1, 4];
		}
		
		public string RowSeperator ()
		{
			return BorderByIndex (1);
		}
		
		/// <summary>
		/// Return the top border of the table.
		/// </summary>
		public string TopBorder ()
		{
			return BorderByIndex (0);
		}
		
		/// <summary>
		/// Render the table.
		/// </summary>
		public override string ToString ()
		{
			string lines = "", seperator = "";
			if (HasTopBorder ())
			{
				lines += TopBorder () + "\n";
			}
			if (HasRowSeperator ())
			{
				seperator = RowSeperator () + "\n";
			}
			lines += Row (0) + "\n";
			for (int i = 1; i < rows.Count; i++)
			{
				lines += seperator + Row (i) + "\n";
			}
			if (HasBottomBorder ())
			{
				lines += BottomBorder () + "\n";
			}
			return lines;
		}
		
		/// <summary>
		/// The width, in characters, of the table
		/// </summary>
		public int Width ()
		{
			int sum = 0;
			foreach (Column column in Columns) 
			{
				sum += column.Width ();
			}
			sum += Borders[1, 0].Length;
			sum += Borders[1, 2].Length;
			sum += Borders[1, 4].Length;
			return sum;
		}
	}
}