namespace DoorControl.Test.Unit
{
    namespace Fakes
    {
        public class FakeDoor: IDoor
        {
            public IDoorControl Controller { get; set; }
            public bool DoorDidOpen { get; private set; }
            public bool DoorDidClose { get; private set; }

            public FakeDoor()
            {
                DoorDidOpen = false;
                DoorDidClose = false;
            }

            public void Open()
            {
                DoorDidOpen = true;
                Controller?.DoorOpened();
            }

            public void Close()
            {
                DoorDidClose = true;
                Controller?.DoorClosed();
            }
        }
    }
}
