using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Ngol.Utilities.TextFormat.Table.Tests
{
    [TestFixture]
    public class TestTable
    {
        [Test]
        public void GetColumnWidths()
        {
            IEnumerable<ICell> row1 = new List<ICell> {
                new Cell("x"),
                new Cell("yy"),
                new Cell("zzz"),
            };
            IEnumerable<ICell> row2 = new List<ICell> {
                new Cell("xyz"),
                new Cell("p"),
                new Cell("pr"),
            };
            IEnumerable<Row> rows = new List<Row> {
                new Row(row1),
                new Row(row2),
            };
            Table table = new Table(rows);
            IList<int> columnWidths = table.GetColumnWidths().ToList();
            Assert.AreEqual(3, columnWidths.Count);
            Assert.AreEqual(3, columnWidths[0]);
            Assert.AreEqual(2, columnWidths[1]);
            Assert.AreEqual(3, columnWidths[2]);
        }
    }
}

