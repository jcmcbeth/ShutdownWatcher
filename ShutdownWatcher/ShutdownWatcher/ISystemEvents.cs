namespace ShutdownWatcher
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Win32;

    public interface ISystemEvents
    {      
        event PowerModeChangedEventHandler PowerModeChanged;
        event SessionEndedEventHandler SessionEnded;
        event SessionEndingEventHandler SessionEnding;
        event SessionSwitchEventHandler SessionSwitch;
    }
}
