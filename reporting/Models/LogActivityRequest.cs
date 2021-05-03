using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace reporting.Models
{
    public class LogActivityRequest : IValidatableObject
    {
        public string Service;
        public string Activity;
        public string ActivityDetail;


        public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            if (this.Service == string.Empty) { yield return new ValidationResult($"{nameof(this.Service)} ({this.Service}) empty_service_description"); }
            if (this.Activity == string.Empty) { yield return new ValidationResult($"{nameof(this.Activity)} ({this.Activity}) empty_activity_description"); }
            if (this.ActivityDetail == string.Empty) { yield return new ValidationResult($"{nameof(this.ActivityDetail)} ({this.ActivityDetail}) empty_activity_detail_description"); }
        }
    }
}
