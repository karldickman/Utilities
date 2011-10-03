using System;
using System.Collections;
using System.Collections.Generic;
using Ngol.Utilities.Collections.Extensions;
using Ngol.Utilities.Collections.ObjectModel;
using Ngol.Utilities.Collections.Specialized;
using Ngol.Utilities.NUnit;
using NUnit.Framework;

namespace Ngol.Utilities.System.Tests
{
    [TestFixture]
    public class TestObservableCollection
    {
        #region Properties

        protected ObservableCollection<int> EmptyCollection { get; set; }

        #endregion

        #region Set up

        [SetUp]
        public void SetUp()
        {
            EmptyCollection = new ObservableCollection<int>();
        }

        #endregion

        #region Tests

        [Test]
        public void Insert()
        {
            NotifyCollectionChangedAction action = (NotifyCollectionChangedAction)int.MinValue;
            IList newItems = null;
            IList oldItems = null;
            EmptyCollection.CollectionChanged += (sender, e) =>
            {
                Console.WriteLine("In handler");
                action = e.Action;
                newItems = e.NewItems;
                oldItems = e.OldItems;
            };
            EmptyCollection.Add(7);
            Assert.AreEqual(NotifyCollectionChangedAction.Add, action);
            MoreAssert.HasCount(1, newItems);
            Assert.AreEqual(7, newItems[0]);
            Assert.IsEmpty(oldItems);
        }

        [Test]
        public void Remove()
        {
            NotifyCollectionChangedAction action = (NotifyCollectionChangedAction)int.MinValue;
            IList newItems = null;
            IList oldItems = null;
            EmptyCollection.AddRange(new List<int> { 7, 5, 3, 4, });
            EmptyCollection.CollectionChanged += (sender, e) =>
            {
                action = e.Action;
                newItems = e.NewItems;
                oldItems = e.OldItems;
            };
            int firstToRemove = 3;
            EmptyCollection.Remove(firstToRemove);
            Assert.AreEqual(NotifyCollectionChangedAction.Remove, action);
            MoreAssert.HasCount(1, oldItems);
            Assert.AreEqual(firstToRemove, oldItems[0]);
            Assert.IsEmpty(newItems);
            EmptyCollection.RemoveAt(0);
            Assert.AreEqual(NotifyCollectionChangedAction.Remove, action);
            MoreAssert.HasCount(1, oldItems);
            Assert.AreEqual(7, oldItems[0]);
            Assert.IsEmpty(newItems);
        }

        #endregion
    }
}

