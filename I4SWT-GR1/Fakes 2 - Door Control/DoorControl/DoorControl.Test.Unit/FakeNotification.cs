namespace DoorControl.Test.Unit
{
    namespace Fakes
    {
        public class FakeNotification: IEntryNotification
        {
            public bool DidReceiveNotification { get; set; }

            public FakeNotification()
            {
                DidReceiveNotification = false;
            }

            public void NotifyEntryGranted()
            {
                DidReceiveNotification = true;
            }
        }
    }
}
