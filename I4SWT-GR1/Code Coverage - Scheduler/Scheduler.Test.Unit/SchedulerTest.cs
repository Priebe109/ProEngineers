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
			_uut = new SchedulerHandout.Scheduler ();
		}

		[Test]
		public void Ctor_EmptyThreadLists_NThreadsEqualsZero ()
		{
			Assert.That (_uut.NThreads, Is.EqualTo (0));
		}

		[Test]
		public void NThreads_AddTwoThreads_NThreadsEqualsTwo ()
		{
			_uut.Spawn ("SomeThread", SchedulerHandout.Scheduler.Priority.Low);
			_uut.Spawn ("AnotherThread", SchedulerHandout.Scheduler.Priority.Med);
			Assert.That (_uut.NThreads, Is.EqualTo (2));
		}
		
		[Test]
		public void Spawn_AddTheSameThreadTwice_ArgumentException ()
		{
			Assert.Catch<ArgumentException>(() => {
				_uut.Spawn ("SomeThread", SchedulerHandout.Scheduler.Priority.Low);
				_uut.Spawn ("SomeThread", SchedulerHandout.Scheduler.Priority.Med);
			});
		}
			
		[Test]
		public void Schedule_NoThreadsAdded_ExceptionNoThreads ()
		{
			Assert.Catch<NoThreadsException>(() => _uut.Schedule());
		}

		[Test]
		public void Schedule_HighAndMedThreadScheduled_ActiveThreadIsHighThread ()
		{
			_uut.Spawn ("HighThread", SchedulerHandout.Scheduler.Priority.High);
			_uut.Spawn ("MedThread", SchedulerHandout.Scheduler.Priority.Med);
			_uut.Schedule ();
			Assert.That (_uut.ActiveThread, Is.EqualTo ("HighThread"));
		}
	}
}

