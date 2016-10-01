namespace ShutdownWatcher
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Win32;

    public class WindowsSystemEvents : ISystemEvents
    {
        public event PowerModeChangedEventHandler PowerModeChanged;
        public event SessionEndedEventHandler SessionEnded;
        public event SessionEndingEventHandler SessionEnding;
        public event SessionSwitchEventHandler SessionSwitch;

        public WindowsSystemEvents()
        {
            SystemEvents.PowerModeChanged += (s, e) => this.OnPowerModeChanged(e);
            SystemEvents.SessionEnded += (s, e) => this.OnSessionEnded(e);
            SystemEvents.SessionEnding += (s, e) => this.OnSessionEnding(e);
            SystemEvents.SessionSwitch += (s, e) => this.OnSessionSwitch(e);
        }

        protected virtual void OnPowerModeChanged(PowerModeChangedEventArgs e)
        {
            this.PowerModeChanged?.Invoke(this, e);
        }

        protected virtual void OnSessionEnded(SessionEndedEventArgs e)
        {
            this.SessionEnded?.Invoke(this, e);
        }

        protected virtual void OnSessionEnding(SessionEndingEventArgs e)
        {
            this.SessionEnding?.Invoke(this, e);
        }

        protected virtual void OnSessionSwitch(SessionSwitchEventArgs e)
        {
            this.SessionSwitch?.Invoke(this, e);
        }
    }
}
