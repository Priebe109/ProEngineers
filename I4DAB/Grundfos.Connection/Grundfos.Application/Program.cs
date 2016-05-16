using System;
using System.Runtime.InteropServices;
using Grundfos.Connection;

namespace Grundfos.Application
{
    public class Program
    {
        static void Main(string[] args)
        {
            var dataRetriever = new DataRetriever();

            for (int i = 1; i < 5; i++)
                Console.WriteLine(dataRetriever.GetJsonResponse(string.Format($"http://userportal.iha.dk/~jrt/i4dab/E14/HandIn4/dataGDL/data/{i}.json")));

            Console.ReadKey();
        }
    }
}
