namespace ShutdownWatcher
{
    using System;
    using System.ServiceProcess;
    using System.Timers;

    public class ShutdownWatcherManager
    {
        private readonly ILogger logger;
        private DateTime startTime;
        private ITimer timer;
        private ISystemEvents systemEvents;
        private IDateTimeProvider dateTimeProvider;
        private ServiceEventObserver serviceObserver;

        public ShutdownWatcherManager(ILogger logger, ServiceEventObserver serviceObserver)
        {
            this.logger = logger;
            this.Started = false;
            this.serviceObserver = serviceObserver;
        }

        public ITimer Timer
        {
            get
            {
                if (this.timer == null)
                {
                    this.timer = new SystemTimer();
                }

                return this.timer;
            }

            set
            {
                if (this.Started)
                {
                    throw new InvalidOperationException("Can't change the timer after the manager has started.");
                }

                this.timer = value;
            }
        }

        public IDateTimeProvider DateTimeProvider
        {
            get
            {
                if (this.dateTimeProvider == null)
                {
                    this.dateTimeProvider = new SystemDateTimeProvider();
                }

                return this.dateTimeProvider;
            }

            set
            {
                this.dateTimeProvider = value;
            }
        }

        public ISystemEvents SystemEvents
        {
            get
            {
                if (this.systemEvents == null)
                {
                    this.systemEvents = new WindowsSystemEvents();
                }

                return this.systemEvents;
            }

            set
            {
                this.systemEvents = value;
            }
        }

        public bool Started
        {
            get;
            protected set;
        }

        public void Start()
        {
            this.logger.ServiceStart();

            this.startTime = this.DateTimeProvider.Now;
            this.Started = true;

            this.serviceObserver.PowerEvent += ServicePowerEvent;
            this.serviceObserver.SessionChange += ServiceSessionChange;
            this.serviceObserver.Shutdown += ServiceShutdown;

            this.Timer.Interval = 1000;
            this.Timer.AutoReset = true;
            this.Timer.Elapsed += TimerElapsed;
            this.Timer.Start();            
        }

        public void Stop()
        {
            this.logger.ServiceStop();

            this.serviceObserver.PowerEvent -= ServicePowerEvent;
            this.serviceObserver.SessionChange -= ServiceSessionChange;
            this.serviceObserver.Shutdown -= ServiceShutdown;

            this.Timer.Elapsed -= TimerElapsed;
            this.Timer.Stop();
            this.Started = false;
        }

        private void ServiceShutdown(object sender, EventArgs e)
        {
            this.logger.ServiceShutdown();
        }

        private void ServiceSessionChange(object sender, SessionChangeDescription e)
        {
            this.logger.ServiceSessionChange(e);
        }

        private void ServicePowerEvent(object sender, PowerBroadcastStatus e)
        {
            this.logger.ServicePowerEvent(e);
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            this.logger.Up();
        }
    }
}
