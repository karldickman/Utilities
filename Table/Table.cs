using Formatting.Table.Exceptions;
using System;
using System.Collections.Generic;

namespace Formatting.Table
{
    
    public class Table
    {
        private int[] maxWidths;
        private List<Row> rows;
        
        protected internal Table (List<Row> rows, int[] maxWidths)
        {
            this.rows = rows;
            this.maxWidths = maxWidths;
            Pad();
        }
        
        /// <summary>
        /// Get the widths of all the columns in the table.
        /// </summary>
        public int[] ColumnWidths ()
        {
            int columnCount = rows[0].ColumnCount, width;
            int[] columnWidths = new int[columnCount];
            foreach (Row row in rows) 
            {
                for (int i = 0; i < columnCount; i++)
                {
                    width = row.ColumnWidth (i);
                    if (width > columnWidths[i] && width < maxWidths[i])
                    {
                        columnWidths[i] = width;
                    }
                }
            }
            return columnWidths;
        }
        
        /// <summary>
        /// Get all the physical lines in a string representation of the table.
        /// </summary>
        public List<string> Lines ()
        {
            List<string> lines = new List<string> ();
            foreach (Row row in rows)
            {
                lines.Add (row.ToString ());
            }
            return lines;
        }
        
        /// <summary>
        /// Pad all cells of the table to the width of their containing rows.
        /// </summary>
        public void Pad ()
        {
            int[] columnWidths = ColumnWidths ();
            foreach (Row row in rows)
            {
                row.Pad (columnWidths);
            }
        }
    }

    public class TableFormatter
    {
        private char bottomBorder;
        private RowFactory rowFactory;
        private RowFactory rowSeperatorFactory;
        private char topBorder;
        
        protected internal char BottomBorder
        {
            get { return bottomBorder; }
        }
        
        protected internal RowFactory RowFactory
        {
            get { return rowFactory; }
        }
        
        protected internal RowFactory RowSeperatorFactory
        {
            get { return rowSeperatorFactory; }
        }
        
        protected internal char TopBorder
        {
             get { return topBorder; }
        }
        
        public TableFormatter (char leftBorder, char columnSeperator, char rightBorder, char topBorder, char bottomBorder, char corner)
        {
            this.topBorder = topBorder;
            this.bottomBorder = bottomBorder;
            rowFactory = new RowFactory (leftBorder, columnSeperator, rightBorder);
            char sepLeftBorder = corner, sepColumnSeperator = corner, sepRightBorder = corner;
            if (leftBorder == '\0')
            {
                sepLeftBorder = '\0';
            }
            if (columnSeperator == '\0')
            {
                sepColumnSeperator = '\0';
            }
            if (rightBorder == '\0')
            {
                sepRightBorder = '\0';
            }
            rowSeperatorFactory = new RowFactory (sepLeftBorder, sepColumnSeperator, sepRightBorder);
        }
        
        protected internal static int[] DefaultMaxWidths(int size)
        {
            int[] maxWidths = new int[size];
            for (int i = 0; i < size; i++)
            {
                maxWidths[i] = int.MaxValue;
            }
            return maxWidths;
        }
        
        public List<string> Format (List<object[]> values)
        {
            return Format (values, null);
        }
        
        public List<string> Format (List<object[]> values, Alignment[] alignments)
        {
            List<Row> rows = FormatRows (values, alignments);
            return new Table(rows, DefaultMaxWidths(rows[0].ColumnCount)).Lines();
        }
        
        protected internal List<Row> FormatRows(List<object[]> values, Alignment[] alignments)
        {
            List<Row> rows = new List<Row>();
            int columnCount = values[0].Length;
            if(topBorder != '\0')
            {
                rows.Add(rowSeperatorFactory.MakeInstance(topBorder, columnCount));
            }
            foreach(object[] valueRow in values)
            {
                rows.Add (rowFactory.MakeInstance (valueRow, alignments));
            }
            if(bottomBorder != '\0')
            {
                rows.Add(rowSeperatorFactory.MakeInstance(bottomBorder, columnCount));
            }
            return rows;
        }
    }
    
    public class LabeledTableFormatter : TableFormatter
    {
        private char bottomBorder;
        private char topBorder;
        
        public LabeledTableFormatter (char leftBorder, char columnSeperator, char rightBorder, char topBorder, char bottomBorder, char bodyTop, char bodyBottom, char corner) : base(leftBorder, columnSeperator, rightBorder, bodyTop, bodyBottom, corner)
        {
            this.bottomBorder = bottomBorder;
            this.topBorder = topBorder;
        }
        
        public new List<string> Format (List<object[]> values)
        {
            return Format (values, null);
        }
        
        public new List<string> Format (List<object[]> values, Alignment[] alignments)
        {
            return Format (null, values, alignments);
        }
        
        public List<string> Format (object[] header, List<object[]> values)
        {
            return Format (header, values, null);
        }
        
        public List<string> Format (object[] header, List<object[]> values, Alignment[] alignments)
        {
            return Format (header, values, null, alignments);
        }
        
        public List<string> Format (object[] header, List<object[]> values, object[] footer)
        {
            return Format (header, values, footer, null);
        }
        
        public List<string> Format (object[] header, List<object[]> values, object[] footer, Alignment[] alignments)
        {
            List<Row> rows = FormatRows (header, values, footer, alignments);
            return new Table (rows, DefaultMaxWidths (rows[0].ColumnCount)).Lines ();
        }
        
        protected internal List<Row> FormatRows (object[] header, List<object[]> values, object[] footer, Alignment[] alignments)
        {
            int columnCount = values[0].Length;
            List<Row> rows = new List<Row> ();
            List<Row> bodyRows = base.FormatRows (values, alignments);
            if (topBorder != '\0')
            {
                rows.Add (RowSeperatorFactory.MakeInstance (topBorder, columnCount));
            }
            if (header != null) 
            {
                rows.Add (RowFactory.MakeInstance (header));
            }
            else if(base.TopBorder != '\0')
            {
                bodyRows.RemoveAt (0);
            }
            rows.AddRange (bodyRows);
            if (footer != null)
            {
                rows.Add (RowFactory.MakeInstance (footer));
            }
            else if(base.BottomBorder != '\0')
            {
                rows.RemoveAt (rows.Count - 1);
            }
            if (bottomBorder != '\0')
            {
                rows.Add (RowSeperatorFactory.MakeInstance (bottomBorder, columnCount));
            }
            return rows;
        }
    }
    
    public class PrettyTableFormatter : LabeledTableFormatter
    {
        public PrettyTableFormatter() : base('|', '|', '|', '-', '-', '-', '-', '+') {}
    }
}
