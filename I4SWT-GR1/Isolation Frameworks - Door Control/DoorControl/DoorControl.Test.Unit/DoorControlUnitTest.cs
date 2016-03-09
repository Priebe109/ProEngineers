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
        private IAlarm _alarm;

        [SetUp]
        public void Init()
        {
            _door = Substitute.For<IDoor>();
            _door.When(door => door.Open()).Do(_ => _door.Controller?.DoorOpened());
            _door.When(door => door.Close()).Do(_ => _door.Controller?.DoorClosed());
            _entryNotification = Substitute.For<IEntryNotification>();
            _userValidation = Substitute.For<IUserValidation>();
            _alarm = Substitute.For<IAlarm>();
            _uut = new DoorControl
            {
                Validation = _userValidation,
                Door = _door,
                Notification = _entryNotification,
                Alarm = _alarm
            };
        }

        // Constructor

        [Test]
        public void Ctor_InitialState_StateIsClosed()
        {
            Assert.That(_uut.State, Is.EqualTo(DoorState.Closed));
        }

        // Request Entry

        [Test]
        public void RequestEntry_NoDoorConnected_StateRemainsClosed()
        {
            _userValidation.ValidateEntryRequest(Arg.Any<int>()).Returns(true);
            _door = null;
            Assert.That(_uut.State, Is.EqualTo(DoorState.Closed));
        }

        [Test]
        public void RequestEntry_DeclinesValidationRequest_StateRemainsClosed()
        {
            _userValidation.ValidateEntryRequest(Arg.Any<int>()).Returns(false);
            _uut.RequestEntry(FakeId);
            Assert.That(_uut.State, Is.EqualTo(DoorState.Closed));
        }

        [Test]
        public void RequestEntry_AcceptsValidationWithTwoImmediateRequests_NoExceptions()
        {
            _userValidation.ValidateEntryRequest(Arg.Any<int>()).Returns(true);
            _uut.RequestEntry(FakeId);
            _uut.RequestEntry(FakeId);
        }

        // DoorControl - Door Interaction

        [Test]
        public void DoorControl_AcceptsValidationRequest_CallDoorOpen()
        {
            _userValidation.ValidateEntryRequest(Arg.Any<int>()).Returns(true);
            _uut.RequestEntry(FakeId);
            _door.Received().Open();
        }

        [Test]
        public void DoorControl_NoValidationConnected_DoorDidNotReceiveOpenCall()
        {
            _userValidation = null;
            _uut.RequestEntry(FakeId);
            _door.DidNotReceive().Open();
        }

        [Test]
        public void DoorControl_AcceptsValidationRequest_CallDoorClose()
        {
            _userValidation.ValidateEntryRequest(Arg.Any<int>()).Returns(true);
            _uut.RequestEntry(FakeId);
            _door.Received().Close();
        }

        [Test]
        public void DoorControl_DeclinesValidationRequest_DoorDidNotReceiveOpenCall()
        {
            _userValidation.ValidateEntryRequest(Arg.Any<int>()).Returns(false);
            _uut.RequestEntry(FakeId);
            _door.DidNotReceive().Open();
        }

        [Test]
        public void DoorControl_NoValidationConnected_DoorDidNotReceiveCloseCall()
        {
            _userValidation = null;
            _uut.RequestEntry(FakeId);
            _door.DidNotReceive().Close();
        }

        [Test]
        public void DoorControl_DeclinesValidationRequest_DoorDidNotReceiveCloseCall()
        {
            _userValidation.ValidateEntryRequest(Arg.Any<int>()).Returns(false);
            _uut.RequestEntry(FakeId);
            _door.DidNotReceive().Close();
        }

        // DoorControl - Notification Interaction

        [Test]
        public void DoorControl_AcceptsValidationRequest_CallNotifyEntryGranted()
        {
            _userValidation.ValidateEntryRequest(Arg.Any<int>()).Returns(true);
            _uut.RequestEntry(FakeId);
            _entryNotification.Received().NotifyEntryGranted();
        }

        [Test]
        public void DoorControl_DeclinesValidation_CallNotifyEntryDenied()
        {
            _userValidation.ValidateEntryRequest(Arg.Any<int>()).Returns(false);
            _uut.RequestEntry(FakeId);
            _entryNotification.Received().NotifyEntryDenied();
        }

        // DoorControl - Alarm Interaction

        [Test]
        public void DoorControl_DoorOpenedWithoutRequestEntry_CallSignalAlarm()
        {
            _door.Open();
            _alarm.Received().SignalAlarm();
        }

        [Test]
        public void DoorControl_AcceptsValidationRequest_AlarmDidNotReceiveSignal()
        {
            _userValidation.ValidateEntryRequest(Arg.Any<int>()).Returns(true);
            _uut.RequestEntry(FakeId);
            _alarm.DidNotReceive().SignalAlarm();
        }
    }
}