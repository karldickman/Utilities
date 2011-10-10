using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Ngol.Utilities.Collections.Extensions;

namespace Ngol.Utilities.TextFormat.Table
{
    /// <summary>
    /// Class to represent a structuring of data into a tabular form.
    /// </summary>
    public class Table
    {
        #region Properties

        /// <summary>
        /// The collection of <see cref="Column" />s in the <see cref="Table" />.
        /// </summary>
        public readonly ColumnCollection Columns;

        /// <summary>
        /// The <see cref="Alignment" /> to use to format values in the header.
        /// </summary>
        public Alignment HeaderAlignment { get; set; }

        /// <summary>
        /// The headers of this <see cref="Table" />.
        /// </summary>
        public IEnumerable<string> Headers
        {
            get { return Columns.Select(column => column.Name); }
        }

        /// <summary>
        /// The collection of <see cref="Row" />s in the <see cref="Table" />.
        /// </summary>
        public readonly RowCollection Rows;

        /// <summary>
        /// The <see cref="DataTable" /> to which most calls are delegated.
        /// </summary>
        protected readonly DataTable InnerTable;

        #endregion

        #region Constructors

        /// <summary>
        /// Construct a new <see cref="Table" />.
        /// </summary>
        /// <param name="tableName">
        /// The name of the <see cref="Table" />.
        /// </param>
        /// <param name="headerAlignment">
        /// The method to use to align the text in the table's header.
        /// Defaults to <see cref="StringFormatting.LeftJustified" />.
        /// </param>
        public Table(string tableName=null, Alignment headerAlignment=null) : this(tableName == null ? new DataTable() : new DataTable(tableName))
        {
            if(headerAlignment == null)
            {
                headerAlignment = StringFormatting.LeftJustified;
            }
            HeaderAlignment = headerAlignment;
        }

        /// <summary>
        /// Construct a new <see cref="Table" />.
        /// </summary>
        /// <param name="innerTable">
        /// The <see cref="DataTable" /> to which to delegate.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="innerTable"/> is <see langword="null" />.
        /// </exception>
        protected Table(DataTable innerTable)
        {
            if(innerTable == null)
            {
                throw new ArgumentNullException("innerTable");
            }
            InnerTable = innerTable;
            Columns = new ColumnCollection(innerTable.Columns);
            Rows = new RowCollection(innerTable.Rows);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Find the widths in characters for all the <see cref="Column" />
        /// in this <see cref="Table" />.
        /// </summary>
        public IEnumerable<int> GetColumnWidths(bool includeHeaders=false)
        {
            IEnumerable<int > columnWidths = Columns.Select(MaxLengthInColumn);
            if(includeHeaders)
            {
                IEnumerable<int > headerWidths = Headers.Select(header => header.ToString().Length);
                return columnWidths.Zip(headerWidths, Math.Max);
            }
            return columnWidths;
        }

        private int MaxLengthInColumn(Column column, int columnIndex)
        {
            uint maxLength = Rows.MaxOrIdentity(row => RowLengthAt(row, columnIndex));
            return Convert.ToInt32(maxLength);
        }

        private uint RowLengthAt(Row row, int columnIndex)
        {
            Column column = Columns[columnIndex];
            Format format = column.InnerFormat;
            return Convert.ToUInt32(format(row[columnIndex]).Length);
        }

        #endregion
    }
}

