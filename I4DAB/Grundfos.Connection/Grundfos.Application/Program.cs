using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Grundfos.Connection;

namespace Grundfos.Application
{
    public class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Grundfos Database");
            var input = "";
            var helper = new ProgramHelper();

            while (true)
            {
                // Request input from user
                Console.WriteLine("Type C to create, R to read, U to Update, D to delete or J to sample JSON response");

                input = Console.ReadLine();

                if (input.StartsWith("C") || input.StartsWith("c"))
                {
                    Console.WriteLine("Type A to create apartments, S to create sensors, R to add readings using JSON");

                    input = Console.ReadLine();

                    if (input.StartsWith("A") || input.StartsWith("a"))
                    {
                        helper.CreateApartments();
                    }
                    else if (input.StartsWith("S") || input.StartsWith("s"))
                    {
                        helper.CreateSensors();
                    }
                    else if (input.StartsWith("R") || input.StartsWith("r"))
                    {
                        Console.WriteLine("Enter beginIndex followed by (after [enter]) endIndex");
                        var beginIndex = int.Parse(Console.ReadLine());
                        var endIndex = int.Parse(Console.ReadLine());
                        helper.CreateReadingsFromJSON(beginIndex, endIndex);
                    }
                }
                else if (input.StartsWith("R") || input.StartsWith("r"))
                {
                    Console.WriteLine("Type A to get a list of all apartments or a number (apartmentId) to get readings from that apartment");

                    input = Console.ReadLine();

                    if (input.StartsWith("A") || input.StartsWith("a"))
                    {
                        helper.ReadApartments();
                    }
                    else
                    {
                        var id = int.Parse(input);
                        helper.ReadReadingsForApartment(id);
                    }
                }
                else if (input.StartsWith("U") || input.StartsWith("u"))
                {
                    Console.WriteLine("Type the id of the sensor you want to calibrate");

                    var id = int.Parse(Console.ReadLine());

                    Console.WriteLine("Type the updated calibration info");

                    input = Console.ReadLine();

                    var crud = new CRUD();
                    crud.UpdateSensorCalibration(id, DateTime.Now, input, input);
                }
                else if (input.StartsWith("D") || input.StartsWith("d"))
                {
                    Console.WriteLine("Type A to delete all apartments, S to delete all sensors or R to delete a reading");

                    input = Console.ReadLine();

                    if (input.StartsWith("A") || input.StartsWith("a"))
                    {
                        helper.DeleteApartments();
                    }
                    else if (input.StartsWith("S") || input.StartsWith("s"))
                    {
                        helper.DeleteSensors();
                    }
                    else if (input.StartsWith("R") || input.StartsWith("R"))
                    {
                        helper.DeleteReadings();
                    }
                }
                else if (input.StartsWith("J") || input.StartsWith("j"))
                {
                    Console.WriteLine("Enter beginIndex followed by (after [enter]) endIndex");
                    var beginIndex = int.Parse(Console.ReadLine());
                    var endIndex = int.Parse(Console.ReadLine());
                    helper.SampleJSONResponse(beginIndex, endIndex);
                }
                else
                {
                    // Error
                    Console.WriteLine("Could not interpret input.");
                }
            }
        }
    }

    public class ProgramHelper
    {
        private int _numberOfApartments = 400;
        private int _sensorsPerApartment = 12;

        public void CreateApartments()
        {
            var crud = new CRUD();

            for (int i = 0; i < _numberOfApartments; i++)
            {
                crud.CreateApartment(i % 3, i % 100, i % 60);
            }
        }

        public void CreateSensors()
        {
            var crud = new CRUD();

            for (int i = 0; i < _numberOfApartments*_sensorsPerApartment; i++)
            {
                crud.CreateSensor(string.Format($"sensor{i}"), "%", DateTime.Now, "equation", "coefficient", "exref");
            }
        }

        public void CreateReadingsFromJSON(int beginIndex, int endIndex)
        {
            var dataRetriever = new DataRetriever();
            var jsonParser = new JsonParser();
            var crud = new CRUD();

            for (var i = beginIndex; i < endIndex; i++)
            {
                var url = string.Format($"http://userportal.iha.dk/~jrt/i4dab/E14/HandIn4/dataGDL/data/{i}.json");

                var rawData = dataRetriever.GetJsonResponse(url);
                var readings = jsonParser.ReadingsFromRawData(rawData);

                foreach (var reading in readings)
                    crud.CreateReading((long)reading.Value, reading.TimeStamp, reading.ApartmentId, reading.SensorId);
            }
        }

        public void ReadApartments()
        {
            var crud = new CRUD();
            var apartments = crud.Apartments();
            foreach (var apartment in apartments)
                Console.WriteLine(string.Format($"Apartment: {apartment.apartmentId}, Floor/room: {apartment.floor}/{apartment.roomNumber}, Size: {apartment.roomSize}"));
        }

        public void ReadReadingsForApartment(int id)
        {
            var crud = new CRUD();
            var readings = crud.ReadingsForApartment(id);
            foreach (var reading in readings)
                Console.WriteLine(string.Format($"Reading: {reading.readingId}, Value: {reading.readingValue}, Apa/sen: {reading.apartmentId}, {reading.sensorId}"));
        }

        public void SampleJSONResponse(int beginIndex, int endIndex)
        {
            var dataRetriever = new DataRetriever();
            var jsonParser = new JsonParser();

            for (var i = beginIndex; i < endIndex; i++)
            {
                var url = string.Format($"http://userportal.iha.dk/~jrt/i4dab/E14/HandIn4/dataGDL/data/{i}.json");

                var rawData = dataRetriever.GetJsonResponse(url);
                var readings = jsonParser.ReadingsFromRawData(rawData);

                foreach (var reading in readings)
                    Console.WriteLine(reading);
            }
        }

        public void DeleteApartments()
        {
            var crud = new CRUD();

            for (int i = 1; i <= _numberOfApartments; i++)
            {
                crud.DeleteApartment(i);
            }
        }

        public void DeleteSensors()
        {
            var crud = new CRUD();

            for (int i = 1; i <= _numberOfApartments*_sensorsPerApartment; i++)
            {
                crud.DeleteSensor(i);
            }
        }

        public void DeleteReadings()
        {
            using (var model = new GrundfosModel())
            {
                foreach (var reading in model.readings)
                {
                    model.readings.Remove(reading);
                }

                model.SaveChanges();
            }
        }
    }
}
