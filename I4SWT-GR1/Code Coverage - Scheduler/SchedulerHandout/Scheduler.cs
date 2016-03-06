using System;
using System.Collections.Generic;
using System.Linq;

namespace SchedulerHandout
{
    // Provided implementation of a Scheduler - contains constructor, Spawn() and the Priority enum
    public class Scheduler
    {
        public enum Priority
        {
            High,
            Med,
            Low
        };

        private readonly List<string>[] _threads;
		public string ActiveThread { get; private set; }
		public int NThreads { get; private set; }

        public Scheduler()
        {
            _threads = new List<string>[Enum.GetNames(typeof (Priority)).Length]; // Create threads array
            for (var i = 0; i < _threads.Count(); i++)
            {
                _threads[i] = new List<string>();
            }
            NThreads = 0;
        }

        public void Spawn(string name, Priority priority)
        {
            for (var i = 0; i < Enum.GetNames(typeof (Priority)).Length; i++)
            {
                for (var j = 0; j < _threads[i].Count; j++)
                {
                    if (_threads[i][j] == name)
                    {
                        throw new System.ArgumentException("Thread already exists");
                    }
                }
            }
            _threads[(int) priority].Add(name);
            NThreads++;
        }

        public void Schedule()
        {
            string _name;

            if (_threads[(int) Priority.High].Count != 0)
            {
                _name = _threads[(int) Priority.High].First();
            }
            else if (_threads[(int) Priority.Med].Count != 0)
            {
                _name = _threads[(int)Priority.Med].First();
            }
            else if (_threads[(int) Priority.Low].Count != 0)
            {
                _name = _threads[(int) Priority.Low].First();
            }
            else
            {
				throw new NoThreadsException("There are no threads to schedule");
            }

            ActiveThread = _name;
        }

        public void Kill(string name)
        {
            string threadName;

            // Look search through the array of lists with threads.
            for (var i = 0; i < Enum.GetNames(typeof(Priority)).Length; i++)
            {
                for (var j = 0; j < _threads[i].Count; j++)
                {
                    threadName = _threads[i][j];
                    if (threadName != name) continue;

                    // If thread is matched, remove it.
                    _threads[i].RemoveAt(j);
                    NThreads--;

                    // Reschedule if removed thread was active.
                    if (NThreads > 1 && threadName == ActiveThread)
                    {
                        Schedule();
                    }
                    else if (NThreads == 1)
                    {
                        ActiveThread = null;
                    }

                    return;
                }
            }

            throw new System.ArgumentException(string.Format($"Thread named '{name}' doesn't exists"));
        }

        public void SetPriority(string name, Priority pri)
        {
            // Look search through the array of lists with threads.
            for (var i = 0; i < Enum.GetNames(typeof(Priority)).Length; i++)
            {
                for (var j = 0; j < _threads[i].Count; j++)
                {
                    if (_threads[i][j] != name) continue;

                    // If thread is matched, remove it.
                    _threads[i].RemoveAt(j);

                    // Add the thread to the new priority list and reschedule.
                    _threads[(int)pri].Add(name);
                    Schedule();
                    return;
                }
            }

            throw new System.ArgumentException(string.Format($"Thread named '{name}' doesn't exists"));
        }
    }
}

public class NoThreadsException : Exception
{
	public NoThreadsException (string message) : base (message) {}
}