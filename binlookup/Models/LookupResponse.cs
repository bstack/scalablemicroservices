using System;

namespace binlookup.Models
{
	public class LookupResponse
	{
		public string card_scheme { get; }
		public string country { get; }
		public string currency { get; }

		public LookupResponse(
			string card_scheme,
			string country,
			string currency)
		{
			this.card_scheme = card_scheme;
			this.country = country;
			this.currency = currency;
		}

		public override string ToString()
		{
			return $"LookupRequest[card_scheme: {this.card_scheme}, country: {this.country}, currency:{this.currency}]";
		}
	}
}
