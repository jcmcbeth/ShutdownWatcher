namespace ShutdownWatcher
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SystemDateTimeProvider : IDateTimeProvider
    {
        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
