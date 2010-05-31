using System;
using System.Collections.Generic;
using TextFormat.Table.Exceptions;

namespace TextFormat.Table
{
    public class Table
    {
        protected internal int[] MaxWidths { get; set; }
        protected internal IList<Row> Rows { get; set; }
        
        protected internal Table (IList<Row> rows, int[] maxWidths)
        {
            Rows = rows;
            MaxWidths = maxWidths;
            Pad();
        }
        
        /// <summary>
        /// Get the widths of all the columns in the table.
        /// </summary>
        public int[] ColumnWidths ()
        {
            int columnCount = Rows[0].ColumnCount, width;
            int[] columnWidths = new int[columnCount];
            foreach (Row row in Rows)
            {
                for (int i = 0; i < columnCount; i++)
                {
                    width = row.ColumnWidth (i);
                    if (width > columnWidths[i] && width < MaxWidths[i])
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
        public IList<string> Lines ()
        {
            IList<string> lines = new List<string> ();
            foreach (Row row in Rows)
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
            foreach (Row row in Rows)
            {
                row.Pad (columnWidths);
            }
        }
    }

    public class TableFormatter
    {
        protected internal char BottomBorder { get; set; }
        protected internal RowFactory RowFactory { get; set; }
        protected internal RowFactory RowSeperatorFactory { get; set; }
        protected internal char TopBorder { get; set; }
        
        public TableFormatter (char columnSeperator) : this('\0', columnSeperator, '\0', '\0', '\0', '\0') {}
        
        public TableFormatter (char leftBorder, char columnSeperator, char rightBorder, char topBorder, char bottomBorder, char corner)
        {
            TopBorder = topBorder;
            BottomBorder = bottomBorder;
            RowFactory = new RowFactory (leftBorder, columnSeperator, rightBorder);
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
            RowSeperatorFactory = new RowFactory (sepLeftBorder, sepColumnSeperator, sepRightBorder);
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
        
        public IList<string> Format (IList<object[]> values)
        {
            return Format (values, null);
        }
        
        public IList<string> Format (IList<object[]> values, Alignment[] alignments)
        {
            IList<Row> rows = FormatRows (values, alignments);
            return new Table(rows, DefaultMaxWidths(rows[0].ColumnCount)).Lines();
        }
        
        protected internal IList<Row> FormatRows(IList<object[]> values, Alignment[] alignments)
        {
            IList<Row> rows = new List<Row>();
            int columnCount = values[0].Length;
            if(TopBorder != '\0')
            {
                rows.Add(RowSeperatorFactory.MakeInstance(TopBorder, columnCount));
            }
            foreach(object[] valueRow in values)
            {
                rows.Add (RowFactory.MakeInstance (valueRow, alignments));
            }
            if(BottomBorder != '\0')
            {
                rows.Add(RowSeperatorFactory.MakeInstance(BottomBorder, columnCount));
            }
            return rows;
        }
    }
    
    public class LabeledTableFormatter : TableFormatter
    {
        new protected internal char BottomBorder { get; set; }
        new protected internal char TopBorder { get; set; }
        
        public LabeledTableFormatter (char leftBorder, char columnSeperator, char rightBorder, char topBorder, char bottomBorder, char bodyTop, char bodyBottom, char corner) : base(leftBorder, columnSeperator, rightBorder, bodyTop, bodyBottom, corner)
        {
            BottomBorder = bottomBorder;
            TopBorder = topBorder;
        }
        
        new public IList<string> Format (IList<object[]> values)
        {
            return Format (values, null);
        }
        
        new public IList<string> Format (IList<object[]> values, Alignment[] alignments)
        {
            return Format (null, values, alignments);
        }
        
        public IList<string> Format (object[] header, IList<object[]> values)
        {
            return Format (header, values, null);
        }
        
        public IList<string> Format (object[] header, IList<object[]> values, Alignment[] alignments)
        {
            return Format (header, values, null, alignments);
        }
        
        public IList<string> Format (object[] header, IList<object[]> values, object[] footer)
        {
            return Format (header, values, footer, null);
        }
        
        public IList<string> Format (object[] header, IList<object[]> values, object[] footer, Alignment[] alignments)
        {
            IList<Row> rows = FormatRows (header, values, footer, alignments);
            return new Table (rows, DefaultMaxWidths (rows[0].ColumnCount)).Lines ();
        }
        
        protected internal IList<Row> FormatRows (object[] header, IList<object[]> values, object[] footer, Alignment[] alignments)
        {
            int columnCount = values[0].Length;
            List<Row> rows = new List<Row> ();
            IList<Row> bodyRows = base.FormatRows (values, alignments);
            if (TopBorder != '\0')
            {
                rows.Add (RowSeperatorFactory.MakeInstance (TopBorder, columnCount));
            }
            if (header != null) 
            {
                rows.Add (RowFactory.MakeInstance (header));
            }
            else if(base.TopBorder != '\0')
            {
                bodyRows.RemoveAt (0);
            }
            rows.AddRange(bodyRows);
            if (footer != null)
            {
                rows.Add (RowFactory.MakeInstance (footer));
            }
            else if(base.BottomBorder != '\0')
            {
                rows.RemoveAt (rows.Count - 1);
            }
            if (BottomBorder != '\0')
            {
                rows.Add (RowSeperatorFactory.MakeInstance (BottomBorder, columnCount));
            }
            return rows;
        }
    }
    
    public class PrettyTableFormatter : LabeledTableFormatter
    {
        public PrettyTableFormatter() : base('|', '|', '|', '-', '-', '-', '-', '+') {}
    }
}
