using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Ngol.Utilities.TextFormat.Table.Tests
{
    [TestFixture]
    public class TestTable
    {
        #region Properties

        protected Table MyTable { get; set; }

        #endregion

        #region Set up

        [SetUp]
        public void SetUp()
        {
            MyTable = new Table("myTable");
            MyTable.Columns.Add("thing", typeof(string));
            MyTable.Columns.Add("stuff", typeof(string));
            MyTable.Rows.Add("xx", "zzz");
            MyTable.Rows.Add("walrus", "thingything");
        }

        #endregion

        [Test]
        public void TestGetColumnWidths()
        {
            IEnumerable<int > expected = new List<int> { "walrus".Length, "thingything".Length, };
            IEnumerable<int > actual = MyTable.GetColumnWidths();
            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void TestGetColumnWidthsWithFormat()
        {
            Table table = new Table();
            table.Columns.Add("time", typeof(double), FormatTime);
            table.Rows.Add(25 * 60 + 10.2);
            table.Rows.Add(27 * 60 + 11.3);
            IEnumerable<int > expected = new List<int> { "25:10.20".Length, };
            IEnumerable<int > actual = table.GetColumnWidths();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestGetColumnWidthsWithHeaders()
        {
            MyTable.Columns[0].Name = new string('x', 80);
            IList<int > expected = MyTable.GetColumnWidths(false).ToList();
            expected[0] = 80;
            IList<int > actual = MyTable.GetColumnWidths(true).ToList();
            Assert.AreEqual(expected, actual);
        }

        public static string FormatTime(object obj)
        {
            double time = Convert.ToDouble(obj);
            int minutes = Convert.ToInt32(Math.Floor(time / 60));
            double seconds = time - minutes * 60;
            return string.Format("{0}:{1:00.00}", minutes, seconds);
        }
    }
}

