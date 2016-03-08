
namespace DoorControl
{
    public enum DoorState
    {
        Closed,
        Opening,
        Open,
        Breached
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
            // Initial state is closed.
            State = DoorState.Closed;
        }

        public void RequestEntry(int id)
        {
            // Reject request if another one is in progress (door is not closed).
            if (State != DoorState.Closed || Validation == null || Door == null) return;

            // Validate failed.
            if (!Validation.ValidateEntryRequest(id))
            {
                Notification?.NotifyEntryDenied();
                return;
            }

            // Validation succeeded.
            State = DoorState.Opening;
            Door.Open();
            Notification?.NotifyEntryGranted();
        }

        public void DoorOpened()
        {
            // Close the door again.
            Door?.Close();

            // Check if door was breached.
            if (State == DoorState.Closed)
            {
                Alarm?.SignalAlarm();
                State = DoorState.Breached;
                return;
            }

            State = DoorState.Open;
        }

        public void DoorClosed()
        {
            // Ready for new request.
            State = DoorState.Closed;
        }
    }
}