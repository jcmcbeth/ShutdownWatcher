namespace ShutdownWatcher
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceProcess;
    using System.Text;
    using System.Threading.Tasks;

    public class ServiceEventObserver
    {
        public event EventHandler Shutdown;
        public event EventHandler<PowerBroadcastStatus> PowerEvent;
        public event EventHandler<SessionChangeDescription> SessionChange;

        public void RaiseShutdown()
        {
            this.Shutdown?.Invoke(this, EventArgs.Empty);
        }

        public void RaisePowerEvent(PowerBroadcastStatus powerStatus)
        {
            this.PowerEvent?.Invoke(this, powerStatus);
        }

        public void RaiseSessionChange(SessionChangeDescription changeDescription)
        {
            this.SessionChange?.Invoke(this, changeDescription);
        }
    }
}
