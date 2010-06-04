using System;
using System.Collections.Generic;
using TextFormat.Table.Exceptions;

namespace TextFormat.Table
{
    /// <summary>
    /// A row in a table.
    /// </summary>
    public class Row
    {
        /// <summary>
        /// The cells in the row.
        /// </summary>
        protected internal ICell[] Cells { get; set; }
        
        /// <summary>
        /// The separators between the cells.
        /// </summary>
        protected internal char[] Separators { get; set; }
        
        /// <summary>
        /// The number of columns in the table.
        /// </summary>
        public int ColumnCount
        {
            get { return Cells.Length; }
        }
      
        /// <summary>
        /// Create a new row.
        /// </summary>
        /// <param name="cells">
        /// A <see cref="ICell[]"/>.  The cells in the row.
        /// </param>
        /// <param name="separators">
        /// A <see cref="System.Char[]"/>.  The separators between the cells.
        /// </param>
        protected internal Row (ICell[] cells, char[] separators)
        {
            Cells = cells;
            Separators = separators;
        }
        
        /// <summary>
        /// Get the width of colum i, zero-indexed.
        /// </summary>
        public int ColumnWidth (int column)
        {
            return Cells[column].Width ();
        }
        
        /// <summary>
        /// Pad all cells in the row to the given widths.
        /// </summary>
        public void Pad (int[] widths)
        {
            if (Cells.Length != widths.Length)
            {
                throw (new DimensionMismatchException ());
            }
            for (int i = 0; i < Cells.Length; i++)
            {
                Cells[i].Pad (widths[i]);
            }
        }
        
        /// <summary>
        /// Format the row.
        /// </summary>
        public override string ToString ()
        {
            int i;
            string result = "";
            for (i = 0; i < Cells.Length; i++)
            {
                if (Separators[i] != '\0')
                {
                    result += Separators[i];
                }
                result += Cells[i];
            }
            if (Separators[i] != '\0')
            {
                result += Separators[i];
            }
            return result;
        }
    }
}
