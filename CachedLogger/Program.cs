using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace CachedLoggerHost
{
    class Program
    {
        static void Main(string[] args)
        {

            var m1 = new LogMessage("log message",MessageSeverity.Error);
            var m2 = new LogMessage("log message", MessageSeverity.Error);
            var start = DateTime.Now;
            Debugger.Log(0,"1",$"start{DateTime.Now}");

            if (m1.GetHashCode() == m2.GetHashCode())
            {
                Console.WriteLine("they are the same!");
            }


            var t1 = Task.Run(() =>
            {
                for (int i = 0; i < 5000000; i++)
                {
                    LogHelper.LogMessage(new LogMessage($"message {i}", MessageSeverity.Error));
                }

            });

            var t4 = Task.Run(() =>
            {
                for (int i = 0; i < 5000000; i++)
                {
                    LogHelper.LogMessage(new LogMessage($"message {i}", MessageSeverity.Info));
                }

            });

            var t2 = Task.Run(() =>
            {
                for (int i = 0; i < 5000000; i++)
                {
                    LogHelper.LogMessage(new LogMessage($"message {i}", MessageSeverity.Error));
                }
            });

            //var t3 = Task.Run(() =>
            //{
            //    for (int i = 50000; i < 100000; i++)
            //    {
            //        LogHelper.LogMessage(null);
            //        Thread.Sleep(10);
            //    }
            //});

            Task.WaitAll(t1,t2,t4);

            Console.WriteLine($"start{start}-end{DateTime.Now}");

            Console.WriteLine("all logged");
            Console.ReadLine();

        }
    }
}
