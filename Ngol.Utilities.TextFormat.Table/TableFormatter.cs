using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        protected char BottomBorder
        {
            get;
            set;
        }

        /// <summary>
        /// The factory used to produce the rows.
        /// </summary>
        protected RowFactory RowFactory
        {
            get;
            set;
        }

        /// <summary>
        /// The factory used to produce the row separators.
        /// </summary>
        protected RowFactory RowSeparatorFactory
        {
            get;
            set;
        }

        /// <summary>
        /// The top border of the table.
        /// </summary>
        protected char TopBorder
        {
            get;
            set;
        }

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
            TopBorder = topBorder;
            BottomBorder = bottomBorder;
            RowFactory = new RowFactory(leftBorder, columnSeparator, rightBorder);
            char sepLeftBorder = corner;
            char sepColumnSeparator = corner;
            char sepRightBorder = corner;
            if(leftBorder == '\0')
            {
                sepLeftBorder = '\0';
            }
            if(columnSeparator == '\0')
            {
                sepColumnSeparator = '\0';
            }
            if(rightBorder == '\0')
            {
                sepRightBorder = '\0';
            }
            RowSeparatorFactory = new RowFactory(sepLeftBorder, sepColumnSeparator, sepRightBorder);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Format a list of objects into a table.
        /// </summary>
        /// <param name="values">
        /// A sequence of values to be formatted into a table.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="values"/> is <see langword="null" />.
        /// </exception>
        public Table Format(IEnumerable<IEnumerable<object>> values)
        {
            if(values == null)
                throw new ArgumentNullException("values");
            return Format(values, null);
        }

        /// <summary>
        /// Format a list of objects into a table, using a special alignment for
        /// each column.
        /// </summary>
        /// <param name="values">
        /// A sequence of values to be formatted into a table.
        /// </param>
        /// <param name="alignments">
        /// The alignments for each column.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="values"/> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the number
        /// of columns does not match the number of <paramref name="alignments"/>.
        /// </exception>
        public Table Format(IEnumerable<IEnumerable<object>> values, IEnumerable<Alignment> alignments)
        {
            if(values == null)
                throw new ArgumentNullException("values");
            if(values.Count() > 0 && values.First().Count() > alignments.Count())
                throw new ArgumentException("The number of alignments must be at least as large as the number of columns.");
            IEnumerable<Row> rows = FormatRows(values, alignments);
            return new Table(rows);
        }

        /// <summary>
        /// Format a list of objects into a table, using a special alignment for
        /// each column.
        /// </summary>
        /// <param name="values">
        /// A sequence of values to be formatted into a table.
        /// </param>
        /// <param name="alignments">
        /// The alignments for each column.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when the number of columns is unequal.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="values"/> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the number
        /// of columns does not match the number of <paramref name="alignments"/>.
        /// </exception>
        protected IEnumerable<Row> FormatRows(IEnumerable<IEnumerable<object>> values, IEnumerable<Alignment> alignments)
        {
            if(values == null)
                throw new ArgumentNullException("values");
            if(values.Count() > 0 && values.First().Count() > alignments.Count())
                throw new ArgumentException("The number of alignments must be at least as large as the number of columns.");
            if(values.Count() == 0)
                return Enumerable.Empty<Row>();
            IEnumerable<int> columnCounts = values.Select(row => row.Count()).Distinct();
            if(columnCounts.Count() != 1)
                throw new ArgumentException("Every row must have exactly the same number of cells.");
            int columnCount = columnCounts.Single();
            return FormatRows(values, alignments, columnCount);
        }

        /// <summary>
        /// Format a list of objects into a table, using a special alignment for
        /// each column.
        /// </summary>
        /// <param name="values">
        /// A sequence of values to be formatted into a table.
        /// </param>
        /// <param name="alignments">
        /// The alignments for each column.
        /// </param>
        /// <param name="columnCount">
        /// The number of columns in the table.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="values"/> is <see langword="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if the number
        /// of columns does not match the number of <paramref name="alignments"/>.
        /// </exception>
        protected IEnumerable<Row> FormatRows(IEnumerable<IEnumerable<object>> values, IEnumerable<Alignment> alignments, int columnCount)
        {
            if(values == null)
                throw new ArgumentNullException("values");
            if(values.Count() > 0 && values.First().Count() > alignments.Count())
                throw new ArgumentException("The number of alignments must be at least as large as the number of columns.");
            if(TopBorder != '\0')
            {
                yield return RowSeparatorFactory.MakeInstance(TopBorder, columnCount);
            }
            foreach(IEnumerable<object> valueRow in values)
            {
                yield return RowFactory.MakeInstance(valueRow, alignments);
            }
            if(BottomBorder != '\0')
            {
                yield return RowSeparatorFactory.MakeInstance(BottomBorder, columnCount);
            }
        }

        #endregion
    }
}
