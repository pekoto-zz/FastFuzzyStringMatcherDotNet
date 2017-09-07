using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastFuzzyStringMatcher
{
    /// <summary>
    /// A class for returning results from the string matcher.
    /// </summary>
    /// <typeparam name="T">The data associated with each result.</typeparam>
    public class SearchResultList<T> : List<SearchResult<T>>
    {
        public bool ContainsKeyword(String keyword)
        {
            return this.FirstOrDefault(x => x.Keyword.Equals(keyword)) != null;
        }
    }
}
