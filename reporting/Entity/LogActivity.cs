using System;

namespace reporting.Entity
{
    public class LogActivity
    {
        public Guid Id { get; set; }
        public Guid CorrelationId { get; set; }
        public Guid RequestId { get; set; }
        public string Service { get; set; }
        public string Activity { get; set; }
        public string ActivityDetail { get; set; }
        public DateTime Timestamp { get; set; }

        // Required for serialization/deserialisation (uses reflection)
        public LogActivity()
        {

        }

        public LogActivity(
            Guid correlationId,
            Guid requestId,
            string service,
            string activity,
            string activityDetail)
        {
            this.Id = Guid.NewGuid();
            this.CorrelationId = correlationId;
            this.RequestId = requestId;
            this.Service = service;
            this.Activity = activity;
            this.ActivityDetail = activityDetail;
            this.Timestamp = DateTime.UtcNow;
        }
    }
}