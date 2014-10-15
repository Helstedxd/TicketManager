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

            if (!string.IsNullOrEmpty(t4y.lookForUpdate("main", version)))
            {
                MessageBox.Show(t4y.lookForUpdate("main", Tickets4YouManager.version));
            }

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
                    if (t.ticketId == TicketId.Text)
                    {
                        ticketFound = true;

                        if (t.valid)
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
            if (!string.IsNullOrEmpty(StaticTicketItems.ListEvents[selectEvent.SelectedIndex].eventId))
            {
                StaticTicketItems.Tickets.Clear();
                StaticTicketItems.Tickets.AddRange(t4y.getAllTickets(StaticTicketItems.ListEvents[selectEvent.SelectedIndex].eventId, userSession));

                listView.Items.Refresh();
            }
        }

        private void MenuItemExit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItemSettings(object sender, RoutedEventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void debugPdfPrint_Click(object sender, RoutedEventArgs e)
        {
            DownloadAndPrintPdf dapp = new DownloadAndPrintPdf("http://tickets4you.dk/api/test.pdf");
            dapp.ShowDialog();
        }

        private void scrollToIndexItem(object sender, RoutedEventArgs e)
        {
            try
            {
                listView.SelectedItem = StaticTicketItems.Tickets[999];
                listView.ScrollIntoView(StaticTicketItems.Tickets[1005]);
            }
            catch (Exception ex) { }
        }

        private void SearchTicket_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchInput.Text))
            {
                listView.ItemsSource = StaticTicketItems.Tickets;
            }
            else
            {
                listView.ItemsSource = from data in StaticTicketItems.Tickets where StaticTicketItems.PartOfString(SearchInput.Text, data.ticketName) || StaticTicketItems.PartOfString(SearchInput.Text, data.ticketMail) select data;
            }

            listView.Items.Refresh();
        }
    }
}
