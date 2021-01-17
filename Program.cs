using System;

namespace Multithreading
{
    public class Program
    {
        public static void Main()
        {
            var t1 = new CustomThread("Thread 1", GetRandomNumber(3000));
            var t2 = new CustomThread("Thread 2", GetRandomNumber(3000));
            var t3 = new CustomThread("Thread 3", GetRandomNumber(3000));
            var t4 = new CustomThread("Thread 4", GetRandomNumber(3000));
            var t5 = new CustomThread("Thread 5", GetRandomNumber(3000));
            var t6 = new CustomThread("Thread 6", GetRandomNumber(3000));
            var t7 = new CustomThread("Thread 7", GetRandomNumber(3000));

            t2.WaitFor(t1);
            t3.WaitFor(t1, t2, t4, t5, t6, t7);
            t4.WaitFor(t6);
            t5.WaitFor(t2);
            t7.WaitFor(t4);

            CustomThread[] threads = { t1, t2, t3, t4, t5, t6, t7 };
            
            foreach (var thread in threads) thread.Start();
        }

        private static int GetRandomNumber(int max) => new Random().Next(max);
    }
}