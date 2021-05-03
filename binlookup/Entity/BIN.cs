using System;

namespace binlookup.Entity
{
    public class BIN
    {
        public long Low { get; set; }
        public long High { get; set; }
        public string CardScheme { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }


        // Required for deserialization (uses reflection)
        public BIN()
        {

        }


        public BIN(
            long low,
            long high,
            string cardScheme,
            string country,
            string currency)
        {
            this.Low = low;
            this.High = high;
            this.CardScheme = cardScheme;
            this.Country = country;
            this.Currency = currency;
        }
    }
}