using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Ngol.Utilities.Collections.Extensions;
using Ngol.Utilities.System.Extensions;

namespace Ngol.Utilities.TextFormat.Table
{
    /// <summary>
    /// Produces tables with a particular format when provided with the cells to
    /// do so.
    /// </summary>
    public class TableFormatter
    {
        #region Properties

        /// <summary>
        /// The bottom border of the table.
        /// </summary>
        public string BottomBorder
        {
            get { return BorderCharToString(BottomBorderChar); }
        }

        /// <summary>
        /// The character used to separate columns.
        /// </summary>
        public string ColumnSeparator
        {
            get { return BorderCharToString(ColumnSeparatorChar); }
        }

        /// <summary>
        /// The character used at the corner of cells.
        /// </summary>
        public string Corner
        {
            get { return BorderCharToString(CornerChar); }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has bottom border.
        /// </summary>
        /// <value>
        /// <see langword="true" /> if this instance has bottom border; otherwise, <see langword="false" />.
        /// </value>
        public bool HasBottomBorder
        {
            get { return BottomBorderChar != '\0'; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has column separator.
        /// </summary>
        /// <value>
        /// <see langword="true" /> if this instance has column separator; otherwise, <see langword="false" />.
        /// </value>
        public bool HasColumnSeparator
        {
            get { return ColumnSeparatorChar != '\0'; }
        }

        /// <summary>
        /// Does this table have a top border?
        /// </summary>
        public bool HasTopBorder
        {
            get { return TopBorderChar != '\0'; }
        }

        /// <summary>
        /// The character used on the left border of the table.
        /// </summary>
        public string LeftBorder
        {
            get { return BorderCharToString(LeftBorderChar); }
        }

        /// <summary>
        /// The character used on the right border of the table.
        /// </summary>
        public string RightBorder
        {
            get { return BorderCharToString(RightBorderChar); }
        }

        /// <summary>
        /// The top border of the table.
        /// </summary>
        public string TopBorder
        {
            get { return BorderCharToString(TopBorderChar); }
        }

        /// <summary>
        /// The bottom border of the table.
        /// </summary>
        protected readonly char BottomBorderChar;

        /// <summary>
        /// The character used to separate columns.
        /// </summary>
        protected readonly char ColumnSeparatorChar;

        /// <summary>
        /// The character used at corners of cells.
        /// </summary>
        protected readonly char CornerChar;

        /// <summary>
        /// The character used on the left border of the table.
        /// </summary>
        protected readonly char LeftBorderChar;

        /// <summary>
        /// The character used on the right border of the table.
        /// </summary>
        protected readonly char RightBorderChar;

        /// <summary>
        /// The top border of the table.
        /// </summary>
        protected readonly char TopBorderChar;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a formatter that produces completely undecorated tables.
        /// </summary>
        public TableFormatter() : this(' ')
        {
        }

        /// <summary>
        /// Create a formatter that produces tables with divisions between the
        /// columns and no other decorations.
        /// </summary>
        /// <param name="columnSeparator">
        /// The <see cref="System.Char"/> used to separate the columns.
        /// </param>
        public TableFormatter(char columnSeparator) : this('\0', columnSeparator, '\0', '\0', '\0', '\0')
        {
        }

        /// <summary>
        /// Create a formatter that produces tables with divisions between the
        /// columns and a vertical border.
        /// </summary>
        /// <param name="verticalBorder">
        /// The <see cref="System.Char"/> used for the tables' vertical border.
        /// </param>
        /// <param name="columnSeparator">
        /// The <see cref="System.Char"/> used to separate the columns.
        /// </param>
        public TableFormatter(char verticalBorder, char columnSeparator) : this(verticalBorder, columnSeparator, verticalBorder, '\0', '\0', '\0')
        {
        }

        /// <summary>
        /// Create a formatter that produces tables with divisions between the
        /// columns and a vertical border.
        /// </summary>
        /// <param name="leftBorder">
        /// The <see cref="System.Char"/> used for the tables' left border.
        /// </param>
        /// <param name="columnSeparator">
        /// The <see cref="System.Char"/> used to separate the columns.
        /// </param>
        /// <param name="rightBorder">
        /// The <see cref="System.Char"/> used for the table's right border.
        /// </param>
        public TableFormatter(char leftBorder, char columnSeparator, char rightBorder) : this(leftBorder, columnSeparator, rightBorder, '\0', '\0', '\0')
        {
        }

        /// <summary>
        /// Create a formatter that produces tables with divisions between the
        /// columns, a vertical border, and a horizontal border.
        /// </summary>
        /// <param name="leftBorder">
        /// The <see cref="System.Char"/> used for the tables' left border.
        /// </param>
        /// <param name="columnSeparator">
        /// The <see cref="System.Char"/> used to separate the columns.
        /// </param>
        /// <param name="rightBorder">
        /// The <see cref="System.Char"/> used for the tables' right border.
        /// </param>
        /// <param name="horizontalBorder">
        /// The <see cref="System.Char"/> used for the tables' horizontal
        /// borders.
        /// </param>
        public TableFormatter(char leftBorder, char columnSeparator, char rightBorder, char horizontalBorder) : this(leftBorder, columnSeparator, rightBorder, horizontalBorder, horizontalBorder, horizontalBorder)
        {
        }

        /// <summary>
        /// Create a formatter that produces tables with divisions between the
        /// columns, a vertical border, and a horizontal border.
        /// </summary>
        /// <param name="leftBorder">
        /// The <see cref="System.Char"/> used for the tables' left border.
        /// </param>
        /// <param name="columnSeparator">
        /// The <see cref="System.Char"/> used to separate the columns.
        /// </param>
        /// <param name="rightBorder">
        /// The <see cref="System.Char"/> used for the tables' right border.
        /// </param>
        /// <param name="topBorder">
        /// The <see cref="System.Char"/> used for the tables' top border.
        /// </param>
        /// <param name="bottomBorder">
        /// The <see cref="System.Char"/> used for the tables' bottom border.
        /// </param>
        /// <param name="corner">
        /// The <see cref="System.Char"/> used for intersections between
        /// borders.
        /// </param>
        public TableFormatter(char leftBorder, char columnSeparator, char rightBorder, char topBorder, char bottomBorder, char corner)
        {
            TopBorderChar = topBorder;
            BottomBorderChar = bottomBorder;
            LeftBorderChar = leftBorder;
            ColumnSeparatorChar = columnSeparator;
            RightBorderChar = rightBorder;
            if(HasColumnSeparator)
            {
                CornerChar = corner;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Format a list of objects into a table.
        /// </summary>
        /// <param name="table">
        /// A sequence of values to be formatted into a table.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="table"/> is <see langword="null" />.
        /// </exception>
        public IEnumerable<string> Format(DataTable table)
        {
            if(table == null)
            {
                throw new ArgumentNullException("table");
            }
            return Format(table, StringFormatting.LeftPadded);
        }

        /// <summary>
        /// Format a list of objects into a table.
        /// </summary>
        /// <param name="table">
        /// A sequence of values to be formatted into a table.
        /// </param>
        /// <param name="alignment">
        /// The function to apply to the cells in the table.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="table"/> or <paramref name="alignment"/> is <see langword="null" />.
        /// </exception>
        public IEnumerable<string> Format(DataTable table, Func<object, int, string> alignment)
        {
            if(table == null)
            {
                throw new ArgumentNullException("table");
            }
            if(alignment == null)
            {
                throw new ArgumentNullException("alignment");
            }
            return Format(table, Enumerable.Repeat(alignment, table.Columns.Count));
        }

        /// <summary>
        /// Format a list of objects into a table, using a special alignment for
        /// each column.
        /// </summary>
        /// <param name="table">
        /// A sequence of values to be formatted into a table.
        /// </param>
        /// <param name="alignments">
        /// The alignments for each column.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="table"/> or <paramref name="alignments"/> (or any member thereof) is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the number
        /// of columns does not match the number of <paramref name="alignments"/>.
        /// </exception>
        public IEnumerable<string> Format(DataTable table, IEnumerable<Func<object, int, string>> alignments)
        {
            if(table == null)
            {
                throw new ArgumentNullException("table");
            }
            if(alignments == null)
            {
                throw new ArgumentNullException("alignments");
            }
            if(alignments.Contains(null))
            {
                throw new ArgumentNullException("alignments");
            }
            int columnCount = table.Columns.Count;
            if(columnCount > alignments.Count())
            {
                throw new ArgumentException("The number of alignments must be at least as large as the number of columns.");
            }
            IEnumerable<DataRow > rows = table.Rows.Cast<DataRow>().ToList();
            IEnumerable<int > columnWidths = GetColumnWidths(table).ToList();
            if(HasTopBorder)
            {
                yield return GetRowSeparator(TopBorderChar, columnWidths);
            }
            foreach(DataRow row in rows)
            {
                yield return FormatRow(row, alignments, columnWidths);
            }
            if(HasBottomBorder)
            {
                yield return GetRowSeparator(BottomBorderChar, columnWidths);
            }
        }

        /// <summary>
        /// Formats a row.
        /// </summary>
        /// <param name="row">
        /// The row to format.
        /// </param>
        /// <param name="columnWidths">
        /// The widths of the columns to format the row.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="row"/> or <paramref name="columnWidths"/>
        /// is <see langword="null" />.
        /// </exception>
        protected string FormatRow(IEnumerable<string> row, IEnumerable<int> columnWidths)
        {
            if(row == null)
            {
                throw new ArgumentNullException("row");
            }
            if(columnWidths == null)
            {
                throw new ArgumentNullException("columnWidths");
            }
            return string.Format("{0}{1}{2}", LeftBorder, ColumnSeparator.Join(row), RightBorder);
        }

        /// <summary>
        /// Formats a <see cref="DataRow" />.
        /// </summary>
        /// <param name="row">
        /// The <see cref="DataRow" /> to format.
        /// </param>
        /// <param name="alignments">
        /// The alignments for each column.
        /// </param>
        /// <param name="columnWidths">
        /// The widths of the columns to format the row.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="row"/> (or <see cref="DataRow.ItemArray" /> thereon), <paramref name="alignments"/> (or any member thereof),
        /// or <paramref name="columnWidths"/>
        /// is <see langword="null" />.
        /// </exception>
        protected string FormatRow(DataRow row, IEnumerable<Func<object, int, string>> alignments, IEnumerable<int> columnWidths)
        {
            if(row == null)
            {
                throw new ArgumentNullException("row");
            }
            if(row.ItemArray == null)
            {
                throw new ArgumentNullException("row");
            }
            if(alignments == null)
            {
                throw new ArgumentNullException("alignments");
            }
            if(alignments.Contains(null))
            {
                throw new ArgumentNullException("alignments");
            }
            if(columnWidths == null)
            {
                throw new ArgumentNullException("columnWidths");
            }
            IEnumerable<string > cells = row.ItemArray.EquiZip(alignments, columnWidths, (value, align, width) => align(value, width));
            return FormatRow(cells, columnWidths);
        }

        /// <summary>
        /// Find the widths in characters for all the columns
        /// in the given <see cref="DataTable" />.
        /// </summary>
        /// <param name="table">
        /// The <see cref="DataTable" /> whose column widths to find.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="table"/> is <see langword="null" /> .
        /// </exception>
        protected IEnumerable<int> GetColumnWidths(DataTable table)
        {
            if(table == null)
            {
                throw new ArgumentNullException("table");
            }
            IEnumerable<DataColumn > columns = table.Columns.Cast<DataColumn>().ToList();
            IEnumerable<DataRow > rows = table.Rows.Cast<DataRow>().ToList();
            Func<DataRow, int, uint > rowLengthAt = (row, columnIndex) => {
                return (uint)row.ItemArray[columnIndex].ToString().Length;
            };
            return columns.Select((column, columnIndex) => (int)rows.MaxOrIdentity(row => rowLengthAt(row, columnIndex)));
        }

        /// <summary>
        /// Generate a separator to insert between rows.
        /// </summary>
        /// <param name="separator">
        /// A <see cref="char" /> to use in the separator.
        /// </param>
        /// <param name="columnWidths">
        /// The widths, in characters, of the columns in the table.
        /// </param>
        protected string GetRowSeparator(char separator, IEnumerable<int> columnWidths)
        {
            string text = Corner.Join(columnWidths.Select(width => new string(separator, width)));
            return Corner + text + Corner;
        }

        /// <summary>
        /// Convert a <see cref="char" /> used for a table border into a more useable <see cref="string" />.
        /// </summary>
        protected static string BorderCharToString(char borderChar)
        {
            return borderChar == '\0' ? string.Empty : borderChar.ToString();
        }

        #endregion
    }
}
