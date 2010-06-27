using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TextFormat.Table
{        
    [TestFixture]
    public class TestTableFormatter
    {
        protected internal TableFormatter Formatter { get; set; }
        protected internal TableFormatter FormatterT { get; set; }
        protected internal TableFormatter FormatterB { get; set; }
        protected internal TableFormatter FormatterTB { get; set; }
        
        [SetUp]
        public void SetUp ()
        {
            Formatter = new TableFormatter ();
            FormatterT = new TableFormatter ('\0', ' ', '\0', '-', '\0', '+');
            FormatterB = new TableFormatter ('\0', ' ', '\0', '\0', '-', '+');
            FormatterTB = new TableFormatter ('\0', ' ', '\0', '-');
        }
        
        [Test]
        public void TestFormat ()
        {
            IList<IList> empty = new List<IList> ();
            IList<string> actual;
            actual = Formatter.Format (empty);
            Assert.AreEqual (0, actual.Count);
            actual = FormatterT.Format (empty);
            Assert.AreEqual (1, actual.Count);
            actual = FormatterB.Format (empty);
            Assert.AreEqual (1, actual.Count);
            actual = FormatterTB.Format (empty);
            Assert.AreEqual (2, actual.Count);
        }
    }
}