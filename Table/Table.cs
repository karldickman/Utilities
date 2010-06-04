using System;
using System.Collections.Generic;
using TextFormat.Table.Exceptions;

namespace TextFormat.Table
{
    /// <summary>
    /// A representation of a table, consisting of a bunch of rows of cells.
    /// </summary>
    public class Table
    {
        /// <summary>
        /// The maximum widths of each column.
        /// </summary>
        protected internal int[] MaxWidths { get; set; }
        
        /// <summary>
        /// The rows of teh table.
        /// </summary>
        protected internal IList<Row> Rows { get; set; }
        
        /// <summary>
        /// Create a new table.
        /// </summary>
        /// <param name="rows">
        /// A <see cref="IList<Row>"/> of the rows in the table.
        /// </param>
        /// <param name="maxWidths">
        /// A <see cref="System.Int32[]"/>.  The maximum widths of the columns.
        /// </param>
        protected internal Table (IList<Row> rows, int[] maxWidths)
        {
            Rows = rows;
            MaxWidths = maxWidths;
            Pad();
        }
        
        /// <summary>
        /// Get the widths of all the columns in the table.
        /// </summary>
        public int[] ColumnWidths ()
        {
            int columnCount = Rows[0].ColumnCount, width;
            int[] columnWidths = new int[columnCount];
            foreach (Row row in Rows)
            {
                for (int i = 0; i < columnCount; i++)
                {
                    width = row.ColumnWidth (i);
                    if (width > columnWidths[i] && width < MaxWidths[i])
                    {
                        columnWidths[i] = width;
                    }
                }
            }
            return columnWidths;
        }
        
        /// <summary>
        /// Get all the physical lines in a string representation of the table.
        /// </summary>
        public IList<string> Lines ()
        {
            IList<string> lines = new List<string> ();
            foreach (Row row in Rows)
            {
                lines.Add (row.ToString ());
            }
            return lines;
        }
        
        /// <summary>
        /// Pad all cells of the table to the width of their containing rows.
        /// </summary>
        public void Pad ()
        {
            int[] columnWidths = ColumnWidths ();
            foreach (Row row in Rows)
            {
                row.Pad (columnWidths);
            }
        }
    }
}
