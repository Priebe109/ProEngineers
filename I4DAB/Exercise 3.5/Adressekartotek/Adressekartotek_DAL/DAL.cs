using System;
using System.Data.SqlClient;

namespace Adressekartotek_DAL
{
    public class Dal
    {
        private readonly SqlConnection _conn;

        public Dal()
        {
            _conn =
                new SqlConnection(
                    "Data Source=10.29.0.29;Initial Catalog=F16I4DABH2Gr2;User ID=F16I4DABH2Gr2;Password=F16I4DABH2Gr2");
        }

        // MARK: - General Data

        public void PerformNonQuery(string cmdText)
        {
            try
            {
                // open connection to database
                _conn.Open();

                // setup a new command
                var cmd = new SqlCommand(cmdText, _conn);

                // execute command
                cmd.ExecuteNonQuery();
            }
            finally
            {
                _conn?.Close();
            }
        }

        // MARK: - Create Data

        public void CreateDataForAdresse()
        {
            // insert address entries to database
            var insertCmdText = @"INSERT INTO Adresse (Bynavn, Husnummer, Postnummer, Type, Vejnavn)
                                VALUES('Aarhus N', 63, 8200, 'Kollega', 'Randersvej'),
                                ('Aarhus N', 17, 8200, 'Kollega', 'Finlandsgade'),
                                ('Brabrand', 199, 8220, 'Kollega', 'Stenaldervej'),
                                ('Kolding', 24, 6000, 'Ven', 'Tvedvej'),
                                ('Haderslev', 109, 6100, 'Ven', 'Naffet')";
            PerformNonQuery(insertCmdText);
        }

        public void CreateDataForPerson()
        {
            // insert person entries to database
            var insertCmdText = @"INSERT INTO Person (Fornavn, Mellemnavn, Efternavn, Type, AdresseID)
                                VALUES ('Alex', 'Justesen', 'Karlsen', 'Kollega', (SELECT AdresseID FROM Adresse WHERE Vejnavn = 'Randersvej')),
                                ('Emil', '', 'Nyborg', 'Kollega', (SELECT AdresseID FROM Adresse WHERE Vejnavn = 'Finlandsgade')),
                                ('Lasse', 'Hammer', 'Priebe', 'Kollega', (SELECT AdresseID FROM Adresse WHERE Vejnavn = 'Stenaldervej')),
                                ('Søren', 'Hedensted', 'Hansen', 'Ven', (SELECT AdresseID FROM Adresse WHERE Vejnavn = 'Tvedvej')),
                                ('Anders', 'Bo', 'Andersen', 'Ven', (SELECT AdresseID FROM Adresse WHERE Vejnavn = 'Naffet')),
                                ('Bente', 'Bo', 'Andersen', 'Ven', (SELECT AdresseID FROM Adresse WHERE Vejnavn = 'Naffet')),
                                ('Frederik', 'Bo', 'Andersen', 'Ven', (SELECT AdresseID FROM Adresse WHERE Vejnavn = 'Naffet')),
                                ('Frida', 'Bo', 'Andersen', 'Ven', (SELECT AdresseID FROM Adresse WHERE Vejnavn = 'Naffet')),
                                ('Ralf', 'Bo', 'Andersen', 'Ven', (SELECT AdresseID FROM Adresse WHERE Vejnavn = 'Naffet')),
                                ('Valentin', 'Hedensted', 'Hansen', 'Ven', (SELECT AdresseID FROM Adresse WHERE Vejnavn = 'Tvedvej')),
                                ('Samson', 'Son', 'Hanson', 'Ven', (SELECT AdresseID FROM Adresse WHERE Vejnavn = 'Tvedvej'))";
            PerformNonQuery(insertCmdText);
        }

