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
            for (var i = 0; i < _threads.Length; i++)
            {
                _threads[i] = new List<string>();
            }
            NThreads = 0;
        }

        public void Spawn(string name, Priority priority)
        {
            AssertThreadNotExisting(name);
            _threads[(int) priority].Add(name);
            NThreads++;
        }

        private void AssertThreadNotExisting(string name)
        {
            // Throws an exception if thread already exists.
            for (var i = 0; i < Enum.GetNames(typeof(Priority)).Length; i++)
            {
                for (var j = 0; j < _threads[i].Count; j++)
                {
                    if (_threads[i][j] == name)
                    {
                        throw new ArgumentException(string.Format($"Thread named {name} already exists"));
                    }
                }
            }
        }

        public void Schedule()
        {
            string name;

            if (_threads[(int) Priority.High].Count != 0)
            {
                name = _threads[(int) Priority.High].First();
            }
            else if (_threads[(int) Priority.Med].Count != 0)
            {
                name = _threads[(int)Priority.Med].First();
            }
            else if (_threads[(int) Priority.Low].Count != 0)
            {
                name = _threads[(int) Priority.Low].First();
            }
            else
            {
				throw new NoThreadsException("There are no threads to schedule");
            }

            ActiveThread = name;
        }

        public void Kill(string name)
        {
            // Look search through the array of lists with threads.
            for (var i = 0; i < Enum.GetNames(typeof(Priority)).Length; i++)
            {
                for (var j = 0; j < _threads[i].Count; j++)
                {
                    var threadName = _threads[i][j];
                    if (threadName != name) continue;

                    // If thread is matched, remove it.
                    _threads[i].RemoveAt(j);
                    NThreads--;

                    // Check if active thread should be set to null.
                    if (NThreads == 0 || threadName == ActiveThread)
                    {
                        ActiveThread = null;
                    }

                    return;
                }
            }

            throw new ArgumentException(string.Format($"Thread named '{name}' doesn't exists"));
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
                    return;
                }
            }

            throw new ArgumentException(string.Format($"Thread named '{name}' doesn't exists"));
        }

        public Priority GetPriority(string name)
        {
            // Look search through the array of lists with threads.
            for (var i = 0; i < Enum.GetNames(typeof(Priority)).Length; i++)
            {
                for (var j = 0; j < _threads[i].Count; j++)
                {
                    var threadName = _threads[i][j];
                    if (threadName != name) continue;

                    // Return the threads priority.
                    return (Priority) i;
                }
            }

            throw new ArgumentException(string.Format($"Thread named '{name}' doesn't exists"));
        }

        public void Rename(string name, string newName)
        {
            AssertThreadNotExisting(newName);

            // Check if the thread exists, if so rename it to the new name.
            for (var i = 0; i < Enum.GetNames(typeof(Priority)).Length; i++)
            {
                for (var j = 0; j < _threads[i].Count; j++)
                {
                    if (_threads[i][j] != name) continue;
                    _threads[i][j] = newName;
                    return;
                }
            }

            throw new ArgumentException(string.Format($"Thread named '{name}' doesn't exists"));
        }

        public int GetNThreads(Priority pri)
        {
            // Returns the number of threads at a given priority.
            return _threads[(int) pri].Count;
        }
    }
}

public class NoThreadsException : InvalidOperationException
{
	public NoThreadsException (string message) : base (message) {}
}