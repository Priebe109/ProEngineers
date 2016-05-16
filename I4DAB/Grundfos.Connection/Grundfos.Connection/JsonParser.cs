using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Grundfos.Connection
{
    public class JsonParser
    {
        public Dictionary<string, object> DeserializeResponse(string response)
        {
            // Deserializes the response into a C# dictionary
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(response);
        }

        public List<Reading> GetReadingsFromDeserializedResponse(Dictionary<string, object> dictionary)
        {
            // Get readings from dictionary
            var readings = dictionary["reading"] as List<Dictionary<string, object>>;

            var readingList = new List<Reading>();
            if (readings == null) return readingList;

            // Parse the reading dictionaries into reading objects
            readingList.AddRange(readings.Select(reading => new Reading((int) reading["sensorId"], (int) reading["appartmentId"], (double) reading["value"], DateTime.Parse((string) reading["timestamp"]))));

            return readingList;
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