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
        private string APIKey = null, userSession = null, userLoginError = null;

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

            string response = req.Post();

            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });

            dynamic data = serializer.Deserialize(response, typeof(object));

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
