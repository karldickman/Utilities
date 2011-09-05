using System;
using System.Collections;
using System.Collections.Generic;

namespace TextFormat.Table
{
    /// <summary>
    /// Produces tables with a particular format when provided with the cells to
    /// do so.
    /// </summary>
    public class TableFormatter
    {
        /// <summary>
        /// The bottom border of the table.
        /// </summary>
        protected internal char BottomBorder { get; set; }
        
        /// <summary>
        /// The factory used to produce the rows.
        /// </summary>
        protected internal RowFactory RowFactory { get; set; }
        
        /// <summary>
        /// The factory used to produce the row separators.
        /// </summary>
        protected internal RowFactory RowSeparatorFactory { get; set; }
        
        /// <summary>
        /// The top border of the table.
        /// </summary>
        protected internal char TopBorder { get; set; }
        
        /// <summary>
        /// Create a formatter that produces completely undecorated tables.
        /// </summary>
        public TableFormatter() : this(' ') {}
        
        /// <summary>
        /// Create a formatter that produces tables with divisions between the
        /// columns and no other decorations.
        /// </summary>
        /// <param name="columnSeparator">
        /// The <see cref="System.Char"/> used to separate the columns.
        /// </param>
        public TableFormatter (char columnSeparator)
        : this('\0', columnSeparator, '\0', '\0', '\0', '\0') {}
        
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
        public TableFormatter(char verticalBorder, char columnSeparator)
            : this(verticalBorder, columnSeparator, verticalBorder, '\0', '\0',
                '\0') {}
        
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
        public TableFormatter(char leftBorder, char columnSeparator,
            char rightBorder)
            : this(leftBorder, columnSeparator, rightBorder, '\0', '\0', '\0') {}
        
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
        public TableFormatter(char leftBorder, char columnSeparator,
            char rightBorder, char horizontalBorder)
            : this(leftBorder, columnSeparator, rightBorder, horizontalBorder,
                horizontalBorder, horizontalBorder) {}
                
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
        public TableFormatter (char leftBorder, char columnSeparator,
            char rightBorder, char topBorder, char bottomBorder, char corner)
        {
            TopBorder = topBorder;
            BottomBorder = bottomBorder;
            RowFactory = new RowFactory (leftBorder, columnSeparator,
                rightBorder);
            char sepLeftBorder = corner;
            char sepColumnSeparator = corner;
            char sepRightBorder = corner;
            if (leftBorder == '\0')
            {
                sepLeftBorder = '\0';
            }
            if (columnSeparator == '\0')
            {
                sepColumnSeparator = '\0';
            }
            if (rightBorder == '\0')
            {
                sepRightBorder = '\0';
            }
            RowSeparatorFactory = new RowFactory (sepLeftBorder,
                sepColumnSeparator, sepRightBorder);
        }
        
        /// <summary>
        /// The default maximum width must be as large as possible so that
        /// normal values are less than the default.
        /// </summary>
        protected internal static IList<int> DefaultMaxWidths(int size)
        {
            IList<int> maxWidths = new List<int>();
            for (int i = 0; i < size; i++)
            {
                maxWidths.Add(int.MaxValue);
            }
            return maxWidths;
        }
        
        /// <summary>
        /// Format a list of objects into a table.
        /// </summary>
        /// <param name="values">
        /// A <see cref="IList<System.Object[]>"/> to be formatted into a table.
        /// </param>
        /// <returns>
        /// A <see cref="IList<System.String>"/> of rows in the table.
        /// </returns>
        public IList<string> Format (IList<IList> values)
        {
            return Format (values, null);
        }
        
        /// <summary>
        /// Format a list of objects into a table, using a special alignment for
        /// each column.
        /// </summary>
        /// <param name="values">
        /// A <see cref="IList<System.Object[]>"/> to be formatted into a table.
        /// </param>
        /// <param name="alignments">
        /// A <see cref="IList<Alignment>"/>.  The alignments for each column.
        /// </param>
        /// <returns>
        /// A <see cref="IList<System.String>"/> of rows in the table.
        /// </returns>
        public IList<string> Format (IList<IList> values,
            IList<Alignment> alignments)
        {
            IList<Row> rows = FormatRows (values, alignments);
            if (rows.Count == 0)
            {
                return new List<string> ();
            }
            return new Table(rows, DefaultMaxWidths(rows[0].ColumnCount)).Lines();
        }
        
        /// <summary>
        /// Format a list of objects into a table, using a special alignment for
        /// each column.
        /// </summary>
        /// <param name="values">
        /// A <see cref="IList<System.Object[]>"/> to be formatted into a table.
        /// </param>
        /// <param name="alignments">
        /// A <see cref="IList<Alignment>"/>.  The alignments for each column.
        /// </param>
        /// <returns>
        /// A <see cref="IList<Row>"/> of rows in the table.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the number of columns is unequal.
        /// </exception>
        protected internal IList<Row> FormatRows(IList<IList> values,
            IList<Alignment> alignments)
        {
            int columnCount;
            if(values.Count == 0)
            {
                columnCount = 1;
            }
            else
            {
                columnCount = values[0].Count;
            }
            foreach(IList row in values)
            {
                if(row.Count != columnCount)
                {
                    throw new ArgumentException(
                        "The number of cells in the row must match the " +
                        "declared number of columns.");
                }
            }
            return FormatRows(values, alignments, columnCount);
        }
        
        /// <summary>
        /// Format a list of objects into a table, using a special alignment for
        /// each column.
        /// </summary>
        /// <param name="values">
        /// A <see cref="IList<System.Object[]>"/> to be formatted into a table.
        /// </param>
        /// <param name="alignments">
        /// A <see cref="IList<Alignment>"/>.  The alignments for each column.
        /// </param>
        /// <param name="columnCount">
        /// The number of columns in the table.
        /// </param>
        /// <returns>
        /// A <see cref="IList<Row>"/> of rows in the table.
        /// </returns>
        protected internal IList<Row> FormatRows(IList<IList> values,
            IList<Alignment> alignments, int columnCount)
        {
            IList<Row> rows = new List<Row>();
            if(TopBorder != '\0')
            {
                rows.Add(RowSeparatorFactory.MakeInstance(TopBorder, columnCount));
            }
            foreach(IList valueRow in values)
            {
                rows.Add (RowFactory.MakeInstance (valueRow, alignments));
            }
            if(BottomBorder != '\0')
            {
                rows.Add(RowSeparatorFactory.MakeInstance(BottomBorder, columnCount));
            }
            return rows;
        }
    }
}
