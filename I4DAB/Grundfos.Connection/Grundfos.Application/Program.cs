using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Grundfos.Connection;

namespace Grundfos.Application
{
    public class Program
    {
        // Console program for testing the CRUD methods and database design

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
                    // Create something in the database

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
                    // Read something from the database

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
                    // Update something in the database

                    Console.WriteLine("Type the id of the sensor you want to calibrate");

                    var id = int.Parse(Console.ReadLine());

                    Console.WriteLine("Type the updated calibration info");

                    input = Console.ReadLine();

                    var crud = new CRUD();
                    crud.UpdateSensorCalibration(id, DateTime.Now, input, input);
                }
                else if (input.StartsWith("D") || input.StartsWith("d"))
                {
                    // Delete something from the database

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
                    // Display some JSON samples from the web server

                    Console.WriteLine("Enter beginIndex followed by (after [enter]) endIndex");
                    var beginIndex = int.Parse(Console.ReadLine());
                    var endIndex = int.Parse(Console.ReadLine());
                    helper.SampleJSONResponse(beginIndex, endIndex);
                }
                else
                {
                    // Error handling

                    Console.WriteLine("Could not interpret input.");
                }
            }
        }
    }

    public class ProgramHelper
    {
        // Class for calling the CRUD statements from the CRUD class

        private int _numberOfApartments = 400;
        private int _sensorsPerApartment = 12;

        public void SampleJSONResponse(int beginIndex, int endIndex)
        {
            // Prints samples of the JSON readings from the server to the console

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

        public void CreateReadingsFromJSON(int beginIndex, int endIndex)
        {
            // Inserts readings into the database after parsing JSON readings from the server

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

        public void CreateApartments()
        {
            // Creates a bunch of apartments in the database

            var crud = new CRUD();

            for (int i = 0; i < _numberOfApartments; i++)
            {
                crud.CreateApartment(i % 3, i % 100, i % 60);
            }
        }

        public void CreateSensors()
        {
            // Creates a bunch of sensors in the database

            var crud = new CRUD();

            for (int i = 0; i < _numberOfApartments*_sensorsPerApartment; i++)
            {
                crud.CreateSensor(string.Format($"sensor{i}"), "%", DateTime.Now, "equation", "coefficient", "exref");
            }
        }

        public void ReadApartments()
        {
            // Prints all apartments currently in the database to the console

            var crud = new CRUD();
            var apartments = crud.Apartments();
            foreach (var apartment in apartments)
                Console.WriteLine(string.Format($"Apartment: {apartment.apartmentId}, Floor/room: {apartment.floor}/{apartment.roomNumber}, Size: {apartment.roomSize}"));
        }

        public void ReadReadingsForApartment(int id)
        {
            // Prints all readings for a given apartment to the console

            var crud = new CRUD();
            var readings = crud.ReadingsForApartment(id);
            foreach (var reading in readings)
                Console.WriteLine(string.Format($"Reading: {reading.readingId}, Value: {reading.readingValue}, Apa/sen: {reading.apartmentId}, {reading.sensorId}"));
        }

        public void DeleteApartments()
        {
            // Deletes all apartments from the database

            var crud = new CRUD();

            for (int i = 1; i <= _numberOfApartments; i++)
            {
                crud.DeleteApartment(i);
            }
        }

        public void DeleteSensors()
        {
            // Deletes all sensors from the database

            var crud = new CRUD();

            for (int i = 1; i <= _numberOfApartments*_sensorsPerApartment; i++)
            {
                crud.DeleteSensor(i);
            }
        }

        public void DeleteReadings()
        {
            // Deletes all readings from the database

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
