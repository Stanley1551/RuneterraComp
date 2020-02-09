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
using RuneterraCompanion.Common;

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
        //DependencyInjection!
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

        //ez geci többször triggerelődne mielőtt be lenne zárva???
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
                    //ez még mindig nem async!
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
            System.IO.Compression.ZipFile.ExtractToDirectory(System.IO.Path.Combine(Directory.GetCurrentDirectory(), Constants.assetsFile), 
                System.IO.Path.Combine(Directory.GetCurrentDirectory(), Constants.assetsDirectoryName));

            File.Delete(System.IO.Path.Combine(Directory.GetCurrentDirectory(), Constants.assetsFile));
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
                await client.DownloadFileTaskAsync(new Uri(Constants.assetsUrl), Constants.assetsFile);
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
                    SetProgressBarValue(e.ProgressPercentage);
                    });
            }
        }

        //TODO check files as well
        private bool IsDownloadNeeded()
        {
            return !Directory.Exists(Constants.assetsDirectoryName);
        }

        private void CreateDirectoryForFiles()
        {
            try
            {
                Directory.CreateDirectory(Constants.assetsDirectoryName);
            }
            catch(Exception e) { throw e; }
        }

        private void SetProgressBarValue(int percentage)
        {
            CheckingProgressBarPercentage = percentage;
            DownloadPercentageText.Text = percentage + "%";
        }
    }
}
