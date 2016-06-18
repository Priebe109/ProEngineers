using System;
using static SchedulerHandout.Scheduler;
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
		public void Ctor_DefaultInitialState_NThreadsEqualsZero ()
		{
			Assert.That(_uut.NThreads, Is.EqualTo (0));
		}

        // NThreads

		[Test]
		public void NThreads_AddTwoThreads_NThreadsEqualsTwo()
		{
			_uut.Spawn("SomeThread", Priority.Low);
			_uut.Spawn("AnotherThread", Priority.Med);
			Assert.That(_uut.NThreads, Is.EqualTo (2));
		}

        // Spawn

        [Test]
		public void Spawn_AddTheSameThreadTwice_ArgumentException()
		{
            _uut.Spawn("SomeThread", Priority.Low);

            Assert.Catch<ArgumentException>(() =>
			{
			    _uut.Spawn("SomeThread", Priority.Med);
			});
		}

        // Schedule

        [Test]
		public void Schedule_CalledWhenNoThreadsAdded_ExceptionNoThreads()
		{
			Assert.Catch<NoThreadsException>(() =>
			{
			    _uut.Schedule();
			});
		}

	    [TestCase("HighThread", Priority.High, "MedThread", Priority.Med, "HighThread")]
        [TestCase("HighThread", Priority.High, "LowThread", Priority.Low, "HighThread")]
        [TestCase("LowThread", Priority.Low, "MedThread", Priority.Med, "MedThread")]
        public void Schedule_AddThreadsOfDifferentPriority_HighestThreadIsActive(string threadA,
	        Priority threadAPriority, string threadB, Priority threadBPriority, string activeThread)
	    {
	        _uut.Spawn(threadA, threadAPriority);
            _uut.Spawn(threadB, threadBPriority);
            _uut.Schedule();
            Assert.That(_uut.ActiveThread, Is.EqualTo(activeThread));
	    }

        [Test]
        public void Schedule_OneThreadsScheduled_ActiveThreadIsSameThread()
        {
            _uut.Spawn("SomeThread", Priority.Low);
            _uut.Schedule();
            Assert.That(_uut.ActiveThread, Is.EqualTo("SomeThread"));
        }

        // Kill

	    [Test]
	    public void Kill_UnexistingThreadKilled_ArgumentException()
	    {
            Assert.Catch<ArgumentException>(() =>
            {
                _uut.Kill("SomeUnexistingThread");
            });
        }

        [Test]
        public void Kill_ThreadSpawnedAndKilled_NThreadsIsZero()
        {
            _uut.Spawn("SomeThread", Priority.High);
            _uut.Kill("SomeThread");
            Assert.That(_uut.NThreads, Is.EqualTo(0));
        }

	    [Test]
	    public void Kill_TwoHighThreadsSpawnedAndLastAddedThreadIsKilled_NThreadsIsOne()
	    {
            _uut.Spawn("HighThread1", Priority.High);
            _uut.Spawn("HighThread2", Priority.High);
            _uut.Kill("HighThread2");
            Assert.That(_uut.NThreads, Is.EqualTo(1));
        }

        // SetPriority

	    [Test]
	    public void SetPriority_OnUnexistingThread_ArgumentException()
	    {
	        Assert.Catch<ArgumentException>(() =>
	        {
	            _uut.SetPriority("SomeUnexistingThread", Priority.High);
	        });
	    }

	    [Test]
	    public void SetPriority_LowAndMedThreadSpawnedAndLowThreadSetToHighPriorityAndScheduled_LowThreadIsActiveThread()
        {
            _uut.Spawn("MedThread", Priority.Med);
            _uut.Spawn("LowThread", Priority.Low);
            _uut.SetPriority("LowThread", Priority.High);
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

	    [TestCase(Priority.Low)]
        [TestCase(Priority.Med)]
        [TestCase(Priority.High)]
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
            _uut.Spawn("LowThread", Priority.Low);
            _uut.Spawn("MedThread", Priority.Med);
            Assert.Catch<ArgumentException>(() =>
            {
                _uut.Rename("LowThread", "MedThread");
            });
        }

        [TestCase(Priority.Low)]
        [TestCase(Priority.Med)]
        [TestCase(Priority.High)]
        public void Rename_ExistingThreadToNewValidName_GetPriorityOfNewThreadNameReturnsOldValue(Priority priority)
	    {
            _uut.Spawn("SomeThread", priority);
            _uut.Rename("SomeThread", "MyLovelyThread");
            Assert.That(_uut.GetPriority("MyLovelyThread"), Is.EqualTo(priority));
        }

	    [Test]
	    public void Rename_TwoThreadsSamePrioritySpawnedAndLastThreadRenamedToFirstThread_GetPriorityOfNewThreadNameReturnsOldValue()
	    {
            _uut.Spawn("HighThreadA", Priority.High);
            _uut.Spawn("HighThreadB", Priority.High);
            _uut.Rename("HighThreadB", "MyLovelyThread");
	        Assert.That(_uut.GetPriority("MyLovelyThread"), Is.EqualTo(Priority.High));
	    }

        // GetNThreads

	    [TestCase(Priority.Low)]
	    [TestCase(Priority.Med)]
	    [TestCase(Priority.High)]
	    public void GetNThreads_TwoThreadsOfSamePrioritySpawned_ReturnsTwoForTheGivenPriority(Priority priority)
	    {
            _uut.Spawn("SomeThreadA", priority);
            _uut.Spawn("SomeThreadB", priority);
            Assert.That(_uut.GetNThreads(priority), Is.EqualTo(2));
        }
    }
}

