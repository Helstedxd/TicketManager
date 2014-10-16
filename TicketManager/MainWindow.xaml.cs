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
        public const double version = 0.1;
        public static Tickets4YouManager t4y = new Tickets4YouManager("test");
        UpdateManager updateManager = new UpdateManager();

        public MainWindow()
        {
            InitializeComponent();

            listView.ItemsSource = StaticTicketItems.Tickets;
            selectEvent.ItemsSource = StaticTicketItems.ListEvents;

            updateManager.registrerUpdate("main", version);

            if (updateManager.update())
            {
                Close();
            }


            if (string.IsNullOrEmpty(t4y.getUserSession()))
            {
                userLogin userLoginWindow = new userLogin();
                if (userLoginWindow.ShowDialog() == true)
                {
                    StaticTicketItems.ListEvents.Add(new ListEvents(null, "Choose Event"));                    
                    StaticTicketItems.ListEvents.AddRange(t4y.getEvents());
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
                validateTicketButton_Click(sender, e);
            }
        }

        private void validateTicketButton_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToBoolean(StaticTicketItems.Tickets.Count))
            {
                Ticket result = StaticTicketItems.Tickets.Find(t => t.ticketId == TicketId.Text);
                if (!string.IsNullOrEmpty(result.ticketId))
                {
                    if (t4y.validateTicket(result.ticketId))
                    {
                        result.SetStage();
                        listView.Items.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Ticket not valid!");
                    }
                }
                else
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
                StaticTicketItems.Tickets.AddRange(t4y.getAllTickets(StaticTicketItems.ListEvents[selectEvent.SelectedIndex].eventId));

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

        private void contextMenuValidateTicket(object sender, RoutedEventArgs e)
        {
            int errors = 0;

            if (Convert.ToBoolean(StaticTicketItems.Tickets.Count))
            {
                foreach (Ticket t in listView.SelectedItems)
                {
                    if (t4y.validateTicket(t.ticketId))
                    {
                        t.SetStage();
                        listView.Items.Refresh();
                    }
                    else
                    {
                        errors++;
                    }
                }

                if (errors == 1)
                {
                    MessageBox.Show("Ticket could not be verified.");
                }
                else if (errors > 1)
                {
                    MessageBox.Show("Some tickets could not be verified.");
                }
            }
        }
    }
}
