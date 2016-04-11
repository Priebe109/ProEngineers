using System;
using Adressekartotek_DAL;

namespace Adressekartotek_ConsoleApplication
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // perform some of the dal operations
            Console.WriteLine("<< ProEngineers Database TestSuite v. 0.2 >>");
            var dal = new Dal();
            ExerciseDal(dal);

            // wait for input before returning
            Console.ReadKey();
        }

        private static void ExerciseDal(Dal dal)
        {
            Console.WriteLine("Deleting all data...");
            dal.DeleteAllData();

            Console.WriteLine("Creating new data...");
            dal.CreateDataForAdresse();
            dal.CreateDataForPerson();
            dal.CreateDataForTelefon();

            Console.WriteLine("Reading from database...\n");
            dal.ReadDataForPerson();
            dal.ReadDataForPersonAndAdresse();
            dal.ReadDataForPersonAndTelefon();

            Console.WriteLine("\nUpdating data...\n");
            dal.UpdateTranslateVen();
            dal.UpdateTranslateKollega();
            dal.UpdateFixEmil();

            Console.WriteLine("Reading updated data...\n");
            dal.ReadDataForPerson();
        }
    }
}
