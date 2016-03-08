namespace DoorControl.Defaults
{
    public class DefaultDoor : IDoor
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
