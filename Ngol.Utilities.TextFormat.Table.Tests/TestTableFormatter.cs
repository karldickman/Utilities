using System;
using System.Collections;
using System.Collections.Generic;
using Ngol.Utilities.NUnit;
using Ngol.Utilities.TextFormat.Table;
using NUnit.Framework;

namespace Ngol.Utilities.TextFormat.Table.Tests
{
    [TestFixture]
    public class TestTableFormatter
    {
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

        [SetUp]
        public void SetUp()
        {
            Formatter = new TableFormatter();
            FormatterT = new TableFormatter('\0', ' ', '\0', '-', '\0', '+');
            FormatterB = new TableFormatter('\0', ' ', '\0', '\0', '-', '+');
            FormatterTB = new TableFormatter('\0', ' ', '\0', '-');
        }

        [Test]
        public void TestFormat()
        {
            IList<IEnumerable<object>> empty = new List<IEnumerable<object>>();
            IEnumerable<string> actual;
            actual = Formatter.Format(empty);
            MoreAssert.IsEmpty(actual);
            actual = FormatterT.Format(empty);
            MoreAssert.HasCount(1, actual);
            actual = FormatterB.Format(empty);
            MoreAssert.HasCount(1, actual);
            actual = FormatterTB.Format(empty);
            MoreAssert.HasCount(2, actual);
        }
    }
}
