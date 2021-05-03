using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace binlookup.Models
{
    public class LookupRequest : IValidatableObject
    {
        public string MerchantId { get; set; }
        public long CardNumberBin { get; set; }


        public IEnumerable<ValidationResult> Validate(
            ValidationContext validationContext)
        {
            if (this.MerchantId == string.Empty) { yield return new ValidationResult($"{nameof(this.MerchantId)} ({this.MerchantId}) empty_merchant_id"); }
            if (this.CardNumberBin <= 0) { yield return new ValidationResult($"{nameof(this.CardNumberBin)} ({this.CardNumberBin}) card_number_bin_must_be_greater_than_zero"); }
        }


        public override string ToString()
        {
            return $"LookupRequest[merchantId: {this.MerchantId}, card_number_bin:{this.CardNumberBin}]";
        }
    }
}
