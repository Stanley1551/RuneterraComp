using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RuneterraCompanion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    /*Logic: 1. Check for files locally
             2. if not downloaded -> download
             3. unzip
             4. copy them in the appropriate folder */
    public partial class CheckPopup : Window
    {
        private const string assetsUrl = @"https://dd.b.pvp.net/datadragon-set1-lite-en_us.zip";
        private const string assetsFile = @"datadragon-set1-lite-en_us.zip";
        private const string assetsDirectoryName = @"Assets";
        private WebClient client;
    

        public string OperationLabel { 
            get => CurrentOperationLabel.Content.ToString();
            set => CurrentOperationLabel.Content = value; 
        }

        public double CheckingProgressBarPercentage {
            get => CheckingProgressBar.Value;
            set => CheckingProgressBar.Value = value;
        }

        public CheckPopup()
        {
            InitializeComponent();
        }

        protected override async void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (IsDownloadNeeded())
            {
                try
                {
                    CreateDirectoryForFiles();
                }
                catch (Exception) { Dispatcher.Invoke(() => 
                { OperationLabel = "Something went wrong during the IO operation..."; }); 
                    return; }

                try
                {
                    await HandleDownload();
                }
                catch(Exception) {
                    Dispatcher.Invoke(() =>
                    { OperationLabel = "Something went wrong during the download operation..."; });
                    return;
                }

                try
                {
                    HandleUnZip();
                }
                catch (Exception)
                {
                    Dispatcher.Invoke(() =>
                    { OperationLabel = "Something went wrong during the IO operation..."; });
                    return;
                }
            }

            CancelButton.IsEnabled = false;
            OperationLabel = "Operation completed";
        }

        //TODO cancel on closing if download is in progress...
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }

        private async Task HandleDownload()
        {
            client = new WebClient();

            await Task.Run(() => DownloadFile());
        }

        private void HandleUnZip()
        {
            System.IO.Compression.ZipFile.ExtractToDirectory(System.IO.Path.Combine(Directory.GetCurrentDirectory(), assetsFile), 
                System.IO.Path.Combine(Directory.GetCurrentDirectory(), assetsDirectoryName));

            File.Delete(System.IO.Path.Combine(Directory.GetCurrentDirectory(), assetsFile));
        }

        //On cancel: abort the operation, then delete the downloaded zip
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            client.CancelAsync();
            OperationLabel = "Operation cancelled";
        }

        private async Task DownloadFile()
        {
            try
            {
                client.DownloadProgressChanged += UpdateDownloadProgress;
                await client.DownloadFileTaskAsync(new Uri(assetsUrl), assetsFile);
                client.DownloadProgressChanged -= UpdateDownloadProgress;
                client.Dispose();
            }
            catch(OperationCanceledException) { 
                Dispatcher.Invoke(() => { OperationLabel = "Cancelled"; }); }
        }

        private void UpdateDownloadProgress(object sender, DownloadProgressChangedEventArgs e)
        {
            if(e.ProgressPercentage > 0)
            {
                Dispatcher.Invoke(() => {
                    CheckingProgressBarPercentage = e.ProgressPercentage;
                    DownloadPercentageText.Text = e.ProgressPercentage + "%";
                    });
            }
        }

        //TODO check files as well
        private bool IsDownloadNeeded()
        {
            return !Directory.Exists(assetsDirectoryName);
        }

        private void CreateDirectoryForFiles()
        {
            try
            {
                Directory.CreateDirectory(assetsDirectoryName);
            }
            catch(Exception e) { throw e; }
        }
    }
}
