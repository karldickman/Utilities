using System;
using System.Collections;
using System.Collections.Generic;

namespace Ngol.Utilities.TextFormat.Table
{
    /// <summary>
    /// A table formatter for tables with one-row headers and footers.
    /// </summary>
    public class LabeledTableFormatter : TableFormatter
    {
        #region Constructors

        /// <summary>
        /// Create a formatter that produces tables with lots of decoration.
        /// </summary>
        /// <param name="leftBorder">
        /// The <see cref="System.Char"/> used for the tables' left border.
        /// </param>
        /// <param name="columnSeparator">
        /// The <see cref="System.Char"/> used to separate the columns.
        /// </param>
        /// <param name="rightBorder">
        /// The <see cref="System.Char"/> used for the tables' right border.
        /// </param>
        /// <param name="topBorder">
        /// The <see cref="System.Char"/> used for the tables' top border.
        /// </param>
        /// <param name="bottomBorder">
        /// The <see cref="System.Char"/> used for the tables' bottom border.
        /// </param>
        /// <param name="bodyTop">
        /// The <see cref="System.Char"/> used for the top border of the table
        /// body.
        /// </param>
        /// <param name="bodyBottom">
        /// The <see cref="System.Char"/> used for the bottom border of the
        /// table body.
        /// </param>
        /// <param name="corner">
        /// The <see cref="System.Char"/> used at intersections of borders.
        /// </param>
        public LabeledTableFormatter(char leftBorder, char columnSeparator, char rightBorder, char topBorder, char bottomBorder, char bodyTop, char bodyBottom, char corner) : base(leftBorder, columnSeparator, rightBorder, bodyTop, bodyBottom, corner)
        {
            BottomBorder = bottomBorder;
            TopBorder = topBorder;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Format some objects into a table.
        /// </summary>
        /// <param name="values">
        /// The sequence of values in the table.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when any of the rows has the wrong number of cells.
        /// </exception>
        public new IList<string> Format(IList<IList> values)
        {
            return Format(values, null);
        }

        /// <summary>
        /// Format some objects into a table.
        /// </summary>
        /// <param name="values">
        /// The sequence of values in the table.
        /// </param>
        /// <param name="alignments">
        /// The sequence of alignments in the table.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when any of the rows has the wrong number of cells.
        /// </exception>
        public new IList<string> Format(IList<IList> values, IList<Alignment> alignments)
        {
            return Format(null, values, alignments);
        }

        /// <summary>
        /// Format some objects into a table.
        /// </summary>
        /// <param name="header">
        /// The heading of the table.
        /// </param>
        /// <param name="values">
        /// The sequence of values in the table.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when any of the rows has the wrong number of cells.
        /// </exception>
        public IList<string> Format(IList header, IList<IList> values)
        {
            return Format(header, values, (IList)null);
        }

        /// <summary>
        /// Format some objects into a table.
        /// </summary>
        /// <param name="header">
        /// The heading of the table.
        /// </param>
        /// <param name="values">
        /// The sequence of values in the table.
        /// </param>
        /// <param name="alignments">
        /// The alignments of the columns.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when any of the rows has the wrong number of cells.
        /// </exception>
        public IList<string> Format(IList header, IList<IList> values, IList<Alignment> alignments)
        {
            return Format(header, values, null, alignments);
        }

        /// <summary>
        /// Format some objects into a table.
        /// </summary>
        /// <param name="header">
        /// The heading of the table.
        /// </param>
        /// <param name="values">
        /// The sequence of values in the table.
        /// </param>
        /// <param name="footer">
        /// The footer of the table.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when any of the rows has the wrong number of cells.
        /// </exception>
        public IList<string> Format(IList header, IList<IList> values, IList footer)
        {
            return Format(header, values, footer, null);
        }

        /// <summary>
        /// Format some objects into a table.
        /// </summary>
        /// <param name="header">
        /// The heading of the table.
        /// </param>
        /// <param name="values">
        /// The sequence of values in teh table.
        /// </param>
        /// <param name="footer">
        /// The footer of the table.
        /// </param>
        /// <param name="alignments">
        /// The alignments in the table.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when any of the rows has the wrong number of cells.
        /// </exception>
        public IList<string> Format(IList header, IList<IList> values, IList footer, IList<Alignment> alignments)
        {
            IList<Row> rows = FormatRows(header, values, footer, alignments);
            if(rows.Count == 0)
            {
                return new List<string>();
            }
            return new Table(rows, GetDefaultMaxWidths(rows[0].ColumnCount)).Lines();
        }

        /// <summary>
        /// Format some objects into a table.
        /// </summary>
        /// <param name="header">
        /// The heading of the table.
        /// </param>
        /// <param name="values">
        /// The sequence of values in teh table.
        /// </param>
        /// <param name="footer">
        /// The footer of the table.
        /// </param>
        /// <param name="alignments">
        /// The alignments in the table.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when any of the rows has the wrong number of cells.
        /// </exception>
        protected IList<Row> FormatRows(IList header, IList<IList> values, IList footer, IList<Alignment> alignments)
        {
            int columnCount;
            //Get the number of columns
            if(header != null)
            {
                columnCount = header.Count;
            }


            else if(footer != null)
            {
                columnCount = footer.Count;
            }
            else if(values.Count > 0)
            {
                columnCount = values[0].Count;
            }
            else
            {
                return new List<Row>();
            }
            if(header != null && header.Count != columnCount || footer != null && footer.Count != columnCount)
            {
                throw new ArgumentException("All column counts must be equal.");
            }
            return FormatRows(header, values, footer, alignments, columnCount);
        }

        /// <summary>
        /// Format some objects into a table.
        /// </summary>
        /// <param name="header">
        /// The heading of the table.
        /// </param>
        /// <param name="values">
        /// The sequence of values in teh table.
        /// </param>
        /// <param name="footer">
        /// The footer of the table.
        /// </param>
        /// <param name="alignments">
        /// The alignments in the table.
        /// </param>
        /// <param name="columnCount">
        /// The number of columns.
        /// </param>
        protected IList<Row> FormatRows(IList header, IList<IList> values, IList footer, IList<Alignment> alignments, int columnCount)
        {
            List<Row> rows = new List<Row>();
            IList<Row> bodyRows = base.FormatRows(values, alignments, columnCount);
            if(TopBorder != '\0')
            {
                rows.Add(RowSeparatorFactory.MakeInstance(TopBorder, columnCount));
            }
            if(header != null)
            {
                rows.Add(RowFactory.MakeInstance(header));
            }
            else if(base.TopBorder != '\0')
            {
                bodyRows.RemoveAt(0);
            }
            rows.AddRange(bodyRows);
            if(footer != null)
            {
                rows.Add(RowFactory.MakeInstance(footer));
            }
            else if(base.BottomBorder != '\0')
            {
                rows.RemoveAt(rows.Count - 1);
            }
            if(BottomBorder != '\0')
            {
                rows.Add(RowSeparatorFactory.MakeInstance(BottomBorder, columnCount));
            }
            return rows;
        }
        
        #endregion
    }
}
