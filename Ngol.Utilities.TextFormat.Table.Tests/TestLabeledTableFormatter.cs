using System;
using System.Collections;
using System.Collections.Generic;
using Ngol.Utilities.TextFormat.Table;
using NUnit.Framework;

namespace Ngol.Utilitities.TextFormat.Table.Tests
{ 
    [TestFixture]
    public class TestLabeledTableFormatter
    {
        protected LabeledTableFormatter Formatter { get; set; }
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
            catch (ArgumentException) {}
        }
    }
}
