using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace gakha.models.UnitTests
{
    [TestClass]
    public class ExtensionsTests
    {
        [TestMethod]
        public void CrossJoinListToEmpty()
        {
            var empty = new List<string>();
            var list = new List<string>() { "one", "two", "three" };
            empty.CrossJoin(list);
            Assert.AreEqual(empty.Count, list.Count, "List counts are not equal");
            for (var i = 0; i < empty.Count; i++)
                Assert.AreEqual(empty[i], list[i], String.Format("Entry {0} in the lists do not match ({1} vs. {2})", i, empty[i], list[i]));
        }

        [TestMethod]
        public void CrossJoinLists()
        {
            var list1 = new List<string>() { "one", "two", "three" };
            var list2 = new List<string>() { "four", "five", "six" };

            list1.CrossJoin(list2);
            Assert.AreEqual(list1.Count, 9, "Expected 9 entries in result list");
            Assert.AreEqual(list1[0], "one four");
            Assert.AreEqual(list1[8], "three six");
        }
    }
}
