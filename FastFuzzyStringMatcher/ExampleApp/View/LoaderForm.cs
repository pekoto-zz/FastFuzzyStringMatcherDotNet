using ExampleApp.Model;
using FastFuzzyStringMatcher;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleApp.View
{
    public partial class LoaderForm : Form
    {
        private IProgress<LoadingStatus> _progressHandler;
        private CancellationTokenSource _cancellationToken;

        public LoaderForm()
        {
            InitializeComponent();

            _progressHandler = new Progress<LoadingStatus>(ReportProgress);
            _cancellationToken = new CancellationTokenSource();
        }

        private async void LoaderForm_Load(object sender, EventArgs e)
        {
            try
            {
                StringMatcher<String> stringMatcher = await LoadTranslationsAsync(_cancellationToken.Token);
                // TODO Show main form
            }
            catch (OperationCanceledException oce)
            {
                Debug.WriteLine($"Cancel requested: {oce.ToString()}");
                MessageBox.Show("Loading cancelled. The application will now exit.");
                this.Close();
            }
        }

        private void ReportProgress(LoadingStatus loadingStatus)
        {
            loading_progressbar.Value = loadingStatus.PercentComplete;
            status_lbl.Text = $"Loaded {loadingStatus.TranslationsLoaded}/{loadingStatus.TotalTranslationsToLoad}...";
        }

        private async Task<StringMatcher<String>> LoadTranslationsAsync(CancellationToken cancellation)
        {
            // TODO load translations line by line
            // Report progress
            // Offer cancelation
            // Pass controller to form

            cancellation.ThrowIfCancellationRequested();

            return new StringMatcher<String>();
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            _cancellationToken.Cancel();
        }
    }
}
