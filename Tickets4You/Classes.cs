using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets4You
{
    public class LoginReturn
    {
        public string key { get; set; }
        public bool response { get; set; }
    }

    public class Ticket
    {
        private string pTicketId;
        private string pTicketName;
        private bool pIsValid = true;
        private DateTime pPurchaseTime = DateTime.Now;
        private DateTime pUsedTime = DateTime.MinValue;
        private bool visible = true;

        public Ticket(string Id, string name, DateTime pDate, bool valid, DateTime usedTime)
        {
            pTicketId = Id;
            pTicketName = name;
            pPurchaseTime = pDate;
            pIsValid = valid;
            pUsedTime = usedTime;
        }

        public bool SetStage()
        {
            if (pIsValid)
            {
                pIsValid = false;
                pUsedTime = DateTime.Now;
                return true;
            }
            return false;
        }

        public string ticketId
        {
            set
            {
                pTicketId = value;
            }
        }

        public string ticketName
        {
            set
            {
                pTicketName = value;
            }
        }

        public int timePurchase
        {
            set
            {
                pPurchaseTime = StaticItems.UnixTimeStampToDateTime(Convert.ToDouble(value));
            }
        }

        public bool valid
        {
            set
            {
                pIsValid = value;
            }
        }

        public int timeUsed
        {
            set
            {
                if (value == 0)
                {
                    pUsedTime = DateTime.MinValue;
                }
                else
                {
                    pUsedTime = StaticItems.UnixTimeStampToDateTime(Convert.ToDouble(value));
                }
            }
        }

        public string returnTicketId
        {
            get
            {
                return pTicketId;
            }
        }

        public string returnName
        {
            get
            {
                return pTicketName;
            }
        }

        public bool stage
        {
            get
            {
                return pIsValid;
            }
        }


        public DateTime pDate
        {
            get
            {
                return pPurchaseTime;
            }
        }


        public string uDate
        {
            get
            {
                if (pUsedTime == DateTime.MinValue)
                {
                    return "Ikke Brugt";
                }
                else
                {
                    return pUsedTime.ToString();
                }
            }
        }

        public bool isVisible
        {
            get
            {
                return true;
            }

            set
            {
                if (visible = value) return;
                visible = value;
            }
        }
    }

    public class ListEvents
    {
        private string pEventId = null, pEventName = null;

        public ListEvents(string id, string name)
        {
            pEventId = id;
            pEventName = name;
        }

        public string getEventName
        {
            get
            {
                return pEventName;
            }
        }

        public string getEventId
        {
            get
            {
                return pEventId;
            }
        }

        public override string ToString()
        {
            return pEventName;
        }

        public string eventId
        {
            set
            {
                if (string.IsNullOrEmpty(pEventId))
                {
                    pEventId = value;
                }
            }
        }

        public string eventName
        {
            set
            {
                if (string.IsNullOrEmpty(pEventName))
                {
                    pEventName = value;
                }
            }
        }
    }

}
