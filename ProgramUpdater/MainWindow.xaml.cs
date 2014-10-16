using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.IO.Compression;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.ComponentModel;


namespace ProgramUpdater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string tmpFolder = "tmp";
        private string downloadFile = "update.zip";
        private WebClient client = new WebClient();

        public MainWindow()
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(StaticItems.updateDownloadUrl))
            {
                Close();
            }
            else
            {
                //start download
                startDownload(StaticItems.updateDownloadUrl);
            }
        }

        private void deleteFolderAndContent(string folder)
        {
            DirectoryInfo downloadedMessageInfo = new DirectoryInfo(folder);

            foreach (FileInfo file in downloadedMessageInfo.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in downloadedMessageInfo.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        private void startDownload(string downloadUrl)
        {
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(onDownloadComplete);

            // Starts the download
            client.DownloadFileAsync(new Uri(downloadUrl), downloadFile);
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;


            DownloadProgressar.Value = int.Parse(Math.Truncate(percentage).ToString());
        }

        private void onDownloadComplete(object sender, AsyncCompletedEventArgs e)
        {
            List<string> ticket4YouFiles = new List<string>(new string[] { "TicketManager.exe", "HashString.dll", "HttpPoster.dll", "Newtonsoft.Json.dll", "Tickets4You.dll" });

            //vertifiser fil

            //set gamle filer
            foreach (string f in ticket4YouFiles)
            {
                if (File.Exists(f))
                {
                    File.Delete(f);
                }
            }

            //create unzip folder
            if (!File.Exists(tmpFolder))
            {
                Directory.CreateDirectory(tmpFolder);
            }
            //unzip
            ZipFile.ExtractToDirectory(downloadFile, tmpFolder);
            //move files
            foreach (string f in ticket4YouFiles)
            {
                if (File.Exists(tmpFolder + "/" + f))
                {
                    File.Move(tmpFolder + "/" + f, f);
                }
            }

            //delete unzip folder & downloaded zip
            deleteFolderAndContent(tmpFolder);
            if (File.Exists(downloadFile))
            {
                File.Delete(downloadFile);
            }

            try
            {
                Process p = new Process();
                p.StartInfo.FileName = "TicketManager.exe";
                p.Start();
            }
            catch (Exception ex) { }

            Close();
        }
    }
}
