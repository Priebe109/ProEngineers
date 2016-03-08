
namespace DoorControl
{
    public class DoorControl: IDoorControl
    {
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

        private bool _busy;

        public DoorControl()
        {
            _busy = false;
        }

        public void RequestEntry(int id)
        {
            // Reject request if busy.
            if (_busy || Validation == null || Door == null || Notification == null) return;

            // Validate entry request.
            if (!Validation.ValidateEntryRequest(id)) return;
            Door.Open();
            Notification.NotifyEntryGranted();

            // Reject requests until door closes again.
            _busy = true;
        }

        public void DoorOpened()
        {
            // Close the door again after a short delay.
            Door?.Close();
        }

        public void DoorClosed()
        {
            // Ready for new request.
            _busy = false;
        }
    }
}