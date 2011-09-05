using System;
using System.Collections;
using System.Collections.Generic;

namespace TextFormat.Table
{
    /// <summary>
    /// A factory for creating rows.
    /// </summary>
    public class RowFactory
    {
        /// <summary>
        /// The character used to separate cells in different columns.
        /// </summary>
        protected internal char ColumnSeparator { get; set; }
        
        /// <summary>
        /// The left border of the row.
        /// </summary>
        protected internal char LeftBorder { get; set; }
        
        /// <summary>
        /// The right border of the row.
        /// </summary>
        protected internal char RightBorder { get; set; }
        
        /// <summary>
        /// Create a factory that produces undecorated rows.
        /// </summary>
        public RowFactory() : this(' ') {}
        
        /// <summary>
        /// Create a factory that produces rows with the cells separated by a
        /// character.
        /// </summary>
        /// <param name="columnSeparator">
        /// The <see cref="System.Char"/> used to separate the columns.
        /// </param>
        public RowFactory(char columnSeparator)
        : this('\0', columnSeparator, '\0') {}
        
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
        public RowFactory(char border, char columnSeparator)
            : this(border, columnSeparator, border) {}
        
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
        public RowFactory (char leftBorder, char columnSeparator,
            char rightBorder)
        {
            LeftBorder = leftBorder;
            RightBorder = rightBorder;
            ColumnSeparator = columnSeparator;
        }
        
        /// <summary>
        /// Make a new horizontal separator.
        /// </summary>
        public Row MakeInstance (char rowSeparator, int columnCount)
        {
            IList<ICell> cells = new HorizontalCellSeparator[columnCount];
            for (int i = 0; i < columnCount; i++)
            {
                cells[i] = new HorizontalCellSeparator (rowSeparator);
            }
            return new Row (cells, Separators (columnCount));
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
        public Row MakeInstance (IList values)
        {
            IList<ICell> cells = new Cell[values.Count];
            for (int i = 0; i < cells.Count; i++) {
                cells[i] = new Cell (values[i]);
            }
            return MakeInstance (cells);
        }
        
        /// <summary>
        /// Make a new row of cells.
        /// </summary>
        /// <param name="values">
        /// A <see cref="System.Object[]"/>.  The values in the cells.
        /// </param>
        /// <param name="alignments">
        /// A <see cref="IList<Alignment>"/>.  The alignments of the cells.  Must be
        /// the same size as values.
        /// </param>
        /// <returns>
        /// A <see cref="Row"/> containing the given values.
        /// </returns>
        public Row MakeInstance (IList values, IList<Alignment> alignments)
        {
            if (alignments == null) 
            {
                return MakeInstance (values);
            }
            IList<ICell> cells = new Cell[values.Count];
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i] = new Cell (values[i], alignments[i]);
            }
            return MakeInstance (cells);
        }
        
        /// <summary>
        /// Make a new row of cells.
        /// </summary>
        /// <param name="cells">
        /// A <see cref="IList<ICell>"/>.  The cells in the row.
        /// </param>
        /// <returns>
        /// A <see cref="Row"/> containing the given cells.
        /// </returns>
        public Row MakeInstance (IList<ICell> cells)
        {
            return new Row (cells, Separators (cells.Count));
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
        public char[] Separators (int columnCount)
        {
            char[] separators = new char[columnCount + 1];
            for (int i = 0; i < columnCount; i++)
            {
                separators[i] = ColumnSeparator;
            }
            separators[0] = LeftBorder;
            separators[columnCount] = RightBorder;
            return separators;
        }
    }
}
