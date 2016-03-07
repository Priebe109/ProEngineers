using System;

namespace DoorControl
{
    namespace Defaults
    {
        public class EntryNotification : IEntryNotification
        {
            public void NotifyEntryGranted()
            {
                Console.WriteLine("Entry granted!");
            }
        }
    }
}