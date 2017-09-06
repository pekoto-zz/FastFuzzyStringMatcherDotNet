using System;

namespace FastFuzzyStringMatcher
{
    /// <summary>
    /// <para>
    /// Calculates the number of operations it takes to turn one string into another.
    /// Also known as the Levenshtein distance.
    /// This implementation uses the iterative approach with two matrix rows. Case insensitive.
    /// </para>
    /// <para>
    /// See <a href="https://en.wikipedia.org/wiki/Levenshtein_distance">Wikipedia</a>.
    /// </para>
    /// <para>
    /// Available operations are:
    /// <list type="bullet">
    /// <item>substitution</item>
    /// <item>insertion</item>
    /// <item>deletion</item>
    /// </list>
    /// </para>
    /// <para>
    /// <example>Example:
    /// To turn kitten --> sitting:
    /// kitten --> sitten (substitute "s" for "k")
    /// sitten --> sittin(substitute "e" for "i")
    /// sittin --> sitting(insert "g")
    /// </para>
    /// <para>
    /// Edit distance = 3
    /// </para>
    /// </example> 
    /// </summary>
    public static class EditDistanceCalculator
    {
        public static int CalcuclateEditDistance(String str1, String str2)
        {
            if(str1 == null || str2 == null)
            {
                throw new ArgumentNullException("Strings cannot be null");
            }

            if(str1.Length == 0)
            {
                return str2.Length;
            }

            if(str2.Length == 0)
            {
                return str1.Length;
            }

            int str1RowLength = str1.Length + 1;
            int str2RowLength = str2.Length + 1;

            int[] previousRow = new int[str1RowLength];
            int[] currentRow = new int[str1RowLength];

            // Initialise the first row of the distance matrix
            for (int i = 0; i < str1RowLength; i++)
            {
                previousRow[i] = i;
            }

            for (int rowIndex = 1; rowIndex < str2RowLength; rowIndex++)
            {
                // Initialise the first column of the distance matrix
                currentRow[0] = rowIndex;

                for (int colIndex = 1; colIndex < str1RowLength; colIndex++)
                {
                    char str1Char = Char.ToLower(str1[colIndex - 1]);
                    char str2Char = Char.ToLower(str2[rowIndex - 1]);

                    int swapCharsCost = (str1Char == str2Char) ? 0 : 1;

                    int substitutionCost = previousRow[colIndex - 1] + swapCharsCost;
                    int insertionCost = previousRow[colIndex] + 1;
                    int deletionCost = currentRow[colIndex - 1] + 1;

                    // Store the least costly edit operation
                    currentRow[colIndex] = Min(insertionCost, deletionCost, substitutionCost);
                }

                int[] swap = previousRow;
                previousRow = currentRow;
                currentRow = swap;
            }

            // The distance is the last element of the last row
            return previousRow[str1RowLength - 1];
        }

        private static int Min(int a, int b, int c)
        {
            return Math.Min(Math.Min(a, b), c);
        }
    }
}
