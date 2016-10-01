namespace ShutdownWatcher
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.ServiceProcess;
    using System.Text;
    using System.Threading.Tasks;

    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            var serviceObserver = new ServiceEventObserver();

            var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "ShutdownWatcher");
            var logger = new FileLogger(directory);
            var manager = new ShutdownWatcherManager(logger, serviceObserver);

            var service = new ShutdownWatcherService(manager, serviceObserver);

            if (Environment.UserInteractive)
            {
                service.InteractiveStart();

                Console.WriteLine("Press any key to stop...");
                Console.ReadKey();

                service.Stop();
                service.Dispose();
            }
            else
            {
                using (service)
                {
                    ServiceBase.Run(service);
                }
            }
        }
    }
}
