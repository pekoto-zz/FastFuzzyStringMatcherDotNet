using ExampleApp.Model;
using System;
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
            await LoadTranslationsAsync(_cancellationToken.Token);

            // TODO Show main form
        }

        private void ReportProgress(LoadingStatus loadingStatus)
        {
            loading_progressbar.Value = loadingStatus.PercentComplete;
            status_lbl.Text = $"Loaded {loadingStatus.TranslationsLoaded}/{loadingStatus.TotalTranslationsToLoad}...";
        }

        private async Task LoadTranslationsAsync(CancellationToken cancellation)
        {
            // TODO load translations line by line
            // Report progress
            // Offer cancelation
            // Pass controller to form

            cancellation.ThrowIfCancellationRequested();
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            _cancellationToken.Cancel();
        }
    }
}
