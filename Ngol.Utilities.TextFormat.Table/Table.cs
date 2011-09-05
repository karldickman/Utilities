using System;
using System.Collections.Generic;

namespace Ngol.Utilities.TextFormat.Table
{
    /// <summary>
    /// A representation of a table, consisting of a bunch of rows of cells.
    /// </summary>
    public class Table
    {
        #region Properties

        /// <summary>
        /// The maximum widths of each column.
        /// </summary>
        protected IList<int> MaxWidths
        {
            get;
            set;
        }

        /// <summary>
        /// The rows of teh table.
        /// </summary>
        protected IList<Row> Rows
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new table.
        /// </summary>
        /// <param name="rows">
        /// A sequence of the rows in the table.
        /// </param>
        /// <param name="maxWidths">
        /// The maximum widths of the columns.
        /// </param>
        public Table(IList<Row> rows, IList<int> maxWidths)
        {
            Rows = rows;
            MaxWidths = maxWidths;
            Pad();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get the widths of all the columns in the table.
        /// </summary>
        public IList<int> ColumnWidths()
        {
            int columnCount = Rows[0].ColumnCount, width;
            IList<int> columnWidths = new int[columnCount];
            foreach(Row row in Rows)
            {
                for(int i = 0; i < columnCount; i++)
                {
                    width = row.ColumnWidth(i);
                    if(width > columnWidths[i] && width < MaxWidths[i])
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
        public IList<string> Lines()
        {
            IList<string> lines = new List<string>();
            foreach(Row row in Rows)
            {
                lines.Add(row.ToString());
            }
            return lines;
        }

        /// <summary>
        /// Pad all cells of the table to the width of their containing rows.
        /// </summary>
        public void Pad()
        {
            IList<int> columnWidths = ColumnWidths();
            foreach(Row row in Rows)
            {
                row.Pad(columnWidths);
            }
        }

        #endregion
    }
}
