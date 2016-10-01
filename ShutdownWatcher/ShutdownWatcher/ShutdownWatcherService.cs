using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ShutdownWatcher
{
    public partial class ShutdownWatcherService : ServiceBase
    {
        private readonly ShutdownWatcherManager manager;
        private readonly ServiceEventObserver serviceObserver;

        public ShutdownWatcherService(ShutdownWatcherManager manager, ServiceEventObserver serviceObserver)
        {
            InitializeComponent();

            this.manager = manager;
            this.serviceObserver = serviceObserver;
        }

        public void InteractiveStart()
        {
            this.OnStart(null);
        }

        public void InteractiveStop()
        {
            this.OnStop();
        }

        protected override void OnStart(string[] args)
        {
            manager.Start();
        }

        protected override void OnStop()
        {
            manager.Stop();
        }

        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            this.serviceObserver.RaisePowerEvent(powerStatus);

            return base.OnPowerEvent(powerStatus);
        }

        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            this.serviceObserver.RaiseSessionChange(changeDescription);

            base.OnSessionChange(changeDescription);
        }

        protected override void OnShutdown()
        {
            this.serviceObserver.RaiseShutdown();

            base.OnShutdown();
        }
    }
}
