using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Web.Script.Serialization;
using HttpPoster;
using HashString;
using Newtonsoft.Json;


namespace Tickets4You
{
    public class Tickets4YouManager
    {
        private string APIKey = null, userSession = null;

        public Tickets4YouManager(string key)
        {
            APIKey = key;
        }

        public bool userLogin(string Username, string Password)
        {
            RemotePost req = new RemotePost("http://tickets4you.dk/api/login");
            req.Timeout = 3;

            req.Add("Username", Username);
            req.Add("Password", HashBuilder.GetHashString(Password));
            req.Add("userSession", this.userSession);
            req.Add("apiKey", this.APIKey);

            LoginReturn lr = JsonConvert.DeserializeObject<LoginReturn>(req.Post());

            if (lr.response)
            {
                userSession = lr.key;
            }

            return lr.response;
        }

        public List<ListEvents> getEvents()
        {
            RemotePost req = new RemotePost("http://tickets4you.dk/api/getEvents");
            req.Timeout = 3;

            req.Add("userSession", this.userSession);

            ListEvents[] le = JsonConvert.DeserializeObject<ListEvents[]>(req.Post());

            return le.ToList<ListEvents>();
        }

        public List<Ticket> getAllTickets(string eventId)
        {
            RemotePost req = new RemotePost("http://tickets4you.dk/api/getTickets");
            req.Timeout = 3;

            req.Add("eventId", eventId);
            req.Add("userSession", this.userSession);
            req.Add("apiKey", this.APIKey);

            Ticket[] tickets = JsonConvert.DeserializeObject<Ticket[]>(req.Post());

            return tickets.ToList<Ticket>();
        }

        public string getUserSession()
        {
            if (string.IsNullOrEmpty(userSession))
            {
                return null;
            }
            else
            {
                return userSession;
            }
        }

        public string lookForUpdate(string item, double version)
        {
            RemotePost req = new RemotePost("http://tickets4you.dk/api/getVersions");
            req.Timeout = 3;

            req.Add("Item", item);
            req.Add("userSession", this.userSession);
            req.Add("apiKey", this.APIKey);

            UpdateReturn ur = JsonConvert.DeserializeObject<UpdateReturn>(req.Post());

            if (ur.version > version)
            {
                return ur.downloadURL;
            }
            else
            {
                return null;
            }
        }

        public bool validateTicket(string ticketId)
        {
            RemotePost req = new RemotePost("http://tickets4you.dk/api/validateEvent");
            req.Timeout = 3;

            req.Add("userSession", this.userSession);
            req.Add("apiKey", this.APIKey);

            validateTicketReturn vtr = JsonConvert.DeserializeObject<validateTicketReturn>(req.Post());

            return vtr.valid;
        }
    }
}
