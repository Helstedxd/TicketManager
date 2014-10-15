using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tickets4You;

namespace TicketManager
{
    class StaticTicketItems
    {
        public static List<Ticket> Tickets = new List<Ticket>();
        public static List<ListEvents> ListEvents = new List<ListEvents>();

        public static bool PartOfString(string needle, string haystack)
        {
            if (haystack.IndexOf(needle, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return true;
            }
            return false;
        }
    }
}
