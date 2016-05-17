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

        public void CreateSensor(string description, string unit, DateTime calibrationDate, string calEq, string calCoef, string exRef)
        {
            var sensor = new sensorCharacteristic()
            {
                description = description,
                unit = unit,
                calibrationDate = calibrationDate,
                calibrationEquation = calEq,
                calibrationCoefficient = calCoef,
                externalRef = exRef
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

        public List<reading> ReadingsForApartment(long id)
        {
            using (var model = new GrundfosModel())
            {
                var readings = model.readings.Where(_reading => _reading.apartmentId == id);
                return readings.ToList();
            }
        }

        public List<apartmentCharacteristic> Apartments()
        {
            using (var model = new GrundfosModel())
            {
                var apartments = model.apartmentCharacteristics;
                return apartments.ToList();
            }
        }

        // Update

        public void UpdateSensorCalibration(long id, DateTime calibrationDate, string calEq, string calCoef)
        {
            using (var model = new GrundfosModel())
            {
                var sensorQuery = model.sensorCharacteristics.Where(_sensor => _sensor.sensorId == id);
                var sensor = sensorQuery.FirstOrDefault();

                sensor.calibrationDate = calibrationDate;
                sensor.calibrationEquation = calEq;
                sensor.calibrationCoefficient = calCoef;

                model.SaveChanges();
            }
        }

        // Delete

        public void DeleteSensor(long id)
        {
            using (var model = new GrundfosModel())
            {
                var sensorQuery = model.sensorCharacteristics.Where(_sensor => _sensor.sensorId == id);
                var sensor = sensorQuery.FirstOrDefault();
                model.sensorCharacteristics.Remove(sensor);
                model.SaveChanges();
            }
        }

        public void DeleteApartment(long id)
        {
            using (var model = new GrundfosModel())
            {
                var apartmentQuery = model.apartmentCharacteristics.Where(_apartment => _apartment.apartmentId == id);
                var apartment = apartmentQuery.FirstOrDefault();
                model.apartmentCharacteristics.Remove(apartment);
                model.SaveChanges();
            }
        }

        public void DeleteReading(long id)
        {
            using (var model = new GrundfosModel())
            {
                var readingQuery = model.readings.Where(_reading => _reading.readingId == id);
                var reading = readingQuery.FirstOrDefault();
                model.readings.Remove(reading);
                model.SaveChanges();
            }
        }
    }
}
