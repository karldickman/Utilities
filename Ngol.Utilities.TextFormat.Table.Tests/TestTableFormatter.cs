using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Ngol.Utilities.NUnit;
using Ngol.Utilities.Reflection.Extensions;
using Ngol.Utilities.TextFormat.Table;
using NUnit.Framework;

namespace Ngol.Utilities.TextFormat.Table.Tests
{
    [TestFixture]
    public class TestTableFormatter
    {
        #region Properties

        protected TableFormatter Formatter
        {
            get;
            set;
        }

        protected TableFormatter FormatterT
        {
            get;
            set;
        }

        protected TableFormatter FormatterB
        {
            get;
            set;
        }

        protected TableFormatter FormatterTB
        {
            get;
            set;
        }

        #endregion

        #region Set up

        [SetUp]
        public void SetUp()
        {
            Formatter = new TableFormatter();
            FormatterT = new TableFormatter('\0', ' ', '\0', '-', '\0', '+');
            FormatterB = new TableFormatter('\0', ' ', '\0', '\0', '-', '+');
            FormatterTB = new TableFormatter('\0', ' ', '\0', '-');
        }

        #endregion

        #region Tests

        [Test]
        public void TestFormatNoColumns()
        {
            Table table = new Table();
            table.Rows.Add();
            table.Rows.Add();
            IEnumerable<string > actual = Formatter.Format(table);
            MoreAssert.HasCount(2, actual);
            foreach(string line in actual)
            {
                MoreAssert.IsEmpty(line);
            }
        }

        [Test]
        public void TestFormatNoRows()
        {
            Table table = new Table();
            table.Columns.Add("x");
            table.Columns.Add("y");
            IEnumerable<string > actual = Formatter.Format(table);
            MoreAssert.IsEmpty(actual);
            actual = FormatterT.Format(table);
            MoreAssert.HasCount(1, actual);
            Assert.AreEqual("+", actual.First());
            actual = FormatterTB.Format(table);
            MoreAssert.HasCount(2, actual);
            foreach(string line in actual)
            {
                Assert.AreEqual("-", line);
            }
        }

        [Test]
        public void TestFormat2x2()
        {
            Table table = new Table();
            table.Columns.Add("thing", typeof(int));
            table.Columns.Add("stuff", typeof(char));
            table.Rows.Add(1, 'a');
            table.Rows.Add(2, 'b');
            table.Rows.Add(3, 'c');
            IEnumerable<string > expected = new List<string> { "1 a", "2 b", "3 c", };
            IEnumerable<string > actual = Formatter.Format(table);
            MoreAssert.CollectionsEqual(expected, actual);
        }

        [Test]
        public void TestGetRowSeparator()
        {
            Table table = new Table();
            table.Columns.Add("thing", typeof(string));
            table.Columns.Add("stuff", typeof(string));
            table.Rows.Add("xx", "zzz");
            table.Rows.Add("walrus", "thingything");
            TableFormatter formatter = new TableFormatter('\0', '|', '\0', '\0', '\0', '+');
            Assert.AreEqual("------+-----------", formatter.GetRowSeparator(table, '-'));
            formatter = new TableFormatter('|', '|', '\0', '\0', '\0', '+');
            Assert.AreEqual("+------+-----------", formatter.GetRowSeparator(table, '-'));
            formatter = new TableFormatter('\0', '|', '|', '\0', '\0', '+');
            Assert.AreEqual("------+-----------+", formatter.GetRowSeparator(table, '-'));
            formatter = new TableFormatter('|', '|', '|', '\0', '\0', '+');
            Assert.AreEqual("+------+-----------+", formatter.GetRowSeparator(table, '-'));
        }

        #endregion
    }
}
