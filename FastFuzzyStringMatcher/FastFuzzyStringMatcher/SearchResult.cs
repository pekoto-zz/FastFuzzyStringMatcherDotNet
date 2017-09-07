using System;
using System.Collections.Generic;
using System.Text;

namespace FastFuzzyStringMatcher
{
    /// <summary>
    /// A result returned after searching using the string matcher.
    /// </summary>
    /// <typeparam name="T">The type of data associated with each string keyword.</typeparam>
    public class SearchResult<T>
    {
        public String Keyword { get; set; }
        public T AssociatedData { get; set; }
        public float MatchPercentage { get; set; }

        public SearchResult(String keyword, T associatedData, float matchPercentage)
        {
            this.Keyword = keyword;
            this.AssociatedData = associatedData;
            this.MatchPercentage = matchPercentage;
        }

        public override string ToString()
        {
            return $"{Keyword}/{AssociatedData}/{MatchPercentage}";
        }
    }
}
