using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TextFormat.Table
{    
    /// <summary>
    /// A table formatter for tables with one-row headers and footers.
    /// </summary>
    public class LabeledTableFormatter : TableFormatter
    {
        /// <summary>
        /// The bottom border of the table.
        /// </summary>
        new protected internal char BottomBorder { get; set; }
        
        /// <summary>
        /// The top border of the table.
        /// </summary>
        new protected internal char TopBorder { get; set; }
        
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
        public LabeledTableFormatter (char leftBorder, char columnSeparator,
            char rightBorder, char topBorder, char bottomBorder, char bodyTop,
            char bodyBottom, char corner)
            : base(leftBorder, columnSeparator, rightBorder, bodyTop,
                bodyBottom, corner)
        {
            BottomBorder = bottomBorder;
            TopBorder = topBorder;
        }
        
        /// <summary>
        /// Format some objects into a table.
        /// </summary>
        /// <param name="values">
        /// A <see cref="IList<System.Object[]>"/> of values in the table.
        /// </param>
        /// <returns>
        /// A <see cref="IList<System.String>"/> of rows in the table.
        /// </returns>
        /// <exception cref="DimensionMismatchException">
        /// Thrown when any of the rows has the wrong number of cells.
        /// </exception>
        new public IList<string> Format (IList<IList> values)
        {
            return Format (values, null);
        }
        
        /// <summary>
        /// Format some objects into a table.
        /// </summary>
        /// <param name="values">
        /// A <see cref="IList<System.Object[]>"/> of values in the table.
        /// </param>
        /// <param name="alignments">
        /// A <see cref="IList<Alignment>"/> of alignments in the table.
        /// </param>
        /// <returns>
        /// A <see cref="IList<System.String>"/> of rows in the table.
        /// </returns>
        /// <exception cref="DimensionMismatchException">
        /// Thrown when any of the rows has the wrong number of cells.
        /// </exception>
        new public IList<string> Format (IList<IList> values,
            IList<Alignment> alignments)
        {
            return Format (null, values, alignments);
        }
        
        /// <summary>
        /// Format some objects into a table.
        /// </summary>
        /// <param name="header">
        /// A <see cref="System.Object[]"/>.  The heading of the table.
        /// </param>
        /// <param name="values">
        /// A <see cref="IList<System.Object[]>"/> of values in the table.
        /// </param>
        /// <returns>
        /// A <see cref="IList<System.String>"/> of rows in the table.
        /// </returns>
        /// <exception cref="DimensionMismatchException">
        /// Thrown when any of the rows has the wrong number of cells.
        /// </exception>
        public IList<string> Format (IList header, IList<IList> values)
        {
            return Format (header, values, (IList)null);
        }
        
        /// <summary>
        /// Format some objects into a table.
        /// </summary>
        /// <param name="header">
        /// A <see cref="System.Object[]"/>.  The heading of the table.
        /// </param>
        /// <param name="values">
        /// A <see cref="IList<System.Object[]>"/> of values in the table.
        /// </param>
        /// <param name="alignments">
        /// A <see cref="IList<Alignment>"/>.  The alignments of the columns.
        /// </param>
        /// <returns>
        /// A <see cref="IList<System.String>"/> of rows in the table.
        /// </returns>
        /// <exception cref="DimensionMismatchException">
        /// Thrown when any of the rows has the wrong number of cells.
        /// </exception>
        public IList<string> Format (IList header, IList<IList> values,
            IList<Alignment> alignments)
        {
            return Format (header, values, null, alignments);
        }
        
        /// <summary>
        /// Format some objects into a table.
        /// </summary>
        /// <param name="header">
        /// A <see cref="System.Object[]"/>.  The heading of the table.
        /// </param>
        /// <param name="values">
        /// A <see cref="IList<System.Object[]>"/> of values in the table.
        /// </param>
        /// <param name="footer">
        /// A <see cref="System.Object[]"/>.  The footer of the table.
        /// </param>
        /// <returns>
        /// A <see cref="IList<System.String>"/> of rows in the table.
        /// </returns>
        /// <exception cref="DimensionMismatchException">
        /// Thrown when any of the rows has the wrong number of cells.
        /// </exception>
        public IList<string> Format (IList header, IList<IList> values,
            IList footer)
        {
            return Format (header, values, footer, null);
        }
        
        /// <summary>
        /// Format some objects into a table.
        /// </summary>
        /// <param name="header">
        /// A <see cref="System.Object[]"/>.  The heading of the table.
        /// </param>
        /// <param name="values">
        /// A <see cref="IList<System.Object[]>"/> of values in teh table.
        /// </param>
        /// <param name="footer">
        /// A <see cref="System.Object[]"/>.  The footer of the table.
        /// </param>
        /// <param name="alignments">
        /// A <see cref="IList<Alignment>"/>.  The alignments in the table.
        /// </param>
        /// <returns>
        /// A <see cref="IList<System.String>"/> of rows in the table.
        /// </returns>
        /// <exception cref="DimensionMismatchException">
        /// Thrown when any of the rows has the wrong number of cells.
        /// </exception>
        public IList<string> Format (IList header, IList<IList> values,
            IList footer, IList<Alignment> alignments)
        {
            IList<Row> rows = FormatRows (header, values, footer, alignments);
            if (rows.Count == 0)
            {
                return new List<string> ();
            }
            return new Table (rows,
                DefaultMaxWidths (rows[0].ColumnCount)).Lines ();
        }
        
        /// <summary>
        /// Format some objects into a table.
        /// </summary>
        /// <param name="header">
        /// A <see cref="System.Object[]"/>.  The heading of the table.
        /// </param>
        /// <param name="values">
        /// A <see cref="IList<System.Object[]>"/> of values in teh table.
        /// </param>
        /// <param name="footer">
        /// A <see cref="System.Object[]"/>.  The footer of the table.
        /// </param>
        /// <param name="alignments">
        /// A <see cref="IList<Alignment>"/>.  The alignments in the table.
        /// </param>
        /// <returns>
        /// A <see cref="IList<Row>"/> of rows in the table.
        /// </returns>
        /// <exception cref="DimensionMismatchException">
        /// Thrown when any of the rows has the wrong number of cells.
        /// </exception>
        protected internal IList<Row> FormatRows (IList header,
            IList<IList> values, IList footer,
            IList<Alignment> alignments)
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
            if(header != null && header.Count != columnCount
                || footer != null && footer.Count != columnCount)
            {
                throw new DimensionMismatchException();
            }
            return FormatRows(header, values, footer, alignments, columnCount);
        }
                
        /// <summary>
        /// Format some objects into a table.
        /// </summary>
        /// <param name="header">
        /// A <see cref="System.Object[]"/>.  The heading of the table.
        /// </param>
        /// <param name="values">
        /// A <see cref="IList<System.Object[]>"/> of values in teh table.
        /// </param>
        /// <param name="footer">
        /// A <see cref="System.Object[]"/>.  The footer of the table.
        /// </param>
        /// <param name="alignments">
        /// A <see cref="IList<Alignment>"/>.  The alignments in the table.
        /// </param>
        /// <param name="columnCount">
        /// A <see cref="System.Int32"/>
        /// </param>
        /// <returns>
        /// A <see cref="IList<Row>"/> of rows in the table.
        /// </returns>
        protected internal IList<Row> FormatRows(IList header,
            IList<IList> values, IList footer,
            IList<Alignment> alignments, int columnCount)
        {
            List<Row> rows = new List<Row> ();
            IList<Row> bodyRows = base.FormatRows (values, alignments,
                columnCount);
            if (TopBorder != '\0')
            {
                rows.Add (RowSeparatorFactory.MakeInstance (TopBorder,
                        columnCount));
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
                rows.Add (RowSeparatorFactory.MakeInstance (BottomBorder,
                        columnCount));
            }
            return rows;
        }
    }
    
    /// <summary>
    /// A formatter that produces standard ASCII tables.
    /// </summary>
    public class PrettyTableFormatter : LabeledTableFormatter
    {
        /// <summary>
        /// Create a formatter that produces standard ASCII tables.
        /// </summary>
        public PrettyTableFormatter()
        : base('|', '|', '|', '-', '-', '-', '-', '+') {}
    }
    
    [TestFixture]
    public class TestLabeledTableFormatter
    {
        protected internal LabeledTableFormatter Formatter { get; set; }
        [SetUp]
        public void TestSetUp ()
        {
            Formatter = new PrettyTableFormatter ();
        }
        
        [Test]
        public void TestFormat ()
        {
            IList label = new ArrayList ();
            label.Add ("thing");
            label.Add ("stuff");
            IList<IList> empty = new List<IList> ();
            IList<string> actual;
            actual = Formatter.Format (empty);
            Assert.AreEqual (0, actual.Count);
            actual = Formatter.Format (label, empty);
            Assert.AreEqual (4, actual.Count);
            actual = Formatter.Format (null, empty, label);
            Assert.AreEqual (4, actual.Count);
            actual = Formatter.Format (label, empty, label);
            Assert.AreEqual (6, actual.Count);
            try
            {
                IList list = new ArrayList ();
                list.Add ("xkcd");
                actual = Formatter.Format (label, empty, list);
                Assert.Fail ("Attempting to use different sized headers and footers should not be allowed.");
            }
            catch (DimensionMismatchException) {}
        }
    }
}
