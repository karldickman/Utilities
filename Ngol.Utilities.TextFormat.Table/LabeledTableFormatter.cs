using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Ngol.Utilities.Collections.Extensions;

namespace Ngol.Utilities.TextFormat.Table
{
    /// <summary>
    /// A table formatter for tables with one-row headers and footers.
    /// </summary>
    public class LabeledTableFormatter : TableFormatter
    {
        #region Properties

        /// <inheritdoc />
        public new string BottomBorder
        {
            get { return BorderCharToString(BottomBorderChar); }
        }

        /// <inheritdoc />
        public new bool HasBottomBorder
        {
            get { return BottomBorderChar != '\0'; }
        }

        /// <inheritdoc />
        public new bool HasTopBorder
        {
            get { return TopBorderChar != '\0'; }
        }

        /// <inheritdoc />
        public new string TopBorder
        {
            get { return BorderCharToString(TopBorderChar); }
        }

        /// <inheritdoc />
        protected new char BottomBorderChar { get; set; }

        /// <inheritdoc />
        protected new char TopBorderChar { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a formatter that produces tables with lots of decoration.
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
        /// <param name="bodyTop">
        /// The <see cref="System.Char"/> used for the top border of the table
        /// body.
        /// </param>
        /// <param name="bodyBottom">
        /// The <see cref="System.Char"/> used for the bottom border of the
        /// table body.
        /// </param>
        /// <param name="corner">
        /// The <see cref="System.Char"/> used at intersections of borders.
        /// </param>
        public LabeledTableFormatter(char leftBorder, char columnSeparator, char rightBorder, char topBorder, char bottomBorder, char bodyTop, char bodyBottom, char corner) : base(leftBorder, columnSeparator, rightBorder, bodyTop, bodyBottom, corner)
        {
            BottomBorderChar = bottomBorder;
            TopBorderChar = topBorder;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Format some objects into a table.
        /// </summary>
        /// <param name="table">
        /// The sequence of values in the table.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when any of the rows has the wrong number of cells.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="values"/> is <see langword="null" />.
        /// </exception>
        public new IEnumerable<string> Format(DataTable table)
        {
            if(table == null)
            {
                throw new ArgumentNullException("values");
            }
            return Format(table, StringFormatting.LeftPadded);
        }

        /// <summary>
        /// Format some objects into a table.
        /// </summary>
        /// <param name="table">
        /// The sequence of values in the table.
        /// </param>
        /// <param name="alignments">
        /// The sequence of alignments in the table.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when any of the rows has the wrong number of cells, or if the number
        /// of columns does not match the number of <paramref name="alignments"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="values"/> is <see langword="null" />.
        /// </exception>
        public new IEnumerable<string> Format(DataTable table, IEnumerable<Func<object, int, string>> alignments)
        {
            if(table == null)
            {
                throw new ArgumentNullException("table");
            }
            if(alignments == null)
            {
                throw new ArgumentNullException("alignments");
            }
            if(table.Columns.Count > alignments.Count())
            {
                throw new ArgumentException("The number of alignments must be at least as large as the number of columns.");
            }
            IEnumerable<string > headers = GetHeaders(table);
            IEnumerable<int > columnWidths = GetColumnWidths(table);
            if(HasTopBorder)
            {
                yield return GetRowSeparator(TopBorderChar, columnWidths);
            }
            yield return FormatRow(headers, columnWidths);
            foreach(string row in base.Format(table, alignments))
            {
                yield return row;
            }
            if(HasBottomBorder)
            {
                yield return GetRowSeparator(BottomBorderChar, columnWidths);
            }
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
        protected new IEnumerable<int> GetColumnWidths(DataTable table)
        {
            if(table == null)
            {
                throw new ArgumentNullException("table");
            }
            IEnumerable<int > columnWidths = base.GetColumnWidths(table);
            IEnumerable<int> headerWidths = GetHeaders(table).Select(header => header.ToString().Length);
            return columnWidths.Zip(headerWidths, Math.Max);
        }

        IEnumerable<string> GetHeaders(DataTable table)
        {
            if(table == null)
            {
                throw new ArgumentNullException("table");
            }
            return table.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
        }

        #endregion
    }
}
