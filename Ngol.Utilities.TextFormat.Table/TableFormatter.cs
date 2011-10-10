using System;
using System.Collections.Generic;
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
        /// Gets a value indicating whether this instance has left border.
        /// </summary>
        /// <value>
        /// <see langword="true" /> if this instance has left border; otherwise, <see langword="false" />.
        /// </value>
        public bool HasLeftBorder
        {
            get { return LeftBorderChar != '\0'; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance has right border.
        /// </summary>
        /// <value>
        /// <see langword="true" /> if this instance has right border; otherwise, <see langword="false" />.
        /// </value>
        public bool HasRightBorder
        {
            get { return RightBorderChar != '\0'; }
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
        /// Format a <see cref="Table" /> to be printed on the console.
        /// </summary>
        /// <param name="table">
        /// The <see cref="Table" /> to format.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="table"/> is <see langword="null" />.
        /// </exception>
        public IEnumerable<string> Format(Table table)
        {
            if(table == null)
            {
                throw new ArgumentNullException("table");
            }
            return GetFormatterFor(table).Format();
        }

        /// <summary>
        /// Get a row separator for the specified <see cref="Table" />.
        /// </summary>
        /// <param name="table">
        /// The <see cref="Table" /> for which to get the row separator.
        /// </param>
        /// <param name="separator">
        /// The character to use to separate the two.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="table"/> is <see langword="null" />.
        /// </exception>
        public string GetRowSeparator(Table table, char separator)
        {
            if(table == null)
            {
                throw new ArgumentNullException("table");
            }
            return GetFormatterFor(table).GetRowSeparator(separator);
        }

        /// <summary>
        /// Convert a <see cref="char" /> used for a table border into a more useable <see cref="string" />.
        /// </summary>
        protected static string BorderCharToString(char borderChar)
        {
            return borderChar == '\0' ? string.Empty : borderChar.ToString();
        }

        private InnerFormatter GetFormatterFor(Table table)
        {
            if(table == null)
            {
                throw new ArgumentNullException("table");
            }
            return new InnerFormatter(table, LeftBorderChar, ColumnSeparatorChar, RightBorderChar, TopBorderChar, BottomBorderChar, CornerChar);
        }

        #endregion

        internal class InnerFormatter
        {
            #region Properties

            /// <summary>
            /// The bottom border of the table.
            /// </summary>
            internal readonly char BottomBorderChar;

            /// <summary>
            /// The character used to separate columns.
            /// </summary>
            internal string ColumnSeparator
            {
                get { return BorderCharToString(ColumnSeparatorChar); }
            }

            internal IEnumerable<int> ColumnWidths { get; set; }

            /// <summary>
            /// The character used to separate columns.
            /// </summary>
            internal readonly char ColumnSeparatorChar;

            /// <summary>
            /// The character used at the corner of cells.
            /// </summary>
            internal string Corner
            {
                get { return BorderCharToString(CornerChar); }
            }

            /// <summary>
            /// The character used at corners of cells.
            /// </summary>
            internal readonly char CornerChar;

            /// <summary>
            /// Gets a value indicating whether this instance has bottom border.
            /// </summary>
            /// <value>
            /// <see langword="true" /> if this instance has bottom border; otherwise, <see langword="false" />.
            /// </value>
            internal bool HasBottomBorder
            {
                get { return BottomBorderChar != '\0'; }
            }

            /// <summary>
            /// Gets a value indicating whether this instance has left border.
            /// </summary>
            /// <value>
            /// <see langword="true" /> if this instance has left border; otherwise, <see langword="false" />.
            /// </value>
            internal bool HasLeftBorder
            {
                get { return LeftBorderChar != '\0'; }
            }

            /// <summary>
            /// Gets a value indicating whether this instance has right border.
            /// </summary>
            /// <value>
            /// <see langword="true" /> if this instance has right border; otherwise, <see langword="false" />.
            /// </value>
            internal bool HasRightBorder
            {
                get { return RightBorderChar != '\0'; }
            }

            /// <summary>
            /// Does this table have a top border?
            /// </summary>
            internal bool HasTopBorder
            {
                get { return TopBorderChar != '\0'; }
            }

            /// <summary>
            /// The character used on the left border of the table.
            /// </summary>
            internal string LeftBorder
            {
                get { return BorderCharToString(LeftBorderChar); }
            }

            /// <summary>
            /// The character used on the left border of the table.
            /// </summary>
            internal readonly char LeftBorderChar;

            /// <summary>
            /// The character used on the right border of the table.
            /// </summary>
            internal string RightBorder
            {
                get { return BorderCharToString(RightBorderChar); }
            }

            /// <summary>
            /// The character used on the right border of the table.
            /// </summary>
            internal readonly char RightBorderChar;

            /// <summary>
            /// The <see cref="Table" /> which is being formatted.
            /// </summary>
            internal readonly Table Table;

            /// <summary>
            /// The top border of the table.
            /// </summary>
            internal readonly char TopBorderChar;

            #endregion

            #region Constructors

            internal InnerFormatter(Table table, char leftBorder, char columnSeparator, char rightBorder, char topBorder, char bottomBorder, char corner)
            {
                if(table == null)
                {
                    throw new ArgumentNullException("table");
                }
                Table = table;
                ColumnWidths = Table.GetColumnWidths().ToList();
                TopBorderChar = topBorder;
                BottomBorderChar = bottomBorder;
                LeftBorderChar = leftBorder;
                ColumnSeparatorChar = columnSeparator;
                RightBorderChar = rightBorder;
                CornerChar = corner;
            }

            #endregion

            #region Methods

            /// <summary>
            /// Format a <see cref="Table" /> to be printed on the console.
            /// </summary>
            internal IEnumerable<string> Format()
            {
                if(HasTopBorder)
                {
                    yield return GetRowSeparator(TopBorderChar);
                }
                foreach(Row row in Table.Rows)
                {
                    yield return FormatRow(row);
                }
                if(HasBottomBorder)
                {
                    yield return GetRowSeparator(BottomBorderChar);
                }
            }

            /// <summary>
            /// Formats a <see cref="Row" />.
            /// </summary>
            /// <param name="row">
            /// The <see cref="Row" /> to format.
            /// </param>
            /// <exception cref="ArgumentNullException">
            /// Thrown if <paramref name="row"/> is <see langword="null" />.
            /// </exception>
            internal string FormatRow(Row row)
            {
                if(row == null)
                {
                    throw new ArgumentNullException("row");
                }
                IEnumerable<string > cells = row.EquiZip(Table.Columns, ColumnWidths, (value, column, width) => column.Format(value, width));
                return LeftBorder + ColumnSeparator.Join(cells) + RightBorder;
            }

            /// <summary>
            /// Generate a separator to insert between rows.
            /// </summary>
            /// <param name="separator">
            /// A <see cref="char" /> to use in the separator.
            /// </param>
            internal string GetRowSeparator(char separator)
            {
                string text = Corner.Join(ColumnWidths.Select(width => new string(separator, width)));
                return string.Format("{0}{1}{2}", HasLeftBorder ? Corner : "", text, HasRightBorder ? Corner : "");
            }

            #endregion
        }
    }
}
