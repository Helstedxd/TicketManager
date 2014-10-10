using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpPoster;
using HashString;
using System.Xml;
using System.IO;
using System.Web.Script.Serialization;

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
            RemotePost req = new RemotePost("http://tickets4you.dk/api/login.php");
            req.Timeout = 3;

            req.Add("Username", Username);
            req.Add("Password", HashBuilder.GetHashString(Password));

            dynamic data = ParseJSON(req.Post());

            if (Convert.ToBoolean(data.response))
            {
                userSession = data.key;
                return Convert.ToBoolean(data.response);
            }
            else
            {
                return Convert.ToBoolean(data.response);
            }
        }

        public List<ListEvents> getEvents(string userSessionKey)
        {
            List<ListEvents> le = new List<ListEvents>();

            RemotePost req = new RemotePost("http://tickets4you.dk/api/getEvents.php");
            req.Timeout = 3;

            req.Add("userSession", userSessionKey);

            dynamic data = ParseJSON(req.Post());

            for (int i = 0; i < data.Length; i++)
            {
                le.Add(new ListEvents(Convert.ToString(data[i].eventId), Convert.ToString(data[i].eventName)));
            }

            return le;
        }

        public List<Ticket> getAllTickets(string eventId, string userSessionKey)
        {
            List<Ticket> Ticket = new List<Ticket>();

            RemotePost req = new RemotePost("http://tickets4you.dk/api/getTickets.php");
            req.Timeout = 3;

            req.Add("userSession", userSessionKey);
            req.Add("eventId", eventId);

            dynamic data = ParseJSON(req.Post());

            for (int i = 0; i < data.Length; i++)
            {
                Ticket.Add(
                        new Ticket(
                                Convert.ToString(data[i].ticketId),
                                Convert.ToString(data[i].ticketName),
                                UnixTimeStampToDateTime(Convert.ToDouble(data[i].timePurchase)), 
                                Convert.ToBoolean(data[i].valid)
                        )
                );
            }

            return Ticket;
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

        private dynamic ParseJSON(string JSONString)
        {
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });

            dynamic data = serializer.Deserialize(JSONString, typeof(object));

            return data;
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
