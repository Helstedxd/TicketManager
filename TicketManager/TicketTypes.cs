using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketManager
{
    class StaticTicketItems
    {
        public static List<Ticket> Tickets = new List<Ticket>();
    }

    class Ticket
    {
        private long ticketId;
        private string ticketName;
        private bool isValid = true;

        public Ticket(long Id, string name)
        {
            ticketId = Id;
            ticketName = name;
        }

        
        public void SetStage()
        {
            if (isValid)
            {
                isValid = false;
            }
            else
            {
                isValid = true;
            }
        }

        public long returnTicketId
        {
            get
            {
                return ticketId;
            }
        }

        public string returnName
        {
            get
            {
                return ticketName;
            }
        }

        public bool stage
        {
            get
            {
                return isValid;
            }
        }

        public override string ToString()
        {
            if(isValid){
                return ticketName + "'s billet er valid";
            }
            else
            {
                return ticketName + "'s billet er ikke valid";
            }
        }
    }
}
