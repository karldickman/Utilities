using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ngol.Utilities.Collections.Extensions;

namespace Ngol.Utilities.TextFormat.Table
{
    /// <summary>
    /// A representation of a table, consisting of a bunch of rows of cells.
    /// </summary>
    public class Table : IEnumerable<string>
    {
        #region Properties

        /// <summary>
        /// The maximum widths of each column.
        /// </summary>
        protected IEnumerable<int> MaxWidths
        {
            get;
            set;
        }

        /// <summary>
        /// The rows of teh table.
        /// </summary>
        protected internal IEnumerable<Row> Rows
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
        /// <exception cref="ArgumentException">
        /// Thrown if a maximum width was not specified for one or more rows.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any argument is <see langword="null" />.
        /// </exception>
        public Table(IEnumerable<Row> rows) : this(rows, Enumerable.Repeat<int>(int.MaxValue, rows.Max(row => row.ColumnCount)))
        {
        }

        /// <summary>
        /// Create a new table.
        /// </summary>
        /// <param name="rows">
        /// A sequence of the rows in the table.
        /// </param>
        /// <param name="maxWidths">
        /// The maximum widths of the columns.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if a maximum width was not specified for one or more rows.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if any argument is <see langword="null" />.
        /// </exception>
        public Table(IEnumerable<Row> rows, IEnumerable<int> maxWidths)
        {
            if(rows == null)
                throw new ArgumentNullException("rows");
            if(maxWidths == null)
                throw new ArgumentNullException("maxWidths");
            if(rows.Any(row => row.ColumnCount > maxWidths.Count()))
                throw new ArgumentException("There must be a maximum width specified for every row.");
            Rows = rows;
            MaxWidths = maxWidths;
            Pad();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get the widths of all the columns in the table.
        /// </summary>
        public IEnumerable<int> GetColumnWidths()
        {
            int columnCount = Rows.First().ColumnCount;
            IList<int> columnWidths = new int[columnCount];
            foreach(Row row in Rows)
            {
                row.GetColumnWidths().ForEachEqualIndexed(MaxWidths.Take(row.ColumnCount), (width, maxWidth, columnIndex) =>
                {
                    if(width > columnWidths[columnIndex] && width < maxWidth)
                    {
                        columnWidths[columnIndex] = width;
                    }
                });
            }
            return columnWidths;
        }

        /// <summary>
        /// Pad all cells of the table to the width of their containing rows.
        /// </summary>
        public void Pad()
        {
            IEnumerable<int> columnWidths = GetColumnWidths();
            foreach(Row row in Rows)
            {
                row.Pad(columnWidths);
            }
        }

        #endregion

        #region IEnumerable[string] implementation

        /// <inheritdoc />
        public IEnumerator<string> GetEnumerator()
        {
            return Rows.Select(row => row.ToString()).GetEnumerator();
        }

        #region IEnumerable implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        #endregion
        
        #endregion
    }
}
