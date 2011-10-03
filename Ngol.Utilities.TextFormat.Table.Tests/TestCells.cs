using System;
using NUnit.Framework;
using Ngol.Utilities.TextFormat;
using Ngol.Utilities.TextFormat.Table;

namespace Ngol.Utilities.TextFormat.Table.Tests
{
    [TestFixture]
    public class TestHorizontalCellSeparator
    {
        [Test]
        public void TestPad()
        {
            HorizontalCellSeparator separator = new HorizontalCellSeparator('-');
            string expected = "--------";
            separator.Pad(8);
            Assert.AreEqual(expected, separator.Text);
        }
    }

    [TestFixture]
    public class TestCell
    {
        [Test]
        public void TestPad()
        {
            string text = "xkcd";
            ICell cell = new Cell(text);
            cell.Pad(8);
            Assert.AreEqual("xkcd    ", cell.ToString());
            cell = new Cell(text, StringFormatting.RightJustified);
            cell.Pad(8);
            Assert.AreEqual("    xkcd", cell.ToString());
            cell = new Cell(text, StringFormatting.Centered);
            cell.Pad(8);
            Assert.AreEqual("  xkcd  ", cell.ToString());
        }
    }
}
