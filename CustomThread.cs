using System;
using System.Collections.Generic;
using System.Threading;

namespace Multithreading
{
    public class CustomThread
    {
        private readonly CountdownEvent _countdown;
        private readonly List<CountdownEvent> _waiters;
        private int _threadsToWait;
        private readonly int _workTime;
        private readonly Thread _thread;
        private string Name { get; }

        public CustomThread(string name, int workTime)
        {
            Name = name;
            _workTime = workTime;
            _countdown = new CountdownEvent(0);
            _waiters = new List<CountdownEvent>();
            _thread = new Thread(DoWork);
        }

        public void WaitFor(params CustomThread[] threadsToWait)
        {
            if (_thread.ThreadState == ThreadState.Running) throw new ThreadStateException("Thread is running");

            foreach (var thread in threadsToWait)
            {
                if (thread == this || thread._waiters.Contains(_countdown)) continue;
                thread._waiters.Add(_countdown);
                _threadsToWait++;
            }
        }

        private void DoWork()
        {
            try
            {
                _countdown.Reset(_threadsToWait);
                _countdown.Wait();
                Console.WriteLine(Name);
                Thread.Sleep(_workTime);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                foreach (var waiter in _waiters) waiter.Signal();
            }
        }

        public void Start() => _thread.Start();
    }
}