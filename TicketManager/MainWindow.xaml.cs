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

namespace TicketManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string userSession = "";
        

        public MainWindow()
        {
            InitializeComponent();
            listView.ItemsSource = StaticTicketItems.Tickets;

            if (userSession.Length == 0)
            {
                userLogin userLoginWindow = new userLogin();
                if (userLoginWindow.ShowDialog() == true)
                {
                    userSession = userLoginWindow.userSessionKey;
                }
                else
                {
                    Close();
                }
            }

            Ticket x = new Ticket("9781447920113", "Kasper Helsted");
            StaticTicketItems.Tickets.Add(x);
            Ticket z = new Ticket("9788777511530", "Kasper Hartvig Laursen");
            StaticTicketItems.Tickets.Add(z);

            selectEvent.Items.Add("test");
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                bool ticketFound = false;
                foreach (Ticket t in StaticTicketItems.Tickets)
                {
                    if (t.returnTicketId.ToString() == TicketBox.Text)
                    {
                        ticketFound = true;

                        if (t.stage)
                        {
                            t.SetStage();
                            listView.Items.Refresh();
                        }
                        else
                        {
                            MessageBox.Show("Ticket not valid!");
                        }
                    }
                }
                if (!ticketFound)
                {
                    MessageBox.Show("Ticket not found!");
                }
                TicketBox.Text = "";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(CreateTicketName.Text) && !string.IsNullOrEmpty(CreateTicketTicketId.Text))
            {
                string test = CreateTicketTicketId.Text;
                Ticket t = new Ticket(test, CreateTicketName.Text);
                StaticTicketItems.Tickets.Add(t);
                listView.Items.Refresh();
            }
        }
    }
}
