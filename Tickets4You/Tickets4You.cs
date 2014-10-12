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
        public const double version = 0.1;
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

            LoginReturn lr = JsonConvert.DeserializeObject<LoginReturn>(req.Post());

            if (lr.response)
            {
                userSession = lr.key;
            }

            return lr.response;
        }

        public List<ListEvents> getEvents(string userSessionKey)
        {
            RemotePost req = new RemotePost("http://tickets4you.dk/api/getEvents");
            req.Timeout = 3;

            req.Add("userSession", userSessionKey);

            ListEvents[] le = JsonConvert.DeserializeObject<ListEvents[]>(req.Post());

            return le.ToList<ListEvents>();
        }

        public List<Ticket> getAllTickets(string eventId, string userSessionKey)
        {
            RemotePost req = new RemotePost("http://tickets4you.dk/api/getTickets");
            req.Timeout = 3;

            req.Add("userSession", userSessionKey);
            req.Add("eventId", eventId);

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
    }
}
