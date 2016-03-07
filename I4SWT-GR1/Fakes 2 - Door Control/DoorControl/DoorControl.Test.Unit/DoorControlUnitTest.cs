using NUnit.Framework;
using DoorControl.Test.Unit.Fakes;

namespace DoorControl.Test.Unit
{
    [TestFixture]
    public class DoorControlUnitTest
    {
        private DoorControl _uut;

        [SetUp]
        public void Init()
        {
            _uut = new DoorControl
            {
                Validation = new FakeValidation(),
                Door = new FakeDoor(),
                Notification = new FakeNotification()
            };
        }

        // Request Entry

        [Test]
        public void RequestEntry_AcceptsValidationWithTwoImmediateRequests_NoExceptions()
        {
            var stubValidation = new FakeValidation { AcceptsValidation = true };
            _uut.Validation = stubValidation;
            _uut.RequestEntry(0);
            Assert.DoesNotThrow(() =>
            {
                _uut.RequestEntry(1);
            });
        }

        // RequestEntry - Door Interaction

        [Test]
        public void RequestEntry_AcceptsValidation_DoorDidOpen()
        {
            var stubValidation = new FakeValidation { AcceptsValidation = true };
            _uut.Validation = stubValidation;
            var mockDoor = new FakeDoor();
            _uut.Door = mockDoor;
            _uut.RequestEntry(0);
            Assert.That(mockDoor.DoorDidOpen, Is.EqualTo(true));
        }

        [Test]
        public void RequestEntry_AcceptsValidation_DoorDidClose()
        {
            var stubValidation = new FakeValidation { AcceptsValidation = true };
            _uut.Validation = stubValidation;
            var mockDoor = new FakeDoor();
            _uut.Door = mockDoor;
            _uut.RequestEntry(0);
            Assert.That(mockDoor.DoorDidClose, Is.EqualTo(true));
        }

        [Test]
        public void RequestEntry_DeclinesValidation_DoorDidNotOpen()
        {
            var stubValidation = new FakeValidation { AcceptsValidation = false };
            _uut.Validation = stubValidation;
            var mockDoor = new FakeDoor();
            _uut.Door = mockDoor;
            _uut.RequestEntry(0);
            Assert.That(mockDoor.DoorDidOpen, Is.EqualTo(false));
        }

        // Request Entry - Notification Interaction

        [Test]
        public void RequestEntry_AcceptValidation_DidReceiveNotification()
        {
            var stubValidation = new FakeValidation { AcceptsValidation = true };
            _uut.Validation = stubValidation;
            var mockNotification = new FakeNotification();
            _uut.Notification = mockNotification;
            _uut.RequestEntry(0);
            Assert.That(mockNotification.DidReceiveNotification, Is.EqualTo(true));
        }
    }
}