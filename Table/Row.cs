using Formatting.Table.Exceptions;
using System;
using System.Collections.Generic;

namespace Formatting.Table
{
    
    /// <summary>
    /// A row in the table.
    /// </summary>
    public class Row
    {
        private List<ICell> cells;
        private List<char> seperators;
        
        /// <summary>
        /// The cells in this row.
        /// </summary>
        public List<ICell> Cells
        {
            get { return cells; }
        }
        
        /// <summary>
        /// The vertical seperators between adjacent cells.
        /// </summary>
        public List<char> Seperators
        {
            get { return seperators; }
        }

        protected internal Row (List<ICell> cells, List<char> seperators)
        {
            this.cells = cells;
            this.seperators = seperators;
        }
        
        public override string ToString ()
        {
            int i;
            string result = "";
            for (i = 0; i < Cells.Count; i++)
            {
                result += Seperators[i];
                result += Cells[i];
            }
            return result + Seperators[i];
        }
    }
    
    /// <summary>
    /// A factory for creating rows.
    /// </summary>
    public class RowFactory
    {
        protected internal static List<char> MakeColumnSeperators(char leftBorder, char columnSeperator, char rightBorder, int columnCount)
        {
            List<char> columnSeperators = new List<char> ();
            columnSeperators.Add (leftBorder);
            for (int i = 0; i < columnCount - 1; i++)
            {
                columnSeperators.Add (columnSeperator);
            }
            columnSeperators.Add (rightBorder);
            return columnSeperators;
        }
        
        public Row MakeInstance (char rowSeperator, int columnCount)
        {
            return MakeInstance (rowSeperator, '\0', columnCount);
        }
        
        public Row MakeInstance (char rowSeperator, char columnSeperator, int columnCount)
        {
            return MakeInstance (rowSeperator, '\0', columnSeperator, '\0', columnCount);
        }
        
        public Row MakeInstance (char rowSeperator, char leftBorder, char columnSeperator, char rightBorder, int columnCount)
        {
            return MakeInstance (rowSeperator, MakeColumnSeperators (leftBorder, columnSeperator, rightBorder, columnCount));
        }
        
        /// <summary>
        /// Make a new horizontal seperator.
        /// </summary>
        public Row MakeInstance (char rowSeperator, List<char> columnSeperators)
        {
            List<ICell> cells = new List<ICell> ();
            for (int i = 0; i < columnSeperators.Count - 1; i++)
            {
                cells.Add (new HorizontalCellSeperator (rowSeperator));
            }
            return MakeInstance (cells, columnSeperators);
        }
        
        public Row MakeInstance (List<ICell> cells)
        {
            return MakeInstance (cells, '\0');
        }
        
        public Row MakeInstance (List<ICell> cells, char columnSeperator)
        {
            return MakeInstance (cells, '\0', columnSeperator, '\0');
        }
        
        /// <summary>
        /// Make a new row with the given values, cell alignments, and column seperators.
        /// </summary>
        public Row MakeInstance (List<ICell> cells, char leftBorder, char columnSeperator, char rightBorder)
        {
            return MakeInstance (cells, MakeColumnSeperators(leftBorder, columnSeperator, rightBorder, cells.Count));
        }
        
        /// <summary>
        /// Make a new row instance.
        /// </summary>
        public Row MakeInstance (List<ICell> cells, List<char> seperators)
        {
            if (seperators.Count + 1 != cells.Count)
            {
                throw (new InvalidRowException ());
            }
            return new Row (cells, seperators);
        }
    }
}
