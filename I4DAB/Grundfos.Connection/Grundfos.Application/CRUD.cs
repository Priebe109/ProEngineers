using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grundfos.Application
{
    public class CRUD
    {
        // Create

        public void CreateApartment(long floor, long roomNumber, float roomSize)
        {
            var apartment = new apartmentCharacteristic()
            {
                floor = floor,
                roomNumber = roomNumber,
                roomSize = roomSize
            };

            using (var model = new GrundfosModel())
            {
                model.apartmentCharacteristics.Add(apartment);
                model.SaveChanges();
            }
        }

        public void CreateSensor(string description, string unit, DateTime calibrationDate)
        {
            var sensor = new sensorCharacteristic()
            {
                description = description,
                unit = unit,
                calibrationDate = calibrationDate,
                calibrationEquation = "",
                calibrationCoefficient = "",
                externalRef = ""
            };

            using (var model = new GrundfosModel())
            {
                model.sensorCharacteristics.Add(sensor);
                model.SaveChanges();
            }
        }

        public void CreateReading(long value, DateTime timeStamp, long apartmentId, long sensorId)
        {
            var reading = new reading()
            {
                readingValue = value,
                readingTimestamp = timeStamp,
                apartmentId = apartmentId,
                sensorId = sensorId
            };

            using (var model = new GrundfosModel())
            {
                model.readings.Add(reading);
                model.SaveChanges();
            }
        }

        // Read

        // Update

        // Delete
    }
}
