using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using Ngol.Utilities.System;

namespace Ngol.Utilities.TextFormat.Table
{
    /// <summary>
    /// A factory for creating rows.
    /// </summary>
    public class RowFactory
    {
        #region Properties

        /// <summary>
        /// The character used to separate cells in different columns.
        /// </summary>
        protected char ColumnSeparator
        {
            get;
            set;
        }

        /// <summary>
        /// The left border of the row.
        /// </summary>
        protected char LeftBorder
        {
            get;
            set;
        }

        /// <summary>
        /// The right border of the row.
        /// </summary>
        protected char RightBorder
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a factory that produces undecorated rows.
        /// </summary>
        public RowFactory() : this(' ')
        {
        }

        /// <summary>
        /// Create a factory that produces rows with the cells separated by a
        /// character.
        /// </summary>
        /// <param name="columnSeparator">
        /// The <see cref="System.Char"/> used to separate the columns.
        /// </param>
        public RowFactory(char columnSeparator) : this('\0', columnSeparator, '\0')
        {
        }

        /// <summary>
        /// Create a factory that produces rows with a vertical border and cells
        /// separated by a character.
        /// </summary>
        /// <param name="border">
        /// The <see cref="System.Char"/> used for the rows' vertical border.
        /// </param>
        /// <param name="columnSeparator">
        /// The <see cref="System.Char"/> used to separate the columns.
        /// </param>
        public RowFactory(char border, char columnSeparator) : this(border, columnSeparator, border)
        {
        }

        /// <summary>
        /// Create a factory that produces rows with a vertical border and cells
        /// separated by a character.
        /// </summary>
        /// <param name="leftBorder">
        /// The <see cref="System.Char"/> used for the rows' left border.
        /// </param>
        /// <param name="columnSeparator">
        /// The <see cref="System.Char"/> used to separate the columns.
        /// </param>
        /// <param name="rightBorder">
        /// The <see cref="System.Char"/> used for the rows' right border.
        /// </param>
        public RowFactory(char leftBorder, char columnSeparator, char rightBorder)
        {
            LeftBorder = leftBorder;
            RightBorder = rightBorder;
            ColumnSeparator = columnSeparator;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Make a new horizontal separator.
        /// </summary>
        public Row MakeInstance(char rowSeparator, int columnCount)
        {
            IEnumerable<ICell> cells = Enumerable.Repeat<ICell>(new HorizontalCellSeparator(rowSeparator), columnCount);
            return new Row(cells, Separators(columnCount));
        }

        /// <summary>
        /// Make a new row of cells.
        /// </summary>
        /// <param name="values">
        /// A <see cref="System.Object[]"/>.  The values in the cells.
        /// </param>
        /// <returns>
        /// A <see cref="Row"/> containing the given values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="values"/> is <see langword="null" />.
        /// </exception>
        public Row MakeInstance(IEnumerable<object> values)
        {
            if(values == null)
                throw new ArgumentNullException("values");
            return MakeInstance(values.Select(value => (ICell)new Cell(value)));
        }

        /// <summary>
        /// Make a new row of cells.
        /// </summary>
        /// <param name="values">
        /// The values in the cells.
        /// </param>
        /// <param name="alignments">
        /// The alignments of the cells.  Must be
        /// the same size as values.
        /// </param>
        /// <returns>
        /// A <see cref="Row"/> containing the given values.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the number
        /// of columns does not match the number of <paramref name="alignments"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="values"/> is <see langword="null" />.
        /// </exception>
        public Row MakeInstance(IEnumerable<object> values, IEnumerable<Alignment> alignments)
        {
            if(values == null)
                throw new ArgumentNullException("values");
            if(alignments == null)
                return MakeInstance(values);
            if(values.Count() > 0 && values.Count() > alignments.Count())
                throw new ArgumentException("The number of alignments must be at least as large as the number of columns.");
            IEnumerable<ICell> cells = values.Zip(alignments.Take(values.Count()), (value, alignment) => (ICell)new Cell(value, alignment));
            return MakeInstance(cells);
        }

        /// <summary>
        /// Make a new row of cells.
        /// </summary>
        /// <param name="cells">
        /// The cells in the row.
        /// </param>
        /// <returns>
        /// A <see cref="Row"/> containing the given cells.
        /// </returns>
        public Row MakeInstance(IEnumerable<ICell> cells)
        {
            return new Row(cells, Separators(cells.Count()));
        }

        /// <summary>
        /// The separators needed for this row.
        /// </summary>
        /// <param name="columnCount">
        /// A <see cref="System.Int32"/>.  The number of columns needed.
        /// </param>
        /// <returns>
        /// A <see cref="System.Char[]"/>.  The separators for the required
        /// number of columns.
        /// </returns>
        public char[] Separators(int columnCount)
        {
            char[] separators = new char[columnCount + 1];
            for(int i = 0; i < columnCount; i++)
            {
                separators[i] = ColumnSeparator;
            }
            separators[0] = LeftBorder;
            separators[columnCount] = RightBorder;
            return separators;
        }

        #endregion
    }
}
