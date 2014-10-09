﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets4You
{
    public class ListEvents
    {
        private string eventId = null, eventName = null;

        public ListEvents(string id, string name)
        {
            eventId = id;
            eventName = name;
        }

        public string getEventName
        {
            get
            {
                return eventName;
            }
        }

        public string getEventId
        {
            get
            {
                return eventId;
            }
        }

        public override string ToString()
        {
            return eventName;
        }
    }
}
