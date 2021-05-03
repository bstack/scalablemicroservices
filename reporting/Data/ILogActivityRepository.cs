using reporting.Entity;
using System;
using System.Collections.Generic;

namespace reporting.Data
{
    public interface ILogActivityRepository
    {
        void Log(
            LogActivity logActivity);

        IEnumerable<LogActivity> GetRecentLogs();
    }
}
