﻿namespace ShutdownWatcher
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}
