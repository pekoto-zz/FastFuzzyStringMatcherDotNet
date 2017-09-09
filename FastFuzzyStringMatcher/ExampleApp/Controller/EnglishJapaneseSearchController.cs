using FastFuzzyStringMatcher;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExampleApp.Controller
{
    public class EnglishJapaneseSearchController
    {
        private static StringMatcher<String> _stringMatcher;
        public static long DictionarySize { get; private set; }

        public EnglishJapaneseSearchController()
        {
            if(_stringMatcher == null)
            {
                LoadDictionary();
            }
        }

        private static void LoadDictionary()
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

        private static List<String> GetLinesFromFile()
        {
            byte[] bytes = Encoding.Default.GetBytes(Properties.Resources.JMDict_po);
            String fileText = Encoding.UTF8.GetString(bytes);
            return new List<String>(fileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None));
        }

        private static String GetParsedTerm(String line)
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
