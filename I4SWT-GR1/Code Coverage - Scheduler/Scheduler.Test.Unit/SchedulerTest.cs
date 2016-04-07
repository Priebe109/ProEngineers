using System;
using NUnit.Framework;

namespace Scheduler.Test.Unit
{
	[TestFixture]
	public class SchedulerTest
	{
		private SchedulerHandout.Scheduler _uut;

		[SetUp]
		public void Setup() {
			_uut = new SchedulerHandout.Scheduler();
		}

        // Constructor

		[Test]
		public void Ctor_EmptyThreadLists_NThreadsEqualsZero ()
		{
			Assert.That(_uut.NThreads, Is.EqualTo (0));
		}

        // NThreads

		[Test]
		public void NThreads_AddTwoThreads_NThreadsEqualsTwo()
		{
			_uut.Spawn("SomeThread", SchedulerHandout.Scheduler.Priority.Low);
			_uut.Spawn("AnotherThread", SchedulerHandout.Scheduler.Priority.Med);
			Assert.That(_uut.NThreads, Is.EqualTo (2));
		}

        // Spawn

        [Test]
		public void Spawn_AddTheSameThreadTwice_ArgumentException()
		{
			Assert.Catch<ArgumentException>(() =>
			{
			    _uut.Spawn("SomeThread", SchedulerHandout.Scheduler.Priority.Low);
			    _uut.Spawn("SomeThread", SchedulerHandout.Scheduler.Priority.Med);
			});
		}

        // Schedule

        [Test]
		public void Schedule_NoThreadsAdded_ExceptionNoThreads()
		{
			Assert.Catch<NoThreadsException>(() => _uut.Schedule());
		}

		[Test]
		public void Schedule_HighAndMedThreadScheduled_ActiveThreadIsHighThread()
		{
			_uut.Spawn("HighThread", SchedulerHandout.Scheduler.Priority.High);
			_uut.Spawn("MedThread", SchedulerHandout.Scheduler.Priority.Med);
			_uut.Schedule ();
			Assert.That(_uut.ActiveThread, Is.EqualTo ("HighThread"));
		}

	    [Test]
	    public void Schedule_MedAndLowThreadScheduled_ActiveThreadIsMedThread()
	    {
	        _uut.Spawn("MedThread", SchedulerHandout.Scheduler.Priority.Med);
            _uut.Spawn("LowThread", SchedulerHandout.Scheduler.Priority.Low);
            _uut.Schedule ();
	        Assert.That(_uut.ActiveThread, Is.EqualTo("MedThread"));
	    }

        [Test]
        public void Schedule_LowThreadsScheduled_ActiveThreadIsLowThread()
        {
            _uut.Spawn("LowThread", SchedulerHandout.Scheduler.Priority.Low);
            _uut.Schedule();
            Assert.That(_uut.ActiveThread, Is.EqualTo("LowThread"));
        }

        // Kill

	    [Test]
	    public void Kill_UnexistingThreadKilled_ArgumentException()
	    {
            Assert.Catch<ArgumentException>(() => _uut.Kill("SomeUnexistingThread"));
        }

        [Test]
        public void Kill_ThreadSpawnedAndKilled_NThreadsIsZero()
        {
            _uut.Spawn("SomeThread", SchedulerHandout.Scheduler.Priority.High);
            _uut.Kill("SomeThread");
            Assert.That(_uut.NThreads, Is.EqualTo(0));
        }

	    [Test]
	    public void Kill_TwoThreadsSpawnedAndActiveThreadKilledAndScheduled_OtherThreadIsScheduled()
	    {
            _uut.Spawn("HighThread", SchedulerHandout.Scheduler.Priority.High);
            _uut.Spawn("MedThread", SchedulerHandout.Scheduler.Priority.Med);
            _uut.Kill("HighThread");
            _uut.Schedule();
            Assert.That(_uut.ActiveThread, Is.EqualTo("MedThread"));
        }

	    [Test]
	    public void Kill_TwoHighThreadsSpawnedAndLastAddedThreadIsKilled_NThreadsIsOne()
	    {
            _uut.Spawn("HighThread1", SchedulerHandout.Scheduler.Priority.High);
            _uut.Spawn("HighThread2", SchedulerHandout.Scheduler.Priority.High);
            _uut.Kill("HighThread2");
            Assert.That(_uut.NThreads, Is.EqualTo(1));
        }

        // SetPriority

	    [Test]
	    public void SetPriority_OnUnexistingThread_ArgumentException()
	    {
	        Assert.Catch<ArgumentException>(() =>
	        {
	            _uut.SetPriority("SomeUnexistingThread", SchedulerHandout.Scheduler.Priority.High);
	        });
	    }

