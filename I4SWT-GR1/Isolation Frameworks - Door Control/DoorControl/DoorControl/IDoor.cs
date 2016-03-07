namespace DoorControl
{
    public interface IDoor
    {
        IDoorControl Controller { get; set; }
        void Open();
        void Close();
    }
}