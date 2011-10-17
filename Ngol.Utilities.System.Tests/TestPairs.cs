using System;
using System.Collections.Generic;
using Ngol.Utilities.Collections.Extensions;
using NUnit.Framework;
using MoreAssert = Ngol.Utilities.NUnit.MoreAssert;

namespace Ngol.Utilities.System.Tests
{
    [TestFixture]
    public class TestPairs
    {
        [Test]
        public void Pairs()
        {
            IEnumerable<int> ints = new List<int> { 1, 2, 3, 4, 5, };
            IEnumerable<Tuple<int, int>> pairs = new List<Tuple<int, int>> { Tuple.Create(1, 2), Tuple.Create(2, 3), Tuple.Create(3, 4), Tuple.Create(4, 5), };
            MoreAssert.CollectionsEqual(pairs, ints.Pairs(Tuple.Create<int, int>));
        }
    }
}

