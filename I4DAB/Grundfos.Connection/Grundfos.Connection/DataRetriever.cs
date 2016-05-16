using System.IO;
using System.Net;

namespace Grundfos.Connection
{
    public class DataRetriever
    {
        public string GetJsonResponse(string url)
        {
            // Create request
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = WebRequestMethods.Http.Get;
            request.Accept = "application/json";


            // Read response
            using (var response = request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
