using System;
using System.Collections.Generic;

namespace GUIEksamen
{
    static class VittighedsListClass
    {
                                // Dato, Vittighed, Kilde, Liste af emner
        public static List<Tuple<DateTime, string, string, string>> VittighedsList = new List<Tuple<DateTime, string, string, string>>();

        static VittighedsListClass()
        {
            //Add hardcoded vittigheder
            DateTime dato = DateTime.Parse("13.6.2016");
            AddVittighed(dato, "Hvorfor gik kyllingen over vejen? For at komme over på den anden side.", "PHP-bog", "kylling, gåde");

            dato = DateTime.Parse("13.6.2016");
            AddVittighed(dato, "Hvorfor gik kalkunen over vejen? Fordi det var kyllingens fridag.", "Arthur", "kalkun, gåde");

            dato = DateTime.Parse("13.6.2016");
            AddVittighed(dato, "Hvorfor gik fasanen over vejen? For at bevise at den ikke var en kylling.", "Sofie", "fasan, gåde");

            dato = DateTime.Parse("13.6.2016");
            AddVittighed(dato, "Hvorfor gik kyllingen over Möbius bånd? For at komme over på den samme side.", "Wikipedia", "kylling, gåde, matematik");
        }

        public static List<Tuple<DateTime, string, string, string>> SearchInVittighedsList(string emne)
        {
            emne = emne.ToLower();
            var list = new List<Tuple<DateTime, string, string, string>>();
            foreach (var vittighed in VittighedsList)
            {
                if (vittighed.Item4.Contains(emne))
                    list.Add(vittighed);
            }

            return list;
        }

        public static void AddVittighed(DateTime dato, string vittighed, string kilde, string emner)
        {
            Tuple<DateTime, string, string, string> tuple = new Tuple<DateTime, string, string, string>(dato, vittighed, kilde, emner);
            VittighedsList.Add(tuple);
        }
    }
}
