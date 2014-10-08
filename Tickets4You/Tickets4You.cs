using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpPoster;
using HashString;


namespace Tickets4You
{
    public class Tickets4YouManager
    {
        private string APIKey = null, userSession = null, userLoginError = null;

        public Tickets4YouManager(string key)
        {
            APIKey = key;
        }

        public string userLogin(string Username, string Password)
        {
            RemotePost req = new RemotePost("http://tickets4you.dk/api/login.php");
            req.Timeout = 3;

            req.Add("Username", Username);
            req.Add("Password", HashBuilder.GetHashString(Password));

            string response = req.Post();

            return response;
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

        private List<string> parseXML(string XMLString)
        {
            List<string> test = new List<string>();

            return test;
        }
    }
}
