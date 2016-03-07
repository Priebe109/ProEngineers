namespace DoorControl
{
    namespace Defaults
    {
        public class Door : IDoor
        {
            public IDoorControl Controller { get; set; }

            public void Open()
            {
                Controller?.DoorOpened();
            }

            public void Close()
            {
                Controller?.DoorClosed();
            }
        }
    }
}
