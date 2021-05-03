using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace reporting.Models
{
    public class LogActivity
    {
        public Guid Id;
        public Guid CorrelationId;
        public Guid RequestId;
        public string Service;
        public string Activity;
        public string ActivityDetail;
        public DateTime Timestamp;
    }
}
