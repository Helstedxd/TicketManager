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
        private string ticketId;
        private string ticketName;
        private bool isValid = true;
        private DateTime purchaseTime = DateTime.Now;
        private DateTime usedTime = DateTime.MinValue;

        public Ticket(string Id, string name)
        {
            ticketId = Id;
            ticketName = name;
        }


        public void SetStage()
        {
            if (isValid)
            {
                isValid = false;
                usedTime = DateTime.Now;
            }
        }

        public string returnTicketId
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

        public DateTime pDate
        {
            get
            {
                return purchaseTime;
            }
        }

        public string uDate
        {
            get
            {
                if (usedTime == DateTime.MinValue)
                {
                    return "Ikke Brugt";
                }
                else
                {
                    return usedTime.ToString();
                }
            }
        }

        public override string ToString()
        {
            if (isValid)
            {
                return ticketName + "'s billet er valid";
            }
            else
            {
                return ticketName + "'s billet er ikke valid";
            }
        }
    }
}
