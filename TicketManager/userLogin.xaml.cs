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
using Tickets4You;

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
            Tickets4YouManager t4y = new Tickets4YouManager("test");

            bool test = t4y.userLogin(Username.Text, Password.Password);
            MessageBox.Show(test.ToString());
            /*
            if (Convert.ToBoolean(response))
            {
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Err0r");
            }
            */

            //this.Close();
        }
    }
}
