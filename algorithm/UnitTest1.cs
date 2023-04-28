using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace algorithm
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            string str = "µ¥¾Ý×´Ì¬,dataItem,QuotedPriceManageListState";

            str.Split(',').ToList().ForEach(m => Console.WriteLine(m));

            Assert.Pass();
        }
    }
}