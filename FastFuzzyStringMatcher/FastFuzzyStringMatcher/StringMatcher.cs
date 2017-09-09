using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FastFuzzyStringMatcher
{
    /// <summary>
    /// <para/>Builds a tree based on edit distance that allows quick fuzzy searching of string keywords, case insensitive.
    /// <para/>Also known as a BK Tree.
    /// See <a href="https://en.wikipedia.org/wiki/BK-tree">BK Tree on Wikipedia</a>
    ///  
    ///  <para/>Percentage Matching
    ///  
    ///  <para/>This implementation also allows the retrieval of strings using percentage matching.
    ///  This is generally much more practical than searching purely on edit distance,
    ///  unless all of your strings are of fixed length.
    ///  
    ///  <para/>Due to rounding, it's possible that strings that match slightly less than the requested
    ///  percentage may be returned.<para/>I've left these in as it's better to have false positives than
    ///  vice versa.
    ///  
    ///  <para/>Generic Data Association
    ///  
    ///  <para/>You can store some associated data with each string keyword.
    ///  The generic parameter refers to this data type.
    ///  
    ///  <example><para/>Example uses:
    ///  <list type="bullet">  
    ///  <item>Search for file name --> Return associated paths of files that match 70%</item>
    ///  <item>Search fund code X000 -->  Return fund names/price for all funds starting with X000</item>
    ///  <item>Search for data in a foreign language --> Return all translations that match 85%, etc.</item>
    ///  </list>
    ///  </example>
    ///  
    ///  <para/>Line Breaks/Spaces
    ///  
    ///  <para/>Large chunks of text can appear different due to line break/white space differences.
    ///  <para/>In most cases, we only want to compare the text itself, so the class allows you to
    ///  ignore line breaks and spaces for comparison purposes.
    /// </summary>
    /// <typeparam name="T">The type of data associated with each string keyword.</typeparam>
    public class StringMatcher<T>
    {
        private Node _root;
        private EditDistanceCalculator _distanceCalculator = new EditDistanceCalculator();
        private MatchingOption _matchingOption = MatchingOption.NONE;

        public StringMatcher() { }

        public StringMatcher(MatchingOption matchingOption)
        {
            this._matchingOption = matchingOption;
        }

        public void Add(String keyword, T associatedData)
        {
            if(keyword == null)
            {
                throw new ArgumentNullException("Strings must not be null");
            }

            if(keyword.Length == 0)
            {
                throw new ArgumentException("Strings must not be empty");
            }

            String normalizedKeyword = GetNormalizedKeyword(keyword);

            if(_root == null)
            {
                _root = new Node(keyword, normalizedKeyword, associatedData);
            }
            else
            {
                // Traverse through the tree, adding the string as a leaf related by edit distance
                Node current = _root;
                int editDistance = _distanceCalculator.CalculateEditDistance(current.NormalizedKeyword, keyword);

                while(current.ContainsChildWithDistance(editDistance))
                {
                    current = current.getChild(editDistance);
                    editDistance = _distanceCalculator.CalculateEditDistance(current.NormalizedKeyword, keyword);

                    if(editDistance == 0)
                    {
                        return; // Duplicate (string already exists in tree)
                    }
                }

                current.AddChild(editDistance, keyword, normalizedKeyword, associatedData);
            }
        }

        private String GetNormalizedKeyword(String str)
        {
            if (_matchingOption == MatchingOption.REMOVE_SPACING_AND_LINEBREAKS)
            {
                return RemoveSpacesAndLinebreaks(str);
            }
            else
            {
                return str;
            }
        }

        private String RemoveSpacesAndLinebreaks(String str)
        {
            return Regex.Replace(str, @"\s+", string.Empty);
        }

    	// Search using % matching.
        // More user-friendly and robust when searching strings of variable length,
        // but may lead to strings slightly less than the matchPercentage being returned due to rounding.
        public SearchResultList<T> search(String keyword, float matchPercentage)
        {
            keyword = GetNormalizedKeyword(keyword);
            int distanceThreshold = ConvertPercentageToEditDistance(keyword, matchPercentage);

            return SearchTree(keyword, distanceThreshold);
        }

        private int ConvertPercentageToEditDistance(String keyword, float matchPercentage)
        {
            return keyword.Length - (int)(Math.Round((keyword.Length * matchPercentage)/100.0f));
        }

        // Search using edit distance (chars different).
        // Less user-friendly and less robust when strings are of variable length,
        // but ensures only strings with a precise number of edits will be returned.
        public SearchResultList<T> Search(String keyword, int distanceThreshold)
        {
            keyword = GetNormalizedKeyword(keyword);
            return SearchTree(keyword, distanceThreshold);
        }

        private SearchResultList<T> SearchTree(String keyword, int distanceThreshold)
        {
            SearchResultList<T> results = new SearchResultList<T>();

            SearchTree(_root, keyword, distanceThreshold, results);
            results.SortByClosestMatch();

            return results;
        }

        // Recursively search the tree, adding any data from nodes within the edit distance threshold.
        // Results are stored in the "results" parameter. This is a bit functionally dirty, but since this
        // method is recursive, it saves a new collection being created/copied with every call.
        private void SearchTree(Node node, String keyword, int distanceThreshold, SearchResultList<T> results)
        {
            int currentDistance = _distanceCalculator.CalculateEditDistance(node.NormalizedKeyword, keyword);

            if(currentDistance <= distanceThreshold)
            {
                // Match found
                float percentageDifference = GetPercentageDifference(node.NormalizedKeyword, keyword, currentDistance);
                SearchResult<T> result = new SearchResult<T>(node.OriginalKeyword, node.AssociatedData, percentageDifference);
                results.Add(result);
            }

            // Get the children to search next
            int minDistance = currentDistance - distanceThreshold;
            int maxDistance = currentDistance + distanceThreshold;

            List<int> childKeysWithinDistanceThreshold = node.GetChildKeysWithinDistance(minDistance, maxDistance);

            foreach(int childKey in childKeysWithinDistanceThreshold)
            {
                Node child = node.getChild(childKey);
                SearchTree(child, keyword, distanceThreshold, results);
            }
        }

        private float GetPercentageDifference(String keyword, String wordToMatch, int editDistance)
        {
            int longestWordLength = Math.Max(keyword.Length, wordToMatch.Length);
            return 100.0f - (((float)editDistance / longestWordLength) * 100.0f);
        }

        public void printTree()
        {
            _root.PrintHierarchy(0);
        }

        /// <summary>
        /// A node in the BK Tree.
        /// </summary>
        private class Node
        {
            public String OriginalKeyword { get; set; }
            public String NormalizedKeyword { get; set; }  // Used for matching
            public T AssociatedData { get; set; }

            private Dictionary<int, Node> _children; // Children are keyed on edit distance

            public Node(String keyword, String normalizedKeyword, T associatedData)
            {
                this.OriginalKeyword = keyword;
                this.NormalizedKeyword = normalizedKeyword;
                this.AssociatedData = associatedData;
            }

            public Node getChild(int key)
            {
                if(ContainsChildWithDistance(key))
                {
                    return _children[key];
                }
                else
                {
                    return null;
                }
            }

            public bool ContainsChildWithDistance(int key)
            {
                return _children != null && _children.ContainsKey(key);
            }

            public List<int> GetChildKeysWithinDistance(int minDistance, int maxDistance)
            {
                if(_children == null)
                {
                    return new List<int>(0);
                }
                else
                {
                    return _children.Keys.Where(x => x >= minDistance && x <= maxDistance).ToList();
                }
            }

            /*
             * 
             * 		public List<Integer> getChildKeysWithinDistance(int minDistance, int maxDistance) {
			if(children == null) {
				return new ArrayList<Integer>(0);
			} else {
				return children.keySet().stream().filter(n -> n >= minDistance && n <= maxDistance)
												 .collect(Collectors.toList());
			}
		}
             * 
             */


            public void AddChild(int key, String keyword, String normalizedKeyword, T associatedData)
            {
                if(_children == null)
                {
                    _children = new Dictionary<int, Node>();
                }

                Node child = new Node(keyword, normalizedKeyword, associatedData);
                _children.Add(key, child);
            }

            public override string ToString()
            {
                return $"{OriginalKeyword}/{NormalizedKeyword}/{AssociatedData}";
            }

            public void PrintHierarchy(int level)
            {
                for(int i = 0; i < level; i++)
                {
                    Debug.Write("\t");
                }

                Debug.WriteLine($"-- {OriginalKeyword}");

                if(_children != null)
                {
                    foreach(Node child in _children.Values)
                    {
                        child.PrintHierarchy(level + 1);
                    }
                }
            }
        }
    }
}
