using System;

namespace DoorControl.Defaults
{
    public class DefaultNotification : IEntryNotification
    {
        public void NotifyEntryGranted()
        {
            Console.WriteLine("Entry granted!");
        }
    }
}