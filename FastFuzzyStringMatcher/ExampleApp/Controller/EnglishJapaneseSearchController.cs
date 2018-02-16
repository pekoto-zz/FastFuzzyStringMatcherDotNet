using FastFuzzyStringMatcher;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExampleApp.Controller
{
    /// <summary>
    /// <para/>An English --> Japanese dictionary with fuzzy lookup.
    /// <para/>Built using the StringMatcher class.
    /// </summary>
    public class EnglishJapaneseSearchController
    {
        public long DictionarySize { get; private set; }

        private StringMatcher<String> _stringMatcher;
        private String _filePath = $"Resources{Path.DirectorySeparatorChar}JMDict_po.txt";

        public EnglishJapaneseSearchController()
        {
            if(_stringMatcher == null)
            {
                LoadDictionary();
            }
        }

        private void LoadDictionary()
        {
            List<String> linesInFile = GetLinesFromFile();
            _stringMatcher = new StringMatcher<String>();

            String englishTerm = "";

            foreach (String line in linesInFile)
            {
                if(line == String.Empty)
                {
                    continue;
                }
                else if (line.StartsWith("msgid"))
                {
                    englishTerm = GetParsedTerm(line);
                }
                else if(line.StartsWith("msgstr"))
                {
                    _stringMatcher.Add(englishTerm, GetParsedTerm(line));
                    DictionarySize++;
                }
            }
        }

        private List<String> GetLinesFromFile()
        {
            String fileText = File.ReadAllText(_filePath, Encoding.UTF8);
         
            return new List<String>(fileText.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));
        }

        private String GetParsedTerm(String line)
        {
            int beginIndex = line.IndexOf('"') + 1;
            int endIndex = line.LastIndexOf('"');
            int length = endIndex - beginIndex;
                        
            return line.Substring(beginIndex, length);
        }

        public SearchResultList<String> Search(String keyword, float matchPercentage)
        {
            return _stringMatcher.Search(keyword, matchPercentage);
        }
    }
}
