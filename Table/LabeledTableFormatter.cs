using NUnit.Framework;
using System;
using System.Collections.Generic;
using TextFormat.Table.Exceptions;

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
        
        new public IList<string> Format (IList<object[]> values)
        {
            return Format (values, null);
        }
        
        new public IList<string> Format (IList<object[]> values,
            Alignment[] alignments)
        {
            return Format (null, values, alignments);
        }
        
        public IList<string> Format (object[] header, IList<object[]> values)
        {
            return Format (header, values, null);
        }
        
        /// <summary>
        /// Format the objects into a table.
        /// </summary>
        /// <param name="header">
        /// A <see cref="System.Object[]"/>.  The heading of the table.
        /// </param>
        /// <param name="values">
        /// A <see cref="IList<System.Object[]>"/> of values in the table.
        /// </param>
        /// <param name="alignments">
        /// A <see cref="Alignment[]"/>.  The alignments of the columns.
        /// </param>
        /// <returns>
        /// A <see cref="IList<System.String>"/> of rows in the table.
        /// </returns>
        public IList<string> Format (object[] header, IList<object[]> values,
            Alignment[] alignments)
        {
            return Format (header, values, null, alignments);
        }
        
        /// <summary>
        /// Format the objects into a table.
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
        public IList<string> Format (object[] header, IList<object[]> values,
            object[] footer)
        {
            return Format (header, values, footer, null);
        }
        
        /// <summary>
        /// Format the objects into a table.
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
        /// A <see cref="Alignment[]"/>.  The alignments in the table.
        /// </param>
        /// <returns>
        /// A <see cref="IList<System.String>"/> of rows in the table.
        /// </returns>
        public IList<string> Format (object[] header, IList<object[]> values,
            object[] footer, Alignment[] alignments)
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
        /// Format the objects into a table.
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
        /// A <see cref="Alignment[]"/>.  The alignments in the table.
        /// </param>
        /// <returns>
        /// A <see cref="IList<Row>"/> of rows in the table.
        /// </returns>
        protected internal IList<Row> FormatRows (object[] header,
            IList<object[]> values, object[] footer, Alignment[] alignments)
        {
            int columnCount;
            //Get the number of columns
            if(header != null)
            {
                columnCount = header.Length;
            }
            else if(footer != null)
            {
                columnCount = footer.Length;
            }
            else if(values.Count > 0)
            {
                columnCount = values[0].Length;
            }
            else
            {
                return new List<Row>();
            }
            if(header != null && header.Length != columnCount
                || footer != null && footer.Length != columnCount)
            {
                throw new DimensionMismatchException();
            }
            return FormatRows(header, values, footer, alignments, columnCount);
        }
                
        /// <summary>
        /// Format the objects into a table.
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
        /// A <see cref="Alignment[]"/>.  The alignments in the table.
        /// </param>
        /// <param name="columnCount">
        /// A <see cref="System.Int32"/>
        /// </param>
        /// <returns>
        /// A <see cref="IList<Row>"/> of rows in the table.
        /// </returns>
        protected internal IList<Row> FormatRows(object[] header,
            IList<object[]> values, object[] footer, Alignment[] alignments,
            int columnCount)
        {
            List<Row> rows = new List<Row> ();
            IList<Row> bodyRows = base.FormatRows (values, alignments, columnCount);
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
            object[] label = { "thing", "stuff" };
            IList<object[]> empty = new List<object[]> ();
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
                actual = Formatter.Format (label, empty, new object[] { "xkcd" });
                Assert.Fail ("Attempting to use different sized headers and footers should not be allowed.");
            }
            catch (DimensionMismatchException) {}
        }
    }
}
