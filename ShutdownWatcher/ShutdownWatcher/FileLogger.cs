

namespace ShutdownWatcher
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.ServiceProcess;
    using System.Text;
    using System.Threading.Tasks;

    public class FileLogger : ILogger
    {
        private readonly string directory;        
        private IDateTimeProvider dateTimeProvider;

        public FileLogger(string directory)
        {
            this.directory = directory;

            this.MaxFileSize = 1024 * 1024 * 10; // 10MB
        }

        public void Log(string message)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("{0:HH:mm:ss}", this.DateTimeProvider.Now);
            
            if (!string.IsNullOrEmpty(message))
            {
                builder.Append(" ");
                builder.Append(message);
            }

            this.WriteLine(builder.ToString());
        }

        public int MaxFileSize
        {
            get;
            set;
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

        public void Log(string format, params object[] parameters)
        {
            var message = string.Format(format, parameters);
            this.Log(message);
        }

        public void ServicePowerEvent(PowerBroadcastStatus powerStatus)
        {
            this.Log("Service power event: {0}", powerStatus);
        }

        public void ServiceSessionChange(SessionChangeDescription changeDescription)
        {
            this.Log("Service session change: {0}", changeDescription);
        }

        public void ServiceShutdown()
        {
            this.Log("Service shutdown");
        }

        public void ServiceStart()
        {
            this.Log("Service starting");
        }

        public void ServiceStop()
        {
            this.Log("Service stop");
        }

        public void Up()
        {
            this.Log("");
        }

        private void Write(string text)
        {
            string fileName = this.GetFullFileName();
            
            if (!Directory.Exists(this.directory))
            {
                Directory.CreateDirectory(this.directory);
            }

            File.AppendAllText(fileName, text);
        }

        private void WriteLine(string text)
        {
            this.Write(text + Environment.NewLine);
        }

        private string GetFullFileName()
        {
            string fileName = string.Format("{0:MMddyyHH}.log", this.DateTimeProvider.Now);
            return Path.Combine(this.directory, fileName);
        }
    }
}
