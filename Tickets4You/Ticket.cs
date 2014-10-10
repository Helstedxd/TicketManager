using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets4You
{
    public class Ticket
    {
        private string ticketId;
        private string ticketName;
        private bool isValid = true;
        private DateTime purchaseTime = DateTime.Now;
        private DateTime usedTime = DateTime.MinValue;

        public Ticket(string Id, string name, DateTime pDate, bool valid)
        {
            ticketId = Id;
            ticketName = name;
            purchaseTime = pDate;
            isValid = valid;
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
