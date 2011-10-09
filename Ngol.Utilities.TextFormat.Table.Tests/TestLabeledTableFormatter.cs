using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Ngol.Utilities.NUnit;
using Ngol.Utilities.TextFormat.Table;
using NUnit.Framework;

namespace Ngol.Utilities.TextFormat.Table.Tests
{
    [TestFixture]
    public class TestLabeledTableFormatter
    {
        protected LabeledTableFormatter Formatter
        {
            get;
            set;
        }
        [SetUp]
        public void TestSetUp()
        {
            Formatter = new PrettyTableFormatter();
        }

        /*[Test]
        public void TestFormat()
        {
            DataTable table = new DataTable("myTable");
            table.Columns.Add("thing", typeof(string));
            table.Columns.Add("stuff", typeof(string));
            IEnumerable<string > actual;
            actual = Formatter.Format(table);
            MoreAssert.IsEmpty(actual);
            actual = Formatter.Format(table);
            MoreAssert.HasCount(4, actual);
        }*/
    }
}
