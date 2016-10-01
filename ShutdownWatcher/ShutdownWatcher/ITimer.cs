using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ShutdownWatcher
{
    public interface ITimer
    {
        bool AutoReset { get; set; }
        bool Enabled { get; set; }
        double Interval { get; set; }

        event ElapsedEventHandler Elapsed;
        void Start();
        void Stop();
    }
}
