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
        private DateTime _timePurchase, _timeUsed;

        public Ticket(string ticketId, string ticketName, string ticketMail)
        {
            this.ticketId = ticketId;
            this.ticketName = ticketName;
            this.ticketMail = ticketMail;
        }

        public string ticketId
        {
            get;
            set;
        }

        public string ticketName
        {
            get;
            set;
        }

        public string ticketMail
        {
            get;
            set;
        }

        public int timePurchase
        {
            set
            {
                _timePurchase = StaticItems.UnixTimeStampToDateTime(Convert.ToDouble(value));
            }
        }

        public DateTime returnTimePurchase
        {
            get
            {
                return _timePurchase;
            }
        }

        public int timeUsed
        {
            set
            {
                _timeUsed = StaticItems.UnixTimeStampToDateTime(Convert.ToDouble(value));
            }
        }

        public DateTime returnTimeUsed
        {
            get
            {
                return _timeUsed;
            }
        }

        public bool valid
        {
            get;
            set;
        }

        public bool SetStage()
        {
            if (valid)
            {
                valid = false;
                _timeUsed = DateTime.Now;
                return true;
            }
            return false;
        }
    }

    public class ListEvents
    {
        public ListEvents(string id, string name)
        {
            eventId = id;
            eventName = name;
        }

        public override string ToString()
        {
            return eventName;
        }

        public string eventId
        {
            set;
            get;
        }

        public string eventName
        {
            set;
            get;
        }
    }

    public class UpdateReturn
    {
        public double version { get; set; }
        public string downloadURL { get; set; }
    }
}
