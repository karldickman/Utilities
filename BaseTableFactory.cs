using System;
using System.Collections.Generic;

namespace Formatting
{
	
	public class BaseTableFactory
	{
		/// <summary>
		/// Create a new instance of BaseTable with the given items in the rows.
		/// </summary>
		public BaseTable newInstance (List<List<object>> rawRows, string label, string[,] borders, List<alignment> alignments)
		{
			int i, numColumns;
			if (rawRows.Count == 0)
			{
				numColumns = 0;
			}
			else
			{
				numColumns = rawRows[0].Count;
			}
			if (alignments.Count > numColumns)
			{
				throw (new InvalidBaseTableException ("Expected no more than " + numColumns + " alignments, but got " + alignments.Count));
			}
			else if (alignments.Count < numColumns)
			{
				for (i = 0; i < alignments.Count - numColumns; i++)
				{
					alignments.Add (Column.DEFAULT_ALIGNMENT);
				}
			}
			List<List<string>> rows = new List<List<string>> ();
			foreach (List<object> rawRow in rawRows)
			{
				List<string> row = new List<string> ();
				if (rawRow.Count != numColumns)
				{
					throw (new InvalidBaseTableException ("All rows must have " + numColumns + " cells."));
				}
				foreach (object cell in rawRow)
				{
					row.Add (cell.ToString ());
				}
				rows.Add (row);
			}
			List<List<string>> columnItems = new List<List<string>> (numColumns);
			foreach (List<string> row in rows)
			{
				for (i = 0; i < numColumns; i++)
				{
					columnItems[i].Add (row[i]);
				}
			}
			List<Column> columns = new List<Column> (numColumns);
			for (i = 0; i < numColumns; i++)
			{
				columns[i] = new Column (columnItems[i], alignments[i]);
			}
			return new BaseTable(rows, columns, label, borders);
		}
	}
}
