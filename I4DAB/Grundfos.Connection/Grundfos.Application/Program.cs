﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Grundfos.Connection;

namespace Grundfos.Application
{
    public class Program
    {
        static void Main(string[] args)
        {
            var dataRetriever = new DataRetriever();
            var jsonParser = new JsonParser();

            for (var i = 1; i < 5; i++)
            {
                var url = string.Format($"http://userportal.iha.dk/~jrt/i4dab/E14/HandIn4/dataGDL/data/{i}.json");

                var rawData = dataRetriever.GetJsonResponse(url);
                var readings = jsonParser.ReadingsFromRawData(rawData);

                foreach (var reading in readings)
                    Console.WriteLine(reading);
            }

            Console.ReadKey();
        }
    }
}
