using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Grundfos.Connection
{
    public class JsonParser
    {
        public JObject DeserializeResponseToJObject(string response)
        {
            // Parse the response into a json object
            return JObject.Parse(response);
        }

        public List<Reading> GetReadingsFromJObject(JObject jObject)
        {
            // Get the readings json objet from the input json object
            var jReading = jObject["reading"];
            var readings = new List<Reading>();

            // Iterate though all readings and add them to an ordinary list
            foreach (var reading in jReading)
                readings.Add(new Reading((int)reading["sensorId"], (int)reading["appartmentId"], (double)reading["value"], DateTime.Now));

            return readings;
        }

        public List<Reading> ReadingsFromRawData(string data)
        {
            // Convenience method
            var response = DeserializeResponseToJObject(data);
            return GetReadingsFromJObject(response);
        } 
    }

    public class Reading
    {
        // Temporary transport class before being casted to real ADO.NET entity class

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