        public void CreateDataForTelefon()
        {
            // insert telephone entries to database
            var insertCmdText = @"INSERT INTO Telefon (Nummer, Type, PersonID)
                                VALUES (23363745, 'Privat', (SELECT PersonID FROM Person WHERE Fornavn = 'Alex')),
                                (28341601, 'Privat', (SELECT PersonID FROM Person WHERE Fornavn = 'Emil')),
                                (93980109, 'Privat', (SELECT PersonID FROM Person WHERE Fornavn = 'Lasse')),
                                (112, 'Arbejde', (SELECT PersonID FROM Person WHERE Fornavn = 'Lasse')),
                                (12345678, 'Privat', (SELECT PersonID FROM Person WHERE Fornavn = 'Søren')),
                                (23456789, 'Privat', (SELECT PersonID FROM Person WHERE Fornavn = 'Anders')),
                                (34567890, 'Privat', (SELECT PersonID FROM Person WHERE Fornavn = 'Bente')),
                                (45678901, 'Privat', (SELECT PersonID FROM Person WHERE Fornavn = 'Frederik')),
                                (56789012, 'Privat', (SELECT PersonID FROM Person WHERE Fornavn = 'Frida')),
                                (67890123, 'Privat', (SELECT PersonID FROM Person WHERE Fornavn = 'Ralf')),
                                (78901234, 'Privat', (SELECT PersonID FROM Person WHERE Fornavn = 'Valentin')),
                                (89012345, 'Privat', (SELECT PersonID FROM Person WHERE Fornavn = 'Samson'))";
            PerformNonQuery(insertCmdText);
        }

        // MARK: - Read Data

        public void ReadDataForPerson()
        {
            SqlDataReader rdr = null;

            try
            {
                // open connection to database
                _conn.Open();

                // setup a new command
                var cmd = new SqlCommand("SELECT Fornavn, Mellemnavn, Efternavn FROM Person", _conn);

                // get reader from command
                rdr = cmd.ExecuteReader();

                // print all results from the reader
                Console.WriteLine(">> Fornavn, Mellemnavn, Efternavn");
                while (rdr.Read())
                {
                    Console.WriteLine(rdr[0].ToString() + " " + rdr[1].ToString() + " " + rdr[2].ToString());
                }
            }
            finally
            {
                rdr?.Close();
                _conn?.Close();
            }
        }

        public void ReadDataForPersonAndAdresse()
        {
            SqlDataReader rdr = null;

            try
            {
                // open connection to database
                _conn.Open();

                // setup a new command
                var cmd = new SqlCommand("SELECT Efternavn, Vejnavn, Husnummer, Bynavn, Postnummer FROM Person JOIN Adresse ON Adresse.AdresseID = Person.AdresseID", _conn);

                // get reader from command
                rdr = cmd.ExecuteReader();

                // print all results from the reader
                Console.WriteLine(">> Fornavn, Vejnavn, Husnummer, Bynavn, Postnummer");
                while (rdr.Read())
                {
                    Console.WriteLine(rdr[0].ToString() + " " + rdr[1].ToString() + " " + rdr[2].ToString() + " " + rdr[3].ToString() + " " + rdr[4].ToString());
                }
            }
            finally
            {
                rdr?.Close();
                _conn?.Close();
            }
        }

        public void ReadDataForPersonAndTelefon()
        {
            SqlDataReader rdr = null;

            try
            {
                // open connection to database
                _conn.Open();

                // setup a new command
                var cmd = new SqlCommand("SELECT Fornavn, Mellemnavn, Efternavn, Nummer, Telefon.Type FROM Person JOIN Telefon ON Telefon.PersonID = Person.PersonID;", _conn);

                // get reader from command
                rdr = cmd.ExecuteReader();

                // print all results from the reader
                Console.WriteLine(">> Fornavn, Mellemnavn, Efternavn, Nummer, Telefon.Type");
                while (rdr.Read())
                {
                    Console.WriteLine(rdr[0].ToString() + " " + rdr[1].ToString() + " " + rdr[2].ToString() + " " + rdr[3].ToString() + " " + rdr[4].ToString());
                }
            }
            finally
            {
                rdr?.Close();
                _conn?.Close();
            }
        }

        // MARK: - Update Data

        public void UpdateTranslateVen()
        {
            PerformNonQuery("UPDATE Adresse SET Type='Privat' WHERE Type='Ven'");
        }

        public void UpdateTranslateKollega()
        {
            PerformNonQuery("UPDATE Adresse SET Type='Arbejde' WHERE Type='Kollega'");
        }

        public void UpdateFixEmil()
        {
            PerformNonQuery("UPDATE Person SET Mellemnavn='Influenza' WHERE Fornavn='Emil';");
        }

        // MARK: - Delete Data

        public void DeleteFakeData()
        {
            // delete all fake 112 telephone entries
            PerformNonQuery("DELETE FROM Telefon WHERE Nummer = 112");
        }

        public void DeleteAllData()
        {
            // delete all data from all tables
            PerformNonQuery("DELETE FROM Telefon");
            PerformNonQuery("DELETE FROM Person");
            PerformNonQuery("DELETE FROM Adresse");
        }

    }
}
