using System;
using NUnit.Framework;
using NSubstitute;

namespace DoorControl.Test.Unit
{
    [TestFixture]
    public class DoorControlUnitTest
    {
        private DoorControl _uut;
        private IDoor _door;
        private IEntryNotification _entryNotification;
        private IUserValidation _userValidation;
        private const int FakeId = 0;

        [SetUp]
        public void Init()
        {
            _door = Substitute.For<IDoor>();
            _door.When(door => door.Open()).Do(_ => _door.Controller?.DoorOpened());
            _door.When(door => door.Close()).Do(_ => _door.Controller?.DoorClosed());
            _entryNotification = Substitute.For<IEntryNotification>();
            _userValidation = Substitute.For<IUserValidation>();
            _uut = new DoorControl
            {
                Validation = _userValidation,
                Door = _door,
                Notification = _entryNotification
            };
        }

        // Request Entry

        [Test]
        public void RequestEntry_AcceptsValidationWithTwoImmediateRequests_NoExceptions()
        {
            _userValidation.ValidateEntryRequest(Arg.Any<int>()).Returns(true);
            _uut.RequestEntry(FakeId);
            _uut.RequestEntry(FakeId);
        }

        // RequestEntry - Door Interaction

        [Test]
        public void RequestEntry_AcceptsValidation_CallDoorOpen()
        {
            _userValidation.ValidateEntryRequest(Arg.Any<int>()).Returns(true);
            _uut.RequestEntry(FakeId);
            _door.Received().Open();
        }

        [Test]
        public void RequestEntry_AcceptsValidation_CallDoorClose()
        {
            _userValidation.ValidateEntryRequest(Arg.Any<int>()).Returns(true);
            _uut.RequestEntry(FakeId);
            _door.Received().Close();
        }

        [Test]
        public void RequestEntry_DeclinesValidation_DoorDidNotReceiveOpenCall()
        {
            _userValidation.ValidateEntryRequest(Arg.Any<int>()).Returns(false);
            _uut.RequestEntry(FakeId);
            Assert.Catch<Exception>(() =>
            {
                _door.Received().Open();
            });
        }

        [Test]
        public void RequestEntry_DeclinesValidation_DoorDidNotReceiveCloseCall()
        {
            _userValidation.ValidateEntryRequest(Arg.Any<int>()).Returns(false);
            _uut.RequestEntry(FakeId);
            Assert.Catch<Exception>(() =>
            {
                _door.Received().Close();
            });
        }

        // Request Entry - Notification Interaction

        [Test]
        public void RequestEntry_AcceptsValidation_DidReceiveNotification()
        {
            _userValidation.ValidateEntryRequest(Arg.Any<int>()).Returns(true);
            _uut.RequestEntry(FakeId);
            _entryNotification.Received().NotifyEntryGranted();
        }
    }
}