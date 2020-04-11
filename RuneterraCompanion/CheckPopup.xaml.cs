﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using RuneterraCompanion.Helpers;
using SimpleInjector;
using Path = System.IO.Path;

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
        //WebClientnek kéne valami scoped lifestyle...
        private WebClient client;
        
        private string CurrentDirectory => Directory.GetCurrentDirectory();

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

            client = new WebClient();
        }

        protected override async void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            if (IsDownloadNeeded())
            {
                try
                {
                    CreateDirectoryForFiles();
                }
                catch (Exception ex)
                {
                    Dispatcher.Invoke(() =>
                    { OperationLabel = "Something went wrong during the IO operation..."; });
                    return;
                }

                try
                {
                    await HandleDownload();
                }
                catch (Exception ex)
                {
                    Dispatcher.Invoke(() =>
                    { OperationLabel = "Something went wrong during the download operation...";
                        DeleteDownloadedZip();
                    });
                    return;
                }

                try
                {
                    //ez még mindig nem async!
                    HandleUnZip();

                    await HandleImageDowngrade();
                }
                catch (Exception ex)
                {
                    Dispatcher.Invoke(() =>
                    { OperationLabel = "Something went wrong during the UnZip operation..."; });
                    return;
                }
            }

            SetProgressBarToCompleted();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            client.Dispose();
        }

        //TODO cancel on closing if download is in progress...
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }

        private async Task HandleDownload()
        {
            await Task.Run(() => DownloadFile());
        }

        private async Task HandleImageDowngrade()
        {
            Directory.CreateDirectory(Path.Combine(CurrentDirectory, Constants.cardThumbnailPath));

            Dispatcher.Invoke(() => {
                OperationLabel = "Creating thumbnails...";
            });

            await Task.Run(() => DownGradeImages());
        }

        private void DownGradeImages()
        {
            var files = Directory.GetFiles(System.IO.Path.Combine(CurrentDirectory, Constants.cardImgPath));
            var imageHelper = new ImageHelper();

            foreach (var img in files)
            {
                var filename = System.IO.Path.GetFileNameWithoutExtension(img);
                var newPath = System.IO.Path.Combine(CurrentDirectory, Constants.cardThumbnailPath, filename+".png");

                imageHelper.SaveImage(newPath, img, 15);
            }
        }

        private void HandleUnZip()
        {
            System.IO.Compression.ZipFile.ExtractToDirectory(System.IO.Path.Combine(CurrentDirectory, Constants.assetsFile), 
                System.IO.Path.Combine(CurrentDirectory, Constants.assetsDirectoryName));

            DeleteDownloadedZip();
        }

        private void DeleteDownloadedZip()
        {
            File.Delete(System.IO.Path.Combine(CurrentDirectory, Constants.assetsFile));
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
                if(client == null)
                {
                    client = new WebClient();
                }
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

        private bool IsDownloadNeeded()
        {
            if(!CheckPath(Constants.assetsDirectoryName))
            {
                return true;
            }
            
            if(!CheckPath(Constants.cardImgPath))
            {
                return true;
            }

            if(!CheckPath(Constants.cardThumbnailPath))
            {
                return true;
            }
            
            if(Directory.GetFiles(Path.Combine(CurrentDirectory, Constants.cardThumbnailPath)).Length < 100
                || Directory.GetFiles(Path.Combine(CurrentDirectory, Constants.cardThumbnailPath)).Length < 100)
            {
                return true;
            }

            return false;
        }

        private bool CheckPath(string path)
        {
            if (!Directory.Exists(path))
            {
                return false;
            }

            if (Directory.GetFiles(Path.Combine(CurrentDirectory, path)).Length == 0)
            {
                return false;
            }

            return true;
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

        private void SetProgressBarToCompleted()
        {
            CancelButton.IsEnabled = false;
            CheckingProgressBarPercentage = 100;
            DownloadPercentageText.Text = "100 %";
            OperationLabel = "Operation completed";
        }
    }
}
