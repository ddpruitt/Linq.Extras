﻿using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Linq.Extras.Tests.XListTests
{
    [TestFixture]
    class AsReadOnlyTests
    {
        [Test]
        public void AsReadOnly_Returns_List_With_Same_Items()
        {
            var items = new List<int> { 4, 8, 15, 16, 23, 42 };
            var result = items.AsReadOnly();
            CollectionAssert.AreEqual(items, result);
        }

        [Test]
        public void AsReadOnly_Returns_A_List_That_Throws_On_Attempt_To_Modify()
        {
            var items = new List<int> { 4, 8, 15, 16, 23, 42 };
            IList<int> result = items.AsReadOnly();
            Assert.Throws<NotSupportedException>(result.Clear);
            Assert.Throws<NotSupportedException>(() => result.Add(99));
            Assert.Throws<NotSupportedException>(() => result.Insert(1, 99));
            Assert.Throws<NotSupportedException>(() => result.RemoveAt(0));
            Assert.Throws<NotSupportedException>(() => result.Remove(42));
            Assert.Throws<NotSupportedException>(() => result[0] = 99);
        }

        [Test]
        public void AsReadOnly_Returns_A_List_That_Reflects_Changes_In_The_Original_List()
        {
            var items = new List<int> { 4, 8, 15, 16, 23, 42 };
            var result = items.AsReadOnly();

            // Make some random changes in the original list
            items.Remove(16);
            items.RemoveAt(1);
            items.Add(99);
            items.Swap(2, 3);

            // The read-only collection is only a view of the original, it should reflect the changes
            CollectionAssert.AreEqual(items, result);
        }


    }
}