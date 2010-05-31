using NUnit.Framework;
using System;
using System.Collections.Generic;
using TextFormat.Table.Exceptions;

namespace TextFormat.Table
{
    /// <summary>
    /// A row in the table.
    /// </summary>
    public class Row
    {
        protected internal ICell[] Cells { get; set; }
        protected internal char[] Seperators { get; set; }
        
        public int ColumnCount
        {
            get { return Cells.Length; }
        }
      
        internal Row (ICell[] cells, char[] seperators)
        {
            Cells = cells;
            Seperators = seperators;
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
        
        public override string ToString ()
        {
            int i;
            string result = "";
            for (i = 0; i < Cells.Length; i++)
            {
                if (Seperators[i] != '\0')
                {
                    result += Seperators[i];
                }
                result += Cells[i];
            }
            if (Seperators[i] != '\0')
            {
                result += Seperators[i];
            }
            return result;
        }
    }
    
    /// <summary>
    /// A factory for creating rows.
    /// </summary>
    public class RowFactory
    {
        protected internal char ColumnSeperator { get; set; }
        protected internal char LeftBorder { get; set; }
        protected internal char RightBorder { get; set; }
        
        public RowFactory(char columnSeperator) : this('\0', columnSeperator, '\0') {}
        
        public RowFactory (char leftBorder, char columnSeperator, char rightBorder)
        {
            LeftBorder = leftBorder;
            RightBorder = rightBorder;
            ColumnSeperator = columnSeperator;
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
                seperators[i] = ColumnSeperator;
            }
            seperators[0] = LeftBorder;
            seperators[columnCount] = RightBorder;
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
