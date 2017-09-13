using ExampleApp.Controller;
using FastFuzzyStringMatcher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleApp
{
    /// <summary>
    /// <para/>An example app showing how StringMatcher can be used to fuzzy search a translation memory dictionary. 
    /// <para/>Note: This is a rough-and-ready example, and does not contain threading or error handling.
    /// <para/>See<a href="http://www.edrdg.org/jmdict/j_jmdict.html"> The JMDict Project</a>.
    /// <para/>See also unit tests in FastFuzzyStringMatcherTests for further examples of usage.
    /// </summary>
    public partial class MainForm : Form
    {
        private EnglishJapaneseSearchController _searchController;

        public MainForm()
        {
            InitializeComponent();
            LoadSearchController();
        }
        
        /// This will take a little time while translation pairs are read from Resources.
        /// In a real app, you should load this data on a separate thread to keep the UI responsive.
        private void LoadSearchController()
        {
            _searchController = new EnglishJapaneseSearchController();
            toolStripStatusLabel.Text = $"Loaded {_searchController.DictionarySize} terms.";
        }

        private void search_btn_Click(object sender, EventArgs e)
        {
            dataGridView.Rows.Clear();
            dataGridView.Refresh();

            String searchTerm = searchTerm_tbx.Text;
            float matchPercentage = (float)matchPercentage_nud.Value;

            if (String.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Please enter a search term.");
                return;
            }

            SearchResultList<String> results = _searchController.Search(searchTerm, matchPercentage);

            if(results.Count == 0)
            {
                MessageBox.Show($"No results found for {searchTerm}");
            }
            else
            {
                BindResults(results, matchPercentage);
            }

            toolStripStatusLabel.Text = $"Searched {_searchController.DictionarySize} terms using {matchPercentage}% matching.";
        }

        private void BindResults(SearchResultList<String> results, float matchPercentage)
        {
            for(int i = 0; i < results.Count; i++)
            {
                SearchResult<String> result = results[i];

                // Due to rounding we occasionally get some false positives when searching using %.
                // We can hard-filter these out like this if desired.
                if (result.MatchPercentage >= matchPercentage)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridView);
                    row.Cells[0].Value = result.Keyword;
                    row.Cells[1].Value = result.AssociatedData;
                    row.Cells[2].Value = result.MatchPercentage;
                    dataGridView.Rows.Add(row);
                }
            }

            dataGridView.Refresh();
        }
    }
}
