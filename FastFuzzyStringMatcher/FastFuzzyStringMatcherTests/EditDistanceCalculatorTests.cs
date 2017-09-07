using FastFuzzyStringMatcher;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFuzzyStringMatcherTests
{
    [TestClass]
    public class EditDistanceCalculatorTests
    {
        private EditDistanceCalculator _distanceCalculator = new EditDistanceCalculator();

        [TestMethod]
        public void TestShortDistance()
        {
            String s1 = "Hat";
            String s2 = "Cat";

            int distance = _distanceCalculator.CalculateEditDistance(s1, s2);

            Assert.AreEqual(1, distance);
        }

        [TestMethod]
        public void TestLongDistance()
        {
            String s1 = "This is a long string";
            String s2 = "Th1s is a l0ng str1ng";

            int distance = _distanceCalculator.CalculateEditDistance(s1, s2);

            Assert.AreEqual(3, distance);
        }

        [TestMethod]
        public void TestStringOneLonger()
        {
            String s1 = "This string is longer";
            String s2 = "This is shorter";

            int distance = _distanceCalculator.CalculateEditDistance(s1, s2);

            Assert.AreEqual(10, distance);
        }

        [TestMethod]
        public void TestStringTwoLonger()
        {
            String s1 = "This is shorter";
            String s2 = "This string is longer";

            int distance = _distanceCalculator.CalculateEditDistance(s1, s2);

            Assert.AreEqual(10, distance);
        }

        [TestMethod]
        public void TestCaseInsensitive()
        {
            String s1 = "Test";
            String s2 = "test";

            int distance = _distanceCalculator.CalculateEditDistance(s1, s2);

            Assert.AreEqual(0, distance);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestStringOneNull()
        {
            String s1 = null;
            String s2 = "test";

            // Throws ArguentNullException
            _distanceCalculator.CalculateEditDistance(s1, s2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestStringTwoNull()
        {
            String s1 = "test";
            String s2 = null;

            // Throws ArguentNullException
            _distanceCalculator.CalculateEditDistance(s1, s2);
        }

        [TestMethod]
        public void TestStringTwoEmpty()
        {
            String s1 = "Test";
            String s2 = String.Empty;
            
            int distance = _distanceCalculator.CalculateEditDistance(s1, s2);

            Assert.AreEqual(4, distance);
        }

        [TestMethod]
        public void TestStringOneEmpty()
        {
            String s1 = String.Empty;
            String s2 = "Test";

            int distance = _distanceCalculator.CalculateEditDistance(s1, s2);

            Assert.AreEqual(4, distance);
        }

        [TestMethod]
        public void TestIdenticalStrings()
        {
            String s1 = "Test";
            String s2 = "Test";

            int distance = _distanceCalculator.CalculateEditDistance(s1, s2);

            Assert.AreEqual(0, distance);
        }
    }
}
