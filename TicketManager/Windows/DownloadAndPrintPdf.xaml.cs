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
using System.Windows.Shapes;
using System.Net;
using System.ComponentModel;
using System.IO;


namespace TicketManager
{
    /// <summary>
    /// Interaction logic for DownloadAndPrintPdf.xaml
    /// </summary>
    public partial class DownloadAndPrintPdf : Window
    {
        private string downloadLocation = AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\tmp\\";
        private string downloadFileName = "test.pdf";
        private string downloadUrl = null;
        private WebClient client = new WebClient();

        public DownloadAndPrintPdf(string downloadUrlParameter)
        {
            InitializeComponent();
            downloadUrl = downloadUrlParameter;
            startDownload();
        }

        private void startDownload()
        {
            bool exists = Directory.Exists(downloadLocation);
            if (!exists)
            {
                Directory.CreateDirectory(downloadLocation);
            }
            try
            {
                File.Delete(downloadLocation + downloadFileName);
            }
            catch (Exception ex)
            {
            }

            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
            
            // Starts the download
            client.DownloadFileAsync(new Uri(downloadUrl), downloadLocation + downloadFileName);
        }

        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;

            downloadProgressBar.Value = int.Parse(Math.Truncate(percentage).ToString());
        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            System.Diagnostics.Process.Start(downloadLocation + downloadFileName);
            Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Is Dot Net Perls awesome?", "Important Question", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                client.CancelAsync();
                File.Delete(downloadLocation + downloadFileName);
            }
        }

    }
}
