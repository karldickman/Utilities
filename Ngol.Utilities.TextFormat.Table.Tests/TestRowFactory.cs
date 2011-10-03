using System;
using NUnit.Framework;
using Ngol.Utilities.TextFormat.Table;

namespace Ngol.Utilities.TextFormat.Table.Tests
{
    [TestFixture]
    public class TestRowFactory
    {
        private RowFactory rowFactory;

        [SetUp]
        public void Setup()
        {
            rowFactory = new RowFactory('/', '|', 'X');
        }

        [Test]
        public void TestSeparators()
        {
            char[] expected = { '/', '|', '|', '|', '|', 'X' };
            char[] actual = rowFactory.Separators(5);
            Assert.AreEqual(expected.Length, actual.Length);
            for(int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
    }
}
