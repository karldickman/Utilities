using System;
using System.Collections.Generic;

namespace Ngol.Utilities.TextFormat.Table
{
    /// <summary>
    /// A row in a table.
    /// </summary>
    public class Row
    {
        #region Properties

        /// <summary>
        /// The cells in the row.
        /// </summary>
        IList<ICell> Cells
        {
            get;
            set;
        }

        /// <summary>
        /// The separators between the cells.
        /// </summary>
        IList<char> Separators
        {
            get;
            set;
        }

        /// <summary>
        /// The number of columns in the table.
        /// </summary>
        public int ColumnCount
        {
            get { return Cells.Count; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new row.
        /// </summary>
        /// <param name="cells">
        /// The cells in the row.
        /// </param>
        /// <param name="separators">
        /// The separators between the cells.
        /// </param>
        public Row(IList<ICell> cells, IList<char> separators)
        {
            Cells = cells;
            Separators = separators;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get the width of colum i, zero-indexed.
        /// </summary>
        public int ColumnWidth(int column)
        {
            return Cells[column].GetWidth();
        }

        /// <summary>
        /// Pad all cells in the row to the given widths.
        /// </summary>
        /// <exception cname="ArgumentException">
        /// Raised when an incorrect number of width is provided for the cells
        /// in the row.
        /// </exception>
        public void Pad(IList<int> widths)
        {
            if(Cells.Count != widths.Count)
            {
                throw new ArgumentException("The number of cells must match the nubmer of widths.");
            }
            for(int i = 0; i < Cells.Count; i++)
            {
                Cells[i].Pad(widths[i]);
            }
        }

        /// <summary>
        /// Format the row.
        /// </summary>
        public override string ToString()
        {
            int i;
            string result = "";
            for(i = 0; i < Cells.Count; i++)
            {
                if(Separators[i] != '\0')
                {
                    result += Separators[i];
                }
                result += Cells[i];
            }
            if(Separators[i] != '\0')
            {
                result += Separators[i];
            }
            return result;
        }

        #endregion
    }
}
