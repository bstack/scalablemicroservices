using System;

namespace binlookup.Entity
{
    public class BINLookupResult
    {
        public string RequestId { get; set; }
        public string CorrelationId { get; set; }
        public string CardScheme { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }


        // Required for deserialization (uses reflection)
        public BINLookupResult()
        {

        }


        public BINLookupResult(
            string requestId,
            string correlationId,
            string cardScheme,
            string country,
            string currency)
        {
            this.RequestId = requestId;
            this.CorrelationId = correlationId;
            this.CardScheme = cardScheme;
            this.Country = country;
            this.Currency = currency;
        }
    }
}