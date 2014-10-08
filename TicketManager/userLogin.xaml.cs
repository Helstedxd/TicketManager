using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

namespace TicketManager
{
    /// <summary>
    /// Interaction logic for userLogin.xaml
    /// </summary>
    public partial class userLogin : Window
    {
        public userLogin()
        {
            InitializeComponent();
        }

        public void getUserData(){

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RemotePost req = new RemotePost("http://helsted.eu");
            req.Timeout = 3;

            req.Add("FirstName", "First");
            req.Add("LastName", "Last");

            string response = req.Post();

            MessageBox.Show(response);

            DialogResult = true;
            this.Close();
        }
    }
}
