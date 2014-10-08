using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Text;
using System.Threading.Tasks;

namespace TicketManager
{
    public class RemotePost
    {
        private System.Collections.Specialized.NameValueCollection Inputs = new System.Collections.Specialized.NameValueCollection();
        private string Url = string.Empty;

        /// <summary>
        /// number of seconds for timeout
        /// </summary>
        public int Timeout = 0;

        /// <summary>
        /// Url to post to
        /// </summary>
        public RemotePost(string url)
        {
            Url = url;
        }

        /// <summary>
        /// Parameters to add to the post
        /// </summary>
        /// <param name="name">parameter name</param>
        /// <param name="value">parameter value</param>
        public void Add(string name, string value)
        {
            Inputs.Add(name, value);
        }

        /// <summary>
        /// Posts data to server using POST method
        /// </summary>
        /// <returns></returns>
        public string Post()
        {
            string postData = String.Empty;

            int keyscount = Inputs.Keys.Count;

            for (int i = 0; i < keyscount; i++)
                postData += "&" + Inputs.Keys[i] + "=" + HttpUtility.UrlEncode(Inputs[Inputs.Keys[i]]);

            if (postData.Length > 0)
                postData = postData.Substring(1);

            HttpWebRequest wr = WebRequest.Create(Url) as HttpWebRequest;
            wr.Method = "POST";
            wr.ContentType = "application/x-www-form-urlencoded";

            using (StreamWriter requestWriter = new StreamWriter(wr.GetRequestStream()))
            {
                requestWriter.Write(postData);
            }

            return GetResponse(wr);
        }

        /// <summary>
        /// Posts data to server using GET method
        /// </summary>
        /// <returns></returns>
        public string Get()
        {
            string postData = String.Empty;

            int keyscount = Inputs.Keys.Count;

            for (int i = 0; i < keyscount; i++)
                postData += "&" + Inputs.Keys[i] + "=" + HttpUtility.UrlEncode(Inputs[Inputs.Keys[i]]);

            if (postData.Length > 0)
                postData = postData.Substring(1);

            string postUrl = Url.Contains("?") ? (Url + "&" + postData) : (Url + "?" + postData);

            HttpWebRequest wr = WebRequest.Create(postUrl) as HttpWebRequest;
            wr.Method = "GET";

            return GetResponse(wr);
        }

        private string GetResponse(HttpWebRequest wr)
        {
            wr.Timeout = Timeout * 1000; //millisecond timeout

            HttpWebResponse response = (HttpWebResponse)wr.GetResponse();

            if (response.StatusCode != HttpStatusCode.OK)
                return String.Empty;

            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader readerStream = new StreamReader(responseStream, System.Text.Encoding.UTF8))
                {
                    return readerStream.ReadToEnd();
                }
            }
        }
    }
}
