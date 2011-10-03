using System;
using System.Collections;
using System.Collections.Generic;
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

        [Test]
        public void TestFormat()
        {
            IEnumerable<object> label = new List<object> { "thing", "stuff", };
            IEnumerable<IEnumerable<object>> empty = new List<IEnumerable<object>>();
            IEnumerable<string> actual;
            actual = Formatter.Format(empty);
            MoreAssert.IsEmpty(actual);
            actual = Formatter.Format(label, empty);
            MoreAssert.HasCount(4, actual);
            actual = Formatter.Format(null, empty, label);
            MoreAssert.HasCount(4, actual);
            actual = Formatter.Format(label, empty, label);
            MoreAssert.HasCount(6, actual);
            try
            {
                IEnumerable<object> list = new List<object> { "xkcd", };
                actual = Formatter.Format(label, empty, list);
                Assert.Fail("Attempting to use different sized headers and footers should not be allowed.");
            }
            catch(ArgumentException)
            {
            }
        }
    }
}
