using FastFuzzyStringMatcher;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFuzzyStringMatcherTests
{
    [TestClass]
    public class StringSearcherTests
    {
        private static StringMatcher<String> _stringMatcher = new StringMatcher<String>();
        
        [ClassInitialize]
        public static void SetupStringMatcher(TestContext context)
        {
            _stringMatcher.Add("0123456789", "10 digit long string");
            _stringMatcher.Add("012345678", "9 digit long string");
            _stringMatcher.Add("01234567", "8 digit long string");
            _stringMatcher.Add("0123456", "7 digit long string");
            _stringMatcher.Add("012345", "6 digit long string");
            _stringMatcher.Add("01234", "5 digit long string");
            _stringMatcher.Add("0123", "4 digit long string");
            _stringMatcher.Add("012", "3 digit long string");
            _stringMatcher.Add("01", "2 digit long string");
            _stringMatcher.Add("0", "1 digit long string");

            _stringMatcher.Add("Test", "String with uppercase char");
            _stringMatcher.Add("test", "String with all lowercase chars");

            _stringMatcher.Add("This is a test", "Multiple word string");

            _stringMatcher.Add("Cat", "Short string");
            _stringMatcher.Add("Bats", "Slightly longer short string");
        }

        [TestMethod]
        public void TestOneHundredPercentMatching()
        {
            SearchResultList<String> results = _stringMatcher.Search("01234", 100.0f);

            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("01234", results[0].Keyword);
        }

        [TestMethod]
        public void TestSeventyFivePercentMatching()
        {
            SearchResultList<String> results = _stringMatcher.Search("0123456789", 75.0f);

            Assert.AreEqual(3, results.Count);
            Assert.IsTrue(results.ContainsKeyword("0123456789"));
            Assert.IsTrue(results.ContainsKeyword("012345678"));
            Assert.IsTrue(results.ContainsKeyword("01234567"));
        }

        [TestMethod]
        public void TestFiftyPercentMatching()
        {
            SearchResultList<String> results = _stringMatcher.Search("0123456789", 50.0f);

            Assert.AreEqual(6, results.Count);
            Assert.IsTrue(results.ContainsKeyword("0123456789"));
            Assert.IsTrue(results.ContainsKeyword("012345678"));
            Assert.IsTrue(results.ContainsKeyword("01234567"));
            Assert.IsTrue(results.ContainsKeyword("0123456"));
            Assert.IsTrue(results.ContainsKeyword("012345"));
            Assert.IsTrue(results.ContainsKeyword("01234"));
        }

        [TestMethod]
        public void TestTwentyFivePercentMatching()
        {
            SearchResultList<String> results = _stringMatcher.Search("0123456789", 25.0f);

            Assert.AreEqual(9, results.Count);
            Assert.IsTrue(results.ContainsKeyword("0123456789"));
            Assert.IsTrue(results.ContainsKeyword("012345678"));
            Assert.IsTrue(results.ContainsKeyword("01234567"));
            Assert.IsTrue(results.ContainsKeyword("0123456"));
            Assert.IsTrue(results.ContainsKeyword("012345"));
            Assert.IsTrue(results.ContainsKeyword("01234"));
            Assert.IsTrue(results.ContainsKeyword("0123"));
            Assert.IsTrue(results.ContainsKeyword("012"));
            // C# rounding will cause an extra result compared to the Java version
            Assert.IsTrue(results.ContainsKeyword("01"));
        }

        [TestMethod]
        public void TestEditDistanceMatching()
        {
            SearchResultList<String> results = _stringMatcher.Search("01234", 1);

            Assert.AreEqual(3, results.Count);
            Assert.IsTrue(results.ContainsKeyword("01234"));
            Assert.IsTrue(results.ContainsKeyword("0123"));
            Assert.IsTrue(results.ContainsKeyword("012345"));
        }

        [TestMethod]
        public void TestResultsInDescendingOrder()
        {
            SearchResultList<String> results = _stringMatcher.Search("Fat", 2);

            Assert.AreEqual("Cat", results[0].Keyword);
            Assert.AreEqual("Bats", results[1].Keyword);
        }

        [TestMethod]
        public void TestResultPercentage()
        {
            SearchResultList<String> results = _stringMatcher.Search("Cat", 2);

            // Float equality inaccuracy requires checking against some delta
            Assert.AreEqual(100.0f, results[0].MatchPercentage, 0.1);
            Assert.AreEqual(50.0f, results[1].MatchPercentage, 0.1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddEmptyString()
        {
            // Throws ArgumentException
            _stringMatcher.Add("", "Empty string");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddNullString()
        {
            // Throws ArgumentNullException
            _stringMatcher.Add(null, "Null string");
        }

        [TestMethod]
        public void TestCaseSensitive()
        {
            SearchResultList<String> results = _stringMatcher.Search("cat", 100.0f);

            Assert.IsTrue(results.ContainsKeyword("Cat"));
        }

        [TestMethod]
        public void TestMultipleWords()
        {
            SearchResultList<String> results = _stringMatcher.Search("This is a vest", 90.0f);

            Assert.IsTrue(results.ContainsKeyword("This is a test"));
        }

        [TestMethod]
        public void TestAssociatedData()
        {
            SearchResultList<String> results = _stringMatcher.Search("01234", 100.0f);

            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("5 digit long string", results[0].AssociatedData);
        }

        [TestMethod]
        public void TestIgnoreSpaces()
        {
            StringMatcher<String> ignoreSpacesMatcher = new StringMatcher<String>(MatchingOption.REMOVE_SPACING_AND_LINEBREAKS);

            ignoreSpacesMatcher.Add("This is a test", "A string with spaces");

            SearchResultList<String> results = ignoreSpacesMatcher.Search("This is  atest", 100.0f);

            Assert.AreEqual(1, results.Count);
            Assert.IsTrue(results.ContainsKeyword("This is a test"));
        }

        [TestMethod]
       public void TestIgnoreTabs()
        {
            StringMatcher<String> ignoreTabsMatcher = new StringMatcher<String>(MatchingOption.REMOVE_SPACING_AND_LINEBREAKS);

            ignoreTabsMatcher.Add("\t\tThis is some tabbed data", "A string with tabs");

            SearchResultList<String> results = ignoreTabsMatcher.Search("This is some tabbed \tdata", 100.0f);

            Assert.AreEqual(1, results.Count);
            Assert.IsTrue(results.ContainsKeyword("\t\tThis is some tabbed data"));
        }

        [TestMethod]
        public void TestIgnoreLinebreaks()
        {
            StringMatcher<String> ignoreLinebreaksMatcher = new StringMatcher<String>(MatchingOption.REMOVE_SPACING_AND_LINEBREAKS);

            ignoreLinebreaksMatcher.Add("This has\nsome line\nbreaks.", "A string with linebreaks");

            SearchResultList<String> results = ignoreLinebreaksMatcher.Search("This has some line breaks.", 100.0f);

            Assert.AreEqual(1, results.Count);
            Assert.IsTrue(results.ContainsKeyword("This has\nsome line\nbreaks."));
        }
    }
}