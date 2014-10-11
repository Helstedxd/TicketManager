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
using Tickets4You;

namespace TicketManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string userSession = "";
        public const double version = 0.1;
        Tickets4YouManager t4y = new Tickets4YouManager("test");        

        public MainWindow()
        {
            InitializeComponent();
            listView.ItemsSource = StaticTicketItems.Tickets;
            selectEvent.ItemsSource = StaticTicketItems.ListEvents;

            if (userSession.Length == 0)
            {
                userLogin userLoginWindow = new userLogin();
                if (userLoginWindow.ShowDialog() == true)
                {
                    userSession = userLoginWindow.userSessionKey;
                    
                    StaticTicketItems.ListEvents.Add(new ListEvents(null, "Choose Event"));                    
                    StaticTicketItems.ListEvents.AddRange(t4y.getEvents(userSession));
                    selectEvent.Items.Refresh();
                }
                else
                {
                    Close();
                }
            }
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                bool ticketFound = false;
                foreach (Ticket t in StaticTicketItems.Tickets)
                {
                    if (t.returnTicketId == TicketId.Text)
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
                TicketId.Text = "";
            }
        }

        private void selectEvent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(StaticTicketItems.ListEvents[selectEvent.SelectedIndex].getEventId))
            {
                StaticTicketItems.Tickets.Clear();
                StaticTicketItems.Tickets.AddRange(t4y.getAllTickets(StaticTicketItems.ListEvents[selectEvent.SelectedIndex].getEventId, userSession));
                listView.Items.Refresh();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
