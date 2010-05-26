using Formatting.Table.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Formatting.Table
{
    
    /// <summary>
    /// A row in the table.
    /// </summary>
    public class Row
    {
        private ICell[] cells;
        private char[] seperators;
        
        public int ColumnCount
        {
            get { return cells.Length; }
        }
      
        internal Row (ICell[] cells, char[] seperators)
        {
            this.cells = cells;
            this.seperators = seperators;
        }
        
        /// <summary>
        /// Get the width of colum i, zero-indexed.
        /// </summary>
        public int ColumnWidth (int column)
        {
            return cells[column].Width ();
        }
        
        /// <summary>
        /// Pad all cells in the row to the given widths.
        /// </summary>
        public void Pad (int[] widths)
        {
            if (cells.Length != widths.Length)
            {
                throw (new DimensionMismatchException ());
            }
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i].Pad (widths[i]);
            }
        }
        
        public override string ToString ()
        {
            int i;
            string result = "";
            for (i = 0; i < cells.Length; i++)
            {
                if (seperators[i] != '\0')
                {
                    result += seperators[i];
                }
                result += cells[i];
            }
            if (seperators[i] != '\0')
            {
                result += seperators[i];
            }
            return result;
        }
    }
    
    /// <summary>
    /// A factory for creating rows.
    /// </summary>
    public class RowFactory
    {
        private char columnSeperator;
        private char leftBorder;
        private char rightBorder;
        
        public RowFactory(char columnSeperator) : this('\0', columnSeperator, '\0') {}
        
        public RowFactory (char leftBorder, char columnSeperator, char rightBorder)
        {
            this.leftBorder = leftBorder;
            this.rightBorder = rightBorder;
            this.columnSeperator = columnSeperator;
        }
        
        /// <summary>
        /// Make a new horizontal seperator.
        /// </summary>
        public Row MakeInstance (char rowSeperator, int columnCount)
        {
            ICell[] cells = new HorizontalCellSeperator[columnCount];
            for (int i = 0; i < columnCount; i++)
            {
                cells[i] = new HorizontalCellSeperator (rowSeperator);
            }
            return new Row (cells, Seperators (columnCount));
        }
        
        public Row MakeInstance (object[] values)
        {
            ICell[] cells = new Cell[values.Length];
            for (int i = 0; i < cells.Length; i++) {
                cells[i] = new Cell (values[i]);
            }
            return MakeInstance (cells);
        }
        
        public Row MakeInstance (object[] values, Alignment[] alignments)
        {
            if (alignments == null) 
            {
               return MakeInstance (values);
            }
            ICell[] cells = new Cell[values.Length];
            for (int i = 0; i < cells.Length; i++)
            {
                cells[i] = new Cell (values[i], alignments[i]);
            }
            return MakeInstance (cells);
        }
        
        /// <summary>
        /// Make a new row with the given cells.
        /// </summary>
        public Row MakeInstance (ICell[] cells)
        {
            return new Row (cells, Seperators (cells.Length));
        }
        
        /// <summary>
        /// The seperators needed for a row with the given number of columns.
        /// </summary>
        public char[] Seperators (int columnCount)
        {
            char[] seperators = new char[columnCount + 1];
            for (int i = 0; i < columnCount; i++)
            {
                seperators[i] = columnSeperator;
            }
            seperators[0] = leftBorder;
            seperators[columnCount] = rightBorder;
            return seperators;
        }
    }
    
    [TestFixture]
    public class TestRowFactory
    {
        private RowFactory rowFactory;
        
        [SetUp]
        public void Setup ()
        {
            rowFactory = new RowFactory ('/', '|', 'X');
        }
        
        [Test]
        public void TestSeperators()
        {
            char[] expected = {'/', '|', '|', '|', '|', 'X'};
            char[] actual = rowFactory.Seperators(5);
            Assert.AreEqual(expected.Length, actual.Length);
            for(int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
    }
}
