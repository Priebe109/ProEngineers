
namespace DoorControl
{
    public enum DoorState
    {
        Closed,
        Opening,
        Open
    }

    public class DoorControl: IDoorControl
    {
        public DoorState State { get; private set; }
        public IAlarm Alarm { get; set; }
        public IUserValidation Validation { get; set; }
        public IEntryNotification Notification { get; set; }
        private IDoor _door;
        public IDoor Door
        {
            get { return _door; }
            set
            {
                _door = value;
                _door.Controller = this;
            }
        }

        public DoorControl()
        {
            State = DoorState.Closed;
        }

        public void RequestEntry(int id)
        {
            // Reject request if another one is in progress (door is not closed).
            if (State != DoorState.Closed || Validation == null || Door == null || Notification == null) return;

            // Validate entry request.
            if (!Validation.ValidateEntryRequest(id)) return;
            Door.Open();
            Notification.NotifyEntryGranted();

            // Reject requests until door closes again.
            State = DoorState.Opening;
        }

        public void DoorOpened()
        {
            // Close the door again.
            State = DoorState.Open;
            Door?.Close();
        }

        public void DoorClosed()
        {
            // Ready for new request.
            State = DoorState.Closed;
        }
    }
}