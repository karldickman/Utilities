using Formatting.Table.Exceptions;
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
        public string[] Lines ()
        {
            string[] lines = new string[rows.Count];
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = rows[i].ToString ();
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
    
    public interface ITableFormatter
    {
        string[] Format(object[,] values);
        string[] Format(object[,] values, int[] maxWidths);
        string[] Format(object[] header, object[,] values);
        string[] Format(object[] header, object[,] values, int[] maxWidths);
        string[] Format(object[] header, object[,] values, object[] footer);
        string[] Format(object[] header, object[,] values, object[] footer, int[] maxWidths);
    }

    public class TableFormatter : ITableFormatter
    {
        private char bodyBottom;
        private char bodyTop;
        private char bottomBorder;
        private RowFactory rowFactory;
        private RowFactory rowSeperatorFactory;
        private char topBorder;
        
        public TableFormatter (char leftBorder, char columnSeperator, char rightBorder, char topBorder, char bottomBorder, char bodyTop, char bodyBottom, char corner)
        {
            this.topBorder = topBorder;
            this.bottomBorder = bottomBorder;
            this.bodyTop = bodyTop;
            this.bodyBottom = bodyBottom;
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
        
        public string[] Format (object[] header, object[,] values, object[] footer)
        {
            return Format (header, values, footer, DefaultMaxWidths(header.Length));
        }


        public string[] Format (object[] header, object[,] values, object[] footer, int[] maxWidths)
        {
            int columnCount = values.GetLength (1);
            List<Row> rows;
            Table table;
            if (header != null && header.Length != columnCount || footer != null && footer.Length != columnCount || maxWidths.Length != columnCount)
            {
                throw (new DimensionMismatchException ());
            }
            rows = FormatLine(header, topBorder, bodyTop);
            foreach (object[] row in values)
            {
                rows.Add (rowFactory.MakeInstance (row));
            }
            rows.AddRange (FormatLine(footer, bodyBottom, bottomBorder));
            table = new Table (rows, maxWidths);
            return table.Lines();
        }

        public string[] Format (object[] header, object[,] values)
        {
            return Format (header, values, DefaultMaxWidths (header.Length));
        }


        public string[] Format (object[] header, object[,] values, int[] maxWidths)
        {
            return Format (header, values, null, maxWidths);
        }


        public string[] Format (object[,] values)
        {
            return Format (values, DefaultMaxWidths(values.GetLength(1)));
        }


        public string[] Format (object[,] values, int[] maxWidths)
        {
            return Format (null, values, maxWidths);
        }
        
        /// <summary>
        /// Format a line of the table with bottom and top border.
        /// </summary>
        protected internal List<Row> FormatLine(object[] values, char topBorder, char bottomBorder)
        {
            List<Row> lines = new List<Row> ();
            if(values != null)
            {
                if (topBorder != '\0')
                {
                    lines.Add (rowSeperatorFactory.MakeInstance (topBorder, values.Length));
                }
                lines.Add (rowFactory.MakeInstance (values));
                if (bottomBorder != '\0')
                {
                    lines.Add (rowSeperatorFactory.MakeInstance (bottomBorder, values.Length));
                }
            }
            return lines;
        }
    }
    
    public class PrettyTableFormatter : TableFormatter
    {
        public PrettyTableFormatter() : base('|', '|', '|', '-', '-', '-', '-', '+') {}
    }
}
