namespace DoorControl
{
    public interface IDoorControl
    {
        void RequestEntry(int id);
        void DoorOpened();
        void DoorClosed();
    }
}
