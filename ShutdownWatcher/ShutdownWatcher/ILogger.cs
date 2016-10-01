namespace ShutdownWatcher
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceProcess;
    using System.Text;
    using System.Threading.Tasks;

    public interface ILogger
    {
        void Log(string message);
        void Log(string format, params object[] parameters);

        void Up();
        void ServiceStart();
        void ServiceStop();
        void ServiceShutdown();
        void ServicePowerEvent(PowerBroadcastStatus powerStatus);
        void ServiceSessionChange(SessionChangeDescription changeDescription);
    }
}
