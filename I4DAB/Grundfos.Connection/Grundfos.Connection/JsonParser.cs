using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Grundfos.Connection
{
    public class JsonParser
    {
        public Dictionary<string, string> DeserializeResponse(string response)
        {
            // Deserializes the response into a C# dictionary
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(response);
        }

        public List<Reading> GetReadingsFromDeserializedResponse(Dictionary<string, string> dictionary)
        {
            // Get readings from dictionary
            return JsonConvert.DeserializeObject<List<Reading>>(dictionary["reading"]);
        }

        public JObject DeserializeResponseToJObject(string response)
        {
            return JObject.Parse(response);
        }

        public List<Reading> GetReadingsFromJObject(JObject jObject)
        {
            var jReading = jObject["reading"];
            var readings = new List<Reading>();

            foreach (var reading in jReading)
                readings.Add(new Reading((int)reading["sensorId"], (int)reading["appartmentId"], (double)reading["value"], DateTime.Now));

            return readings;
        }

        public List<Reading> GetReadingsFromUrl(string url)
        {
            var parser = new JsonParser();
            return parser.GetReadingsFromJObject(parser.DeserializeResponseToJObject(url));
        }
    }

    public class Reading
    {
        public int SensorId;
        public int ApartmentId;
        public double Value;
        public DateTime TimeStamp;

        public Reading(int sensorId, int apartmentId, double value, DateTime timeStamp)
        {
            SensorId = sensorId;
            ApartmentId = apartmentId;
            Value = value;
            TimeStamp = timeStamp;
        }

        public override string ToString()
        {
            return string.Format($"Reading: [Value: {Value}, TimeStamp: {TimeStamp.ToShortDateString()}, SensorId: {SensorId}, ApartmentId: {ApartmentId}]");
        }
    }
}