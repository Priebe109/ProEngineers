using System.IO;
using System.Net;

namespace Grundfos.Connection
{
    public class DataRetriever
    {




        public void GetJSONResponse(string url)
        {
            // Create request
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;
            request.Accept = "application/json";


            string text;
            var response = (HttpWebResponse)request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
            }
        }
    }
}