	    [Test]
	    public void SetPriority_LowAndMedThreadSpawnedAndLowThreadSetToHighPriorityAndScheduled_LowThreadIsActiveThread()
        {
            _uut.Spawn("MedThread", SchedulerHandout.Scheduler.Priority.Med);
            _uut.Spawn("LowThread", SchedulerHandout.Scheduler.Priority.Low);
            _uut.SetPriority("LowThread", SchedulerHandout.Scheduler.Priority.High);
            _uut.Schedule();
            Assert.That(_uut.ActiveThread, Is.EqualTo("LowThread"));
        }

        // GetPriority

	    [Test]
	    public void GetPriority_OfUnexistingThread_ArgumentException()
	    {
	        Assert.Catch<ArgumentException>(() =>
	        {
	            _uut.GetPriority("SomeUnexistingThread");
	        });
	    }

	    [TestCase(SchedulerHandout.Scheduler.Priority.Low)]
        [TestCase(SchedulerHandout.Scheduler.Priority.Med)]
        [TestCase(SchedulerHandout.Scheduler.Priority.High)]
        public void GetPriority_OfExistingThread_CorrectPriorityReturned(SchedulerHandout.Scheduler.Priority priority)
	    {
	        _uut.Spawn("SomeThread", priority);
            Assert.That(_uut.GetPriority("SomeThread"), Is.EqualTo(priority));
	    }

	    [Test]
	    public void GetPriority_TwoHighThreadsSpawnedAndLastOneChecked_EqualPriority()
	    {
	        _uut.Spawn("HighThreadA", SchedulerHandout.Scheduler.Priority.High);
            _uut.Spawn("HighThreadB", SchedulerHandout.Scheduler.Priority.High);
            Assert.That(_uut.GetPriority("HighThreadA"), Is.EqualTo(_uut.GetPriority("HighThreadB")));
        }

        // Rename

        [Test]
	    public void Rename_UnexistingThread_ArgumentException()
	    {
	        Assert.Catch<ArgumentException>(() =>
	        {
                _uut.Rename("SomeUnexistingThread", "NewThreadName");
            }); 
	    }

        [Test]
	    public void Rename_ExistingThreadToAnotherExistingThreadName_ArgumentException()
	    {
            _uut.Spawn("LowThread", SchedulerHandout.Scheduler.Priority.Low);
            _uut.Spawn("MedThread", SchedulerHandout.Scheduler.Priority.Med);
            Assert.Catch<ArgumentException>(() =>
            {
                _uut.Rename("LowThread", "MedThread");
            });
        }

        [TestCase(SchedulerHandout.Scheduler.Priority.Low)]
        [TestCase(SchedulerHandout.Scheduler.Priority.Med)]
        [TestCase(SchedulerHandout.Scheduler.Priority.High)]
        public void Rename_ExistingThreadToNewValidName_GetPriorityOfNewThreadNameReturnsOldValue(SchedulerHandout.Scheduler.Priority priority)
	    {
            _uut.Spawn("SomeThread", priority);
            _uut.Rename("SomeThread", "MyLovelyThread");
            Assert.That(_uut.GetPriority("MyLovelyThread"), Is.EqualTo(priority));
        }

	    [Test]
	    public void Rename_TwoThreadsSamePrioritySpawnedAndLastThreadRenamedToFirstThread_GetPriorityOfNewThreadNameReturnsOldValue()
	    {
            _uut.Spawn("HighThreadA", SchedulerHandout.Scheduler.Priority.High);
            _uut.Spawn("HighThreadB", SchedulerHandout.Scheduler.Priority.High);
            _uut.Rename("HighThreadB", "MyLovelyThread");
	        Assert.That(_uut.GetPriority("MyLovelyThread"), Is.EqualTo(SchedulerHandout.Scheduler.Priority.High));
	    }

        // GetNThreads

	    [TestCase(SchedulerHandout.Scheduler.Priority.Low)]
	    [TestCase(SchedulerHandout.Scheduler.Priority.Med)]
	    [TestCase(SchedulerHandout.Scheduler.Priority.High)]
	    public void GetNThreads_TwoThreadsOfSamePrioritySpawned_ReturnsTwoForTheGivenPriority(SchedulerHandout.Scheduler.Priority priority)
	    {
            _uut.Spawn("SomeThreadA", priority);
            _uut.Spawn("SomeThreadB", priority);
            Assert.That(_uut.GetNThreads(priority), Is.EqualTo(2));
        }
    }
}

