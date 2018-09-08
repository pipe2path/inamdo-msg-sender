using System;
using System.Threading;
using System.Threading.Tasks;

namespace inamdo_msg_sender
{
    class Program
    {
        private static Timer timer;
        static void Main(string[] args)
        {
            var timerState = new TimerState { Counter = 1 };

            timer = new Timer(
                callback: new TimerCallback(TimerTask),
                state: timerState,
                dueTime: 1000,
                period: 2000);

            while (timerState.Counter >= 1)
            {
                Task.Delay(1000).Wait();
            }

            timer.Dispose();
            Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff}: done.");
        }

        private static void TimerTask(object timerState)
        {
            if (DateTime.Now.Hour == 22)
            {
                //Console.WriteLine($"{DateTime.Now:HH:mm:ss.fff}: starting a new callback.");
                //var state = timerState as TimerState;
                //Interlocked.Increment(ref state.Counter);

                // need to pull in the user coupon list and send message




            }
        }

        class TimerState
        {
            public int Counter;
        }
    }
}
